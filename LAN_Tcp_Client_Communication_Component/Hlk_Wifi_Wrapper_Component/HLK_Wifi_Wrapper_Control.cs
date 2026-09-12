using LAN_Tcp_Client_Communication_Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Hlk_Wifi_Wrapper_Component
{
    public partial class HLK_Wifi_Wrapper : UserControl
    {
        // ============================================================
        // UI size
        // ============================================================

        private readonly Size Min_Size = new Size(35, 35);
        private readonly Size Max_Size = new Size(576, 516);

        private bool _isExpanded = false;

        // ============================================================
        // Protocol constants
        // ============================================================

        private const int FRAME_SIZE = 8;
        private const int SAMPLES_PER_FRAME = 3;

        private const int CMD_FRAME_SIZE = 16;

        private const int DEFAULT_FRAMES_PER_PACKET = 240;

        private const byte CMD_HEARTBEAT = 0x01;
        private const byte CMD_SET_SAMPLING_PARAMETERS = 0x02;
        private const byte CMD_SET_FILTERING_PARAMETERS = 0x03;
        private const byte CMD_SET_PULSE_PARAMETERS = 0x04;
        private const byte CMD_SET_MEASUREMENT_PARAMETERS = 0x06;

        private const byte FRAME_HEADER = 0x55;

        private const byte PACKET_MAGIC0 = 0xA5;
        private const byte PACKET_MAGIC1 = 0x5A;
        private const byte PACKET_MAGIC2 = 0xC3;
        private const byte PACKET_MAGIC3 = 0x3C;

        private const int PACKET_SIZE = 2048;
        private const int PAYLOAD_SIZE = 2045;
        private const int GLOBAL_CHECKSUM_SIZE = 3;

        private const int PACKET_HEADER_SIZE = 8;

        private const ushort WAVE_MAX_VALUE = 4015;
        private const ushort RAMP_START_STEP = 10;

        // ============================================================
        // Configurable protocol properties
        // ============================================================

        private int _samplesPerPacket = DEFAULT_FRAMES_PER_PACKET * SAMPLES_PER_FRAME;
        private bool _internalPacketSettingChange = false;
        private bool _initialized = false;

        [Category("Protocol")]
        [Description("Number of ADC samples in one complete MCU packet. Must be a multiple of 3.")]
        public int SamplesPerPacket
        {
            get
            {
                return _samplesPerPacket;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(SamplesPerPacket));

                if (value % SAMPLES_PER_FRAME != 0)
                    throw new ArgumentException("SamplesPerPacket must be a multiple of 3.");

                if (value > MaxSamplesPerPacket)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SamplesPerPacket),
                        $"Maximum allowed samples per packet is {MaxSamplesPerPacket}."
                    );
                }

                _samplesPerPacket = value;

                SyncFramesNumericControl();

                if (_initialized)
                    ResetParser();
            }
        }

        [Category("Protocol")]
        [Description("Number of 8-byte frames in one complete MCU packet.")]
        public int FramesPerPacket
        {
            get
            {
                return SamplesPerPacket / SAMPLES_PER_FRAME;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(FramesPerPacket));

                if (value > MaxFramesPerPacket)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(FramesPerPacket),
                        $"Maximum allowed frames per packet is {MaxFramesPerPacket}."
                    );
                }

                SamplesPerPacket = value * SAMPLES_PER_FRAME;
            }
        }

        private readonly object _rawRxLock = new object();
        private byte[] _rxBytes;

        public event EventHandler Received_Data_Ready;
        public event EventHandler Connection_Ready;
        public event EventHandler Connection_Failed;
        public event EventHandler Connection_Lost;

        [Browsable(false)]
        public byte[] Rx_Bytes
        {
            get
            {
                lock (_rawRxLock)
                {
                    if (_rxBytes == null)
                    {
                        return null;
                    }

                    return (byte[])_rxBytes.Clone();
                }
            }
        }

        [Browsable(false)]
        public int MaxFramesPerPacket
        {
            get
            {
                return (PAYLOAD_SIZE - PACKET_HEADER_SIZE) / FRAME_SIZE;
            }
        }

        [Browsable(false)]
        public int MaxSamplesPerPacket
        {
            get
            {
                return MaxFramesPerPacket * SAMPLES_PER_FRAME;
            }
        }

        [Browsable(false)]
        public int UsedPayloadBytes
        {
            get
            {
                return PACKET_HEADER_SIZE + FramesPerPacket * FRAME_SIZE;
            }
        }

        [Browsable(false)]
        public int PacketPaddingBytes
        {
            get
            {
                return PAYLOAD_SIZE - UsedPayloadBytes;
            }
        }

        [Browsable(false)]
        public int PacketSizeBytes
        {
            get
            {
                return PACKET_SIZE;
            }
        }

        // ============================================================
        // TCP exposed properties
        // ============================================================

        [Category("TCP")]
        public string ServerIp
        {
            get
            {
                return lanTcpClient.ServerIp;
            }
            set
            {
                lanTcpClient.ServerIp = value;
            }
        }

        [Category("TCP")]
        public int ServerPort
        {
            get
            {
                return lanTcpClient.ServerPort;
            }
            set
            {
                lanTcpClient.ServerPort = value;
            }
        }

        [Category("TCP")]
        public int ConnectTimeoutMs
        {
            get
            {
                return lanTcpClient.ConnectTimeoutMs;
            }
            set
            {
                lanTcpClient.ConnectTimeoutMs = value;
            }
        }

        [Category("TCP")]
        public int ReceiveBufferSize
        {
            get
            {
                return lanTcpClient.ReceiveBufferSize;
            }
            set
            {
                lanTcpClient.ReceiveBufferSize = value;
            }
        }

        [Category("TCP")]
        public bool AutoReconnect
        {
            get
            {
                return lanTcpClient.AutoReconnect;
            }
            set
            {
                lanTcpClient.AutoReconnect = value;
            }
        }

        [Category("TCP")]
        public int AutoReconnectDelayMs
        {
            get
            {
                return lanTcpClient.AutoReconnectDelayMs;
            }
            set
            {
                lanTcpClient.AutoReconnectDelayMs = value;
            }
        }

        [Category("TCP")]
        public bool ReconnectOnIpTextChanged
        {
            get
            {
                return lanTcpClient.ReconnectOnIpTextChanged;
            }
            set
            {
                lanTcpClient.ReconnectOnIpTextChanged = value;
            }
        }

        [Category("TCP")]
        public int IpChangeReconnectDelayMs
        {
            get
            {
                return lanTcpClient.IpChangeReconnectDelayMs;
            }
            set
            {
                lanTcpClient.IpChangeReconnectDelayMs = value;
            }
        }

        [Browsable(false)]
        public bool IsConnected
        {
            get
            {
                return lanTcpClient.IsConnected;
            }
        }

        [Browsable(false)]
        public LAN_Tcp_Client_State TcpState
        {
            get
            {
                return lanTcpClient.State;
            }
        }

        // ============================================================
        // Handshake properties
        // ============================================================

        [Category("Handshake")]
        public bool EnableHandshake
        {
            get
            {
                return lanTcpClient.EnableHandshake;
            }
            set
            {
                lanTcpClient.EnableHandshake = value;
            }
        }

        [Category("Handshake")]
        public bool SendHandshakeOnConnect
        {
            get
            {
                return lanTcpClient.SendHandshakeOnConnect;
            }
            set
            {
                lanTcpClient.SendHandshakeOnConnect = value;
            }
        }

        [Category("Handshake")]
        public bool SendHandshakeWhenNoRx
        {
            get
            {
                return lanTcpClient.SendHandshakeWhenNoRx;
            }
            set
            {
                lanTcpClient.SendHandshakeWhenNoRx = value;
            }
        }

        [Category("Handshake")]
        public int HandshakeIntervalMs
        {
            get
            {
                return lanTcpClient.HandshakeIntervalMs;
            }
            set
            {
                lanTcpClient.HandshakeIntervalMs = value;
            }
        }

        [Category("Handshake")]
        public string HandshakeHexString
        {
            get
            {
                return lanTcpClient.HandshakeHexString;
            }
            set
            {
                lanTcpClient.HandshakeHexString = value;
            }
        }

        // ============================================================
        // Performance properties
        // ============================================================

        [Category("Performance")]
        [Description("When true, chart, stats, and log textbox updates are skipped while the component is minimized.")]
        public bool SuspendUiWorkWhenMinimized { get; set; } = true;

        [Category("Performance")]
        [Description("Enable or disable chart updating.")]
        public bool EnableChartUpdate { get; set; } = true;

        [Category("Performance")]
        [Description("Enable or disable log textbox updating.")]
        public bool EnableLogUpdate { get; set; } = true;

        [Category("Performance")]
        [Description("Enable or disable status label updating.")]
        public bool EnableStatsUpdate { get; set; } = true;

        [Category("Startup")]
        [Description("Automatically start TCP reconnect loop when the component loads.")]
        public bool AutoConnectOnLoad { get; set; } = true;

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;

                if (_isExpanded)
                {
                    this.Size = Max_Size;
                }
                else
                {
                    this.Size = Min_Size;

                    RefreshUiAfterExpand();
                }
            }
        }

        private bool ShouldDoUiWork
        {
            get
            {
                return !SuspendUiWorkWhenMinimized || IsExpanded;
            }
        }

        // ============================================================
        // Command option properties
        // ============================================================

        private bool _autoHeartbeatEnabled = true;
        private byte[] _lastCommandFrame;

        [Category("Command Options")]
        public bool AutoHeartbeatEnabled
        {
            get
            {
                if (chkAutoHeartbeat != null)
                    return chkAutoHeartbeat.Checked;

                return _autoHeartbeatEnabled;
            }
            set
            {
                _autoHeartbeatEnabled = value;

                if (chkAutoHeartbeat != null)
                    chkAutoHeartbeat.Checked = value;
            }
        }

        [Category("Command Options - Sampling")]
        public int DownSampleRatio { get; set; } = 1;

        [Category("Command Options - Sampling")]
        public int StartIndex { get; set; } = 0;

        [Category("Command Options - Sampling")]
        public int PreTriggerTime { get; set; } = 0;

        [Category("Command Options - Filtering")]
        public int LpfMode { get; set; } = 0;

        [Category("Command Options - Filtering")]
        public int LpfFrequency { get; set; } = 0;

        [Category("Command Options - Filtering")]
        public int HpfMode { get; set; } = 0;

        [Category("Command Options - Filtering")]
        public int HpfFrequency { get; set; } = 0;

        [Category("Command Options - Pulse")]
        public int PulseWidth { get; set; } = 0;

        [Category("Command Options - Measurement")]
        public int MeasurementMode { get; set; } = 0;

        [Category("Command Options - Measurement")]
        public int SampleLength { get; set; } = 0;

        [Category("Command Options - Measurement")]
        public int Velocity { get; set; } = 0;

        [Browsable(false)]
        public byte[] LastCommandFrame
        {
            get
            {
                if (_lastCommandFrame == null)
                    return null;

                return (byte[])_lastCommandFrame.Clone();
            }
        }

        // ============================================================
        // Parser state
        // ============================================================

        private readonly List<byte> _rxBuffer = new List<byte>(PACKET_SIZE * 10);

        private ushort[] _samplesPacket;
        private double[] _lastPacketSamples;

        private int _sampleIndex = 0;
        private int _framesInPacket = 0;

        private long _validFrames = 0;
        private long _badDrops = 0;
        private long _packetCounter = 0;
        private long _badPackets = 0;
        private long _resyncCount = 0;
        private long _packetCounterErrors = 0;

        private byte? _expectedFrameIndex = null;
        private int _frameErrorCount = 0;

        private uint? _lastMcuPacketCounter = null;

        private ushort? _lastFirstSample = null;
        private bool _continuityOk = true;

        private Series _series;

        // ============================================================
        // Public packet output
        // ============================================================

        public event EventHandler<HLK_Wifi_PacketCompleted_EventArgs> PacketCompleted;

        [Browsable(false)]
        public double[] LastPacketSamples
        {
            get
            {
                if (_lastPacketSamples == null)
                    return null;

                return (double[])_lastPacketSamples.Clone();
            }
        }

        [Browsable(false)]
        public long PacketCounter
        {
            get
            {
                return _packetCounter;
            }
        }

        [Browsable(false)]
        public long BadPackets
        {
            get
            {
                return _badPackets;
            }
        }

        [Browsable(false)]
        public long BadDrops
        {
            get
            {
                return _badDrops;
            }
        }

        [Browsable(false)]
        public long ResyncCount
        {
            get
            {
                return _resyncCount;
            }
        }

        // ============================================================
        // Constructor
        // ============================================================

        public HLK_Wifi_Wrapper()
        {
            InitializeComponent();

            _isExpanded = this.Size != Min_Size;

            Initialize_Tcp();
        }

        // ============================================================
        // Initialization
        // ============================================================

        private void Initialize_Tcp()
        {
            lanTcpClient.ServerPort = 5000;
            lanTcpClient.ReceiveBufferSize = 8192;
            lanTcpClient.ConnectTimeoutMs = 5000;
            lanTcpClient.AutoReconnect = true;
            lanTcpClient.AutoReconnectDelayMs = 2000;

            lanTcpClient.LockIpTextBoxWhileConnected = false;
            lanTcpClient.ReconnectOnIpTextChanged = true;
            lanTcpClient.IpChangeReconnectDelayMs = 1000;

            lanTcpClient.EnableHandshake = true;
            lanTcpClient.SendHandshakeOnConnect = true;
            lanTcpClient.SendHandshakeWhenNoRx = true;
            lanTcpClient.HandshakeIntervalMs = 1500;

            lanTcpClient.HandshakeFrame = new byte[]
            {
                0x51, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x51
            };

            lanTcpClient.DataReceived += LanTcpClient_DataReceived;
            lanTcpClient.StateChanged += LanTcpClient_StateChanged;
            lanTcpClient.ErrorOccurred += LanTcpClient_ErrorOccurred;
            lanTcpClient.LogReceived += LanTcpClient_LogReceived;
            lanTcpClient.Connected += LanTcpClient_Connected;
            lanTcpClient.Disconnected += LanTcpClient_Disconnected;
            lanTcpClient.Connection_Picturebox_Ctrl_Clicked += Connection_Ctrl_Clicked;

            btnConnect.Click += btnConnect_Click;
            btnDisconnect.Click += btnDisconnect_Click;

            numFramesPerPacket.ValueChanged += numFramesPerPacket_ValueChanged;

            ConfigureFramesNumericControl();

            ConfigureChart();

            _initialized = true;

            ResetParser();

            lblConnection.Text = "Disconnected";
            lblPacket.Text = "Packet: 0";
            lblStats.Text = "No data";
        }

        private void ConfigureFramesNumericControl()
        {
            try
            {
                _internalPacketSettingChange = true;

                numFramesPerPacket.Minimum = 1;
                numFramesPerPacket.Maximum = MaxFramesPerPacket;
                numFramesPerPacket.Value = FramesPerPacket;
                numFramesPerPacket.Enabled = true;
            }
            catch
            {
                // Ignore designer range problems.
            }
            finally
            {
                _internalPacketSettingChange = false;
            }
        }

        private void SyncFramesNumericControl()
        {
            if (numFramesPerPacket == null)
                return;

            try
            {
                _internalPacketSettingChange = true;

                numFramesPerPacket.Minimum = 1;
                numFramesPerPacket.Maximum = MaxFramesPerPacket;

                int frames = FramesPerPacket;

                if (frames >= (int)numFramesPerPacket.Minimum &&
                    frames <= (int)numFramesPerPacket.Maximum)
                {
                    numFramesPerPacket.Value = frames;
                }
            }
            catch
            {
                // Ignore designer-time problems.
            }
            finally
            {
                _internalPacketSettingChange = false;
            }
        }

        private void Connection_Ctrl_Clicked(object sender, EventArgs e)
        {
            if (_isExpanded)
            {
                this.Size = Min_Size;
                _isExpanded = false;
            }
            else
            {
                this.Size = Max_Size;
                _isExpanded = true;

                RefreshUiAfterExpand();
            }
        }

        private void RefreshUiAfterExpand()
        {
            if (_lastPacketSamples != null && EnableChartUpdate)
                UpdateChart(_lastPacketSamples);

            if (EnableStatsUpdate)
            {
                lblPacket.Text = $"Packet: {_packetCounter}";

                lblStats.Text =
                    $"Frames/Packet={FramesPerPacket}, " +
                    $"Samples/Packet={SamplesPerPacket}, " +
                    $"PacketBytes={PacketSizeBytes}, " +
                    $"Payload={PAYLOAD_SIZE}, " +
                    $"UsedPayload={UsedPayloadBytes}, " +
                    $"Padding={PacketPaddingBytes}";
            }
        }

        private void ConfigureChart()
        {
            chartSamples.Series.Clear();
            chartSamples.ChartAreas.Clear();

            ChartArea area = new ChartArea("MainArea");
            area.BackColor = Color.FromArgb(30, 30, 30);

            area.AxisX.Title = "Sample Index";
            area.AxisX.TitleForeColor = Color.FromArgb(200, 200, 200);
            area.AxisX.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(200, 200, 200);
            area.AxisX.MajorTickMark.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisX.MinorTickMark.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisX.MajorGrid.Enabled = true;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(80, 80, 80);

            area.AxisY.Title = "Value";
            area.AxisY.TitleForeColor = Color.FromArgb(200, 200, 200);
            area.AxisY.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(200, 200, 200);
            area.AxisY.MajorTickMark.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisY.MinorTickMark.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(80, 80, 80);

            area.AxisX2.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisY2.LineColor = Color.FromArgb(200, 200, 200);
            area.AxisX2.LabelStyle.ForeColor = Color.FromArgb(200, 200, 200);
            area.AxisY2.LabelStyle.ForeColor = Color.FromArgb(200, 200, 200);

            chartSamples.ChartAreas.Add(area);

            _series = new Series("Samples");
            _series.ChartType = SeriesChartType.FastPoint;
            _series.ChartArea = "MainArea";
            _series.BorderWidth = 1;
            _series.MarkerStyle = MarkerStyle.Triangle;
            _series.Color = Color.Lime;

            chartSamples.Series.Add(_series);

            UpdateChartWithEmptyData();
        }

        private void ResetParser()
        {
            _rxBuffer.Clear();

            _samplesPacket = new ushort[SamplesPerPacket];
            _lastPacketSamples = null;

            _sampleIndex = 0;
            _framesInPacket = 0;

            _validFrames = 0;
            _badDrops = 0;
            _packetCounter = 0;
            _badPackets = 0;
            _resyncCount = 0;
            _packetCounterErrors = 0;

            _expectedFrameIndex = null;
            _frameErrorCount = 0;

            _lastMcuPacketCounter = null;

            _lastFirstSample = null;
            _continuityOk = true;

            if (ShouldDoUiWork && EnableChartUpdate)
                UpdateChartWithEmptyData();

            if (ShouldDoUiWork && EnableStatsUpdate)
            {
                if (lblPacket != null)
                    lblPacket.Text = "Packet: 0";

                if (lblStats != null)
                {
                    lblStats.Text =
                        $"Frames/Packet={FramesPerPacket}, " +
                        $"Samples/Packet={SamplesPerPacket}, " +
                        $"PacketBytes={PacketSizeBytes}, " +
                        $"Payload={PAYLOAD_SIZE}, " +
                        $"UsedPayload={UsedPayloadBytes}, " +
                        $"Padding={PacketPaddingBytes}";
                }
            }
        }

        private void UpdateChartWithEmptyData()
        {
            if (_series == null)
                return;

            if (chartSamples == null)
                return;

            if (chartSamples.ChartAreas.Count == 0)
                return;

            double[] empty = new double[SamplesPerPacket];

            _series.Points.Clear();
            _series.Points.DataBindY(empty);

            chartSamples.ChartAreas[0].AxisX.Minimum = 0;
            chartSamples.ChartAreas[0].AxisX.Maximum = SamplesPerPacket - 1;
            chartSamples.ChartAreas[0].AxisY.Minimum = 0;
            chartSamples.ChartAreas[0].AxisY.Maximum = 4096;
        }

        // ============================================================
        // TCP component events
        // ============================================================

        private void LanTcpClient_DataReceived(
            object sender,
            LAN_Tcp_Client_DataReceived_EventArgs e)
        {
            if (e.Data == null || e.Data.Length == 0)
            {
                return;
            }

            lock (_rawRxLock)
            {
                _rxBytes = (byte[])e.Data.Clone();
            }

            Received_Data_Ready?.Invoke(this, EventArgs.Empty);
        }

        private void LanTcpClient_StateChanged(
            object sender,
            LAN_Tcp_Client_StateChanged_EventArgs e)
        {
            if (ShouldDoUiWork && EnableStatsUpdate)
            {
                lblConnection.Text = e.Message;
            }

            btnConnect.Enabled =
                e.State == LAN_Tcp_Client_State.Disconnected ||
                e.State == LAN_Tcp_Client_State.Error;

            btnDisconnect.Enabled =
                e.State == LAN_Tcp_Client_State.Connected;

            if (e.State == LAN_Tcp_Client_State.Error)
            {
                Connection_Failed?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task Send_Data_Async(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length == 0)
            {
                throw new ArgumentException(
                    "Data cannot be empty.",
                    nameof(data));
            }

            if (!lanTcpClient.IsConnected)
            {
                throw new InvalidOperationException(
                    "The Wi-Fi TCP client is not connected.");
            }

            byte[] dataCopy = (byte[])data.Clone();

            await lanTcpClient.SendAsync(dataCopy);
        }

        public async void Send_Data(byte[] data)
        {
            try
            {
                await Send_Data_Async(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Wi-Fi transmission failed: " +
                    ex.Message);
            }
        }

        private void LanTcpClient_ErrorOccurred(object sender, LAN_Tcp_Client_Error_EventArgs e)
        {
            AppendLog("ERROR: " + e.Message + " | " + e.Exception.Message);
        }

        private void LanTcpClient_LogReceived(object sender, LAN_Tcp_Client_Log_EventArgs e)
        {
            AppendLog(e.Timestamp.ToString("HH:mm:ss.fff") + "  " + e.Message);
        }

        private void LanTcpClient_Connected(object sender, EventArgs e)
        {
            AppendLog("Connected event");
            Connection_Ready?.Invoke(this, EventArgs.Empty);
        }


        private void LanTcpClient_Disconnected(object sender, EventArgs e)
        {
            AppendLog("Disconnected event");
            Connection_Lost?.Invoke(this, EventArgs.Empty);
        }

        // ============================================================
        // Raw byte receiver and packet parser
        // ============================================================

        private void AddRawBytes(byte[] data)
        {
            if (data == null || data.Length == 0)
                return;

            _rxBuffer.AddRange(data);

            int maxBuffer = PACKET_SIZE * 12;

            if (_rxBuffer.Count > maxBuffer)
            {
                int keepBytes = PACKET_SIZE * 4;
                int removeCount = _rxBuffer.Count - keepBytes;

                if (removeCount > 0)
                {
                    _rxBuffer.RemoveRange(0, removeCount);
                    _badDrops += removeCount;
                    _resyncCount++;

                    AppendLog("RX buffer trimmed. Parser forced to resync.");
                }
            }
        }

        private void TryExtractPackets()
        {
            while (_rxBuffer.Count >= PACKET_SIZE)
            {
                int packetStart = FindValidPacketStart();

                if (packetStart < 0)
                {
                    int keepBytes = PACKET_SIZE - 1;

                    if (_rxBuffer.Count > keepBytes)
                    {
                        int removeCount = _rxBuffer.Count - keepBytes;
                        _rxBuffer.RemoveRange(0, removeCount);

                        _badDrops += removeCount;
                        _badPackets++;
                        _resyncCount++;

                        AppendLog($"No valid packet found. Dropped {removeCount} bytes and kept {keepBytes} bytes.");
                    }

                    return;
                }

                if (packetStart > 0)
                {
                    _rxBuffer.RemoveRange(0, packetStart);

                    _badDrops += packetStart;
                    _resyncCount++;

                    AppendLog($"Resynchronized. Dropped {packetStart} bytes before valid packet.");
                }

                if (_rxBuffer.Count < PACKET_SIZE)
                    return;

                byte[] packet = _rxBuffer.GetRange(0, PACKET_SIZE).ToArray();
                _rxBuffer.RemoveRange(0, PACKET_SIZE);

                ProcessPacket(packet);
            }
        }

        private int FindValidPacketStart()
        {
            int maxStart = _rxBuffer.Count - PACKET_SIZE;

            for (int start = 0; start <= maxStart; start++)
            {
                if (_rxBuffer[start] != PACKET_MAGIC0)
                    continue;

                if (_rxBuffer[start + 1] != PACKET_MAGIC1)
                    continue;

                if (_rxBuffer[start + 2] != PACKET_MAGIC2)
                    continue;

                if (_rxBuffer[start + 3] != PACKET_MAGIC3)
                    continue;

                if (IsValidPacketAt(start))
                    return start;
            }

            return -1;
        }

        private bool IsValidPacketAt(int start)
        {
            if (start < 0)
                return false;

            if (_rxBuffer.Count < start + PACKET_SIZE)
                return false;

            if (_rxBuffer[start + 0] != PACKET_MAGIC0) return false;
            if (_rxBuffer[start + 1] != PACKET_MAGIC1) return false;
            if (_rxBuffer[start + 2] != PACKET_MAGIC2) return false;
            if (_rxBuffer[start + 3] != PACKET_MAGIC3) return false;

            uint computedChecksum = 0;

            for (int i = 0; i < PAYLOAD_SIZE; i++)
            {
                computedChecksum += _rxBuffer[start + i];
            }

            computedChecksum &= 0xFFFFFF;

            uint receivedChecksum =
                (uint)(_rxBuffer[start + PAYLOAD_SIZE] |
                      (_rxBuffer[start + PAYLOAD_SIZE + 1] << 8) |
                      (_rxBuffer[start + PAYLOAD_SIZE + 2] << 16));

            if (computedChecksum != receivedChecksum)
                return false;

            for (int frame = 0; frame < FramesPerPacket; frame++)
            {
                int index = start + PACKET_HEADER_SIZE + frame * FRAME_SIZE;

                if (_rxBuffer[index] != FRAME_HEADER)
                    return false;

                byte frameIndex = _rxBuffer[index + 7];

                if (frameIndex != (byte)frame)
                    return false;
            }

            for (int i = UsedPayloadBytes; i < PAYLOAD_SIZE; i++)
            {
                if (_rxBuffer[start + i] != 0x00)
                    return false;
            }

            return true;
        }

        private void ProcessPacket(byte[] packet)
        {
            if (packet == null || packet.Length != PACKET_SIZE)
                return;

            uint mcuPacketCounter = ToUInt32LE(
                packet[4],
                packet[5],
                packet[6],
                packet[7]);

            if (_lastMcuPacketCounter.HasValue)
            {
                uint expected = _lastMcuPacketCounter.Value + 1;

                if (mcuPacketCounter != expected)
                {
                    _packetCounterErrors++;

                    AppendLog(
                        $"PACKET COUNTER JUMP: expected {expected}, got {mcuPacketCounter}"
                    );
                }
            }

            _lastMcuPacketCounter = mcuPacketCounter;

            _sampleIndex = 0;
            _framesInPacket = 0;

            for (int frame = 0; frame < FramesPerPacket; frame++)
            {
                int index = PACKET_HEADER_SIZE + frame * FRAME_SIZE;

                ushort s0 = ToUInt16LE(packet[index + 1], packet[index + 2]);
                ushort s1 = ToUInt16LE(packet[index + 3], packet[index + 4]);
                ushort s2 = ToUInt16LE(packet[index + 5], packet[index + 6]);

                byte frameIndex = packet[index + 7];

                if (_expectedFrameIndex.HasValue)
                {
                    byte expectedFrame = (byte)((_expectedFrameIndex.Value + 1) % FramesPerPacket);

                    if (frameIndex != expectedFrame)
                    {
                        _frameErrorCount++;

                        AppendLog(
                            $"FRAME INDEX JUMP: expected {expectedFrame}, got {frameIndex}"
                        );
                    }
                }

                _expectedFrameIndex = frameIndex;

                AddSamples(s0, s1, s2);

                _validFrames++;
            }
        }

        private static ushort ToUInt16LE(byte lo, byte hi)
        {
            return (ushort)(lo | (hi << 8));
        }

        private static uint ToUInt32LE(byte b0, byte b1, byte b2, byte b3)
        {
            return ((uint)b0) |
                   ((uint)b1 << 8) |
                   ((uint)b2 << 16) |
                   ((uint)b3 << 24);
        }

        private void AddSamples(ushort s0, ushort s1, ushort s2)
        {
            if (_samplesPacket == null || _samplesPacket.Length == 0)
                return;

            if (_sampleIndex + 2 >= _samplesPacket.Length)
            {
                _sampleIndex = 0;
                _framesInPacket = 0;
                return;
            }

            _samplesPacket[_sampleIndex++] = s0;
            _samplesPacket[_sampleIndex++] = s1;
            _samplesPacket[_sampleIndex++] = s2;

            _framesInPacket++;

            if (_framesInPacket >= FramesPerPacket)
            {
                ProcessCompletePacket();

                _sampleIndex = 0;
                _framesInPacket = 0;
            }
        }

        private void ProcessCompletePacket()
        {
            if (_samplesPacket == null || _samplesPacket.Length == 0)
                return;

            _packetCounter++;

            ushort first = _samplesPacket[0];
            ushort last = _samplesPacket[_samplesPacket.Length - 1];

            _continuityOk = true;

            if (_lastFirstSample.HasValue)
            {
                ushort expectedFirst;

                if (_lastFirstSample.Value + RAMP_START_STEP > WAVE_MAX_VALUE)
                    expectedFirst = 0;
                else
                    expectedFirst = (ushort)(_lastFirstSample.Value + RAMP_START_STEP);

                _continuityOk = first == expectedFirst;
            }

            _lastFirstSample = first;

            double[] samples = new double[_samplesPacket.Length];

            for (int i = 0; i < _samplesPacket.Length; i++)
                samples[i] = _samplesPacket[i];

            _lastPacketSamples = (double[])samples.Clone();

            RaisePacketCompleted(samples, first, last);

            if (ShouldDoUiWork)
            {
                if (EnableChartUpdate && IsExpanded)
                    UpdateChart(samples);

                if (EnableStatsUpdate)
                {
                    lblPacket.Text = $"Packet: {_packetCounter}";

                    string mcuCounterText = _lastMcuPacketCounter.HasValue
                        ? _lastMcuPacketCounter.Value.ToString()
                        : "-";

                    lblStats.Text =
                        $"MCU Packet={mcuCounterText} | " +
                        $"First={first} | Last={last} | Continuity={_continuityOk} | " +
                        $"ValidFrames={_validFrames} | BadPackets={_badPackets} | BadDrops={_badDrops} | " +
                        $"Resync={_resyncCount} | FrameErr={_frameErrorCount} | PacketCtrErr={_packetCounterErrors} | " +
                        $"RX={lanTcpClient.TotalBytesReceived} bytes | TX={lanTcpClient.TotalBytesSent} bytes";
                }
            }

            if (AutoHeartbeatEnabled && lanTcpClient.IsConnected)
            {
                Send_Heartbeat();
            }
        }

        private void RaisePacketCompleted(
            double[] samples,
            ushort first,
            ushort last)
        {
            double[] eventSamples = (double[])samples.Clone();

            PacketCompleted?.Invoke(
                this,
                new HLK_Wifi_PacketCompleted_EventArgs(
                    eventSamples,
                    _packetCounter,
                    _lastMcuPacketCounter,
                    _continuityOk,
                    first,
                    last
                )
            );
        }

        private void UpdateChart(double[] samples)
        {
            if (samples == null || samples.Length == 0)
                return;

            if (_series == null)
                return;

            if (chartSamples == null)
                return;

            if (chartSamples.ChartAreas.Count == 0)
                return;

            _series.Points.DataBindY(samples);

            double min = samples[0];
            double max = samples[0];

            for (int i = 1; i < samples.Length; i++)
            {
                if (samples[i] < min) min = samples[i];
                if (samples[i] > max) max = samples[i];
            }

            int margin = 50;

            chartSamples.ChartAreas[0].AxisX.Minimum = 0;
            chartSamples.ChartAreas[0].AxisX.Maximum = samples.Length - 1;

            chartSamples.ChartAreas[0].AxisY.Minimum = Math.Max(0, min - margin);
            chartSamples.ChartAreas[0].AxisY.Maximum = Math.Min(65535, max + margin);

            if (chartSamples.ChartAreas[0].AxisY.Maximum < 4096)
                chartSamples.ChartAreas[0].AxisY.Maximum = 4096;
        }

        // ============================================================
        // Command frame builders
        // ============================================================

        private static byte[] CreateEmptyCommandFrame(byte command)
        {
            byte[] frame = new byte[CMD_FRAME_SIZE];

            frame[0] = FRAME_HEADER;
            frame[1] = command;

            return frame;
        }

        private static byte Calculate_Checksum(byte[] frame)
        {
            int sum = 0;

            for (int i = 0; i < frame.Length - 1; i++)
                sum += frame[i];

            return (byte)(sum & 0xFF);
        }

        private static void FinalizeCommandFrame(byte[] frame)
        {
            frame[CMD_FRAME_SIZE - 1] = Calculate_Checksum(frame);
        }

        private static void CheckRange(string name, int value, int min, int max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(
                    name,
                    $"{name} must be between {min} and {max}."
                );
            }
        }

        private static void PutU16LE(byte[] frame, int index, int value, string name)
        {
            CheckRange(name, value, 0, 0xFFFF);

            frame[index + 0] = (byte)((value >> 0) & 0xFF);
            frame[index + 1] = (byte)((value >> 8) & 0xFF);
        }

        private static void PutU24LE(byte[] frame, int index, int value, string name)
        {
            CheckRange(name, value, 0, 0xFFFFFF);

            frame[index + 0] = (byte)((value >> 0) & 0xFF);
            frame[index + 1] = (byte)((value >> 8) & 0xFF);
            frame[index + 2] = (byte)((value >> 16) & 0xFF);
        }

        private static void PutU32LE(byte[] frame, int index, int value, string name)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(name, $"{name} cannot be negative.");

            frame[index + 0] = (byte)((value >> 0) & 0xFF);
            frame[index + 1] = (byte)((value >> 8) & 0xFF);
            frame[index + 2] = (byte)((value >> 16) & 0xFF);
            frame[index + 3] = (byte)((value >> 24) & 0xFF);
        }

        private static byte[] Make_Heartbeat()
        {
            byte[] frame = CreateEmptyCommandFrame(CMD_HEARTBEAT);

            FinalizeCommandFrame(frame);

            return frame;
        }

        private static byte[] Make_Sampling_Parameters(
            int downSampleRatio,
            int startIndex,
            int preTriggerTime)
        {
            CheckRange(nameof(downSampleRatio), downSampleRatio, 0, 0xFF);

            byte[] frame = CreateEmptyCommandFrame(CMD_SET_SAMPLING_PARAMETERS);

            frame[2] = (byte)downSampleRatio;

            PutU16LE(frame, 3, startIndex, nameof(startIndex));
            PutU24LE(frame, 5, preTriggerTime, nameof(preTriggerTime));

            FinalizeCommandFrame(frame);

            return frame;
        }

        private static byte[] Make_Filtering_Parameters(
            int lpfMode,
            int lpfFrequency,
            int hpfMode,
            int hpfFrequency)
        {
            CheckRange(nameof(lpfMode), lpfMode, 0, 0xFF);
            CheckRange(nameof(hpfMode), hpfMode, 0, 0xFF);

            byte[] frame = CreateEmptyCommandFrame(CMD_SET_FILTERING_PARAMETERS);

            frame[2] = (byte)lpfMode;
            PutU16LE(frame, 3, lpfFrequency, nameof(lpfFrequency));

            frame[5] = (byte)hpfMode;
            PutU16LE(frame, 6, hpfFrequency, nameof(hpfFrequency));

            FinalizeCommandFrame(frame);

            return frame;
        }

        private static byte[] Make_Pulse_Parameters(int pulseWidth)
        {
            byte[] frame = CreateEmptyCommandFrame(CMD_SET_PULSE_PARAMETERS);

            PutU32LE(frame, 2, pulseWidth, nameof(pulseWidth));

            FinalizeCommandFrame(frame);

            return frame;
        }

        private static byte[] Make_Measurement_Options(
            int measurementMode,
            int sampleLength,
            int velocity)
        {
            CheckRange(nameof(measurementMode), measurementMode, 0, 0xFF);

            byte[] frame = CreateEmptyCommandFrame(CMD_SET_MEASUREMENT_PARAMETERS);

            frame[2] = (byte)measurementMode;

            PutU16LE(frame, 3, sampleLength, nameof(sampleLength));
            PutU16LE(frame, 5, velocity, nameof(velocity));

            FinalizeCommandFrame(frame);

            return frame;
        }

        // ============================================================
        // Command transmit core
        // ============================================================

        public async Task SendCommandFrameAsync(byte[] frame)
        {
            if (frame == null)
                throw new ArgumentNullException(nameof(frame));

            if (frame.Length != CMD_FRAME_SIZE)
                throw new ArgumentException($"Command frame must be {CMD_FRAME_SIZE} bytes.");

            _lastCommandFrame = (byte[])frame.Clone();

            await lanTcpClient.SendAsync(frame);

            AppendLog("CMD TX: " + BitConverter.ToString(frame));
        }

        public void SendCommandFrame(byte[] frame)
        {
            FireAndForget(
                SendCommandFrameAsync(frame),
                "SendCommandFrame failed"
            );
        }

        private void FireAndForget(Task task, string errorMessage)
        {
            _ = FireAndForgetAsync(task, errorMessage);
        }

        private async Task FireAndForgetAsync(Task task, string errorMessage)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                AppendLog(errorMessage + ": " + ex.Message);
            }
        }

        // ============================================================
        // Public command senders - async versions
        // Recommended when you want to know success/failure.
        // ============================================================

        public Task Send_HeartbeatAsync()
        {
            byte[] frame = Make_Heartbeat();

            return SendCommandFrameAsync(frame);
        }

        public Task Set_Sampling_ParametersAsync(
            int downSampleRatio,
            int startIndex,
            int preTriggerTime)
        {
            DownSampleRatio = downSampleRatio;
            StartIndex = startIndex;
            PreTriggerTime = preTriggerTime;

            byte[] frame = Make_Sampling_Parameters(
                DownSampleRatio,
                StartIndex,
                PreTriggerTime
            );

            return SendCommandFrameAsync(frame);
        }

        public Task Send_Configured_Sampling_ParametersAsync()
        {
            byte[] frame = Make_Sampling_Parameters(
                DownSampleRatio,
                StartIndex,
                PreTriggerTime
            );

            return SendCommandFrameAsync(frame);
        }

        public Task Set_Filtering_ParametersAsync(
            int lpfMode,
            int lpfFrequency,
            int hpfMode,
            int hpfFrequency)
        {
            LpfMode = lpfMode;
            LpfFrequency = lpfFrequency;
            HpfMode = hpfMode;
            HpfFrequency = hpfFrequency;

            byte[] frame = Make_Filtering_Parameters(
                LpfMode,
                LpfFrequency,
                HpfMode,
                HpfFrequency
            );

            return SendCommandFrameAsync(frame);
        }

        public Task Send_Configured_Filtering_ParametersAsync()
        {
            byte[] frame = Make_Filtering_Parameters(
                LpfMode,
                LpfFrequency,
                HpfMode,
                HpfFrequency
            );

            return SendCommandFrameAsync(frame);
        }

        public Task Set_Pulse_ParametersAsync(int pulseWidth)
        {
            PulseWidth = pulseWidth;

            byte[] frame = Make_Pulse_Parameters(PulseWidth);

            return SendCommandFrameAsync(frame);
        }

        public Task Send_Configured_Pulse_ParametersAsync()
        {
            byte[] frame = Make_Pulse_Parameters(PulseWidth);

            return SendCommandFrameAsync(frame);
        }

        public Task Set_Measurment_OptionsAsync(
            int measurementMode,
            int sampleLength,
            int velocity)
        {
            MeasurementMode = measurementMode;
            SampleLength = sampleLength;
            Velocity = velocity;

            byte[] frame = Make_Measurement_Options(
                MeasurementMode,
                SampleLength,
                Velocity
            );

            return SendCommandFrameAsync(frame);
        }

        public Task Send_Configured_Measurment_OptionsAsync()
        {
            byte[] frame = Make_Measurement_Options(
                MeasurementMode,
                SampleLength,
                Velocity
            );

            return SendCommandFrameAsync(frame);
        }

        // ============================================================
        // Public command senders - non-async wrappers
        // Safe to call from normal non-async methods.
        // These are fire-and-forget and log errors internally.
        // ============================================================

        public void Send_Heartbeat()
        {
            FireAndForget(
                Send_HeartbeatAsync(),
                "Heartbeat failed"
            );
        }

        public void Set_Sampling_Parameters(
            int downSampleRatio,
            int startIndex,
            int preTriggerTime)
        {
            FireAndForget(
                Set_Sampling_ParametersAsync(
                    downSampleRatio,
                    startIndex,
                    preTriggerTime
                ),
                "Set_Sampling_Parameters failed"
            );
        }

        public void Send_Configured_Sampling_Parameters()
        {
            FireAndForget(
                Send_Configured_Sampling_ParametersAsync(),
                "Send_Configured_Sampling_Parameters failed"
            );
        }

        public void Set_Filtering_Parameters(
            int lpfMode,
            int lpfFrequency,
            int hpfMode,
            int hpfFrequency)
        {
            FireAndForget(
                Set_Filtering_ParametersAsync(
                    lpfMode,
                    lpfFrequency,
                    hpfMode,
                    hpfFrequency
                ),
                "Set_Filtering_Parameters failed"
            );
        }

        public void Send_Configured_Filtering_Parameters()
        {
            FireAndForget(
                Send_Configured_Filtering_ParametersAsync(),
                "Send_Configured_Filtering_Parameters failed"
            );
        }

        public void Set_Pulse_Parameters(int pulseWidth)
        {
            FireAndForget(
                Set_Pulse_ParametersAsync(pulseWidth),
                "Set_Pulse_Parameters failed"
            );
        }

        public void Send_Configured_Pulse_Parameters()
        {
            FireAndForget(
                Send_Configured_Pulse_ParametersAsync(),
                "Send_Configured_Pulse_Parameters failed"
            );
        }

        public void Set_Measurment_Options(
            int measurementMode,
            int sampleLength,
            int velocity)
        {
            FireAndForget(
                Set_Measurment_OptionsAsync(
                    measurementMode,
                    sampleLength,
                    velocity
                ),
                "Set_Measurment_Options failed"
            );
        }

        public void Send_Configured_Measurment_Options()
        {
            FireAndForget(
                Send_Configured_Measurment_OptionsAsync(),
                "Send_Configured_Measurment_Options failed"
            );
        }

        // ============================================================
        // Button events
        // ============================================================

        private void btnConnect_Click(object sender, EventArgs e)
        {
            lanTcpClient.StartAutoReconnect();
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            lanTcpClient.StopAutoReconnect();
            await lanTcpClient.DisconnectAsync();
        }

        private void numFramesPerPacket_ValueChanged(object sender, EventArgs e)
        {
            if (_internalPacketSettingChange)
                return;

            try
            {
                int frames = (int)numFramesPerPacket.Value;

                FramesPerPacket = frames;

                AppendLog(
                    $"Packet setting changed: Frames={FramesPerPacket}, " +
                    $"Samples={SamplesPerPacket}, Bytes={PacketSizeBytes}, " +
                    $"UsedPayload={UsedPayloadBytes}, Padding={PacketPaddingBytes}"
                );
            }
            catch (Exception ex)
            {
                AppendLog("Packet setting error: " + ex.Message);
            }
        }

        private void AppendLog(string text)
        {
            if (!EnableLogUpdate)
                return;

            if (!ShouldDoUiWork)
                return;

            if (txtLog == null)
                return;

            if (txtLog.TextLength > 20000)
                txtLog.Clear();

            txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + "  " + text + Environment.NewLine);
        }

        // ============================================================
        // Public connect / disconnect helpers
        // ============================================================

        public void StartAutoReconnect()
        {
            lanTcpClient.StartAutoReconnect();
        }

        public void StopAutoReconnect()
        {
            lanTcpClient.StopAutoReconnect();
        }

        public async Task DisconnectAsync()
        {
            try
            {
                lanTcpClient.StopAutoReconnect();
                await lanTcpClient.DisconnectAsync();
            }
            catch
            {
                // Ignore shutdown errors.
            }
        }

        private void HLK_Wifi_Wrapper_Load(object sender, EventArgs e)
        {
            if (AutoConnectOnLoad)
                lanTcpClient.StartAutoReconnect();
        }
    }

    // ============================================================
    // Packet completed event args
    // ============================================================

    public sealed class HLK_Wifi_PacketCompleted_EventArgs : EventArgs
    {
        public double[] Samples { get; }
        public long PacketCounter { get; }
        public uint? McuPacketCounter { get; }
        public bool ContinuityOk { get; }
        public ushort FirstSample { get; }
        public ushort LastSample { get; }
        public DateTime Timestamp { get; }

        public HLK_Wifi_PacketCompleted_EventArgs(
            double[] samples,
            long packetCounter,
            uint? mcuPacketCounter,
            bool continuityOk,
            ushort firstSample,
            ushort lastSample)
        {
            Samples = samples;
            PacketCounter = packetCounter;
            McuPacketCounter = mcuPacketCounter;
            ContinuityOk = continuityOk;
            FirstSample = firstSample;
            LastSample = lastSample;
            Timestamp = DateTime.Now;
        }
    }
}