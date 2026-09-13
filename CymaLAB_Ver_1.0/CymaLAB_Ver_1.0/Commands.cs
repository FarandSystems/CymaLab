using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CymaLAB_Ver_1._0.Enums;

namespace CymaLAB_Ver_1._0
{
    public sealed class Commands
    {
        private readonly Communication communication;

        public Commands(Communication communication)
        {
            this.communication = communication;
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
