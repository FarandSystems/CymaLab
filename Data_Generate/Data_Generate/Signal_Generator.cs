using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Generate
{
    public partial class Signal_Generator : UserControl
    {
        private const int MaxTofHistoryPoints = 720;

        private bool IsInDesignMode
        {
            get
            {
                return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                       DesignMode ||
                       (Site != null && Site.DesignMode);
            }
        }

        public enum Filter_Mode_Enum
        {
            No_Filter,
            Weak_BandPass_Filter,
            Strong_BandPass_Filter
        }

        public enum Filter_State_Enum
        {
            Fast,
            Stable,
            Ultra_Stable
        }

        Filter_State_Enum filter_State = Filter_State_Enum.Fast;

        int filter_Counter = 0;

        double a = 0.95;

        double tOF_Filtered_Sec = 0;

        double total_Time = 0;

        double f_Center_kHz = 20;

        double[,] Capture_History = new double[64, 14400];

        int Capture_No = 0;

        public event EventHandler Mode_Is_Changed;

        public event EventHandler Data_Is_Ready;

        double[] data_Sine_Product = new double[720];

        double peak_Time_Sec = 0;

        double signal_Phase = 0;

        double best_Frequency_kHz = 20.0;

        double system_time_Sec = 0;

        double Ts_Sec = 1 / (7.2E6); //Sample Interval

        private double alpha = 0;

        private double beta = 1;

        private double time_Of_Flight_Sec;
        private double Time_Of_Flight_Sec
        {
            get { return time_Of_Flight_Sec; }
            set { time_Of_Flight_Sec = value; }
        }

        private int average_Count = 1;
        public int Average_Count
        {
            get { return average_Count; }
            set { average_Count = value; }
        }

        private bool _isSimulated_Data_Choosen = false;
        public bool _IsSimulated_Data_Choosen
        {
            get { return _isSimulated_Data_Choosen; }
            set
            {
                _isSimulated_Data_Choosen = value;
                checkBox_use_Simulated.Checked = value;
                Update_Info_Icon_Pic();
            }
        }

        private int start_Index = 0;
        public int Start_Index
        {
            get { return start_Index; }
            set
            {
                start_Index = value;
                numericUpDown_Start_Index.Value = start_Index;
                Update_Display_Period();
            }
        }

        private int down_Sample_Raito = 20;
        public int Down_Sample_Raito
        {
            get { return down_Sample_Raito; }
            set
            {
                down_Sample_Raito = value;
                numericUpDown_Down_Sample_Ratio.Value = down_Sample_Raito;
                Update_Display_Period();
            }
        }
        private Filter_Mode_Enum filter_Mode;
        public Filter_Mode_Enum Filter_Mode
        {
            get { return filter_Mode; }
            set
            {
                filter_Mode = value;
                Calculate_IIR_Filter_Parameters();
            }
        }

        private double fc_LPF_kHz = 40;
        public double Fc_LPF_kHz
        {
            get { return fc_LPF_kHz; }
            set
            {
                fc_LPF_kHz = value;
                Calculate_IIR_Filter_Parameters();
            }
        }

        private double fc_HPF_kHz = 10;
        public double Fc_HPF_kHz
        {
            get { return fc_HPF_kHz; }
            set
            {
                fc_HPF_kHz = value;
                Calculate_IIR_Filter_Parameters();
            }
        }

        private bool _isMaximized = false;
        public bool _IsMaximized
        {
            get { return _isMaximized; }
            set
            {
                _isMaximized = value;
                Update_Size();
            }
        }

        private double[] data_Filtered = new double[720];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double[] Data_Filter
        {
            get { return data_Filtered; }
            set { data_Filtered = value; }
        }

        private double t_Delay_uSec = 100;
        public double T_Delay_uSec
        {
            get { return t_Delay_uSec; }
            set
            {
                t_Delay_uSec = value;
                numericUpDown_T_Delay.Value = Convert.ToDecimal(t_Delay_uSec);
            }
        }

        private double t_Rise_uSec = 250;
        public double T_Rise_uSec
        {
            get { return t_Rise_uSec; }
            set
            {
                t_Rise_uSec = value;
                numericUpDown_T_Rise.Value = Convert.ToDecimal(t_Rise_uSec);
            }
        }

        private double t1_uSec = 50;
        public double T1_uSec
        {
            get { return t1_uSec; }
            set
            {
                t1_uSec = value;
                numericUpDown_T1.Value = Convert.ToDecimal(t1_uSec);
            }
        }

        private double amplitude = 1;
        public double Amplitude
        {
            get { return amplitude; }
            set
            {
                amplitude = value;
                numericUpDown_Amplitude.Value = Convert.ToDecimal(amplitude);
            }
        }

        private double f_kHz = 20.2;
        public double F_kHz
        {
            get { return f_kHz; }
            set
            {
                f_kHz = value;
                numericUpDown_F.Value = Convert.ToDecimal(f_kHz);
            }
        }

        private double noise_Intensity = 0.1;
        public double Noise_Intensity
        {
            get { return noise_Intensity; }
            set
            {
                noise_Intensity = value;
                numericUpDown_Noise_Intensity.Value = Convert.ToDecimal(noise_Intensity);
            }
        }

        private double tStart_mSec = 0;
        public double TStart_mSec
        {
            get { return tStart_mSec; }
            set
            {
                tStart_mSec = value;
                numericUpDown_Start_Index.Value = Convert.ToDecimal(tStart_mSec);
            }
        }

        private double tEnd_mSec = 10;
        private double TEnd_mSec
        {
            get { return tEnd_mSec; }
            set
            {
                tEnd_mSec = value;
                numericUpDown_Down_Sample_Ratio.Value = Convert.ToDecimal(tEnd_mSec);
            }
        }

        public Signal_Generator()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.Width = 24;
            this.Height = 24;
            _isMaximized = false;

            numericUpDown_Amplitude.Value = Convert.ToDecimal(amplitude);
            numericUpDown_T_Delay.Value = Convert.ToDecimal(t_Delay_uSec);
            numericUpDown_T_Rise.Value = Convert.ToDecimal(t_Rise_uSec);
            numericUpDown_T1.Value = Convert.ToDecimal(t1_uSec);
            numericUpDown_F.Value = Convert.ToDecimal(f_kHz);

            Calculate_IIR_Filter_Parameters();
            Update_Display_Period();
            Update_Info_Icon_Pic();
        }

        public double[] Generate_Data()
        {
            Random rnd = new Random();

            double[] data_Raw = new double[14400];
            double Jitter_Sec = (double)numericUpDown_Delay_Jitter.Value * 1E-6;
            double t_Delay_Sec = t_Delay_uSec * 1E-6 + (2 * rnd.NextDouble() - 1) * Jitter_Sec;
            double t_Rise_Sec = t_Rise_uSec * 1E-6;
            double t1_Sec = t1_uSec * 1E-6;

            //double DC_Shift = 0.1 * Math.Sin(2 * Math.PI * 0.5 * system_time_Sec);
            system_time_Sec += 0.125;


            for (int k = 0; k < data_Raw.Length; k++)
            {
                double t_Sec = k * Ts_Sec;
                if (t_Sec < t_Delay_Sec)
                {
                    data_Raw[k] = 0;
                }
                else if (t_Sec >= t_Delay_Sec && t_Sec < t_Delay_Sec + t_Rise_Sec)
                {
                    double T_Shift = t_Delay_Sec;
                    data_Raw[k] = amplitude * (1 - Math.Exp(-(t_Sec - T_Shift) / t1_Sec)) * Math.Sin(2 * Math.PI * f_kHz * 1E3 * (t_Sec - t_Delay_Sec));
                }
                else if (t_Sec >= t_Delay_Sec + t_Rise_Sec)
                {
                    double T_Shift = t_Delay_Sec + t_Rise_Sec;
                    double amp_Max = amplitude * (1 - Math.Exp(-t_Rise_Sec / t1_Sec)) * Math.Sin(2 * Math.PI * f_kHz * 1E3 * (t_Sec - t_Delay_Sec));
                    data_Raw[k] = amp_Max * Math.Exp(-(t_Sec - T_Shift) / t1_Sec);
                }

                double noise = 0.1 * noise_Intensity * (2 * rnd.NextDouble() - 1);
                data_Raw[k] = data_Raw[k] + noise + 0.1 * Math.Sin(2 * Math.PI * 250 * (system_time_Sec + t_Sec));

            }
            return data_Raw;
        }

        private void Show_Signal(double[] signal_data)
        {
            chart_Data.Series[0].Points.Clear();
            chart_Data.Series[1].Points.Clear();
            chart_Data.Series[2].Points.Clear();
            chart_Data.Series[3].Points.Clear();
            chart_Data.Series[4].Points.Clear();

            double t_Start_uSec = Ts_Sec * start_Index * 1E6;
            double t_End_uSec = 720 * Ts_Sec * down_Sample_Raito * 1E6 + t_Start_uSec;

            for (int k = 0; k < signal_data.Length; k++)
            {
                double t_uSec = k * Ts_Sec * down_Sample_Raito * 1E6 + t_Start_uSec; // mSec

                chart_Data.Series[0].Points.AddXY(t_uSec, signal_data[k]);
                chart_Data.Series[1].Points.AddXY(t_uSec, data_Sine_Product[k]);
            }
            chart_Data.ChartAreas[0].AxisX.Minimum = Math.Round(t_Start_uSec, 2);
            chart_Data.ChartAreas[0].AxisX.Maximum = Math.Round(t_End_uSec, 2);

            chart_Data.Series[2].Points.AddXY(peak_Time_Sec * 1E6, 1);
            chart_Data.Series[2].Points.AddXY(peak_Time_Sec * 1E6, -1);

            chart_Data.Series[3].Points.AddXY(time_Of_Flight_Sec * 1E6, 1);
            chart_Data.Series[3].Points.AddXY(time_Of_Flight_Sec * 1E6, -1);


            chart_Data.Series[4].Points.AddXY(tOF_Filtered_Sec * 1E6, 1);
            chart_Data.Series[4].Points.AddXY(tOF_Filtered_Sec * 1E6, -1);


        }

        private void Show_TOF_Signal()
        {


            chart1.Series[0].Points.AddXY(total_Time,time_Of_Flight_Sec * 1E6);
            chart1.Series[1].Points.AddXY(total_Time, tOF_Filtered_Sec * 1E6);

            while (chart1.Series[0].Points.Count > MaxTofHistoryPoints)
                chart1.Series[0].Points.RemoveAt(0);

            while (chart1.Series[1].Points.Count > MaxTofHistoryPoints)
                chart1.Series[1].Points.RemoveAt(0);

            total_Time += 0.125;

            if(total_Time > chart1.ChartAreas[0].AxisX.Maximum)
            {
                chart1.ChartAreas[0].AxisX.Minimum += 1;
                chart1.ChartAreas[0].AxisX.Maximum += 1;
            }
        }

        private void Update_Filter_State_Machine()
        {
            switch (filter_State)
            {
                case Filter_State_Enum.Fast:
                    filter_Counter++;
                    if(filter_Counter == 8)
                    {
                        filter_State = Filter_State_Enum.Stable;
                        a = 0.95;
                        filter_Counter = 0;
                    }
                    break;
                case Filter_State_Enum.Stable:
                    if (Math.Abs(tOF_Filtered_Sec - time_Of_Flight_Sec) > 5E-6)
                    {
                        filter_State = Filter_State_Enum.Fast;
                        a = 0;
                        filter_Counter = 0;
                    }
                    else
                    {
                        filter_Counter++;
                        if(filter_Counter == 24)
                        {
                            filter_State=Filter_State_Enum.Ultra_Stable;
                            a = 0.995;
                        }
                    }
                    break;
                case Filter_State_Enum.Ultra_Stable:
                    if (Math.Abs(tOF_Filtered_Sec - time_Of_Flight_Sec) > 5E-6)
                    {
                        filter_State = Filter_State_Enum.Fast;
                        a = 0;
                        filter_Counter = 0;
                    }
                    break;



            }

            tOF_Filtered_Sec = a * tOF_Filtered_Sec + (1 - a) * time_Of_Flight_Sec;
        }

        public void Send_Data_In_Interval(double time_Start_mSec, double time_End_mSec, double time_Delay_mSec)
        {
            tStart_mSec = time_Start_mSec;
            tEnd_mSec = time_End_mSec;
            t_Delay_uSec = time_Delay_mSec;
        }

        public void Set_Filter_Parameters(Filter_Mode_Enum mode, double F_Center)
        {
            filter_Mode = mode;
            f_Center_kHz = F_Center;
            //fc_LPF_kHz = Flpf;
            //fc_HPF_kHz = Fhpf;

            Calculate_IIR_Filter_Parameters();
        }

        private void Calculate_IIR_Filter_Parameters()
        {
            switch (filter_Mode)
            {
                case Filter_Mode_Enum.No_Filter:
                    break;
                case Filter_Mode_Enum.Weak_BandPass_Filter:
                    fc_LPF_kHz = f_Center_kHz / 0.5;
                    fc_HPF_kHz = f_Center_kHz * 0.5;
                    break;
                case Filter_Mode_Enum.Strong_BandPass_Filter:
                    fc_LPF_kHz = f_Center_kHz / 0.95;
                    fc_HPF_kHz = f_Center_kHz * 0.95;
                    break;
                default:
                    break;
            }

            double T_LPF = 1 / (2 * 3.14 * fc_LPF_kHz * 1E3);
            beta = Ts_Sec / (T_LPF + Ts_Sec);

            double T_HPF = 1 / (2 * 3.14 * fc_HPF_kHz * 1E3);
            alpha = T_HPF / (T_HPF + Ts_Sec);


        }

        private double[] Apply_HPF(double[] x)
        {
            if (filter_Mode == Filter_Mode_Enum.No_Filter)
            {
                return x;
            }

            double[] x_HPF = new double[x.Length];
            x_HPF[0] = x[0];
            for (int k = 1; k < x.Length; k++)
            {
                x_HPF[k] = alpha * (x_HPF[k - 1] + x[k] - x[k - 1]);
            }

            return x_HPF;

        }

        private double[] Apply_LPF(double[] x)
        {
            if (filter_Mode == Filter_Mode_Enum.No_Filter)
            {
                return x;
            }

            double[] x_LPF = new double[x.Length];
            x_LPF[0] = x[0];
            for (int k = 1; k < x.Length; k++)
            {
                x_LPF[k] = beta * x[k] + (1 - beta) * x_LPF[k - 1];
            }
            return x_LPF;
        }

        private void Update_Display_Period()
        {
            start_Index = (int)numericUpDown_Start_Index.Value;
            down_Sample_Raito = (int)numericUpDown_Down_Sample_Ratio.Value;

            double start_Time = Ts_Sec * start_Index * 1E6;
            double end_Time = 720 * down_Sample_Raito * Ts_Sec * 1E6 + start_Time;

            label_time_Start.Text = "Start Time (uSec)   " + start_Time.ToString("0.00");
            label_time_End.Text = "End Time (uSec)   " + end_Time.ToString("0.00");
        }

        private double Find_Signal_Phase_And_Frequency(double[] x)
        {
            double bestCorrelation = double.MinValue;
            double best_Phase = 0;

            for (int k = 0; k <= 20; k++)
            {
                double f = 19 + 0.1 * k;
                double sineCorrelation = 0;
                double cosineCorrelation = 0;

                for (int i = 0; i < x.Length; i += 5)
                {
                    double angle = 2 * Math.PI * f * 1E3 * i * Ts_Sec;
                    sineCorrelation += Math.Sin(angle) * x[i];
                    cosineCorrelation += Math.Cos(angle) * x[i];
                }

                // Maximizing sum(x * sin(angle - phase)) over every possible
                // phase is equivalent to taking the magnitude of these two
                // orthogonal correlations. This replaces the 100-phase scan.
                double correlation = sineCorrelation * sineCorrelation +
                                     cosineCorrelation * cosineCorrelation;

                if (correlation > bestCorrelation)
                {
                    bestCorrelation = correlation;
                    best_Phase = Math.Atan2(-cosineCorrelation, sineCorrelation);

                    if (best_Phase < 0)
                        best_Phase += 2 * Math.PI;

                    best_Frequency_kHz = f;
                }
            }

            return best_Phase;
        }

        private double[] Creat_Sine_Product(double[] x, double freq_kHz, double phase)
        {
            double[] y = new double[x.Length];
            double peak = 0;
            for (int k = 0; k < x.Length; k++)
            {
                double t = Ts_Sec * k;
                y[k] = x[k] * Math.Sin(2 * Math.PI * freq_kHz * 1E3 * t - phase);

                if (y[k] > peak)
                {
                    peak = y[k];
                    peak_Time_Sec = t;
                }
            }

            return y;
        }

        private double Find_TOF_Sec(double[] x)
        {
            int n;
            double T_Period_Sec = 1 / (best_Frequency_kHz * 1E3);
            for (n = 1; n < 20; n++)
            {
                double previous_Peak_Time = peak_Time_Sec - n * T_Period_Sec / 2;

                if (Evaluate_Peak(previous_Peak_Time, x) == false) break;
            }

            double TOF_Sec = peak_Time_Sec - (n - 1) * T_Period_Sec / 2 - T_Period_Sec / 4;

            return TOF_Sec;
        }

        private bool Evaluate_Peak(double p_Time_Sec, double[] x)
        {
            bool _is_Good_Peak = false;

            double sum = 0;
            double T_Period_Sec = 1 / (best_Frequency_kHz * 1E3);
            int k0 = (int)Math.Round(p_Time_Sec / Ts_Sec, 0);
            int dk = (int)Math.Round(T_Period_Sec / (4 * Ts_Sec), 0);

            int k_Low = k0 - dk;
            if (k_Low < 0)
            {
                k_Low = 0;
            }

            int k_High = k0 + dk;
            if (k_High > x.Length - 1)
            {
                k_High = x.Length - 1;
            }

            for (int k = k_Low; k <= k_High; k++)
            {
                sum += x[k];

            }

            double average = sum / (2 * dk);
            double ratio = 0;
            double peak_Value = 0;

            if (k0 > 0 && k0 < x.Length)
            {
                peak_Value = x[k0];
            }
            else
            {
                peak_Value = 0;
            }

            if(peak_Value > 0.02)
            {
                ratio = average / x[k0];  // This should be around 0.5
            }
            else
            {
                ratio = 1000;
            }

            if (0.2 <= ratio && ratio <= 0.8)
            {
                _is_Good_Peak = true;
            }
            else
            {
                _is_Good_Peak = false;
            }

            return _is_Good_Peak;
        }

        private void Show_Processing_Result()
        {
            string s = "";
            s += "Filter Mode:\t" + filter_Mode.ToString() + "\r\n";
            s += "Averaging Capture Count:\t" + average_Count.ToString() + "\r\n";
            s += "Detected Frequency(kHz):\t" + best_Frequency_kHz.ToString() + "\r\n";
            s += "Detected Phase(Rad):\t" + signal_Phase.ToString("0.00") + "\r\n";
            s += "Max Peak Time(uSec):\t" + (peak_Time_Sec * 1E6).ToString("0.00") + "\r\n";
            s += "Time Of Flight(uSec):\t" + (time_Of_Flight_Sec * 1E6).ToString("0.00") + "\r\n";
            s += "TOF Filtered(uSec):\t" + (tOF_Filtered_Sec * 1E6).ToString("0.00") + "\r\n";
            s += "TOF Rounded(uSec):\t" + (0.5*Math.Round(tOF_Filtered_Sec * 1E6/0.5,0)).ToString("0.0") + "\r\n";
            s += "Filter State:\t" + filter_State.ToString() + "\r\n";

            textBox1.Text = s;
        }

        private void Update_Info_Icon_Pic()
        {
            if (_isSimulated_Data_Choosen == true)
            {
                pictureBox_Info.Image = Properties.Resources.icons8_information_24_Red__1_;
            }
            else
            {
                pictureBox_Info.Image = Properties.Resources.icons8_information_24__1_;
            }
        }
        public void Start_Generating_Data()
        {
            if (!IsInDesignMode)
                timer1.Start();
        }

        public void Stop_Generating_Data()
        {
            timer1.Stop();
        }
        private void numericUpDown_Amplitude_ValueChanged(object sender, EventArgs e)
        {
            amplitude = (double)numericUpDown_Amplitude.Value;
            Generate_Data();
        }

        private void numericUpDown_T_Delay_ValueChanged(object sender, EventArgs e)
        {
            t_Delay_uSec = (double)numericUpDown_T_Delay.Value;
            Generate_Data();
        }

        private void numericUpDown_T_Rise_ValueChanged(object sender, EventArgs e)
        {
            t_Rise_uSec = (double)numericUpDown_T_Rise.Value;
            Generate_Data();
        }

        private void numericUpDown_T1_ValueChanged(object sender, EventArgs e)
        {
            t1_uSec = (double)numericUpDown_T1.Value;
            Generate_Data();
        }

        private void numericUpDown_F_ValueChanged(object sender, EventArgs e)
        {
            f_kHz = (double)numericUpDown_F.Value;
            Generate_Data();
        }

        private void numericUpDown_Noise_ValueChanged(object sender, EventArgs e)
        {
            noise_Intensity = (double)numericUpDown_Noise_Intensity.Value;
            Generate_Data();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_isSimulated_Data_Choosen)
            {
                Stop_Generating_Data();
                return;
            }

            double[] x0 = Generate_Data();
            double[] x1 = Apply_HPF(x0);
            double[] x2 = Apply_LPF(x1);
            Save_In_Capture_History(x2);


            double[] x3 = Apply_Capture_Averaging(average_Count);

            signal_Phase = Find_Signal_Phase_And_Frequency(x3);
            label_time_End.Text = signal_Phase.ToString();
            //label2.Text = best_Frequency_kHz.ToString();

            double[] x4 = Creat_Sine_Product(x3, best_Frequency_kHz, signal_Phase);
            time_Of_Flight_Sec = Find_TOF_Sec(x4);

            data_Filtered = Down_Sample_Data(x3);
            data_Sine_Product = Down_Sample_Data(x4);
           
            Update_Filter_State_Machine();

            // These diagnostic charts are hidden while the generator is collapsed.
            // Avoid rebuilding thousands of DataPoints when only the main chart is visible.
            if (_isMaximized)
            {
                Show_Signal(data_Filtered);
                Show_Processing_Result();
                Show_TOF_Signal();
            }

            if (Data_Is_Ready != null)
            {
                Data_Is_Ready(this, EventArgs.Empty);
            }
            // label_time_End.Text = Capture_No.ToString();

        }

        public void Set_Average_Count(int n)
        {
            average_Count = n;
        }

        private double[] Copy_History(int index)
        {
            double[] x = new double[14400];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = Capture_History[index, i];
            }
            return x;
        }

        private void Save_In_Capture_History(double[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                Capture_History[Capture_No, i] = x[i];
            }

            Capture_No++;
            if (Capture_No == 64)
            {
                Capture_No = 0;
            }
        }

        private double[] Apply_Capture_Averaging(int average_count)
        {
            double[] x = new double[14400];

            for (int i = 0; i < x.Length; i++)
            {
                int Capture_index = 0;
                double Sum = 0;

                for (int k = 0; k < average_count; k++)
                {
                    Capture_index = Capture_No - k - 1;
                    if (Capture_index < 0)
                    {
                        Capture_index += 64;
                    }
                    Sum += Capture_History[Capture_index, i];
                }
                x[i] = Sum / average_count;
            }
            return x;
        }

        private double[] Down_Sample_Data(double[] x)
        {
            double[] x_Down_Sampled = new double[720];
            int k = 0;
            for (int i = start_Index; i < x.Length; i += down_Sample_Raito)
            {
                x_Down_Sampled[k] = x[i];
                k++;
                if (k == 720) break;
            }

            if (k < 720)
            {
                for (int j = k; j < 720; j++)
                {
                    x_Down_Sampled[j] = 0;
                }
            }
            return x_Down_Sampled;
        }


        private void pictureBox_Info_MouseClick(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                _isMaximized = !_isMaximized;
                Update_Size();
            }
        }

        private void Update_Size()
        {
            if (_isMaximized == true)
            {
                this.Width = 720;
                this.Height = 450;
            }
            else
            {
                this.Width = 24;
                this.Height = 24;
            }
        }

        private void numericUpDown_Start_Index_ValueChanged(object sender, EventArgs e)
        {
            Update_Display_Period();
        }

        public void checkBox_use_Simulated_Click(object sender, EventArgs e)
        {
            _isSimulated_Data_Choosen = !_isSimulated_Data_Choosen;

            if (_isSimulated_Data_Choosen)
                Start_Generating_Data();
            else
                Stop_Generating_Data();

            Update_Info_Icon_Pic();
            if (Mode_Is_Changed != null)
            {
                Mode_Is_Changed(this, EventArgs.Empty);
            }

        }

        private void checkBox_use_Simulated_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
