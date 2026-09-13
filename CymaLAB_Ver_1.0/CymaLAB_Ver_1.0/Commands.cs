using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static CymaLAB_Ver_1._0.Enums;

namespace CymaLAB_Ver_1._0
{
    public sealed class Commands
    {
        private readonly Communication communication;

        public event Action<Exception> CommandFailed;

        private int sentDownSampleRatio = Constants.DEFAULT_DOWNSAMPLE_RATIO;

        private int sentStartIndex = Constants.DEFAULT_SAMPLE_START_INDEX;

        public event Action<DeviceData> MeasurementReceived;

        public Commands(Communication communication)
        {
            if (communication == null)
                throw new ArgumentNullException(nameof(communication));

            this.communication = communication;

            communication.PacketReceived += Communication_PacketReceived;
            communication.ConnectionLost += Communication_ConnectionLost;
        }

        private readonly object commandLock = new object();

        private readonly List<byte[]> pendingCommands = new List<byte[]>();

        private bool captureInProgress;

        public bool CaptureInProgress
        {
            get
            {
                lock (commandLock)
                {
                    return captureInProgress;
                }
            }
        }

        private void Communication_PacketReceived(byte[] packet)
        {
            DeviceData data;

            try
            {
                lock (commandLock)
                {
                    if (!captureInProgress)
                        return;

                    data = PacketParser.Parse(packet);

                    if (data == null)
                        return;

                    double adcIntervalUs = Constants.MICROSECONDS_PER_SECOND / Constants.ADC_SAMPLE_RATE_HZ;

                    data.StartTimeUs = sentStartIndex * adcIntervalUs;

                    data.SampleIntervalUs = sentDownSampleRatio * adcIntervalUs;

                    // This response may change sampling for the NEXT capture.
                    RespondToPacket();
                }
            }
            catch (IOException ex)
            {
                HandleSendFailure(ex);
                return;
            }
            catch (InvalidOperationException ex)
            {
                HandleSendFailure(ex);
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                HandleSendFailure(ex);
                return;
            }
            catch (TimeoutException ex)
            {
                HandleSendFailure(ex);
                return;
            }

            MeasurementReceived?.Invoke(data);
        }

        private void Communication_ConnectionLost()
        {
            Reset();
        }

        private void HandleSendFailure(Exception exception)
        {
            Reset();

            CommandFailed?.Invoke(exception);
        }

        public void RespondToPacket()
        {
            lock (commandLock)
            {
                if (!captureInProgress)
                    return;

                if (pendingCommands.Count > 0)
                {
                    byte[] command = pendingCommands[0];

                    SendImmediate(command);

                    // Remove only after sending succeeds.
                    pendingCommands.RemoveAt(0);
                }
                else
                {
                    SendImmediate((byte[])Constants.HEARTBEAT_COMMAND.Clone());
                }
            }
        }
        public void Reset()
        {
            lock (commandLock)
            {
                captureInProgress = false;
                pendingCommands.Clear();
            }
        }

        public void Dispose()
        {
            communication.PacketReceived -= Communication_PacketReceived;
            communication.ConnectionLost -= Communication_ConnectionLost;

            Reset();
        }

        private void SubmitCommand(byte[] command)
        {
            lock (commandLock)
            {
                if (!communication.IsConnected)
                {
                    throw new InvalidOperationException("The device is not connected.");
                }

                if (!captureInProgress)
                {
                    SendImmediate(command);
                    return;
                }

                byte commandId = command[Constants.COMMAND_ID_INDEX];

                // Replace an older pending value for the same setting.
                for (int index = 0; index < pendingCommands.Count; index++)
                {
                    if (pendingCommands[index][Constants.COMMAND_ID_INDEX] == commandId)
                    {
                        pendingCommands[index] = (byte[])command.Clone();
                        return;
                    }
                }

                if (pendingCommands.Count >= Constants.MAX_PENDING_COMMANDS)
                {
                    throw new InvalidOperationException("The pending command queue is full.");
                }

                pendingCommands.Add((byte[])command.Clone());
            }
        }



