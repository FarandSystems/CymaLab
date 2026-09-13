using Measurement_Mode_Control;
using Signal_Strength_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

            settingsInitialized = true;

            tof_Control.Close_Button_Clicked += Tof_Control_Close_Button_Clicked;

            advanced_Settings_Control.Advanced_Settings_Changed += advanced_Settings_Control_Advanced_Settings_Changed;
            measurement_Mode_Control.Measurement_Mode_isChanged += measurement_Mode_Control_Measurement_Mode_isChanged;
            signal_Strength_Control.Signal_Intensity_isChanged += signal_Strength_Control_Signal_Intensity_isChanged;


            farand_Tablet_Chart_Control1.signal_Generator1.Mode_Is_Changed += Signal_Generator1_Mode_Is_Changed;

            farand_Tablet_Chart_Control1.signal_Generator1.Data_Is_Ready += Signal_Generator1_Data_Is_Ready;


            farand_Tablet_Chart_Control1.InitChartBehavior(
                        renderFps: 8,
                        interactionHz: 8,
                        visiblePointCount: 720,
                        maxStoredPointCount: 720,
                        autoStart: true,
                        drainAllQueuedBatches: true,
                        fixedYMin: -1,
                        fixedYMax: 1,
                        enableAutoScaleY: false
                    );

            communication = new Communication();

            communication.StatusChanged += Communication_StatusChanged;

            communication.Start();

            //timer_Auto_Start_Simulation.Start();

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


        private void Communication_StatusChanged(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }


        private void signal_Strength_Control_Signal_Intensity_isChanged(object sender, EventArgs e)
        {
            settings.TransducerPowerLevel = signal_Strength_Control.Transmitter_Power;

            settings.PulseWidthUs = Constants.PULSE_WIDTHS_US[settings.TransducerPowerLevel - 1];

            settings.AmplifierGain = signal_Strength_Control.Reciever_Sensitivity;
        }

        private void Signal_Generator1_Data_Is_Ready(object sender, EventArgs e)
        {
            if (system_mode == System_Mode_Enum.Test_Mode)
            {
                Capture_Data = farand_Tablet_Chart_Control1.signal_Generator1.Data_Filter;

                Update_chart();
            }
        }

        private void measurement_Mode_Control_Measurement_Mode_isChanged(object sender, EventArgs e)
        {
            settings.SampleLengthCm = measurement_Mode_Control.Length;

            settings.SampleVelocityMs = measurement_Mode_Control.Velocity;

            settings.MeasurementMode = measurement_Mode_Control.Measurement_Mode == Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Velocity_Calculation
                                                                                    ? "Velocity"
                                                                                    : "Length";
        }

        private void advanced_Settings_Control_Advanced_Settings_Changed(object sender, EventArgs e)
        {
            settings.ReferenceTofUs = advanced_Settings_Control.Reference_TOF_uSec;

            settings.DiscardTimeUs = advanced_Settings_Control.Discard_Time_uSec;
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
            int n = averaging_Control.Averaging_Captures_Count;
            switch (system_mode)
            {
                case System_Mode_Enum.Test_Mode:
                    farand_Tablet_Chart_Control1.signal_Generator1.Set_Average_Count(n);
                    break;
                case System_Mode_Enum.Normal_operation:
                    //Send command to hardware
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
                    switch (filter_Control.Filter_Mode)
                    {
                        case Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter:
                            //Send command to hardware
                            break;
                        case Filter_Control.Filter_Control.Filter_Mode_Enum.Weak_BandPass:
                            //Send command to hardware
                            break;
                        case Filter_Control.Filter_Control.Filter_Mode_Enum.Strong_BandPass:
                            //Send command to hardware
                            break;
                    }
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
            Close();
        }

        private void timer_Auto_Start_Simulation_Tick(object sender, EventArgs e)
        {
            farand_Tablet_Chart_Control1.signal_Generator1.checkBox_use_Simulated_Click(null, null);
            timer_Auto_Start_Simulation.Stop();
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
            if (communication != null)
            {
                communication.StatusChanged -= Communication_StatusChanged;
                communication.Dispose();
                communication = null;
            }

            base.OnFormClosed(e);
        }
    }
}
