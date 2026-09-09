namespace CymaLAB_Ver_1._0
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.measurement_Mode_Control1 = new Measurement_Mode_Control.Measurement_Mode_Control();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.averaging_Control1 = new Averaging_Control.Averaging_Control();
            this.transducer_Type1 = new Transducer_Type.Transducer_Type();
            this.filter_Control1 = new Filter_Control.Filter_Control();
            this.advanced_Settings_Control1 = new Advanced_Settings_Control.Advanced_Settings_Control();
            this.signal_Strength_Control1 = new Signal_Strength_Control.Signal_Strength_Control();
            this.command_Box1 = new Command_Box.Command_Box();
            this.tof_Control = new TOF_Contorol.TOF_Control();
            this.farand_Tablet_Chart_Control1 = new Farand_Tablet_Chart.Farand_Tablet_Chart_Control();
            this.timer_Auto_Start_Simulation = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // measurement_Mode_Control1
            // 
            this.measurement_Mode_Control1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.measurement_Mode_Control1.Length = 0D;
            this.measurement_Mode_Control1.Location = new System.Drawing.Point(2, 178);
            this.measurement_Mode_Control1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.measurement_Mode_Control1.Measurement_Mode = Measurement_Mode_Control.Measurement_Mode_Control.Measurement_Mode_Enum.Length_Calculation;
            this.measurement_Mode_Control1.Name = "measurement_Mode_Control1";
            this.measurement_Mode_Control1.Size = new System.Drawing.Size(311, 191);
            this.measurement_Mode_Control1.TabIndex = 6;
            this.measurement_Mode_Control1.Velocity = 0D;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.command_Box1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tof_Control, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.farand_Tablet_Chart_Control1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1426, 774);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.averaging_Control1);
            this.panel1.Controls.Add(this.transducer_Type1);
            this.panel1.Controls.Add(this.filter_Control1);
            this.panel1.Controls.Add(this.advanced_Settings_Control1);
            this.panel1.Controls.Add(this.measurement_Mode_Control1);
            this.panel1.Controls.Add(this.signal_Strength_Control1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 74);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 698);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(182, 295);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // averaging_Control1
            // 
            this.averaging_Control1.Averaging_Captures_Count = 1;
            this.averaging_Control1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.averaging_Control1.Location = new System.Drawing.Point(2, 501);
            this.averaging_Control1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.averaging_Control1.Name = "averaging_Control1";
            this.averaging_Control1.Size = new System.Drawing.Size(311, 126);
            this.averaging_Control1.TabIndex = 8;
            this.averaging_Control1.AveragingChanged += new System.EventHandler(this.averaging_Control1_AveragingChanged);
            // 
            // transducer_Type1
            // 
            this.transducer_Type1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.transducer_Type1.Location = new System.Drawing.Point(0, 2);
            this.transducer_Type1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.transducer_Type1.Name = "transducer_Type1";
            this.transducer_Type1.Piezo_frequency_kHz = 20D;
            this.transducer_Type1.Size = new System.Drawing.Size(311, 98);
            this.transducer_Type1.TabIndex = 10;
            this.transducer_Type1.piezo_frequency_Changed += new System.EventHandler(this.transducer_Type1_piezo_frequency_Changed);
            // 
            // filter_Control1
            // 
            this.filter_Control1.AutoSize = true;
            this.filter_Control1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.filter_Control1.Filter_Mode = Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter;
            this.filter_Control1.Location = new System.Drawing.Point(0, 98);
            this.filter_Control1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.filter_Control1.Name = "filter_Control1";
            this.filter_Control1.Size = new System.Drawing.Size(311, 84);
            this.filter_Control1.TabIndex = 5;
            this.filter_Control1.Filter_Mode_Changed += new System.EventHandler(this.filter_Control1_Filter_Mode_Changed);
            // 
            // advanced_Settings_Control1
            // 
            this.advanced_Settings_Control1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.advanced_Settings_Control1.Discard_Time_uSec = 0D;
            this.advanced_Settings_Control1.Location = new System.Drawing.Point(0, 626);
            this.advanced_Settings_Control1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.advanced_Settings_Control1.Name = "advanced_Settings_Control1";
            this.advanced_Settings_Control1.Reference_TOF_uSec = 0D;
            this.advanced_Settings_Control1.Size = new System.Drawing.Size(311, 130);
            this.advanced_Settings_Control1.TabIndex = 9;
            // 
            // signal_Strength_Control1
            // 
            this.signal_Strength_Control1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.signal_Strength_Control1.Location = new System.Drawing.Point(2, 364);
            this.signal_Strength_Control1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.signal_Strength_Control1.Name = "signal_Strength_Control1";
            this.signal_Strength_Control1.Reciever_Sensitivity = 1;
            this.signal_Strength_Control1.Size = new System.Drawing.Size(311, 143);
            this.signal_Strength_Control1.TabIndex = 7;
            this.signal_Strength_Control1.Transmitter_Power = 1;
            // 
            // command_Box1
            // 
            this.command_Box1._IsConnected = false;
            this.command_Box1._IsLive = true;
            this.command_Box1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.command_Box1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.command_Box1.Load_Path = null;
            this.command_Box1.Location = new System.Drawing.Point(2, 2);
            this.command_Box1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.command_Box1.Name = "command_Box1";
            this.command_Box1.Save_Path = null;
            this.command_Box1.Size = new System.Drawing.Size(334, 68);
            this.command_Box1.TabIndex = 1;
            // 
            // tof_Control
            // 
            this.tof_Control.Auto_TOF = false;
            this.tof_Control.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tof_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tof_Control.Location = new System.Drawing.Point(340, 2);
            this.tof_Control.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tof_Control.MinimumSize = new System.Drawing.Size(525, 72);
            this.tof_Control.Name = "tof_Control";
            this.tof_Control.Sample_Length_cm = 30D;
            this.tof_Control.Sample_Velocity_m_Sec = 3000D;
            this.tof_Control.Size = new System.Drawing.Size(1084, 72);
            this.tof_Control.TabIndex = 0;
            this.tof_Control.TOF_mSec = 0.1D;
            this.tof_Control.TOF_Stable = false;
            // 
            // farand_Tablet_Chart_Control1
            // 
            this.farand_Tablet_Chart_Control1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.farand_Tablet_Chart_Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.farand_Tablet_Chart_Control1.Location = new System.Drawing.Point(341, 75);
            this.farand_Tablet_Chart_Control1.Name = "farand_Tablet_Chart_Control1";
            this.farand_Tablet_Chart_Control1.Size = new System.Drawing.Size(1082, 696);
            this.farand_Tablet_Chart_Control1.TabIndex = 2;
            // 
            // timer_Auto_Start_Simulation
            // 
            this.timer_Auto_Start_Simulation.Interval = 500;
            this.timer_Auto_Start_Simulation.Tick += new System.EventHandler(this.timer_Auto_Start_Simulation_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1426, 774);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(979, 381);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TOF_Contorol.TOF_Control tof_Control;
        private Command_Box.Command_Box command_Box1;
        private Farand_Tablet_Chart.Farand_Tablet_Chart_Control farand_Tablet_Chart_Control1;
        private Filter_Control.Filter_Control filter_Control1;
        private Measurement_Mode_Control.Measurement_Mode_Control measurement_Mode_Control1;
        private Signal_Strength_Control.Signal_Strength_Control signal_Strength_Control1;
        private Averaging_Control.Averaging_Control averaging_Control1;
        private Advanced_Settings_Control.Advanced_Settings_Control advanced_Settings_Control1;
        private Transducer_Type.Transducer_Type transducer_Type1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_Auto_Start_Simulation;
    }
}