        private void SendImmediate(byte[] command)
        {
            communication.Send(command);

            if (command[Constants.COMMAND_ID_INDEX] == (byte)DeviceCommand.SamplingParameters)
            {
                sentDownSampleRatio = command[Constants.SAMPLING_DOWNSAMPLE_INDEX];

                sentStartIndex = Utils.GetUInt16LE(command, Constants.SAMPLING_START_INDEX);
            }
        }

        public void StartCapture(AppSettings settings, int downSampleRatio, int startIndex)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.Validate();

            // DiscardTimeUs is a double in settings but one byte on the wire.
            if (double.IsNaN(settings.DiscardTimeUs) || double.IsInfinity(settings.DiscardTimeUs) || settings.DiscardTimeUs < 0 || settings.DiscardTimeUs > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(settings.DiscardTimeUs));
            }

            var measurementMode = settings.MeasurementMode == "Velocity"
                    ? Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Velocity_Calculation
                    : Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Length_Calculation;

            // Build and validate everything before sending any command.
            byte[][] startupCommands =
            {
                BuildSamplingCommand(downSampleRatio,startIndex, settings.PreTriggerTimeUs,(int)settings.DiscardTimeUs),

                BuildSignalIntensityCommand(settings.TransducerPowerLevel),

                BuildAmplifierGainCommand(settings.AmplifierGain),

                BuildAveragingCommand(settings.CaptureAveragingCount),

                BuildFilterCommand(settings.FilterMode,settings.PiezoFrequencyKhz),

                BuildMeasurementCommand(measurementMode,settings.SampleLengthCm,settings.SampleVelocityMs)
            };

