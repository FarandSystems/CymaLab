using Command_Box;
using Measurement_Mode_Control;
using Signal_Strength_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transducer_Type;

namespace CymaLAB_Ver_1._0
{
    public partial class Form1 : Form
    {
        double[] Capture_Data = null;

        private AppSettings settings = new AppSettings();

        private readonly string settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PreciTest", "CymaLab", "Settings.txt");
        private bool settingsInitialized;

        private Communication communication;
        private Commands commands;

        private DeviceData latestDeviceData;

        private readonly SamplingParameters sampling = new SamplingParameters();

        private readonly System.Windows.Forms.Timer hardwareDisplayTimer = new System.Windows.Forms.Timer
        {
            Interval = Constants.HARDWARE_DISPLAY_INTERVAL_MS
        };

        private static bool IsInDesignMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        enum System_Mode_Enum
        {
            Test_Mode,
            Normal_operation,
            Idle
        }

        System_Mode_Enum system_mode = System_Mode_Enum.Normal_operation;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (IsInDesignMode)
                return;


            LoadSettings();
            ApplySettingsToControls();

            tof_Control.Auto_TOF = true;

            settingsInitialized = true;

            tof_Control.Close_Button_Clicked += Tof_Control_Close_Button_Clicked;

            advanced_Settings_Control.Advanced_Settings_Changed += advanced_Settings_Control_Advanced_Settings_Changed;
            measurement_Mode_Control.Measurement_Mode_isChanged += measurement_Mode_Control_Measurement_Mode_isChanged;
            signal_Strength_Control.Signal_Intensity_isChanged += signal_Strength_Control_Signal_Intensity_isChanged;


            farand_Tablet_Chart_Control1.signal_Generator1.Mode_Is_Changed += Signal_Generator1_Mode_Is_Changed;

            farand_Tablet_Chart_Control1.signal_Generator1.Data_Is_Ready += Signal_Generator1_Data_Is_Ready;

            tof_Control.TOF_Changed += Tof_Control_TOF_Changed;

            command_Box.CommunicationModeChanged += Command_Box_CommunicationModeChanged;


            farand_Tablet_Chart_Control1.InitChartBehavior(
                        renderFps: 8,
                        interactionHz: 8,
                        visiblePointCount: Constants.CAPTURE_SAMPLE_COUNT,
                        maxStoredPointCount: Constants.CAPTURE_SAMPLE_COUNT,
                        autoStart: true,
                        drainAllQueuedBatches: true,
                        fixedYMin: -1100,
                        fixedYMax: 1100,
                        enableAutoScaleY: true
                    );

            ConfigureHardwareChart();

            communication = new Communication();

            commands = new Commands(communication);
            commands.CommandFailed += Commands_CommandFailed;

            communication.StatusChanged += Communication_StatusChanged;
            communication.Connected += Communication_ConnectionChanged;
            communication.ConnectionLost += Communication_ConnectionChanged;

            command_Box.LiveKey_Clicked += Command_Box_LiveKey_Clicked;


            UpdateConnectionControls();

            commands.MeasurementReceived += Commands_MeasurementReceived;

            farand_Tablet_Chart_Control1.XAxisViewChanged += Chart_XAxisViewChanged;

            hardwareDisplayTimer.Tick += HardwareDisplayTimer_Tick;
            hardwareDisplayTimer.Start();

            communication.Start(GetSelectedTransport());

            //timer_Auto_Start_Simulation.Start();

        }

        private Enums.CommunicationTransport GetSelectedTransport()
        {
            return command_Box.CommunicationMode == Command_Box.CommunicationModeEnum.USB
                    ? Enums.CommunicationTransport.Usb
                    : Enums.CommunicationTransport.Wifi;
        }

        private void Command_Box_CommunicationModeChanged(object sender, EventArgs e)
        {
            if (communication == null || commands == null)
                return;

            command_Box.Enabled = false;

            try
            {
                if (communication.IsConnected && commands.CaptureInProgress)
                {
                    try
                    {
                        commands.StopCapture();
                    }
                    catch (Exception ex) when (ex is IOException || ex is SocketException || ex is TimeoutException || ex is InvalidOperationException || ex is UnauthorizedAccessException)
                    {
                        // The old connection may already have failed.
                        System.Diagnostics.Debug.WriteLine("Could not stop the previous capture: " + ex.Message);
                    }
                }

                communication.Stop();

                commands.Reset();

                System.Threading.Interlocked.Exchange(ref latestDeviceData, null);

                communication.Start(GetSelectedTransport());
            }
            finally
            {
                command_Box.Enabled = true;
                UpdateConnectionControls();
            }
        }

