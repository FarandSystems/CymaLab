namespace Test_App
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tableLayoutLeft = new System.Windows.Forms.TableLayoutPanel();
            this.lblConnection = new System.Windows.Forms.Label();
            this.lblPacketInfo = new System.Windows.Forms.Label();
            this.txtSamplesPreview = new System.Windows.Forms.TextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.panelConnection = new System.Windows.Forms.Panel();
            this.gbTcp = new System.Windows.Forms.GroupBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.lblFramesPerPacket = new System.Windows.Forms.Label();
            this.numFramesPerPacket = new System.Windows.Forms.NumericUpDown();
            this.btnApplyConnection = new System.Windows.Forms.Button();
            this.btnStartReconnect = new System.Windows.Forms.Button();
            this.btnStopReconnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.chkAutoConnectOnLoad = new System.Windows.Forms.CheckBox();
            this.chkAutoHeartbeat = new System.Windows.Forms.CheckBox();
            this.chkChartUpdate = new System.Windows.Forms.CheckBox();
            this.chkLogUpdate = new System.Windows.Forms.CheckBox();
            this.chkStatsUpdate = new System.Windows.Forms.CheckBox();
            this.chkSuspendUiWhenMinimized = new System.Windows.Forms.CheckBox();
            this.btnApplyOptions = new System.Windows.Forms.Button();
            this.tabCommands = new System.Windows.Forms.TabPage();
            this.panelCommands = new System.Windows.Forms.Panel();
            this.gbHeartbeat = new System.Windows.Forms.GroupBox();
            this.btnHeartbeatAsync = new System.Windows.Forms.Button();
            this.btnHeartbeatFireForget = new System.Windows.Forms.Button();
            this.gbSampling = new System.Windows.Forms.GroupBox();
            this.lblDownSampleRatio = new System.Windows.Forms.Label();
            this.numDownSampleRatio = new System.Windows.Forms.NumericUpDown();
            this.lblStartIndex = new System.Windows.Forms.Label();
            this.numStartIndex = new System.Windows.Forms.NumericUpDown();
            this.lblPreTriggerTime = new System.Windows.Forms.Label();
            this.numPreTriggerTime = new System.Windows.Forms.NumericUpDown();
            this.btnSendSamplingAsync = new System.Windows.Forms.Button();
            this.gbFiltering = new System.Windows.Forms.GroupBox();
            this.lblLpfMode = new System.Windows.Forms.Label();
            this.numLpfMode = new System.Windows.Forms.NumericUpDown();
            this.lblLpfFrequency = new System.Windows.Forms.Label();
            this.numLpfFrequency = new System.Windows.Forms.NumericUpDown();
            this.lblHpfMode = new System.Windows.Forms.Label();
            this.numHpfMode = new System.Windows.Forms.NumericUpDown();
            this.lblHpfFrequency = new System.Windows.Forms.Label();
            this.numHpfFrequency = new System.Windows.Forms.NumericUpDown();
            this.btnSendFilteringAsync = new System.Windows.Forms.Button();
            this.gbPulse = new System.Windows.Forms.GroupBox();
            this.lblPulseWidth = new System.Windows.Forms.Label();
            this.numPulseWidth = new System.Windows.Forms.NumericUpDown();
            this.btnSendPulseAsync = new System.Windows.Forms.Button();
            this.gbMeasurement = new System.Windows.Forms.GroupBox();
            this.lblMeasurementMode = new System.Windows.Forms.Label();
            this.numMeasurementMode = new System.Windows.Forms.NumericUpDown();
            this.lblSampleLength = new System.Windows.Forms.Label();
            this.numSampleLength = new System.Windows.Forms.NumericUpDown();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.numVelocity = new System.Windows.Forms.NumericUpDown();
            this.btnSendMeasurementAsync = new System.Windows.Forms.Button();
            this.gbLastCommand = new System.Windows.Forms.GroupBox();
            this.lblLastCommand = new System.Windows.Forms.Label();
            this.tabPropertyGrid = new System.Windows.Forms.TabPage();
            this.propertyGridWrapper = new System.Windows.Forms.PropertyGrid();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.hlkWifiWrapper1 = new Hlk_Wifi_Wrapper_Component.HLK_Wifi_Wrapper();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tableLayoutLeft.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabConnection.SuspendLayout();
            this.panelConnection.SuspendLayout();
            this.gbTcp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFramesPerPacket)).BeginInit();
            this.gbOptions.SuspendLayout();
            this.tabCommands.SuspendLayout();
            this.panelCommands.SuspendLayout();
            this.gbHeartbeat.SuspendLayout();
            this.gbSampling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDownSampleRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPreTriggerTime)).BeginInit();
            this.gbFiltering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLpfMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLpfFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHpfMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHpfFrequency)).BeginInit();
            this.gbPulse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidth)).BeginInit();
            this.gbMeasurement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurementMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVelocity)).BeginInit();
            this.gbLastCommand.SuspendLayout();
            this.tabPropertyGrid.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tableLayoutLeft);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainerMain.Size = new System.Drawing.Size(1400, 850);
            this.splitContainerMain.SplitterDistance = 577;
            this.splitContainerMain.TabIndex = 0;
            // 
            // tableLayoutLeft
            // 
            this.tableLayoutLeft.ColumnCount = 1;
            this.tableLayoutLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutLeft.Controls.Add(this.hlkWifiWrapper1, 0, 0);
            this.tableLayoutLeft.Controls.Add(this.lblConnection, 0, 1);
            this.tableLayoutLeft.Controls.Add(this.lblPacketInfo, 0, 2);
            this.tableLayoutLeft.Controls.Add(this.txtSamplesPreview, 0, 3);
            this.tableLayoutLeft.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutLeft.Name = "tableLayoutLeft";
            this.tableLayoutLeft.RowCount = 4;
            this.tableLayoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 530F));
            this.tableLayoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutLeft.Size = new System.Drawing.Size(579, 847);
            this.tableLayoutLeft.TabIndex = 0;
            // 
            // lblConnection
            // 
            this.lblConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConnection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConnection.Location = new System.Drawing.Point(3, 530);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(573, 32);
            this.lblConnection.TabIndex = 1;
            this.lblConnection.Text = "Connection: -";
            this.lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPacketInfo
            // 
            this.lblPacketInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPacketInfo.Location = new System.Drawing.Point(3, 562);
            this.lblPacketInfo.Name = "lblPacketInfo";
            this.lblPacketInfo.Size = new System.Drawing.Size(573, 70);
            this.lblPacketInfo.TabIndex = 2;
            this.lblPacketInfo.Text = "Packet: -";
            this.lblPacketInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSamplesPreview
            // 
            this.txtSamplesPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSamplesPreview.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtSamplesPreview.Location = new System.Drawing.Point(3, 635);
            this.txtSamplesPreview.Multiline = true;
            this.txtSamplesPreview.Name = "txtSamplesPreview";
            this.txtSamplesPreview.ReadOnly = true;
            this.txtSamplesPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSamplesPreview.Size = new System.Drawing.Size(573, 209);
            this.txtSamplesPreview.TabIndex = 3;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabConnection);
            this.tabControlMain.Controls.Add(this.tabCommands);
            this.tabControlMain.Controls.Add(this.tabPropertyGrid);
            this.tabControlMain.Controls.Add(this.tabLog);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(819, 850);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabConnection
            // 
            this.tabConnection.Controls.Add(this.panelConnection);
            this.tabConnection.Location = new System.Drawing.Point(4, 22);
            this.tabConnection.Name = "tabConnection";
            this.tabConnection.Size = new System.Drawing.Size(811, 824);
            this.tabConnection.TabIndex = 0;
            this.tabConnection.Text = "Connection / Properties";
            // 
            // panelConnection
            // 
            this.panelConnection.AutoScroll = true;
            this.panelConnection.Controls.Add(this.gbTcp);
            this.panelConnection.Controls.Add(this.gbOptions);
            this.panelConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConnection.Location = new System.Drawing.Point(0, 0);
            this.panelConnection.Name = "panelConnection";
            this.panelConnection.Padding = new System.Windows.Forms.Padding(10);
            this.panelConnection.Size = new System.Drawing.Size(811, 824);
            this.panelConnection.TabIndex = 0;
            // 
            // gbTcp
            // 
            this.gbTcp.Controls.Add(this.lblIp);
            this.gbTcp.Controls.Add(this.txtIp);
            this.gbTcp.Controls.Add(this.lblPort);
            this.gbTcp.Controls.Add(this.numPort);
            this.gbTcp.Controls.Add(this.lblFramesPerPacket);
            this.gbTcp.Controls.Add(this.numFramesPerPacket);
            this.gbTcp.Controls.Add(this.btnApplyConnection);
            this.gbTcp.Controls.Add(this.btnStartReconnect);
            this.gbTcp.Controls.Add(this.btnStopReconnect);
            this.gbTcp.Controls.Add(this.btnDisconnect);
            this.gbTcp.Location = new System.Drawing.Point(10, 10);
            this.gbTcp.Name = "gbTcp";
            this.gbTcp.Size = new System.Drawing.Size(720, 150);
            this.gbTcp.TabIndex = 0;
            this.gbTcp.TabStop = false;
            this.gbTcp.Text = "TCP Connection";
            // 
            // lblIp
            // 
            this.lblIp.Location = new System.Drawing.Point(20, 33);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(120, 22);
            this.lblIp.TabIndex = 0;
            this.lblIp.Text = "Server IP:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(150, 30);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(150, 20);
            this.txtIp.TabIndex = 1;
            this.txtIp.Text = "192.168.16.254";
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(20, 68);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(120, 22);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(150, 65);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(100, 20);
            this.numPort.TabIndex = 3;
            this.numPort.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // lblFramesPerPacket
            // 
            this.lblFramesPerPacket.Location = new System.Drawing.Point(20, 103);
            this.lblFramesPerPacket.Name = "lblFramesPerPacket";
            this.lblFramesPerPacket.Size = new System.Drawing.Size(120, 22);
            this.lblFramesPerPacket.TabIndex = 4;
            this.lblFramesPerPacket.Text = "Frames/Packet:";
            // 
            // numFramesPerPacket
            // 
            this.numFramesPerPacket.Location = new System.Drawing.Point(150, 100);
            this.numFramesPerPacket.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numFramesPerPacket.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFramesPerPacket.Name = "numFramesPerPacket";
            this.numFramesPerPacket.Size = new System.Drawing.Size(100, 20);
            this.numFramesPerPacket.TabIndex = 5;
            this.numFramesPerPacket.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // btnApplyConnection
            // 
            this.btnApplyConnection.Location = new System.Drawing.Point(330, 28);
            this.btnApplyConnection.Name = "btnApplyConnection";
            this.btnApplyConnection.Size = new System.Drawing.Size(150, 30);
            this.btnApplyConnection.TabIndex = 6;
            this.btnApplyConnection.Text = "Apply Properties";
            this.btnApplyConnection.Click += new System.EventHandler(this.btnApplyConnection_Click);
            // 
            // btnStartReconnect
            // 
            this.btnStartReconnect.Location = new System.Drawing.Point(330, 63);
            this.btnStartReconnect.Name = "btnStartReconnect";
            this.btnStartReconnect.Size = new System.Drawing.Size(150, 30);
            this.btnStartReconnect.TabIndex = 7;
            this.btnStartReconnect.Text = "Start Auto Reconnect";
            this.btnStartReconnect.Click += new System.EventHandler(this.btnStartReconnect_Click);
            // 
            // btnStopReconnect
            // 
            this.btnStopReconnect.Location = new System.Drawing.Point(330, 98);
            this.btnStopReconnect.Name = "btnStopReconnect";
            this.btnStopReconnect.Size = new System.Drawing.Size(150, 30);
            this.btnStopReconnect.TabIndex = 8;
            this.btnStopReconnect.Text = "Stop Auto Reconnect";
            this.btnStopReconnect.Click += new System.EventHandler(this.btnStopReconnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(500, 63);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(120, 30);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chkAutoConnectOnLoad);
            this.gbOptions.Controls.Add(this.chkAutoHeartbeat);
            this.gbOptions.Controls.Add(this.chkChartUpdate);
            this.gbOptions.Controls.Add(this.chkLogUpdate);
            this.gbOptions.Controls.Add(this.chkStatsUpdate);
            this.gbOptions.Controls.Add(this.chkSuspendUiWhenMinimized);
            this.gbOptions.Controls.Add(this.btnApplyOptions);
            this.gbOptions.Location = new System.Drawing.Point(10, 175);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(720, 220);
            this.gbOptions.TabIndex = 1;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Wrapper Options";
            // 
            // chkAutoConnectOnLoad
            // 
            this.chkAutoConnectOnLoad.Location = new System.Drawing.Point(20, 30);
            this.chkAutoConnectOnLoad.Name = "chkAutoConnectOnLoad";
            this.chkAutoConnectOnLoad.Size = new System.Drawing.Size(260, 24);
            this.chkAutoConnectOnLoad.TabIndex = 0;
            this.chkAutoConnectOnLoad.Text = "Auto connect on load";
            // 
            // chkAutoHeartbeat
            // 
            this.chkAutoHeartbeat.Location = new System.Drawing.Point(20, 60);
            this.chkAutoHeartbeat.Name = "chkAutoHeartbeat";
            this.chkAutoHeartbeat.Size = new System.Drawing.Size(260, 24);
            this.chkAutoHeartbeat.TabIndex = 1;
            this.chkAutoHeartbeat.Text = "Auto heartbeat";
            // 
            // chkChartUpdate
            // 
            this.chkChartUpdate.Location = new System.Drawing.Point(20, 90);
            this.chkChartUpdate.Name = "chkChartUpdate";
            this.chkChartUpdate.Size = new System.Drawing.Size(260, 24);
            this.chkChartUpdate.TabIndex = 2;
            this.chkChartUpdate.Text = "Enable chart update";
            // 
            // chkLogUpdate
            // 
            this.chkLogUpdate.Location = new System.Drawing.Point(20, 120);
            this.chkLogUpdate.Name = "chkLogUpdate";
            this.chkLogUpdate.Size = new System.Drawing.Size(260, 24);
            this.chkLogUpdate.TabIndex = 3;
            this.chkLogUpdate.Text = "Enable log update";
            // 
            // chkStatsUpdate
            // 
            this.chkStatsUpdate.Location = new System.Drawing.Point(20, 150);
            this.chkStatsUpdate.Name = "chkStatsUpdate";
            this.chkStatsUpdate.Size = new System.Drawing.Size(260, 24);
            this.chkStatsUpdate.TabIndex = 4;
            this.chkStatsUpdate.Text = "Enable stats update";
            // 
            // chkSuspendUiWhenMinimized
            // 
            this.chkSuspendUiWhenMinimized.Location = new System.Drawing.Point(20, 180);
            this.chkSuspendUiWhenMinimized.Name = "chkSuspendUiWhenMinimized";
            this.chkSuspendUiWhenMinimized.Size = new System.Drawing.Size(300, 24);
            this.chkSuspendUiWhenMinimized.TabIndex = 5;
            this.chkSuspendUiWhenMinimized.Text = "Suspend UI work when minimized";
            // 
            // btnApplyOptions
            // 
            this.btnApplyOptions.Location = new System.Drawing.Point(330, 30);
            this.btnApplyOptions.Name = "btnApplyOptions";
            this.btnApplyOptions.Size = new System.Drawing.Size(150, 30);
            this.btnApplyOptions.TabIndex = 6;
            this.btnApplyOptions.Text = "Apply Options";
            this.btnApplyOptions.Click += new System.EventHandler(this.btnApplyOptions_Click);
            // 
            // tabCommands
            // 
            this.tabCommands.Controls.Add(this.panelCommands);
            this.tabCommands.Location = new System.Drawing.Point(4, 22);
            this.tabCommands.Name = "tabCommands";
            this.tabCommands.Size = new System.Drawing.Size(811, 824);
            this.tabCommands.TabIndex = 1;
            this.tabCommands.Text = "Commands";
            // 
            // panelCommands
            // 
            this.panelCommands.AutoScroll = true;
            this.panelCommands.Controls.Add(this.gbHeartbeat);
            this.panelCommands.Controls.Add(this.gbSampling);
            this.panelCommands.Controls.Add(this.gbFiltering);
            this.panelCommands.Controls.Add(this.gbPulse);
            this.panelCommands.Controls.Add(this.gbMeasurement);
            this.panelCommands.Controls.Add(this.gbLastCommand);
            this.panelCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCommands.Location = new System.Drawing.Point(0, 0);
            this.panelCommands.Name = "panelCommands";
            this.panelCommands.Padding = new System.Windows.Forms.Padding(10);
            this.panelCommands.Size = new System.Drawing.Size(811, 824);
            this.panelCommands.TabIndex = 0;
            // 
            // gbHeartbeat
            // 
            this.gbHeartbeat.Controls.Add(this.btnHeartbeatAsync);
            this.gbHeartbeat.Controls.Add(this.btnHeartbeatFireForget);
            this.gbHeartbeat.Location = new System.Drawing.Point(10, 10);
            this.gbHeartbeat.Name = "gbHeartbeat";
            this.gbHeartbeat.Size = new System.Drawing.Size(720, 80);
            this.gbHeartbeat.TabIndex = 0;
            this.gbHeartbeat.TabStop = false;
            this.gbHeartbeat.Text = "Heartbeat";
            // 
            // btnHeartbeatAsync
            // 
            this.btnHeartbeatAsync.Location = new System.Drawing.Point(20, 30);
            this.btnHeartbeatAsync.Name = "btnHeartbeatAsync";
            this.btnHeartbeatAsync.Size = new System.Drawing.Size(180, 30);
            this.btnHeartbeatAsync.TabIndex = 0;
            this.btnHeartbeatAsync.Text = "Send Heartbeat Async";
            this.btnHeartbeatAsync.Click += new System.EventHandler(this.btnHeartbeatAsync_Click);
            // 
            // btnHeartbeatFireForget
            // 
            this.btnHeartbeatFireForget.Location = new System.Drawing.Point(220, 30);
            this.btnHeartbeatFireForget.Name = "btnHeartbeatFireForget";
            this.btnHeartbeatFireForget.Size = new System.Drawing.Size(210, 30);
            this.btnHeartbeatFireForget.TabIndex = 1;
            this.btnHeartbeatFireForget.Text = "Send Heartbeat Non-Async";
            this.btnHeartbeatFireForget.Click += new System.EventHandler(this.btnHeartbeatFireForget_Click);
            // 
            // gbSampling
            // 
            this.gbSampling.Controls.Add(this.lblDownSampleRatio);
            this.gbSampling.Controls.Add(this.numDownSampleRatio);
            this.gbSampling.Controls.Add(this.lblStartIndex);
            this.gbSampling.Controls.Add(this.numStartIndex);
            this.gbSampling.Controls.Add(this.lblPreTriggerTime);
            this.gbSampling.Controls.Add(this.numPreTriggerTime);
            this.gbSampling.Controls.Add(this.btnSendSamplingAsync);
            this.gbSampling.Location = new System.Drawing.Point(10, 105);
            this.gbSampling.Name = "gbSampling";
            this.gbSampling.Size = new System.Drawing.Size(720, 150);
            this.gbSampling.TabIndex = 1;
            this.gbSampling.TabStop = false;
            this.gbSampling.Text = "Sampling Parameters";
            // 
            // lblDownSampleRatio
            // 
            this.lblDownSampleRatio.Location = new System.Drawing.Point(20, 33);
            this.lblDownSampleRatio.Name = "lblDownSampleRatio";
            this.lblDownSampleRatio.Size = new System.Drawing.Size(130, 22);
            this.lblDownSampleRatio.TabIndex = 0;
            this.lblDownSampleRatio.Text = "Down Sample Ratio:";
            // 
            // numDownSampleRatio
            // 
            this.numDownSampleRatio.Location = new System.Drawing.Point(170, 30);
            this.numDownSampleRatio.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numDownSampleRatio.Name = "numDownSampleRatio";
            this.numDownSampleRatio.Size = new System.Drawing.Size(100, 20);
            this.numDownSampleRatio.TabIndex = 1;
            this.numDownSampleRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblStartIndex
            // 
            this.lblStartIndex.Location = new System.Drawing.Point(20, 68);
            this.lblStartIndex.Name = "lblStartIndex";
            this.lblStartIndex.Size = new System.Drawing.Size(130, 22);
            this.lblStartIndex.TabIndex = 2;
            this.lblStartIndex.Text = "Start Index:";
            // 
            // numStartIndex
            // 
            this.numStartIndex.Location = new System.Drawing.Point(170, 65);
            this.numStartIndex.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numStartIndex.Name = "numStartIndex";
            this.numStartIndex.Size = new System.Drawing.Size(100, 20);
            this.numStartIndex.TabIndex = 3;
            // 
            // lblPreTriggerTime
            // 
            this.lblPreTriggerTime.Location = new System.Drawing.Point(20, 103);
            this.lblPreTriggerTime.Name = "lblPreTriggerTime";
            this.lblPreTriggerTime.Size = new System.Drawing.Size(130, 22);
            this.lblPreTriggerTime.TabIndex = 4;
            this.lblPreTriggerTime.Text = "Pre Trigger Time:";
            // 
            // numPreTriggerTime
            // 
            this.numPreTriggerTime.Location = new System.Drawing.Point(170, 100);
            this.numPreTriggerTime.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
            this.numPreTriggerTime.Name = "numPreTriggerTime";
            this.numPreTriggerTime.Size = new System.Drawing.Size(120, 20);
            this.numPreTriggerTime.TabIndex = 5;
            // 
            // btnSendSamplingAsync
            // 
            this.btnSendSamplingAsync.Location = new System.Drawing.Point(330, 30);
            this.btnSendSamplingAsync.Name = "btnSendSamplingAsync";
            this.btnSendSamplingAsync.Size = new System.Drawing.Size(120, 30);
            this.btnSendSamplingAsync.TabIndex = 6;
            this.btnSendSamplingAsync.Text = "Send Async";
            this.btnSendSamplingAsync.Click += new System.EventHandler(this.btnSendSamplingAsync_Click);
            // 
            // gbFiltering
            // 
            this.gbFiltering.Controls.Add(this.lblLpfMode);
            this.gbFiltering.Controls.Add(this.numLpfMode);
            this.gbFiltering.Controls.Add(this.lblLpfFrequency);
            this.gbFiltering.Controls.Add(this.numLpfFrequency);
            this.gbFiltering.Controls.Add(this.lblHpfMode);
            this.gbFiltering.Controls.Add(this.numHpfMode);
            this.gbFiltering.Controls.Add(this.lblHpfFrequency);
            this.gbFiltering.Controls.Add(this.numHpfFrequency);
            this.gbFiltering.Controls.Add(this.btnSendFilteringAsync);
            this.gbFiltering.Location = new System.Drawing.Point(10, 270);
            this.gbFiltering.Name = "gbFiltering";
            this.gbFiltering.Size = new System.Drawing.Size(720, 170);
            this.gbFiltering.TabIndex = 2;
            this.gbFiltering.TabStop = false;
            this.gbFiltering.Text = "Filtering Parameters";
            // 
            // lblLpfMode
            // 
            this.lblLpfMode.Location = new System.Drawing.Point(20, 33);
            this.lblLpfMode.Name = "lblLpfMode";
            this.lblLpfMode.Size = new System.Drawing.Size(130, 22);
            this.lblLpfMode.TabIndex = 0;
            this.lblLpfMode.Text = "LPF Mode:";
            // 
            // numLpfMode
            // 
            this.numLpfMode.Location = new System.Drawing.Point(170, 30);
            this.numLpfMode.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numLpfMode.Name = "numLpfMode";
            this.numLpfMode.Size = new System.Drawing.Size(100, 20);
            this.numLpfMode.TabIndex = 1;
            // 
            // lblLpfFrequency
            // 
            this.lblLpfFrequency.Location = new System.Drawing.Point(20, 68);
            this.lblLpfFrequency.Name = "lblLpfFrequency";
            this.lblLpfFrequency.Size = new System.Drawing.Size(130, 22);
            this.lblLpfFrequency.TabIndex = 2;
            this.lblLpfFrequency.Text = "LPF Frequency:";
            // 
            // numLpfFrequency
            // 
            this.numLpfFrequency.Location = new System.Drawing.Point(170, 65);
            this.numLpfFrequency.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numLpfFrequency.Name = "numLpfFrequency";
            this.numLpfFrequency.Size = new System.Drawing.Size(100, 20);
            this.numLpfFrequency.TabIndex = 3;
            // 
            // lblHpfMode
            // 
            this.lblHpfMode.Location = new System.Drawing.Point(20, 103);
            this.lblHpfMode.Name = "lblHpfMode";
            this.lblHpfMode.Size = new System.Drawing.Size(130, 22);
            this.lblHpfMode.TabIndex = 4;
            this.lblHpfMode.Text = "HPF Mode:";
            // 
            // numHpfMode
            // 
            this.numHpfMode.Location = new System.Drawing.Point(170, 100);
            this.numHpfMode.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numHpfMode.Name = "numHpfMode";
            this.numHpfMode.Size = new System.Drawing.Size(100, 20);
            this.numHpfMode.TabIndex = 5;
            // 
            // lblHpfFrequency
            // 
            this.lblHpfFrequency.Location = new System.Drawing.Point(20, 138);
            this.lblHpfFrequency.Name = "lblHpfFrequency";
            this.lblHpfFrequency.Size = new System.Drawing.Size(130, 22);
            this.lblHpfFrequency.TabIndex = 6;
            this.lblHpfFrequency.Text = "HPF Frequency:";
            // 
            // numHpfFrequency
            // 
            this.numHpfFrequency.Location = new System.Drawing.Point(170, 135);
            this.numHpfFrequency.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numHpfFrequency.Name = "numHpfFrequency";
            this.numHpfFrequency.Size = new System.Drawing.Size(100, 20);
            this.numHpfFrequency.TabIndex = 7;
            // 
            // btnSendFilteringAsync
            // 
            this.btnSendFilteringAsync.Location = new System.Drawing.Point(330, 30);
            this.btnSendFilteringAsync.Name = "btnSendFilteringAsync";
            this.btnSendFilteringAsync.Size = new System.Drawing.Size(120, 30);
            this.btnSendFilteringAsync.TabIndex = 8;
            this.btnSendFilteringAsync.Text = "Send Async";
            this.btnSendFilteringAsync.Click += new System.EventHandler(this.btnSendFilteringAsync_Click);
            // 
            // gbPulse
            // 
            this.gbPulse.Controls.Add(this.lblPulseWidth);
            this.gbPulse.Controls.Add(this.numPulseWidth);
            this.gbPulse.Controls.Add(this.btnSendPulseAsync);
            this.gbPulse.Location = new System.Drawing.Point(10, 455);
            this.gbPulse.Name = "gbPulse";
            this.gbPulse.Size = new System.Drawing.Size(720, 110);
            this.gbPulse.TabIndex = 3;
            this.gbPulse.TabStop = false;
            this.gbPulse.Text = "Pulse Parameters";
            // 
            // lblPulseWidth
            // 
            this.lblPulseWidth.Location = new System.Drawing.Point(20, 38);
            this.lblPulseWidth.Name = "lblPulseWidth";
            this.lblPulseWidth.Size = new System.Drawing.Size(130, 22);
            this.lblPulseWidth.TabIndex = 0;
            this.lblPulseWidth.Text = "Pulse Width:";
            // 
            // numPulseWidth
            // 
            this.numPulseWidth.Location = new System.Drawing.Point(170, 35);
            this.numPulseWidth.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numPulseWidth.Name = "numPulseWidth";
            this.numPulseWidth.Size = new System.Drawing.Size(120, 20);
            this.numPulseWidth.TabIndex = 1;
            // 
            // btnSendPulseAsync
            // 
            this.btnSendPulseAsync.Location = new System.Drawing.Point(330, 30);
            this.btnSendPulseAsync.Name = "btnSendPulseAsync";
            this.btnSendPulseAsync.Size = new System.Drawing.Size(120, 30);
            this.btnSendPulseAsync.TabIndex = 2;
            this.btnSendPulseAsync.Text = "Send Async";
            this.btnSendPulseAsync.Click += new System.EventHandler(this.btnSendPulseAsync_Click);
            // 
            // gbMeasurement
            // 
            this.gbMeasurement.Controls.Add(this.lblMeasurementMode);
            this.gbMeasurement.Controls.Add(this.numMeasurementMode);
            this.gbMeasurement.Controls.Add(this.lblSampleLength);
            this.gbMeasurement.Controls.Add(this.numSampleLength);
            this.gbMeasurement.Controls.Add(this.lblVelocity);
            this.gbMeasurement.Controls.Add(this.numVelocity);
            this.gbMeasurement.Controls.Add(this.btnSendMeasurementAsync);
            this.gbMeasurement.Location = new System.Drawing.Point(10, 580);
            this.gbMeasurement.Name = "gbMeasurement";
            this.gbMeasurement.Size = new System.Drawing.Size(720, 150);
            this.gbMeasurement.TabIndex = 4;
            this.gbMeasurement.TabStop = false;
            this.gbMeasurement.Text = "Measurement Options";
            // 
            // lblMeasurementMode
            // 
            this.lblMeasurementMode.Location = new System.Drawing.Point(20, 33);
            this.lblMeasurementMode.Name = "lblMeasurementMode";
            this.lblMeasurementMode.Size = new System.Drawing.Size(130, 22);
            this.lblMeasurementMode.TabIndex = 0;
            this.lblMeasurementMode.Text = "Measurement Mode:";
            // 
            // numMeasurementMode
            // 
            this.numMeasurementMode.Location = new System.Drawing.Point(170, 30);
            this.numMeasurementMode.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numMeasurementMode.Name = "numMeasurementMode";
            this.numMeasurementMode.Size = new System.Drawing.Size(100, 20);
            this.numMeasurementMode.TabIndex = 1;
            // 
            // lblSampleLength
            // 
            this.lblSampleLength.Location = new System.Drawing.Point(20, 68);
            this.lblSampleLength.Name = "lblSampleLength";
            this.lblSampleLength.Size = new System.Drawing.Size(130, 22);
            this.lblSampleLength.TabIndex = 2;
            this.lblSampleLength.Text = "Sample Length:";
            // 
            // numSampleLength
            // 
            this.numSampleLength.Location = new System.Drawing.Point(170, 65);
            this.numSampleLength.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSampleLength.Name = "numSampleLength";
            this.numSampleLength.Size = new System.Drawing.Size(100, 20);
            this.numSampleLength.TabIndex = 3;
            // 
            // lblVelocity
            // 
            this.lblVelocity.Location = new System.Drawing.Point(20, 103);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(130, 22);
            this.lblVelocity.TabIndex = 4;
            this.lblVelocity.Text = "Velocity:";
            // 
            // numVelocity
            // 
            this.numVelocity.Location = new System.Drawing.Point(170, 100);
            this.numVelocity.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numVelocity.Name = "numVelocity";
            this.numVelocity.Size = new System.Drawing.Size(100, 20);
            this.numVelocity.TabIndex = 5;
            // 
            // btnSendMeasurementAsync
            // 
            this.btnSendMeasurementAsync.Location = new System.Drawing.Point(330, 30);
            this.btnSendMeasurementAsync.Name = "btnSendMeasurementAsync";
            this.btnSendMeasurementAsync.Size = new System.Drawing.Size(120, 30);
            this.btnSendMeasurementAsync.TabIndex = 6;
            this.btnSendMeasurementAsync.Text = "Send Async";
            this.btnSendMeasurementAsync.Click += new System.EventHandler(this.btnSendMeasurementAsync_Click);
            // 
            // gbLastCommand
            // 
            this.gbLastCommand.Controls.Add(this.lblLastCommand);
            this.gbLastCommand.Location = new System.Drawing.Point(10, 745);
            this.gbLastCommand.Name = "gbLastCommand";
            this.gbLastCommand.Size = new System.Drawing.Size(720, 71);
            this.gbLastCommand.TabIndex = 5;
            this.gbLastCommand.TabStop = false;
            this.gbLastCommand.Text = "Last Command Frame";
            // 
            // lblLastCommand
            // 
            this.lblLastCommand.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblLastCommand.Location = new System.Drawing.Point(6, 16);
            this.lblLastCommand.Name = "lblLastCommand";
            this.lblLastCommand.Size = new System.Drawing.Size(680, 45);
            this.lblLastCommand.TabIndex = 0;
            this.lblLastCommand.Text = "Last CMD: -";
            // 
            // tabPropertyGrid
            // 
            this.tabPropertyGrid.Controls.Add(this.propertyGridWrapper);
            this.tabPropertyGrid.Location = new System.Drawing.Point(4, 22);
            this.tabPropertyGrid.Name = "tabPropertyGrid";
            this.tabPropertyGrid.Size = new System.Drawing.Size(259, 824);
            this.tabPropertyGrid.TabIndex = 2;
            this.tabPropertyGrid.Text = "PropertyGrid";
            // 
            // propertyGridWrapper
            // 
            this.propertyGridWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridWrapper.Location = new System.Drawing.Point(0, 0);
            this.propertyGridWrapper.Name = "propertyGridWrapper";
            this.propertyGridWrapper.Size = new System.Drawing.Size(259, 824);
            this.propertyGridWrapper.TabIndex = 0;
            this.propertyGridWrapper.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridWrapper_PropertyValueChanged);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(259, 824);
            this.tabLog.TabIndex = 3;
            this.tabLog.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(259, 824);
            this.txtLog.TabIndex = 0;
            // 
            // hlkWifiWrapper1
            // 
            this.hlkWifiWrapper1.AutoConnectOnLoad = true;
            this.hlkWifiWrapper1.AutoHeartbeatEnabled = true;
            this.hlkWifiWrapper1.AutoReconnect = true;
            this.hlkWifiWrapper1.AutoReconnectDelayMs = 2000;
            this.hlkWifiWrapper1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.hlkWifiWrapper1.ConnectTimeoutMs = 5000;
            this.hlkWifiWrapper1.DownSampleRatio = 1;
            this.hlkWifiWrapper1.EnableChartUpdate = true;
            this.hlkWifiWrapper1.EnableHandshake = true;
            this.hlkWifiWrapper1.EnableLogUpdate = true;
            this.hlkWifiWrapper1.EnableStatsUpdate = true;
            this.hlkWifiWrapper1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hlkWifiWrapper1.FramesPerPacket = 240;
            this.hlkWifiWrapper1.HandshakeHexString = "51 00 00 00 00 00 00 00 00 00 00 00 00 00 00 51";
            this.hlkWifiWrapper1.HandshakeIntervalMs = 1500;
            this.hlkWifiWrapper1.HpfFrequency = 0;
            this.hlkWifiWrapper1.HpfMode = 0;
            this.hlkWifiWrapper1.IpChangeReconnectDelayMs = 1000;
            this.hlkWifiWrapper1.IsExpanded = false;
            this.hlkWifiWrapper1.Location = new System.Drawing.Point(3, 3);
            this.hlkWifiWrapper1.LpfFrequency = 0;
            this.hlkWifiWrapper1.LpfMode = 0;
            this.hlkWifiWrapper1.MeasurementMode = 0;
            this.hlkWifiWrapper1.MinimumSize = new System.Drawing.Size(30, 30);
            this.hlkWifiWrapper1.Name = "hlkWifiWrapper1";
            this.hlkWifiWrapper1.PreTriggerTime = 0;
            this.hlkWifiWrapper1.PulseWidth = 0;
            this.hlkWifiWrapper1.ReceiveBufferSize = 8192;
            this.hlkWifiWrapper1.ReconnectOnIpTextChanged = true;
            this.hlkWifiWrapper1.SampleLength = 0;
            this.hlkWifiWrapper1.SamplesPerPacket = 720;
            this.hlkWifiWrapper1.SendHandshakeOnConnect = true;
            this.hlkWifiWrapper1.SendHandshakeWhenNoRx = true;
            this.hlkWifiWrapper1.ServerIp = "192.168.16.254";
            this.hlkWifiWrapper1.ServerPort = 5000;
            this.hlkWifiWrapper1.Size = new System.Drawing.Size(35, 35);
            this.hlkWifiWrapper1.StartIndex = 0;
            this.hlkWifiWrapper1.SuspendUiWorkWhenMinimized = true;
            this.hlkWifiWrapper1.TabIndex = 0;
            this.hlkWifiWrapper1.Velocity = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 850);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HLK WiFi Wrapper Test App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tableLayoutLeft.ResumeLayout(false);
            this.tableLayoutLeft.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabConnection.ResumeLayout(false);
            this.panelConnection.ResumeLayout(false);
            this.gbTcp.ResumeLayout(false);
            this.gbTcp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFramesPerPacket)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.tabCommands.ResumeLayout(false);
            this.panelCommands.ResumeLayout(false);
            this.gbHeartbeat.ResumeLayout(false);
            this.gbSampling.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDownSampleRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPreTriggerTime)).EndInit();
            this.gbFiltering.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLpfMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLpfFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHpfMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHpfFrequency)).EndInit();
            this.gbPulse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidth)).EndInit();
            this.gbMeasurement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurementMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVelocity)).EndInit();
            this.gbLastCommand.ResumeLayout(false);
            this.tabPropertyGrid.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutLeft;
        private Hlk_Wifi_Wrapper_Component.HLK_Wifi_Wrapper hlkWifiWrapper1;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Label lblPacketInfo;
        private System.Windows.Forms.TextBox txtSamplesPreview;

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabConnection;
        private System.Windows.Forms.TabPage tabCommands;
        private System.Windows.Forms.TabPage tabPropertyGrid;
        private System.Windows.Forms.TabPage tabLog;

        private System.Windows.Forms.Panel panelConnection;
        private System.Windows.Forms.GroupBox gbTcp;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label lblFramesPerPacket;
        private System.Windows.Forms.NumericUpDown numFramesPerPacket;
        private System.Windows.Forms.Button btnApplyConnection;
        private System.Windows.Forms.Button btnStartReconnect;
        private System.Windows.Forms.Button btnStopReconnect;
        private System.Windows.Forms.Button btnDisconnect;

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chkAutoConnectOnLoad;
        private System.Windows.Forms.CheckBox chkAutoHeartbeat;
        private System.Windows.Forms.CheckBox chkChartUpdate;
        private System.Windows.Forms.CheckBox chkLogUpdate;
        private System.Windows.Forms.CheckBox chkStatsUpdate;
        private System.Windows.Forms.CheckBox chkSuspendUiWhenMinimized;
        private System.Windows.Forms.Button btnApplyOptions;

        private System.Windows.Forms.Panel panelCommands;

        private System.Windows.Forms.GroupBox gbHeartbeat;
        private System.Windows.Forms.Button btnHeartbeatAsync;
        private System.Windows.Forms.Button btnHeartbeatFireForget;

        private System.Windows.Forms.GroupBox gbSampling;
        private System.Windows.Forms.Label lblDownSampleRatio;
        private System.Windows.Forms.NumericUpDown numDownSampleRatio;
        private System.Windows.Forms.Label lblStartIndex;
        private System.Windows.Forms.NumericUpDown numStartIndex;
        private System.Windows.Forms.Label lblPreTriggerTime;
        private System.Windows.Forms.NumericUpDown numPreTriggerTime;
        private System.Windows.Forms.Button btnSendSamplingAsync;

        private System.Windows.Forms.GroupBox gbFiltering;
        private System.Windows.Forms.Label lblLpfMode;
        private System.Windows.Forms.NumericUpDown numLpfMode;
        private System.Windows.Forms.Label lblLpfFrequency;
        private System.Windows.Forms.NumericUpDown numLpfFrequency;
        private System.Windows.Forms.Label lblHpfMode;
        private System.Windows.Forms.NumericUpDown numHpfMode;
        private System.Windows.Forms.Label lblHpfFrequency;
        private System.Windows.Forms.NumericUpDown numHpfFrequency;
        private System.Windows.Forms.Button btnSendFilteringAsync;

        private System.Windows.Forms.GroupBox gbPulse;
        private System.Windows.Forms.Label lblPulseWidth;
        private System.Windows.Forms.NumericUpDown numPulseWidth;
        private System.Windows.Forms.Button btnSendPulseAsync;

        private System.Windows.Forms.GroupBox gbMeasurement;
        private System.Windows.Forms.Label lblMeasurementMode;
        private System.Windows.Forms.NumericUpDown numMeasurementMode;
        private System.Windows.Forms.Label lblSampleLength;
        private System.Windows.Forms.NumericUpDown numSampleLength;
        private System.Windows.Forms.Label lblVelocity;
        private System.Windows.Forms.NumericUpDown numVelocity;
        private System.Windows.Forms.Button btnSendMeasurementAsync;

        private System.Windows.Forms.GroupBox gbLastCommand;
        private System.Windows.Forms.Label lblLastCommand;

        private System.Windows.Forms.PropertyGrid propertyGridWrapper;
        private System.Windows.Forms.TextBox txtLog;
    }
}