            lock (commandLock)
            {
                if (!communication.IsConnected)
                {
                    throw new InvalidOperationException("The device is not connected.");
                }

                if (captureInProgress)
                {
                    throw new InvalidOperationException("Stop capture before applying startup settings.");
                }

                pendingCommands.Clear();

                foreach (byte[] command in startupCommands)
                {
                    SendImmediate(command);
                }

                StartCapture();
            }
        }

        public void StartCapture()
        {
            lock (commandLock)
            {
                if (captureInProgress)
                    return;

                pendingCommands.Clear();

                // Set before sending so the first received packet gets a response.
                captureInProgress = true;

                try
                {
                    SendImmediate(BuildCaptureCommand(true));
                }
                catch
                {
                    captureInProgress = false;
                    pendingCommands.Clear();

                    throw;
                }
            }
        }

        public void StopCapture()
        {
            lock (commandLock)
            {
                SendImmediate(BuildCaptureCommand(false));

                captureInProgress = false;
                pendingCommands.Clear();
            }
        }

        public void SetAmplifierGain(int gainLevel)
        {
            SubmitCommand(BuildAmplifierGainCommand(gainLevel));
        }

        public void SetSignalIntensity(int powerLevel)
        {
            SubmitCommand(BuildSignalIntensityCommand(powerLevel));
        }

        public void SetSampling(int downSampleRatio, int startIndex, double preTriggerUs, int discardUs)
        {
            byte[] command = BuildSamplingCommand(downSampleRatio, startIndex, preTriggerUs, discardUs);

            SubmitCommand(command);
        }

        public void SetAveraging(int count)
        {
            SubmitCommand(BuildAveragingCommand(count));
        }

        public void SetFilter(Filter_Control.Filter_Control.Filter_Mode_Enum mode, double centerFrequencyKhz)
        {
            SubmitCommand(BuildFilterCommand(mode, centerFrequencyKhz));
        }

        public void SetMeasurement(Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum mode, double sampleLengthCm, double sampleVelocityMs)
        {
            SubmitCommand(BuildMeasurementCommand(mode, sampleLengthCm, sampleVelocityMs));
        }

        public byte[] BuildMeasurementCommand(Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum mode, double sampleLengthCm, double sampleVelocityMs)
        {
            if (double.IsNaN(sampleLengthCm) || double.IsInfinity(sampleLengthCm) || sampleLengthCm <= 0 || sampleLengthCm > short.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleLengthCm));
            }

            if (double.IsNaN(sampleVelocityMs) || double.IsInfinity(sampleVelocityMs) || sampleVelocityMs <= 0 || sampleVelocityMs > short.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleVelocityMs));
            }

            byte modeValue;

            switch (mode)
            {
                case Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Velocity_Calculation:
                    modeValue = Constants.MEASUREMENT_VELOCITY_MODE;
                    break;

                case Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Length_Calculation:
                    modeValue = Constants.MEASUREMENT_LENGTH_MODE;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }

            byte[] frame = CreateCommandFrame(DeviceCommand.MeasurementOptions);

            frame[Constants.MEASUREMENT_MODE_INDEX] = modeValue;

            Utils.SetInt16LE(frame, Constants.MEASUREMENT_LENGTH_INDEX, (int)sampleLengthCm);

            Utils.SetInt16LE(frame, Constants.MEASUREMENT_VELOCITY_INDEX, (int)sampleVelocityMs);

            frame[Constants.MEASUREMENT_OPTIONS_INDEX] = Constants.MEASUREMENT_OPTIONS_VALUE;

            return FinalizeCommandFrame(frame);
        }

        public byte[] BuildFilterCommand(Filter_Control.Filter_Control.Filter_Mode_Enum mode, double centerFrequencyKhz)
        {
            if (double.IsNaN(centerFrequencyKhz) || double.IsInfinity(centerFrequencyKhz) || centerFrequencyKhz <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(centerFrequencyKhz));
            }

            bool enabled;
            double ratio;

            switch (mode)
            {
                case Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter:
                    enabled = false;
                    ratio = Constants.WEAK_FILTER_RATIO;
                    break;

                case Filter_Control.Filter_Control.Filter_Mode_Enum.Weak_BandPass:
                    enabled = true;
                    ratio = Constants.WEAK_FILTER_RATIO;
                    break;

                case Filter_Control.Filter_Control.Filter_Mode_Enum.Strong_BandPass:
                    enabled = true;
                    ratio = Constants.STRONG_FILTER_RATIO;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }

            double lowPassValue = centerFrequencyKhz * ratio * Constants.FILTER_FREQUENCY_SCALE;

            double highPassValue = centerFrequencyKhz / ratio * Constants.FILTER_FREQUENCY_SCALE;

            if (lowPassValue > ushort.MaxValue || highPassValue > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(centerFrequencyKhz), "Filter frequencies exceed the protocol field range.");
            }

            byte[] frame = CreateCommandFrame(DeviceCommand.FilterParameters);

            frame[Constants.FILTER_LPF_ENABLED_INDEX] = enabled ? (byte)1 : (byte)0;

            Utils.SetInt16LE(frame, Constants.FILTER_LPF_FREQUENCY_INDEX, (int)lowPassValue);

            frame[Constants.FILTER_HPF_ENABLED_INDEX] = enabled ? (byte)1 : (byte)0;

            Utils.SetInt16LE(frame, Constants.FILTER_HPF_FREQUENCY_INDEX, (int)highPassValue);

            return FinalizeCommandFrame(frame);
        }

        public byte[] BuildAveragingCommand(int count)
        {
            if (Array.IndexOf(Constants.AVERAGING_COUNTS, count) < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Averaging count must be 1, 4, 8, 16 or 32.");
            }

            byte[] frame = CreateCommandFrame(DeviceCommand.AveragingCount);

            frame[Constants.AVERAGING_COUNT_INDEX] = (byte)count;

            return FinalizeCommandFrame(frame);
        }

        public byte[] BuildAmplifierGainCommand(int gainLevel)
        {
            if (gainLevel < 1 || gainLevel > Constants.AMPLIFIER_GAIN_CHANNELS.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(gainLevel));
            }

            byte[] frame = CreateCommandFrame(DeviceCommand.SelectChannel);

            frame[Constants.AMPLIFIER_GAIN_CHANNEL_INDEX] = Constants.AMPLIFIER_GAIN_CHANNELS[gainLevel - 1];

            return FinalizeCommandFrame(frame);
        }

        public byte[] BuildSamplingCommand(int downSampleRatio, int startIndex, double preTriggerUs, int discardUs)
        {
            if (downSampleRatio < Constants.MIN_DOWNSAMPLE_RATIO || downSampleRatio > Constants.MAX_DOWNSAMPLE_RATIO)
            {
                throw new ArgumentOutOfRangeException(nameof(downSampleRatio));
            }

            if (startIndex < 0 || startIndex > Constants.MAX_SAMPLE_START_INDEX)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (double.IsNaN(preTriggerUs) || double.IsInfinity(preTriggerUs) || preTriggerUs < 0 || preTriggerUs > Constants.MAX_PRETRIGGER_US)
            {
                throw new ArgumentOutOfRangeException(nameof(preTriggerUs));
            }

            if (discardUs < 0 || discardUs > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(discardUs));
            }

            int preTriggerCount = (int)(preTriggerUs * Constants.PRETRIGGER_COUNTS_PER_US);

            byte[] frame = CreateCommandFrame(DeviceCommand.SamplingParameters);

            frame[Constants.SAMPLING_DOWNSAMPLE_INDEX] = (byte)downSampleRatio;

            Utils.SetInt16LE(frame, Constants.SAMPLING_START_INDEX, startIndex);

            Utils.SetInt16LE(frame, Constants.SAMPLING_PRETRIGGER_INDEX, preTriggerCount);

            frame[Constants.SAMPLING_DISCARD_INDEX] = (byte)discardUs;

            return FinalizeCommandFrame(frame);
        }

        public byte[] BuildSignalIntensityCommand(int powerLevel)
        {
            if (powerLevel < 1 || powerLevel > Constants.PULSE_WIDTHS_US.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(powerLevel));
            }

            double pulseWidthUs = Constants.PULSE_WIDTHS_US[powerLevel - 1];

            if (double.IsNaN(pulseWidthUs) || double.IsInfinity(pulseWidthUs) || pulseWidthUs <= 0 || pulseWidthUs > int.MaxValue || pulseWidthUs != Math.Truncate(pulseWidthUs))
            {
                throw new InvalidOperationException("Pulse widths must be positive whole microseconds " + "within the Int32 range.");
            }

            byte[] command = CreateCommandFrame(Enums.DeviceCommand.SignalIntensity);

            Utils.SetInt32LE(command, Constants.SIGNAL_PULSE_WIDTH_INDEX, (int)pulseWidthUs);

            command[Constants.SIGNAL_OPTIONS_INDEX] = Constants.SIGNAL_OPTIONS_VALUE;

            return FinalizeCommandFrame(command);
        }

        public byte[] BuildCaptureCommand(bool start)
        {
            byte[] frame = CreateCommandFrame(DeviceCommand.CaptureControl);

            frame[Constants.COMMAND_VALUE_INDEX] = start ? (byte)1 : (byte)0;

            return FinalizeCommandFrame(frame);
        }

        private byte[] CreateCommandFrame(DeviceCommand command)
        {
            byte[] frame = new byte[Constants.COMMAND_FRAME_SIZE_BYTES];

            frame[Constants.COMMAND_ID_INDEX] = (byte)command;

            return frame;
        }

        private byte[] FinalizeCommandFrame(byte[] frame)
        {
            frame[Constants.COMMAND_CHECKSUM_INDEX] = Utils.Calculate_Checksum8(frame, Constants.COMMAND_FRAME_SIZE_BYTES);

            return frame;
        }
    }
}