        private void UpdateMeasurementResult()
        {
            double tofUs = tof_Control.TOF_uSec;

            double lengthCm = settings.SampleLengthCm;
            double velocityMs = settings.SampleVelocityMs;

            bool validTof = !double.IsNaN(tofUs) && !double.IsInfinity(tofUs) && tofUs > 0;

            if (settings.MeasurementMode == "Velocity")
            {
                bool validLength = !double.IsNaN(lengthCm) && !double.IsInfinity(lengthCm) && lengthCm > 0;

                velocityMs = validTof && validLength
                    ? MeasurementCalculator.CalculateVelocity(lengthCm, tofUs)
                    : double.NaN;
            }
            else
            {
                bool validVelocity = !double.IsNaN(velocityMs) && !double.IsInfinity(velocityMs) && velocityMs > 0;

                lengthCm = validTof && validVelocity
                    ? MeasurementCalculator.CalculateLength(velocityMs, tofUs)
                    : double.NaN;
            }

            tof_Control.ShowMeasurementResult(lengthCm, velocityMs);
        }

        private void Commands_MeasurementReceived(DeviceData data)
        {
            System.Threading.Interlocked.Exchange(ref latestDeviceData, data);
        }

        private void ApplyHardwareSetting(Action applySetting)
        {
            if (!settingsInitialized || system_mode != System_Mode_Enum.Normal_operation || communication == null || commands == null || !communication.IsConnected)
            {
                return;
            }

            try
            {
                applySetting();
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is TimeoutException || ex is InvalidOperationException || ex is ArgumentException)
            {
                MessageBox.Show(
                    this,
                    "Could not send the setting to the device."
                    + Environment.NewLine
                    + ex.Message,
                    "Device settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                UpdateConnectionControls();
            }
        }

        private void HardwareDisplayTimer_Tick(object sender, EventArgs e)
        {
            DeviceData data = System.Threading.Interlocked.Exchange(ref latestDeviceData, null);

            if (data == null)
                return;

            if (communication == null || !communication.IsConnected || system_mode != System_Mode_Enum.Normal_operation)
            {
                return;
            }

            Capture_Data = new double[data.CapturedSignal.Length];

            for (int sampleIndex = 0; sampleIndex < data.CapturedSignal.Length; sampleIndex++)
            {
                Capture_Data[sampleIndex] = data.CapturedSignal[sampleIndex];
            }

            farand_Tablet_Chart_Control1.ShowCapture(Capture_Data, data.StartTimeUs, data.SampleIntervalUs);

            UpdateTofDisplay(data);
            UpdateMeasurementResult();
        }

        private void Command_Box_LiveKey_Clicked(object sender, EventArgs e)
        {
            if (communication == null || commands == null || !communication.IsConnected)
            {
                UpdateConnectionControls();
                return;
            }

            if (system_mode != System_Mode_Enum.Normal_operation)
                return;

            try
            {
                if (commands.CaptureInProgress)
                {
                    commands.StopCapture();

                    System.Diagnostics.Debug.WriteLine(
                        "Capture stop command sent.");
                }
                else
                {
                    commands.StartCapture(settings, sampling.DownSampleRatio, sampling.StartIndex);

                    System.Diagnostics.Debug.WriteLine("Startup settings and capture start command sent.");
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is TimeoutException || ex is InvalidOperationException || ex is ArgumentException)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Capture",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UpdateConnectionControls();
            }
        }

        private void RunOnUiThread(Action action)
        {
            if (IsDisposed || Disposing || !IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (!IsDisposed && !Disposing)
                            action();
                    }));
                }
                catch (InvalidOperationException)
                {
                    // The form's handle may disappear during closing.
                }

                return;
            }

            action();
        }

        private void Communication_ConnectionChanged()
        {
            RunOnUiThread(() =>
            {
                if (communication == null || !communication.IsConnected)
                {
                    System.Threading.Interlocked.Exchange(ref latestDeviceData, null);

                    tof_Control.TOF_Stable = false;
                }

                UpdateConnectionControls();
            });
        }

        private void UpdateConnectionControls()
        {
            bool connected = communication != null && communication.IsConnected;

            bool capturing = connected && commands != null && commands.CaptureInProgress;

            command_Box._IsConnected = connected;
            command_Box._IsLive = capturing;
        }


        private void ApplySettingsToControls()
        {
            averaging_Control.Averaging_Captures_Count = settings.CaptureAveragingCount;

            advanced_Settings_Control.Reference_TOF_uSec = settings.ReferenceTofUs;

            advanced_Settings_Control.Discard_Time_uSec = settings.DiscardTimeUs;

            measurement_Mode_Control.Length = settings.SampleLengthCm;

            measurement_Mode_Control.Velocity = settings.SampleVelocityMs;

            measurement_Mode_Control.Measurement_Mode = settings.MeasurementMode == "Velocity"
                                                        ? Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Velocity_Calculation
                                                        : Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Length_Calculation;

            signal_Strength_Control.Transmitter_Power = settings.TransducerPowerLevel;
            settings.PulseWidthUs = Constants.PULSE_WIDTHS_US[settings.TransducerPowerLevel - 1];
            signal_Strength_Control.Reciever_Sensitivity = settings.AmplifierGain;

            transducer_Type.Piezo_frequency_kHz = settings.PiezoFrequencyKhz;

            filter_Control.Filter_Mode = settings.FilterMode;


        }

        private void SaveSettings()
        {
            settings.Validate();

            string directory = Path.GetDirectoryName(settingsFilePath);

            Directory.CreateDirectory(directory);

            SettingsFile.Save(settingsFilePath, settings);
        }

        private void LoadSettings()
        {
            try
            {
                settings = SettingsFile.Load(settingsFilePath);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is FormatException || ex is ArgumentException)
            {
                settings = new AppSettings();

                MessageBox.Show(
                    this,
                    "Could not load the settings. Default values will be used."
                    + Environment.NewLine
                    + Environment.NewLine
                    + ex.Message,
                    "Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void Commands_CommandFailed(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Command failed: " + exception.Message);

            RunOnUiThread(UpdateConnectionControls);
        }

        private void Communication_StatusChanged(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        private void Tof_Control_TOF_Changed(object sender, EventArgs e)
        {
            UpdateMeasurementResult();
        }


        private void signal_Strength_Control_Signal_Intensity_isChanged(object sender, EventArgs e)
        {
            int powerLevel = signal_Strength_Control.Transmitter_Power;
            int gainLevel = signal_Strength_Control.Reciever_Sensitivity;

            bool powerChanged = settings.TransducerPowerLevel != powerLevel;

            bool gainChanged = settings.AmplifierGain != gainLevel;

            settings.TransducerPowerLevel = powerLevel;
            settings.PulseWidthUs = Constants.PULSE_WIDTHS_US[powerLevel - 1];

            settings.AmplifierGain = gainLevel;

            ApplyHardwareSetting(() =>
            {
                if (powerChanged)
                    commands.SetSignalIntensity(powerLevel);

                if (gainChanged)
                    commands.SetAmplifierGain(gainLevel);
            });
        }


        private void Signal_Generator1_Data_Is_Ready(object sender, EventArgs e)
        {
            if (system_mode == System_Mode_Enum.Test_Mode)
            {
                Capture_Data = farand_Tablet_Chart_Control1.signal_Generator1.Data_Filter;

                Update_chart();
            }
        }

        private void UpdateTofDisplay(DeviceData data)
        {
            tof_Control.TOF_Stable = data.FilterTofMode == Constants.TOF_FILTER_STABLE_MODE;

            if (!tof_Control.Auto_TOF)
                return;

            double correctedTofUs = data.AutoTimeOfFlightUs + settings.TofOffsetUs;

            if (double.IsNaN(correctedTofUs) || double.IsInfinity(correctedTofUs))
            {
                tof_Control.TOF_Stable = false;
                return;
            }

            tof_Control.TOF_uSec = correctedTofUs;
        }

        private void measurement_Mode_Control_Measurement_Mode_isChanged(object sender, EventArgs e)
        {
            var mode = measurement_Mode_Control.Measurement_Mode;

            settings.SampleLengthCm = measurement_Mode_Control.Length;
            settings.SampleVelocityMs = measurement_Mode_Control.Velocity;

            settings.MeasurementMode =
                mode == Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Velocity_Calculation
                        ? "Velocity"
                        : "Length";

            ApplyHardwareSetting(() => commands.SetMeasurement(mode, settings.SampleLengthCm, settings.SampleVelocityMs));

            UpdateMeasurementResult();
        }

        private void advanced_Settings_Control_Advanced_Settings_Changed(object sender, EventArgs e)
        {
            bool discardChanged = settings.DiscardTimeUs != advanced_Settings_Control.Discard_Time_uSec;

            settings.ReferenceTofUs = advanced_Settings_Control.Reference_TOF_uSec;

            settings.DiscardTimeUs = advanced_Settings_Control.Discard_Time_uSec;

            if (discardChanged)
            {
                ApplyHardwareSetting(() =>
                {
                    commands.SetSampling(sampling.DownSampleRatio, sampling.StartIndex, settings.PreTriggerTimeUs, GetDiscardTimeUs());
                });
            }
        }

        private void Signal_Generator1_Mode_Is_Changed(object sender, EventArgs e)
        {
            if (farand_Tablet_Chart_Control1.signal_Generator1._IsSimulated_Data_Choosen == true)
            {
                system_mode = System_Mode_Enum.Test_Mode;
            }
            else
            {
                system_mode = System_Mode_Enum.Normal_operation;
            }

            label1.Text = system_mode.ToString();
        }

        private void Update_chart()
        {
            farand_Tablet_Chart_Control1.PushSamples(Capture_Data, copyInputBuffer: false);
        }

        private void filter_Control_Filter_Mode_Changed(object sender, EventArgs e)
        {
            settings.FilterMode = filter_Control.Filter_Mode;

            Update_Filter_Parameters();
        }

        private void Update_Averaging()
        {
            int count = settings.CaptureAveragingCount;

            switch (system_mode)
            {
                case System_Mode_Enum.Test_Mode:
                    farand_Tablet_Chart_Control1.signal_Generator1.Set_Average_Count(count);
                    break;

                case System_Mode_Enum.Normal_operation:
                    ApplyHardwareSetting(() => commands.SetAveraging(count));
                    break;
            }
        }

        private void averaging_Control_AveragingChanged(object sender, EventArgs e)
        {
            settings.CaptureAveragingCount = averaging_Control.Averaging_Captures_Count;

            Update_Averaging();
        }

        private void Update_Filter_Parameters()
        {
            double f = transducer_Type.Piezo_frequency_kHz;

            switch (system_mode)
            {
                case System_Mode_Enum.Test_Mode:
                    switch (filter_Control.Filter_Mode)
                    {
                        case Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter:
                            farand_Tablet_Chart_Control1.signal_Generator1.Set_Filter_Parameters(Data_Generate.Signal_Generator.Filter_Mode_Enum.No_Filter, f);
                            break;
                        case Filter_Control.Filter_Control.Filter_Mode_Enum.Weak_BandPass:
                            farand_Tablet_Chart_Control1.signal_Generator1.Set_Filter_Parameters(Data_Generate.Signal_Generator.Filter_Mode_Enum.Weak_BandPass_Filter, f);
                            break;
                        case Filter_Control.Filter_Control.Filter_Mode_Enum.Strong_BandPass:
                            farand_Tablet_Chart_Control1.signal_Generator1.Set_Filter_Parameters(Data_Generate.Signal_Generator.Filter_Mode_Enum.Strong_BandPass_Filter, f);
                            break;
                        default:
                            break;
                    }
                    break;
                case System_Mode_Enum.Normal_operation:
                    ApplyHardwareSetting(() => commands.SetFilter(settings.FilterMode, settings.PiezoFrequencyKhz));
                    break;
            }

        }

        private void transducer_Type_piezo_frequency_Changed(object sender, EventArgs e)
        {
            settings.TransducerType = "P";

            settings.PiezoFrequencyKhz = transducer_Type.Piezo_frequency_kHz;

            Update_Filter_Parameters();
        }

        private void Tof_Control_Close_Button_Clicked(object sender, EventArgs e)
        {
            bool resumeCapture = communication != null && communication.IsConnected && commands != null && commands.CaptureInProgress;

            try
            {
                if (resumeCapture)
                    commands.StopCapture();

                UpdateConnectionControls();
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is TimeoutException || ex is InvalidOperationException)
            {
                MessageBox.Show(
                    this,
                    "Could not stop capture."
                    + Environment.NewLine
                    + ex.Message,
                    "Capture",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                UpdateConnectionControls();
                return;
            }

            DialogResult result = MessageBox.Show(
                this,
                "Do you want to exit the application?",
                "Exit?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Close();

                // Closing may be cancelled if saving settings fails.
                if (IsDisposed || Disposing)
                    return;
            }

            // Resume after Cancel, or if OnFormClosing cancelled the close.
            if (resumeCapture && communication != null && communication.IsConnected && commands != null)
            {
                try
                {
                    commands.StartCapture();
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is TimeoutException || ex is InvalidOperationException)
                {
                    MessageBox.Show(
                        this,
                        "Could not resume capture."
                        + Environment.NewLine
                        + ex.Message,
                        "Capture",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            UpdateConnectionControls();
        }

        private void ConfigureHardwareChart()
        {
            double adcIntervalUs = Constants.MICROSECONDS_PER_SECOND / Constants.ADC_SAMPLE_RATE_HZ;

            double minimumWindowUs = Constants.CAPTURE_SAMPLE_COUNT * Constants.MIN_DOWNSAMPLE_RATIO * adcIntervalUs;

            double maximumWindowUs = Constants.CAPTURE_SAMPLE_COUNT * Constants.MAX_DOWNSAMPLE_RATIO * adcIntervalUs;

            farand_Tablet_Chart_Control1.ConfigureCaptureView(sampling.StartTimeUs, sampling.StartTimeUs + Constants.CAPTURE_SAMPLE_COUNT * sampling.SampleIntervalUs,
                                                              Constants.MAX_SAMPLE_START_INDEX * adcIntervalUs, minimumWindowUs, maximumWindowUs);
        }

        private void timer_Auto_Start_Simulation_Tick(object sender, EventArgs e)
        {
            farand_Tablet_Chart_Control1.signal_Generator1.checkBox_use_Simulated_Click(null, null);
            timer_Auto_Start_Simulation.Stop();
        }

        private void Chart_XAxisViewChanged(object sender, Farand_Tablet_Chart.XAxisViewChangedEventArgs e)
        {
            if (!settingsInitialized || system_mode != System_Mode_Enum.Normal_operation)
            {
                return;
            }

            bool changed = sampling.SetTimeWindow(e.XMin, e.XMax);

            if (!changed)
                return;

            System.Diagnostics.Debug.WriteLine($"Sampling requested: start={sampling.StartIndex}, " + $"ratio={sampling.DownSampleRatio}");

            ApplyHardwareSetting(() =>
            {
                commands.SetSampling(sampling.DownSampleRatio, sampling.StartIndex, settings.PreTriggerTimeUs, GetDiscardTimeUs());
            });
        }

        private int GetDiscardTimeUs()
        {
            double value = settings.DiscardTimeUs;

            if (double.IsNaN(value) || double.IsInfinity(value) || value < 0 || value > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(settings.DiscardTimeUs));
            }

            return (int)value;
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.Cancel || !settingsInitialized)
                return;

            try
            {
                SaveSettings();
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is ArgumentException)
            {
                e.Cancel = true;

                MessageBox.Show(
                    this,
                    "Could not save settings."
                    + Environment.NewLine
                    + Environment.NewLine
                    + ex.Message,
                    "Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            hardwareDisplayTimer.Stop();
            hardwareDisplayTimer.Tick -= HardwareDisplayTimer_Tick;
            hardwareDisplayTimer.Dispose();

            command_Box.LiveKey_Clicked -= Command_Box_LiveKey_Clicked;

            tof_Control.TOF_Changed -= Tof_Control_TOF_Changed;

            command_Box.CommunicationModeChanged -= Command_Box_CommunicationModeChanged;

            // Stop the worker before removing its command handlers.
            if (communication != null)
            {
                communication.Stop();
            }

            System.Threading.Interlocked.Exchange(ref latestDeviceData, null);


            if (commands != null)
            {
                commands.MeasurementReceived -= Commands_MeasurementReceived;
                commands.CommandFailed -= Commands_CommandFailed;
                commands.Dispose();
                commands = null;
            }

            if (communication != null)
            {
                communication.StatusChanged -= Communication_StatusChanged;
                communication.Dispose();
                communication = null;
            }

            farand_Tablet_Chart_Control1.XAxisViewChanged -= Chart_XAxisViewChanged;

            base.OnFormClosed(e);
        }
    }
}
