namespace Hlk_Wifi_Wrapper_Component
{
    partial class HLK_Wifi_Wrapper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// Do not create another Dispose(bool disposing) in Form1.cs.
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
        /// Required method for Designer support.
        /// Do not modify the contents of this method with the code editor unless necessary.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblConnection = new System.Windows.Forms.Label();
            this.groupBoxPacket = new System.Windows.Forms.GroupBox();
            this.labelFrames = new System.Windows.Forms.Label();
            this.numFramesPerPacket = new System.Windows.Forms.NumericUpDown();
            this.chkAutoHeartbeat = new System.Windows.Forms.CheckBox();
            this.lblPacket = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lanTcpClient = new LAN_Tcp_Client_Communication_Component.LAN_Tcp_Client_Communication_Component();
            this.btnConnect = new System.Windows.Forms.Button();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.chartSamples = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelTop.SuspendLayout();
            this.groupBoxPacket.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFramesPerPacket)).BeginInit();
            this.panelStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panelTop.Controls.Add(this.lblConnection);
            this.panelTop.Controls.Add(this.groupBoxPacket);
            this.panelTop.Controls.Add(this.btnDisconnect);
            this.panelTop.Controls.Add(this.lanTcpClient);
            this.panelTop.Controls.Add(this.btnConnect);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(8);
            this.panelTop.Size = new System.Drawing.Size(570, 80);
            this.panelTop.TabIndex = 0;
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblConnection.Location = new System.Drawing.Point(178, 50);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(79, 15);
            this.lblConnection.TabIndex = 3;
            this.lblConnection.Text = "Disconnected";
            // 
            // groupBoxPacket
            // 
            this.groupBoxPacket.Controls.Add(this.labelFrames);
            this.groupBoxPacket.Controls.Add(this.numFramesPerPacket);
            this.groupBoxPacket.Controls.Add(this.chkAutoHeartbeat);
            this.groupBoxPacket.Controls.Add(this.lblPacket);
            this.groupBoxPacket.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBoxPacket.Location = new System.Drawing.Point(263, 3);
            this.groupBoxPacket.Name = "groupBoxPacket";
            this.groupBoxPacket.Size = new System.Drawing.Size(303, 69);
            this.groupBoxPacket.TabIndex = 1;
            this.groupBoxPacket.TabStop = false;
            this.groupBoxPacket.Text = "Packet Settings";
            // 
            // labelFrames
            // 
            this.labelFrames.AutoSize = true;
            this.labelFrames.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelFrames.Location = new System.Drawing.Point(12, 22);
            this.labelFrames.Name = "labelFrames";
            this.labelFrames.Size = new System.Drawing.Size(88, 15);
            this.labelFrames.TabIndex = 0;
            this.labelFrames.Text = "Frames/Packet:";
            // 
            // numFramesPerPacket
            // 
            this.numFramesPerPacket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(74)))));
            this.numFramesPerPacket.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.numFramesPerPacket.Location = new System.Drawing.Point(108, 19);
            this.numFramesPerPacket.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFramesPerPacket.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFramesPerPacket.Name = "numFramesPerPacket";
            this.numFramesPerPacket.Size = new System.Drawing.Size(80, 23);
            this.numFramesPerPacket.TabIndex = 1;
            this.numFramesPerPacket.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // chkAutoHeartbeat
            // 
            this.chkAutoHeartbeat.AutoSize = true;
            this.chkAutoHeartbeat.Checked = true;
            this.chkAutoHeartbeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoHeartbeat.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkAutoHeartbeat.Location = new System.Drawing.Point(194, 22);
            this.chkAutoHeartbeat.Name = "chkAutoHeartbeat";
            this.chkAutoHeartbeat.Size = new System.Drawing.Size(107, 19);
            this.chkAutoHeartbeat.TabIndex = 2;
            this.chkAutoHeartbeat.Text = "Auto Heartbeat";
            this.chkAutoHeartbeat.UseVisualStyleBackColor = true;
            // 
            // lblPacket
            // 
            this.lblPacket.AutoSize = true;
            this.lblPacket.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPacket.Location = new System.Drawing.Point(12, 42);
            this.lblPacket.Name = "lblPacket";
            this.lblPacket.Size = new System.Drawing.Size(54, 15);
            this.lblPacket.TabIndex = 3;
            this.lblPacket.Text = "Packet: 0";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.AutoSize = true;
            this.btnDisconnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(74)))));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDisconnect.Location = new System.Drawing.Point(82, 42);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(78, 27);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            // 
            // lanTcpClient
            // 
            this.lanTcpClient.AutoReconnect = false;
            this.lanTcpClient.AutoReconnectDelayMs = 2000;
            this.lanTcpClient.BackColor = System.Drawing.Color.Transparent;
            this.lanTcpClient.BlinkOnReceive = true;
            this.lanTcpClient.ConnectTimeoutMs = 5000;
            this.lanTcpClient.EnableHandshake = false;
            this.lanTcpClient.EnableKeepAlive = true;
            this.lanTcpClient.HandshakeFrame = new byte[] {
        ((byte)(81)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(81))};
            this.lanTcpClient.HandshakeHexString = "51 00 00 00 00 00 00 51";
            this.lanTcpClient.HandshakeIntervalMs = 2000;
            this.lanTcpClient.IpChangeReconnectDelayMs = 1000;
            this.lanTcpClient.Location = new System.Drawing.Point(0, 0);
            this.lanTcpClient.LockIpTextBoxWhileConnected = false;
            this.lanTcpClient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lanTcpClient.Name = "lanTcpClient";
            this.lanTcpClient.NoDelay = true;
            this.lanTcpClient.ReceiveBufferSize = 8192;
            this.lanTcpClient.ReconnectOnIpTextChanged = true;
            this.lanTcpClient.RxActivityHoldMs = 500;
            this.lanTcpClient.SendHandshakeOnConnect = true;
            this.lanTcpClient.SendHandshakeWhenNoRx = true;
            this.lanTcpClient.ServerIp = "192.168.16.254";
            this.lanTcpClient.ServerPort = 5000;
            this.lanTcpClient.Size = new System.Drawing.Size(246, 30);
            this.lanTcpClient.SynchronizingObject = null;
            this.lanTcpClient.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.AutoSize = true;
            this.btnConnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(74)))));
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnConnect.Location = new System.Drawing.Point(11, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(64, 27);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Location = new System.Drawing.Point(0, 151);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(8);
            this.panelStats.Size = new System.Drawing.Size(809, 30);
            this.panelStats.TabIndex = 2;
            // 
            // lblStats
            // 
            this.lblStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblStats.Location = new System.Drawing.Point(3, 8);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(567, 14);
            this.lblStats.TabIndex = 0;
            this.lblStats.Text = "No data";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chartSamples
            // 
            this.chartSamples.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            chartArea4.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea4.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea4.AxisX.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea4.AxisX.Title = "Sample Index";
            chartArea4.AxisX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            chartArea4.AxisX2.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            chartArea4.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea4.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea4.AxisY.Title = "Value";
            chartArea4.AxisY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            chartArea4.AxisY2.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            chartArea4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            chartArea4.Name = "MainArea";
            this.chartSamples.ChartAreas.Add(chartArea4);
            this.chartSamples.Location = new System.Drawing.Point(0, 3);
            this.chartSamples.Name = "chartSamples";
            series4.BorderWidth = 2;
            series4.ChartArea = "MainArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Name = "Samples";
            this.chartSamples.Series.Add(series4);
            this.chartSamples.Size = new System.Drawing.Size(570, 259);
            this.chartSamples.TabIndex = 0;
            this.chartSamples.Text = "chartSamples";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtLog.Location = new System.Drawing.Point(0, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(570, 151);
            this.txtLog.TabIndex = 0;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.splitContainerMain.Location = new System.Drawing.Point(0, 75);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.chartSamples);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.txtLog);
            this.splitContainerMain.Panel2.Controls.Add(this.panelStats);
            this.splitContainerMain.Size = new System.Drawing.Size(570, 437);
            this.splitContainerMain.SplitterDistance = 257;
            this.splitContainerMain.TabIndex = 1;
            // 
            // HLK_Wifi_Wrapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(30, 30);
            this.Name = "HLK_Wifi_Wrapper";
            this.Size = new System.Drawing.Size(576, 516);
            this.Load += new System.EventHandler(this.HLK_Wifi_Wrapper_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBoxPacket.ResumeLayout(false);
            this.groupBoxPacket.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFramesPerPacket)).EndInit();
            this.panelStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSamples)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblConnection;
        private LAN_Tcp_Client_Communication_Component.LAN_Tcp_Client_Communication_Component lanTcpClient;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBoxPacket;
        private System.Windows.Forms.Label labelFrames;
        private System.Windows.Forms.NumericUpDown numFramesPerPacket;
        private System.Windows.Forms.CheckBox chkAutoHeartbeat;
        private System.Windows.Forms.Label lblPacket;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSamples;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.SplitContainer splitContainerMain;
    }
}