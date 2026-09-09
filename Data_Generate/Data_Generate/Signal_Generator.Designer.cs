namespace Data_Generate
{
    partial class Signal_Generator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_Data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.numericUpDown_Amplitude = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_T_Delay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_T_Rise = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_T1 = new System.Windows.Forms.NumericUpDown();
            this.label_Amplitude = new System.Windows.Forms.Label();
            this.label_T_Delay_uSec = new System.Windows.Forms.Label();
            this.label_T_Rise_uSec = new System.Windows.Forms.Label();
            this.label_T1 = new System.Windows.Forms.Label();
            this.numericUpDown_F = new System.Windows.Forms.NumericUpDown();
            this.label_F = new System.Windows.Forms.Label();
            this.numericUpDown_Noise_Intensity = new System.Windows.Forms.NumericUpDown();
            this.label_Noise = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown_Down_Sample_Ratio = new System.Windows.Forms.NumericUpDown();
            this.label_time_End = new System.Windows.Forms.Label();
            this.numericUpDown_Start_Index = new System.Windows.Forms.NumericUpDown();
            this.label_time_Start = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox_Info = new System.Windows.Forms.PictureBox();
            this.label_Down_Sample_Ratio = new System.Windows.Forms.Label();
            this.label_Start_index = new System.Windows.Forms.Label();
            this.numericUpDown_Delay_Jitter = new System.Windows.Forms.NumericUpDown();
            this.label_Delay_jitter = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox_use_Simulated = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_T_Delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_T_Rise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_T1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_F)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Noise_Intensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Down_Sample_Ratio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Start_Index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Delay_Jitter)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_Data
            // 
            this.chart_Data.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.Maximum = 10D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Time(mSec)";
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.Interval = 0.2D;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.AxisY.Minimum = -1D;
            chartArea1.AxisY.Title = "Normalized Intensity";
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Gray;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            chartArea1.Name = "ChartArea1";
            this.chart_Data.ChartAreas.Add(chartArea1);
            this.chart_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Data.Location = new System.Drawing.Point(333, 259);
            this.chart_Data.Name = "chart_Data";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Tomato;
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Aqua;
            series3.Name = "Series3";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Gold;
            series4.Name = "Series4";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            series5.Name = "Series5";
            this.chart_Data.Series.Add(series1);
            this.chart_Data.Series.Add(series2);
            this.chart_Data.Series.Add(series3);
            this.chart_Data.Series.Add(series4);
            this.chart_Data.Series.Add(series5);
            this.chart_Data.Size = new System.Drawing.Size(746, 538);
            this.chart_Data.TabIndex = 0;
            this.chart_Data.Text = "chart1";
            // 
            // numericUpDown_Amplitude
            // 
            this.numericUpDown_Amplitude.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Amplitude.Location = new System.Drawing.Point(195, 47);
            this.numericUpDown_Amplitude.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Amplitude.Name = "numericUpDown_Amplitude";
            this.numericUpDown_Amplitude.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_Amplitude.TabIndex = 1;
            this.numericUpDown_Amplitude.ValueChanged += new System.EventHandler(this.numericUpDown_Amplitude_ValueChanged);
            // 
            // numericUpDown_T_Delay
            // 
            this.numericUpDown_T_Delay.DecimalPlaces = 2;
            this.numericUpDown_T_Delay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_T_Delay.Location = new System.Drawing.Point(195, 78);
            this.numericUpDown_T_Delay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_T_Delay.Name = "numericUpDown_T_Delay";
            this.numericUpDown_T_Delay.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_T_Delay.TabIndex = 2;
            this.numericUpDown_T_Delay.ValueChanged += new System.EventHandler(this.numericUpDown_T_Delay_ValueChanged);
            // 
            // numericUpDown_T_Rise
            // 
            this.numericUpDown_T_Rise.DecimalPlaces = 2;
            this.numericUpDown_T_Rise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_T_Rise.Location = new System.Drawing.Point(195, 140);
            this.numericUpDown_T_Rise.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_T_Rise.Name = "numericUpDown_T_Rise";
            this.numericUpDown_T_Rise.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_T_Rise.TabIndex = 3;
            this.numericUpDown_T_Rise.ValueChanged += new System.EventHandler(this.numericUpDown_T_Rise_ValueChanged);
            // 
            // numericUpDown_T1
            // 
            this.numericUpDown_T1.DecimalPlaces = 2;
            this.numericUpDown_T1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_T1.Location = new System.Drawing.Point(195, 171);
            this.numericUpDown_T1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_T1.Name = "numericUpDown_T1";
            this.numericUpDown_T1.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_T1.TabIndex = 4;
            this.numericUpDown_T1.ValueChanged += new System.EventHandler(this.numericUpDown_T1_ValueChanged);
            // 
            // label_Amplitude
            // 
            this.label_Amplitude.AutoSize = true;
            this.label_Amplitude.ForeColor = System.Drawing.Color.White;
            this.label_Amplitude.Location = new System.Drawing.Point(3, 51);
            this.label_Amplitude.Name = "label_Amplitude";
            this.label_Amplitude.Size = new System.Drawing.Size(67, 16);
            this.label_Amplitude.TabIndex = 5;
            this.label_Amplitude.Text = "Amplitude";
            // 
            // label_T_Delay_uSec
            // 
            this.label_T_Delay_uSec.AutoSize = true;
            this.label_T_Delay_uSec.ForeColor = System.Drawing.Color.White;
            this.label_T_Delay_uSec.Location = new System.Drawing.Point(3, 82);
            this.label_T_Delay_uSec.Name = "label_T_Delay_uSec";
            this.label_T_Delay_uSec.Size = new System.Drawing.Size(97, 16);
            this.label_T_Delay_uSec.TabIndex = 6;
            this.label_T_Delay_uSec.Text = "T Delay (uSec)";
            // 
            // label_T_Rise_uSec
            // 
            this.label_T_Rise_uSec.AutoSize = true;
            this.label_T_Rise_uSec.ForeColor = System.Drawing.Color.White;
            this.label_T_Rise_uSec.Location = new System.Drawing.Point(3, 144);
            this.label_T_Rise_uSec.Name = "label_T_Rise_uSec";
            this.label_T_Rise_uSec.Size = new System.Drawing.Size(89, 16);
            this.label_T_Rise_uSec.TabIndex = 7;
            this.label_T_Rise_uSec.Text = "T Rise (uSec)";
            // 
            // label_T1
            // 
            this.label_T1.AutoSize = true;
            this.label_T1.ForeColor = System.Drawing.Color.White;
            this.label_T1.Location = new System.Drawing.Point(3, 175);
            this.label_T1.Name = "label_T1";
            this.label_T1.Size = new System.Drawing.Size(139, 16);
            this.label_T1.TabIndex = 8;
            this.label_T1.Text = "Time Constant (mSec)";
            // 
            // numericUpDown_F
            // 
            this.numericUpDown_F.DecimalPlaces = 2;
            this.numericUpDown_F.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_F.Location = new System.Drawing.Point(195, 202);
            this.numericUpDown_F.Name = "numericUpDown_F";
            this.numericUpDown_F.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_F.TabIndex = 9;
            this.numericUpDown_F.ValueChanged += new System.EventHandler(this.numericUpDown_F_ValueChanged);
            // 
            // label_F
            // 
            this.label_F.AutoSize = true;
            this.label_F.ForeColor = System.Drawing.Color.White;
            this.label_F.Location = new System.Drawing.Point(3, 206);
            this.label_F.Name = "label_F";
            this.label_F.Size = new System.Drawing.Size(98, 16);
            this.label_F.TabIndex = 10;
            this.label_F.Text = "Frequency (Hz)";
            // 
            // numericUpDown_Noise_Intensity
            // 
            this.numericUpDown_Noise_Intensity.DecimalPlaces = 1;
            this.numericUpDown_Noise_Intensity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Noise_Intensity.Location = new System.Drawing.Point(195, 233);
            this.numericUpDown_Noise_Intensity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Noise_Intensity.Name = "numericUpDown_Noise_Intensity";
            this.numericUpDown_Noise_Intensity.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_Noise_Intensity.TabIndex = 11;
            this.numericUpDown_Noise_Intensity.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Noise_Intensity.ValueChanged += new System.EventHandler(this.numericUpDown_Noise_ValueChanged);
            // 
            // label_Noise
            // 
            this.label_Noise.AutoSize = true;
            this.label_Noise.ForeColor = System.Drawing.Color.White;
            this.label_Noise.Location = new System.Drawing.Point(6, 237);
            this.label_Noise.Name = "label_Noise";
            this.label_Noise.Size = new System.Drawing.Size(94, 16);
            this.label_Noise.TabIndex = 12;
            this.label_Noise.Text = "Noise Intensity";
            // 
            // timer1
            // 
            this.timer1.Interval = 125;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numericUpDown_Down_Sample_Ratio
            // 
            this.numericUpDown_Down_Sample_Ratio.Location = new System.Drawing.Point(195, 295);
            this.numericUpDown_Down_Sample_Ratio.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_Down_Sample_Ratio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Down_Sample_Ratio.Name = "numericUpDown_Down_Sample_Ratio";
            this.numericUpDown_Down_Sample_Ratio.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_Down_Sample_Ratio.TabIndex = 11;
            this.numericUpDown_Down_Sample_Ratio.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_Down_Sample_Ratio.ValueChanged += new System.EventHandler(this.numericUpDown_Start_Index_ValueChanged);
            // 
            // label_time_End
            // 
            this.label_time_End.AutoSize = true;
            this.label_time_End.ForeColor = System.Drawing.Color.White;
            this.label_time_End.Location = new System.Drawing.Point(9, 361);
            this.label_time_End.Name = "label_time_End";
            this.label_time_End.Size = new System.Drawing.Size(65, 16);
            this.label_time_End.TabIndex = 12;
            this.label_time_End.Text = "Time End";
            // 
            // numericUpDown_Start_Index
            // 
            this.numericUpDown_Start_Index.Location = new System.Drawing.Point(195, 264);
            this.numericUpDown_Start_Index.Maximum = new decimal(new int[] {
            14399,
            0,
            0,
            0});
            this.numericUpDown_Start_Index.Name = "numericUpDown_Start_Index";
            this.numericUpDown_Start_Index.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_Start_Index.TabIndex = 11;
            this.numericUpDown_Start_Index.ValueChanged += new System.EventHandler(this.numericUpDown_Start_Index_ValueChanged);
            // 
            // label_time_Start
            // 
            this.label_time_Start.AutoSize = true;
            this.label_time_Start.ForeColor = System.Drawing.Color.White;
            this.label_time_Start.Location = new System.Drawing.Point(6, 330);
            this.label_time_Start.Name = "label_time_Start";
            this.label_time_Start.Size = new System.Drawing.Size(68, 16);
            this.label_time_Start.TabIndex = 12;
            this.label_time_Start.Text = "Time Start";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(0, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "Ultrasonic Piezo Receiver Signal Simulator";
            // 
            // pictureBox_Info
            // 
            this.pictureBox_Info.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox_Info.Image = global::Data_Generate.Properties.Resources.icons8_information_24__1_;
            this.pictureBox_Info.Location = new System.Drawing.Point(3, 0);
            this.pictureBox_Info.Name = "pictureBox_Info";
            this.pictureBox_Info.Size = new System.Drawing.Size(24, 26);
            this.pictureBox_Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Info.TabIndex = 13;
            this.pictureBox_Info.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox_Info, "Receiver Signal Simulator");
            this.pictureBox_Info.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Info_MouseClick);
            // 
            // label_Down_Sample_Ratio
            // 
            this.label_Down_Sample_Ratio.AutoSize = true;
            this.label_Down_Sample_Ratio.ForeColor = System.Drawing.Color.White;
            this.label_Down_Sample_Ratio.Location = new System.Drawing.Point(6, 299);
            this.label_Down_Sample_Ratio.Name = "label_Down_Sample_Ratio";
            this.label_Down_Sample_Ratio.Size = new System.Drawing.Size(164, 16);
            this.label_Down_Sample_Ratio.TabIndex = 12;
            this.label_Down_Sample_Ratio.Text = "Down Sample Ratio (1..20)";
            // 
            // label_Start_index
            // 
            this.label_Start_index.AutoSize = true;
            this.label_Start_index.ForeColor = System.Drawing.Color.White;
            this.label_Start_index.Location = new System.Drawing.Point(6, 268);
            this.label_Start_index.Name = "label_Start_index";
            this.label_Start_index.Size = new System.Drawing.Size(131, 16);
            this.label_Start_index.TabIndex = 12;
            this.label_Start_index.Text = "Start Index (0...14399)";
            // 
            // numericUpDown_Delay_Jitter
            // 
            this.numericUpDown_Delay_Jitter.DecimalPlaces = 2;
            this.numericUpDown_Delay_Jitter.Location = new System.Drawing.Point(195, 109);
            this.numericUpDown_Delay_Jitter.Name = "numericUpDown_Delay_Jitter";
            this.numericUpDown_Delay_Jitter.Size = new System.Drawing.Size(70, 22);
            this.numericUpDown_Delay_Jitter.TabIndex = 2;
            // 
            // label_Delay_jitter
            // 
            this.label_Delay_jitter.AutoSize = true;
            this.label_Delay_jitter.ForeColor = System.Drawing.Color.White;
            this.label_Delay_jitter.Location = new System.Drawing.Point(3, 113);
            this.label_Delay_jitter.Name = "label_Delay_jitter";
            this.label_Delay_jitter.Size = new System.Drawing.Size(116, 16);
            this.label_Delay_jitter.TabIndex = 6;
            this.label_Delay_jitter.Text = "Delay Jitter (uSec)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chart1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chart_Data, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1082, 800);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            chartArea2.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisX.Maximum = 5D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.Title = "Time(mSec)";
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.Gray;
            chartArea2.AxisY.Interval = 50D;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            chartArea2.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisY.Maximum = 200D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.Title = "Normalized Intensity";
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.Gray;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(333, 3);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Gold;
            series6.Name = "Series1";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            series7.Name = "Series2";
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(746, 250);
            this.chart1.TabIndex = 18;
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.checkBox_use_Simulated);
            this.panel1.Controls.Add(this.label_F);
            this.panel1.Controls.Add(this.numericUpDown_Amplitude);
            this.panel1.Controls.Add(this.numericUpDown_T_Delay);
            this.panel1.Controls.Add(this.numericUpDown_T_Rise);
            this.panel1.Controls.Add(this.label_Start_index);
            this.panel1.Controls.Add(this.numericUpDown_Delay_Jitter);
            this.panel1.Controls.Add(this.label_Down_Sample_Ratio);
            this.panel1.Controls.Add(this.numericUpDown_T1);
            this.panel1.Controls.Add(this.label_time_Start);
            this.panel1.Controls.Add(this.label_Amplitude);
            this.panel1.Controls.Add(this.label_time_End);
            this.panel1.Controls.Add(this.label_T_Delay_uSec);
            this.panel1.Controls.Add(this.label_Noise);
            this.panel1.Controls.Add(this.label_Delay_jitter);
            this.panel1.Controls.Add(this.numericUpDown_Start_Index);
            this.panel1.Controls.Add(this.label_T_Rise_uSec);
            this.panel1.Controls.Add(this.numericUpDown_Down_Sample_Ratio);
            this.panel1.Controls.Add(this.label_T1);
            this.panel1.Controls.Add(this.numericUpDown_Noise_Intensity);
            this.panel1.Controls.Add(this.numericUpDown_F);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 538);
            this.panel1.TabIndex = 16;
            // 
            // checkBox_use_Simulated
            // 
            this.checkBox_use_Simulated.AutoSize = true;
            this.checkBox_use_Simulated.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_use_Simulated.ForeColor = System.Drawing.Color.White;
            this.checkBox_use_Simulated.Location = new System.Drawing.Point(9, 13);
            this.checkBox_use_Simulated.Name = "checkBox_use_Simulated";
            this.checkBox_use_Simulated.Size = new System.Drawing.Size(174, 23);
            this.checkBox_use_Simulated.TabIndex = 13;
            this.checkBox_use_Simulated.Text = "Use Simulated Data";
            this.checkBox_use_Simulated.UseVisualStyleBackColor = true;
            this.checkBox_use_Simulated.CheckedChanged += new System.EventHandler(this.checkBox_use_Simulated_CheckedChanged);
            this.checkBox_use_Simulated.Click += new System.EventHandler(this.checkBox_use_Simulated_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.pictureBox_Info);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(324, 250);
            this.panel2.TabIndex = 17;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(6, 49);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(309, 105);
            this.textBox1.TabIndex = 16;
            // 
            // Signal_Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Signal_Generator";
            this.Size = new System.Drawing.Size(1082, 800);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_T_Delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_T_Rise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_T1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_F)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Noise_Intensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Down_Sample_Ratio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Start_Index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Delay_Jitter)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Data;
        private System.Windows.Forms.NumericUpDown numericUpDown_Amplitude;
        private System.Windows.Forms.NumericUpDown numericUpDown_T_Delay;
        private System.Windows.Forms.NumericUpDown numericUpDown_T_Rise;
        private System.Windows.Forms.NumericUpDown numericUpDown_T1;
        private System.Windows.Forms.Label label_Amplitude;
        private System.Windows.Forms.Label label_T_Delay_uSec;
        private System.Windows.Forms.Label label_T_Rise_uSec;
        private System.Windows.Forms.Label label_T1;
        private System.Windows.Forms.NumericUpDown numericUpDown_F;
        private System.Windows.Forms.Label label_F;
        private System.Windows.Forms.NumericUpDown numericUpDown_Noise_Intensity;
        private System.Windows.Forms.Label label_Noise;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Down_Sample_Ratio;
        private System.Windows.Forms.Label label_time_End;
        private System.Windows.Forms.NumericUpDown numericUpDown_Start_Index;
        private System.Windows.Forms.Label label_time_Start;
        private System.Windows.Forms.PictureBox pictureBox_Info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label_Down_Sample_Ratio;
        private System.Windows.Forms.Label label_Start_index;
        private System.Windows.Forms.NumericUpDown numericUpDown_Delay_Jitter;
        private System.Windows.Forms.Label label_Delay_jitter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_use_Simulated;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
