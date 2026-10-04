namespace Farand_Tablet_Chart
{
    partial class Farand_Tablet_Chart_Control
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (renderTimer != null)
                {
                    renderTimer.Stop();
                    renderTimer.Dispose();
                    renderTimer = null;
                }

                if (interactionTimer != null)
                {
                    interactionTimer.Stop();
                    interactionTimer.Dispose();
                    interactionTimer = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBox_ZoomYIn = new System.Windows.Forms.PictureBox();
            this.pictureBox_ZoomXOut = new System.Windows.Forms.PictureBox();
            this.pictureBox_ZoomYOut = new System.Windows.Forms.PictureBox();
            this.pictureBox_ZoomXin = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.signal_Generator1 = new Data_Generate.Signal_Generator();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXin)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartMain
            // 
            this.chartMain.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
            this.chartMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Location = new System.Drawing.Point(0, 0);
            this.chartMain.Margin = new System.Windows.Forms.Padding(0);
            this.chartMain.Name = "chartMain";
            this.chartMain.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.chartMain.Size = new System.Drawing.Size(751, 390);
            this.chartMain.TabIndex = 7;
            // 
            // pictureBox_ZoomYIn
            // 
            this.pictureBox_ZoomYIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomYIn.Image = global::Farand_Tablet_Chart.Properties.Resources.V_Z_In_200;
            this.pictureBox_ZoomYIn.Location = new System.Drawing.Point(2, 139);
            this.pictureBox_ZoomYIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox_ZoomYIn.Name = "pictureBox_ZoomYIn";
            this.pictureBox_ZoomYIn.Size = new System.Drawing.Size(44, 48);
            this.pictureBox_ZoomYIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomYIn.TabIndex = 8;
            this.pictureBox_ZoomYIn.TabStop = false;
            this.pictureBox_ZoomYIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomYIn.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomYIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // pictureBox_ZoomXOut
            // 
            this.pictureBox_ZoomXOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomXOut.Image = global::Farand_Tablet_Chart.Properties.Resources.H_Z_Out_200;
            this.pictureBox_ZoomXOut.Location = new System.Drawing.Point(372, 396);
            this.pictureBox_ZoomXOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox_ZoomXOut.Name = "pictureBox_ZoomXOut";
            this.pictureBox_ZoomXOut.Size = new System.Drawing.Size(44, 49);
            this.pictureBox_ZoomXOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomXOut.TabIndex = 8;
            this.pictureBox_ZoomXOut.TabStop = false;
            this.pictureBox_ZoomXOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomXOut.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomXOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // pictureBox_ZoomYOut
            // 
            this.pictureBox_ZoomYOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomYOut.Image = global::Farand_Tablet_Chart.Properties.Resources.V_Z_Out_200;
            this.pictureBox_ZoomYOut.Location = new System.Drawing.Point(2, 207);
            this.pictureBox_ZoomYOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox_ZoomYOut.Name = "pictureBox_ZoomYOut";
            this.pictureBox_ZoomYOut.Size = new System.Drawing.Size(44, 48);
            this.pictureBox_ZoomYOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomYOut.TabIndex = 8;
            this.pictureBox_ZoomYOut.TabStop = false;
            this.pictureBox_ZoomYOut.Click += new System.EventHandler(this.pictureBox_ZoomYOut_Click);
            this.pictureBox_ZoomYOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomYOut.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomYOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // pictureBox_ZoomXin
            // 
            this.pictureBox_ZoomXin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomXin.Image = global::Farand_Tablet_Chart.Properties.Resources.H_Z_In_200;
            this.pictureBox_ZoomXin.Location = new System.Drawing.Point(435, 396);
            this.pictureBox_ZoomXin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox_ZoomXin.Name = "pictureBox_ZoomXin";
            this.pictureBox_ZoomXin.Size = new System.Drawing.Size(44, 49);
            this.pictureBox_ZoomXin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomXin.TabIndex = 4;
            this.pictureBox_ZoomXin.TabStop = false;
            this.pictureBox_ZoomXin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomXin.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomXin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomXin, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomXOut, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomYIn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomYOut, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(819, 447);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 5);
            this.panel3.Controls.Add(this.signal_Generator1);
            this.panel3.Controls.Add(this.chartMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(50, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 5);
            this.panel3.Size = new System.Drawing.Size(751, 390);
            this.panel3.TabIndex = 2;
            // 
            // signal_Generator1
            // 
            this.signal_Generator1._IsMaximized = false;
            this.signal_Generator1._IsSimulated_Data_Choosen = true;
            this.signal_Generator1.Amplitude = 1D;
            this.signal_Generator1.AutoScroll = true;
            this.signal_Generator1.Average_Count = 1;
            this.signal_Generator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.signal_Generator1.Down_Sample_Raito = 20;
            this.signal_Generator1.F_kHz = 20D;
            this.signal_Generator1.Fc_HPF_kHz = 10D;
            this.signal_Generator1.Fc_LPF_kHz = 40D;
            this.signal_Generator1.Filter_Mode = Data_Generate.Signal_Generator.Filter_Mode_Enum.No_Filter;
            this.signal_Generator1.Location = new System.Drawing.Point(2, 2);
            this.signal_Generator1.Margin = new System.Windows.Forms.Padding(2);
            this.signal_Generator1.Name = "signal_Generator1";
            this.signal_Generator1.Noise_Intensity = 0.1D;
            this.signal_Generator1.Size = new System.Drawing.Size(18, 20);
            this.signal_Generator1.Start_Index = 0;
            this.signal_Generator1.T_Delay_uSec = 100D;
            this.signal_Generator1.T_Rise_uSec = 250D;
            this.signal_Generator1.T1_uSec = 50D;
            this.signal_Generator1.TabIndex = 8;
            this.signal_Generator1.TStart_mSec = 0D;
            this.signal_Generator1.Mode_Is_Changed += new System.EventHandler(this.signal_Generator1_Simulated_Data_Choosen);
            // 
            // Farand_Tablet_Chart_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Farand_Tablet_Chart_Control";
            this.Size = new System.Drawing.Size(819, 447);
            this.Load += new System.EventHandler(this.Farand_Tablet_Chart_Control_Load);
            this.SizeChanged += new System.EventHandler(this.Farand_Tablet_Chart_Control_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXin)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.PictureBox pictureBox_ZoomXin;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private System.Windows.Forms.PictureBox pictureBox_ZoomYIn;
        private System.Windows.Forms.PictureBox pictureBox_ZoomYOut;
        private System.Windows.Forms.PictureBox pictureBox_ZoomXOut;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        public Data_Generate.Signal_Generator signal_Generator1;
    }
}