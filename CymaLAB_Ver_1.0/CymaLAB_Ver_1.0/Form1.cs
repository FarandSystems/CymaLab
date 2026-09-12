using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CymaLAB_Ver_1._0
{
    public partial class Form1 : Form
    {
        double[] Capture_Data = null;

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

            timer_Auto_Start_Simulation.Start();

        }

        private void Signal_Generator1_Data_Is_Ready(object sender, EventArgs e)
        {
            if (system_mode == System_Mode_Enum.Test_Mode)
            {
                Capture_Data = farand_Tablet_Chart_Control1.signal_Generator1.Data_Filter;

                Update_chart();
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
            Commands.
            farand_Tablet_Chart_Control1.PushSamples(Capture_Data, copyInputBuffer: false);
        }

        private void filter_Control1_Filter_Mode_Changed(object sender, EventArgs e)
        {
            Update_Filter_Parameters();
        }

        //private void Update_Filter_Mode()
        //{
        //    switch (system_mode)
        //    {
        //        case System_Mode_Enum.Test_Mode:
        //            switch (filter_Control1.Filter_Mode)
        //            {
        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter:
        //                    farand_Tablet_Chart_Control1.signal_Generator1.Set_Filter_Parameters(Data_Generate.Signal_Generator.Filter_Mode_Enum.No_Filter, 10, 40);
        //                    break;

        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.Weak_BandPass:
        //                    farand_Tablet_Chart_Control1.signal_Generator1.Set_Filter_Parameters(Data_Generate.Signal_Generator.Filter_Mode_Enum.Weak_BandPass_Filter, 10, 40);
        //                    break;

        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.Strong_BandPass:
        //                    farand_Tablet_Chart_Control1.signal_Generator1.Set_Filter_Parameters(Data_Generate.Signal_Generator.Filter_Mode_Enum.Strong_BandPass_Filter, 10, 40);
        //                    break;
        //            }
        //            label1.Text = filter_Control1.Filter_Mode.ToString();
        //            break;

        //        case System_Mode_Enum.Normal_operation:
        //            switch (filter_Control1.Filter_Mode)
        //            {
        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter:
        //                    // Send Command to PT-201   
        //                    break;

        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.Weak_BandPass:
        //                    // Send Command to PT-201
        //                    break;

        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.Strong_BandPass:
        //                    // Send Command to PT-201  blet_Chart_Control1.signal_Generator1.Set_Filter_Parameters(Data_Generate.Signal_Generator.Filter_Mode_Enum.Strong_BandPass_Filter, 10, 40);
        //                    break;
        //            }
        //            label1.Text = filter_Control1.Filter_Mode.ToString();
        //            break;
        //        case System_Mode_Enum.Idle:
        //            switch (filter_Control1.Filter_Mode)
        //            {
        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter:
        //                    // Send Command to PT-201
        //                    break;

        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.Weak_BandPass:
        //                    // Send Command to PT-201
        //                    break;
        //                case Filter_Control.Filter_Control.Filter_Mode_Enum.Strong_BandPass:
        //                    // Send Command to PT-201
        //                    break;
        //            }
        //            label1.Text = filter_Control1.Filter_Mode.ToString();
        //            break;

        //    }
        //}

        private void Update_Averaging()
        {
            int n = averaging_Control1.Averaging_Captures_Count;
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

        private void averaging_Control1_AveragingChanged(object sender, EventArgs e)
        {
            Update_Averaging();
        }

        private void Update_Filter_Parameters()
        {
            double f = transducer_Type1.Piezo_frequency_kHz;

            switch (system_mode)
            {
                case System_Mode_Enum.Test_Mode:
                    switch (filter_Control1.Filter_Mode)
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
                    switch (filter_Control1.Filter_Mode)
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

        private void transducer_Type1_piezo_frequency_Changed(object sender, EventArgs e)
        {
            Update_Filter_Parameters();
        }

        private void timer_Auto_Start_Simulation_Tick(object sender, EventArgs e)
        {
            farand_Tablet_Chart_Control1.signal_Generator1.checkBox_use_Simulated_Click(null, null);
            timer_Auto_Start_Simulation.Stop();
        }
    }
}
