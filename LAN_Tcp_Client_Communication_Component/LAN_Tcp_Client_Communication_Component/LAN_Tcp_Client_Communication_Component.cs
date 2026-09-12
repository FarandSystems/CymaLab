using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAN_Tcp_Client_Communication_Component
{
    public partial class LAN_Tcp_Client_Communication_Component: UserControl
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private CancellationTokenSource _rxCancellation;

        private CancellationTokenSource _autoReconnectCancellation;
        private bool _autoReconnectLoopRunning = false;

        private Task _rxTask;
        private long _lastRxTicks = 0;
        private bool _rxBlinkPhase = false;
        private bool _connectionFailed = false;
        private bool _internalIpTextChange = false;
        private string _pendingServerIp = "";

        // HandShake
        private DateTime _lastRxTimeUtc = DateTime.MinValue;
        private DateTime _lastHandshakeTxUtc = DateTime.MinValue;
        private bool _handshakeTxBusy = false;
        private bool _handshakeImageToggle = false;

        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        private LAN_Tcp_Client_State _state = LAN_Tcp_Client_State.Disconnected;

        // ============================================================
        // Public properties
        // ============================================================

        private string _serverIp = "192.168.16.254";

        [Category("TCP Settings")]
        public string ServerIp
        {
            get
            {
                if (textBox_Ip != null)
                    return textBox_Ip.Text.Trim();

                return _serverIp;
            }
            set
            {
                _serverIp = value ?? "";

                if (textBox_Ip != null)
                {
                    _internalIpTextChange = true;
                    textBox_Ip.Text = _serverIp;
                    _internalIpTextChange = false;
                }
            }
        }

        [Category("TCP Settings")]
        [Description("TCP server port.")]
        public int ServerPort { get; set; } = 5000;

        [Category("TCP Settings")]
        public bool ReconnectOnIpTextChanged { get; set; } = true;

        [Category("TCP Settings")]
        public int IpChangeReconnectDelayMs { get; set; } = 1000;

        [Category("Handshake")]
        public bool EnableHandshake { get; set; } = false;

        [Category("Handshake")]
        public bool SendHandshakeOnConnect { get; set; } = true;

        [Category("Handshake")]
        public bool SendHandshakeWhenNoRx { get; set; } = true;

        [Category("Handshake")]
        public int HandshakeIntervalMs { get; set; } = 2000;

        private byte[] _handshakeFrame = new byte[]
        {
            0x51, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x51
        };

        [Browsable(false)]
        public byte[] HandshakeFrame
        {
            get
            {
                byte[] copy = new byte[_handshakeFrame.Length];
                Array.Copy(_handshakeFrame, copy, copy.Length);
                return copy;
            }
            set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("HandshakeFrame cannot be null or empty.");

                _handshakeFrame = new byte[value.Length];
                Array.Copy(value, _handshakeFrame, value.Length);
            }
        }

        [Category("Handshake")]
        public string HandshakeHexString
        {
            get
            {
                return BytesToHexString(_handshakeFrame, _handshakeFrame.Length);
            }
            set
            {
                HandshakeFrame = HexStringToBytes(value);
            }
        }

        public int ConnectTimeoutMs { get; set; } = 5000;

        public int ReceiveBufferSize { get; set; } = 8192;

        public bool NoDelay { get; set; } = true;

        public bool EnableKeepAlive { get; set; } = true;

        public bool AutoReconnect { get; set; } = false;

        public int AutoReconnectDelayMs { get; set; } = 2000;

        public bool IsConnected
        {
            get
            {
                try
                {
                    return _tcpClient != null &&
                           _tcpClient.Client != null &&
                           _tcpClient.Connected &&
                           _networkStream != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        [Category("UI")]
        public bool LockIpTextBoxWhileConnected { get; set; } = false;

        [Category("UI")]
        public bool BlinkOnReceive { get; set; } = true;

        [Category("UI")]
        public int RxActivityHoldMs { get; set; } = 500;

        public LAN_Tcp_Client_State State => _state;

        public long TotalBytesReceived { get; private set; }

        public long TotalBytesSent { get; private set; }

        public DateTime? LastReceiveTime { get; private set; }

        public DateTime? LastSendTime { get; private set; }

        public string LastErrorMessage { get; private set; } = "";

        /// <summary>
        /// Set this to your Form, UserControl, or other UI object.
        /// Then all events will be raised on the UI thread.
        /// </summary>
        public ISynchronizeInvoke SynchronizingObject { get; set; }

        // ============================================================
        // Events
        // ============================================================

        public event EventHandler<LAN_Tcp_Client_DataReceived_EventArgs> DataReceived;

        public event EventHandler<LAN_Tcp_Client_StateChanged_EventArgs> StateChanged;

        public event EventHandler<LAN_Tcp_Client_Error_EventArgs> ErrorOccurred;

        public event EventHandler<LAN_Tcp_Client_Log_EventArgs> LogReceived;

        public event EventHandler<LAN_Tcp_Client_Log_EventArgs> HandshakeSent;

        public event EventHandler Connected;

        public event EventHandler Disconnected;

        public event EventHandler Connection_Picturebox_Ctrl_Clicked;


        // ============================================================
        // Connect / Disconnect
        // ============================================================

        public async Task ConnectAsync()
        {
            if (IsConnected)
                return;

            _serverIp = textBox_Ip.Text.Trim();

            if (string.IsNullOrWhiteSpace(_serverIp))
            {
                LastErrorMessage = "Server IP is empty.";
                SetState(LAN_Tcp_Client_State.Disconnected, "Server IP is empty");
                RaiseLog(LastErrorMessage);
                return;
            }

            SetState(LAN_Tcp_Client_State.Connecting, "Connecting...");

            _rxCancellation = new CancellationTokenSource();

            _tcpClient = new TcpClient
            {
                NoDelay = NoDelay,
                ReceiveBufferSize = ReceiveBufferSize,
                SendBufferSize = ReceiveBufferSize
            };

            if (EnableKeepAlive)
            {
                _tcpClient.Client.SetSocketOption(
                    SocketOptionLevel.Socket,
                    SocketOptionName.KeepAlive,
                    true);
            }

            try
            {
                RaiseLog($"Connecting to {_serverIp}:{ServerPort}");

                Task connectTask = _tcpClient.ConnectAsync(_serverIp, ServerPort);
                Task timeoutTask = Task.Delay(ConnectTimeoutMs);

                Task finishedTask = await Task.WhenAny(connectTask, timeoutTask);

                /*
                 * Do not throw a TimeoutException.
                 * Close the socket and return normally.
                 */
                if (finishedTask == timeoutTask)
                {
                    LastErrorMessage = "TCP connection timeout.";

                    CleanupSocket();

                    // Observe a possible exception that may occur after closing the socket.
                    _ = connectTask.ContinueWith(
                        task =>
                        {
                            Exception ignoredException = task.Exception;
                        },
                        TaskContinuationOptions.OnlyOnFaulted);

                    SetState(
                        LAN_Tcp_Client_State.Disconnected,
                        "Server unavailable");

                    RaiseLog(
                        $"Could not connect to {_serverIp}:{ServerPort}: " +
                        LastErrorMessage);

                    return;
                }

                /*
                 * Do not use:
                 *
                 * await connectTask;
                 *
                 * because it rethrows the SocketException. Inspect the task instead.
                 */
                if (connectTask.IsCanceled)
                {
                    LastErrorMessage = "TCP connection was canceled.";

                    CleanupSocket();
                    SetState(
                        LAN_Tcp_Client_State.Disconnected,
                        "Connection canceled");

                    RaiseLog(LastErrorMessage);
                    return;
                }

                if (connectTask.IsFaulted)
                {
                    Exception connectionException =
                        connectTask.Exception?.GetBaseException();

                    LastErrorMessage =
                        connectionException?.Message ??
                        "TCP connection failed.";

                    CleanupSocket();
                    SetState(
                        LAN_Tcp_Client_State.Disconnected,
                        "Server unavailable");

                    RaiseLog(
                        $"Could not connect to {_serverIp}:{ServerPort}: " +
                        LastErrorMessage);

                    return;
                }

                if (!_tcpClient.Connected)
                {
                    LastErrorMessage = "The TCP connection was not established.";

                    CleanupSocket();
                    SetState(
                        LAN_Tcp_Client_State.Disconnected,
                        "Not connected");

                    RaiseLog(LastErrorMessage);
                    return;
                }

                _networkStream = _tcpClient.GetStream();

                TotalBytesReceived = 0;
                TotalBytesSent = 0;
                LastReceiveTime = null;
                LastSendTime = null;
                LastErrorMessage = string.Empty;

                SetState(LAN_Tcp_Client_State.Connected, "Connected");
                RaiseConnected();

                _lastRxTimeUtc = DateTime.MinValue;
                _lastHandshakeTxUtc = DateTime.MinValue;

                _rxTask = Task.Run(
                    () => ReceiveLoopAsync(_rxCancellation.Token));

                if (EnableHandshake && SendHandshakeOnConnect)
                {
                    /*
                     * Observe errors inside SendHandshakeAsync so that a handshake
                     * failure does not become an unobserved task exception.
                     */
                    _ = SendInitialHandshakeSafelyAsync();
                }
            }
            catch (Exception ex)
            {
                /*
                 * This is only for unexpected local errors such as GetStream(),
                 * disposed objects, or socket configuration problems.
                 */
                LastErrorMessage = ex.Message;

                CleanupSocket();

                SetState(
                    LAN_Tcp_Client_State.Disconnected,
                    "Server unavailable");

                RaiseLog(
                    $"TCP connection could not be established: {ex.Message}");

                // Do not call throw or throw ex.
            }
        }

        private async Task SendInitialHandshakeSafelyAsync()
        {
            try
            {
                await SendHandshakeAsync("initial connect");
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                RaiseLog($"Initial handshake failed: {ex.Message}");
            }
        }

        public async Task SendHandshakeAsync(string reason = "manual")
        {
            if (!EnableHandshake)
                return;

            if (!IsConnected)
                return;

            if (_handshakeTxBusy)
                return;

            _handshakeTxBusy = true;

            try
            {
                byte[] frame = HandshakeFrame;

                await SendAsync(frame);

                _lastHandshakeTxUtc = DateTime.UtcNow;

                ToggleHandshakePicture();

                string msg = $"Handshake TX ({reason}): {BitConverter.ToString(frame)}";

                RaiseLog(msg);
                RaiseHandshakeSent(msg);
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                RaiseError(ex, "Handshake send failed");
            }
            finally
            {
                _handshakeTxBusy = false;
            }
        }

        public async Task DisconnectAsync()
        {
            if (_state == LAN_Tcp_Client_State.Disconnected)
                return;

            SetState(LAN_Tcp_Client_State.Disconnecting, "Disconnecting...");

            try
            {
                _rxCancellation?.Cancel();

                try
                {
                    if (_rxTask != null)
                        await Task.WhenAny(_rxTask, Task.Delay(500));
                }
                catch
                {
                    // Ignore shutdown errors.
                }

                CleanupSocket();
            }
            finally
            {
                SetState(LAN_Tcp_Client_State.Disconnected, "Disconnected");
                RaiseDisconnected();
            }
        }

        public void StartAutoReconnect()
        {
            AutoReconnect = true;

            _autoReconnectCancellation?.Cancel();
            _autoReconnectCancellation = new CancellationTokenSource();

            _ = AutoReconnectLoopAsync(_autoReconnectCancellation.Token, true);
        }

        public void StopAutoReconnect()
        {
            AutoReconnect = false;

            try
            {
                _autoReconnectCancellation?.Cancel();
            }
            catch
            {
                // Ignore.
            }
        }


        // ============================================================
        // Send methods
        // ============================================================

        public async Task SendAsync(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            await SendAsync(data, 0, data.Length);
        }

        public async Task SendAsync(byte[] data, int offset, int count)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!IsConnected)
                throw new InvalidOperationException("TCP client is not connected.");

            await _sendLock.WaitAsync();

            try
            {
                await _networkStream.WriteAsync(data, offset, count);
                await _networkStream.FlushAsync();

                TotalBytesSent += count;
                LastSendTime = DateTime.Now;

                RaiseLog($"TX: {count} bytes");
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                RaiseError(ex, "Send failed");
                throw;
            }
            finally
            {
                _sendLock.Release();
            }
        }

        public Task SendTextAsync(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            return SendAsync(data);
        }

        public Task SendHexStringAsync(string hex)
        {
            byte[] data = HexStringToBytes(hex);
            return SendAsync(data);
        }

        // ============================================================
        // Receive loop
        // ============================================================

        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            byte[] buffer = new byte[ReceiveBufferSize];

            try
            {
                while (!token.IsCancellationRequested)
                {
                    int count = await _networkStream.ReadAsync(buffer, 0, buffer.Length, token);

                    if (count <= 0)
                    {
                        RaiseLog("TCP server closed the connection.");
                        break;
                    }

                    byte[] received = new byte[count];
                    Buffer.BlockCopy(buffer, 0, received, 0, count);

                    TotalBytesReceived += count;
                    LastReceiveTime = DateTime.Now;
                    _lastRxTimeUtc = DateTime.UtcNow;

                    Interlocked.Exchange(ref _lastRxTicks, LastReceiveTime.Value.Ticks);

                    RaiseDataReceived(received, count);
                }
            }
            catch (OperationCanceledException)
            {
                // Normal disconnect.
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                RaiseError(ex, "Receive failed");
            }
            finally
            {
                bool shouldReconnect = AutoReconnect && !token.IsCancellationRequested;

                CleanupSocket();

                SetState(LAN_Tcp_Client_State.Disconnected, "Disconnected");
                RaiseDisconnected();

                if (shouldReconnect)
                {
                    _autoReconnectCancellation?.Cancel();
                    _autoReconnectCancellation = new CancellationTokenSource();

                    _ = AutoReconnectLoopAsync(_autoReconnectCancellation.Token, false);
                }
            }
        }

        private async Task AutoReconnectLoopAsync(CancellationToken token, bool tryImmediately = false)
        {
            if (_autoReconnectLoopRunning)
                return;

            _autoReconnectLoopRunning = true;

            try
            {
                bool firstTry = true;

                while (AutoReconnect && !token.IsCancellationRequested)
                {
                    try
                    {
                        if (!firstTry || !tryImmediately)
                        {
                            await Task.Delay(AutoReconnectDelayMs, token);
                        }

                        firstTry = false;

                        if (IsConnected)
                            return;

                        if (_state == LAN_Tcp_Client_State.Connecting ||
                            _state == LAN_Tcp_Client_State.Disconnecting)
                        {
                            continue;
                        }

                        RaiseLog("Auto reconnect attempt...");

                        await ConnectAsync();

                        if (IsConnected)
                        {
                            RaiseLog("Auto reconnect successful.");
                            return;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        LastErrorMessage = ex.Message;

                        // Do not show MessageBox here.
                        // Only log the failed attempt and keep trying.
                        RaiseLog("Auto reconnect failed: " + ex.Message);
                    }
                }
            }
            finally
            {
                _autoReconnectLoopRunning = false;
            }
        }

        // ============================================================
        // Utilities
        // ============================================================

        public void ResetCounters()
        {
            TotalBytesReceived = 0;
            TotalBytesSent = 0;
            LastReceiveTime = null;
            LastSendTime = null;
        }

        public static byte[] HexStringToBytes(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return Array.Empty<byte>();

            hex = hex.Replace(" ", "")
                     .Replace("-", "")
                     .Replace("0x", "")
                     .Replace("0X", "");

            if (hex.Length % 2 != 0)
                throw new FormatException("Hex string length must be even.");

            byte[] data = new byte[hex.Length / 2];

            for (int i = 0; i < data.Length; i++)
                data[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return data;
        }

        public static string BytesToHexString(byte[] data, int maxBytes = 64)
        {
            if (data == null)
                return "";

            int count = Math.Min(data.Length, maxBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    sb.Append(" ");

                sb.Append(data[i].ToString("X2"));
            }

            if (data.Length > maxBytes)
                sb.Append(" ...");

            return sb.ToString();
        }

        private void CleanupSocket()
        {
            try { _networkStream?.Close(); } catch { }
            try { _tcpClient?.Close(); } catch { }

            _networkStream = null;
            _tcpClient = null;
        }

        private void SetState(LAN_Tcp_Client_State state, string message)
        {
            _state = state;

            UpdateInternalUiByState(state);

            RaiseStateChanged(state, message);
            RaiseLog(message);
        }

        // ============================================================
        // UI-safe event raisers
        // ============================================================

        private void RaiseDataReceived(byte[] data, int count)
        {
            RaiseOnUi(DataReceived, new LAN_Tcp_Client_DataReceived_EventArgs(data, count));
        }

        private void RaiseStateChanged(LAN_Tcp_Client_State state, string message)
        {
            RaiseOnUi(StateChanged, new LAN_Tcp_Client_StateChanged_EventArgs(state, message));
        }

        private void RaiseError(Exception ex, string message)
        {
            RaiseOnUi(ErrorOccurred, new LAN_Tcp_Client_Error_EventArgs(ex, message));
        }

        private void RaiseLog(string message)
        {
            RaiseOnUi(LogReceived, new LAN_Tcp_Client_Log_EventArgs(message));
        }

        private void RaiseHandshakeSent(string message)
        {
            RaiseOnUi(HandshakeSent, new LAN_Tcp_Client_Log_EventArgs(message));
        }

        private void RaiseConnected()
        {
            RaiseOnUi(Connected, EventArgs.Empty);
        }

        private void RaiseDisconnected()
        {
            RaiseOnUi(Disconnected, EventArgs.Empty);
        }

        private void Raise_Connection_Picturebox_Ctrl_Clicked()
        {
            RaiseOnUi(Connection_Picturebox_Ctrl_Clicked, EventArgs.Empty);
        }

        private void RaiseOnUi<T>(EventHandler<T> handler, T args) where T : EventArgs
        {
            if (handler == null)
                return;

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() => handler(this, args)));
                }
                catch
                {
                    // Control disposed.
                }
            }
            else
            {
                handler(this, args);
            }
        }

        private void RaiseOnUi(EventHandler handler, EventArgs args)
        {
            if (handler == null)
                return;

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() => handler(this, args)));
                }
                catch
                {
                    // Control disposed.
                }
            }
            else
            {
                handler(this, args);
            }
        }

        private void UpdateInternalUiByState(LAN_Tcp_Client_State state)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateInternalUiByState(state)));
                return;
            }

            if (LockIpTextBoxWhileConnected && textBox_Ip != null)
            {
                textBox_Ip.Enabled =
                    state == LAN_Tcp_Client_State.Disconnected ||
                    state == LAN_Tcp_Client_State.Error;
            }

            switch (state)
            {
                case LAN_Tcp_Client_State.Connected:
                    _connectionFailed = false;
                    SetConnectionPicture(Properties.Resources.Connection_RX_IDLE);
                    break;

                case LAN_Tcp_Client_State.Connecting:
                case LAN_Tcp_Client_State.Disconnecting:
                    SetConnectionPicture(Properties.Resources.Connection_IDLE);
                    break;

                case LAN_Tcp_Client_State.Error:
                    _connectionFailed = true;
                    SetConnectionPicture(Properties.Resources.Connection_Fail);
                    break;

                case LAN_Tcp_Client_State.Disconnected:
                default:
                    if (_connectionFailed)
                        SetConnectionPicture(Properties.Resources.Connection_Fail);
                    else
                        SetConnectionPicture(Properties.Resources.Connection_IDLE);
                    break;
            }
        }


        private void SetConnectionPicture(Image image)
        {
            if (pictureBox_Connection == null)
                return;

            if (pictureBox_Connection.InvokeRequired)
            {
                pictureBox_Connection.BeginInvoke(new Action(() => SetConnectionPicture(image)));
                return;
            }

            pictureBox_Connection.Image = image;
        }

        public LAN_Tcp_Client_Communication_Component()
        {
            InitializeComponent();

            _internalIpTextChange = true;
            textBox_Ip.Text = _serverIp;
            _internalIpTextChange = false;

            timerIpReconnect.Interval = IpChangeReconnectDelayMs;

            UpdateInternalUiByState(LAN_Tcp_Client_State.Disconnected);
        }

        private void ToggleHandshakePicture()
        {
            if (_state != LAN_Tcp_Client_State.Connected)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(ToggleHandshakePicture));
                return;
            }

            _handshakeImageToggle = !_handshakeImageToggle;

            if (_handshakeImageToggle)
                SetConnectionPicture(Properties.Resources.Connection_IDLE);
            else
                SetConnectionPicture(Properties.Resources.Connection_RX_IDLE);
        }

        private void timerConnectionStatus_Tick(object sender, EventArgs e)
        {
            HandleHandshakeTimer();

            HandleRxBlinkTimer();
        }

        private async void HandleHandshakeTimer()
        {
            if (!EnableHandshake)
                return;

            if (!SendHandshakeWhenNoRx)
                return;

            if (_state != LAN_Tcp_Client_State.Connected)
                return;

            if (!IsConnected)
                return;

            DateTime now = DateTime.UtcNow;

            bool noRxYet = _lastRxTimeUtc == DateTime.MinValue;
            bool rxTimeout = !noRxYet && (now - _lastRxTimeUtc).TotalMilliseconds >= HandshakeIntervalMs;

            bool handshakeIntervalPassed =
                _lastHandshakeTxUtc == DateTime.MinValue ||
                (now - _lastHandshakeTxUtc).TotalMilliseconds >= HandshakeIntervalMs;

            if ((noRxYet || rxTimeout) && handshakeIntervalPassed)
            {
                await SendHandshakeAsync("no RX");
            }
        }

        private void HandleRxBlinkTimer()
        {
            if (!BlinkOnReceive)
                return;

            if (_state != LAN_Tcp_Client_State.Connected)
                return;

            long lastTicks = Interlocked.Read(ref _lastRxTicks);

            if (lastTicks == 0)
                return;

            double elapsedMs = (DateTime.Now - new DateTime(lastTicks)).TotalMilliseconds;

            if (elapsedMs <= RxActivityHoldMs)
            {
                _rxBlinkPhase = !_rxBlinkPhase;

                if (_rxBlinkPhase)
                    SetConnectionPicture(Properties.Resources.Connection_RX_OK);
                else
                    SetConnectionPicture(Properties.Resources.Connection_RX_IDLE);
            }
            else
            {
                _rxBlinkPhase = false;
                SetConnectionPicture(Properties.Resources.Connection_RX_IDLE);
            }
        }

        private void textBox_Ip_TextChanged(object sender, EventArgs e)
        {
            if (_internalIpTextChange)
                return;

            string newIp = textBox_Ip.Text.Trim();

            _pendingServerIp = newIp;
            _serverIp = newIp;

            if (string.IsNullOrWhiteSpace(newIp))
                return;

            if (!ReconnectOnIpTextChanged)
                return;

            // If disconnected, just store the new IP. No reconnect needed.
            if (_state == LAN_Tcp_Client_State.Disconnected ||
                _state == LAN_Tcp_Client_State.Error)
            {
                return;
            }

            // Debounce reconnect while user is typing
            timerIpReconnect.Stop();
            timerIpReconnect.Interval = IpChangeReconnectDelayMs;
            timerIpReconnect.Start();

            RaiseLog("IP changed. Reconnect scheduled...");
        }

        private async void timerIpReconnect_Tick(object sender, EventArgs e)
        {
            timerIpReconnect.Stop();

            if (!ReconnectOnIpTextChanged)
                return;

            string newIp = _pendingServerIp.Trim();

            if (string.IsNullOrWhiteSpace(newIp))
                return;

            try
            {
                RaiseLog($"Reconnecting to new IP: {newIp}");

                await DisconnectAsync();

                _serverIp = newIp;

                await ConnectAsync();
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                SetState(LAN_Tcp_Client_State.Error, "Reconnect failed");
                RaiseError(ex, "Reconnect failed");
            }
        }

        private void pictureBox_Connection_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                Raise_Connection_Picturebox_Ctrl_Clicked();
            }
        }
    }

    public enum LAN_Tcp_Client_State
    {
        Disconnected,
        Connecting,
        Connected,
        Disconnecting,
        Error
    }
    public sealed class LAN_Tcp_Client_DataReceived_EventArgs : EventArgs
    {
        public byte[] Data { get; }
        public int Count { get; }
        public DateTime Timestamp { get; }

        public LAN_Tcp_Client_DataReceived_EventArgs(byte[] data, int count)
        {
            Data = data;
            Count = count;
            Timestamp = DateTime.Now;
        }
    }

    public sealed class LAN_Tcp_Client_StateChanged_EventArgs : EventArgs
    {
        public LAN_Tcp_Client_State State { get; }
        public string Message { get; }

        public LAN_Tcp_Client_StateChanged_EventArgs(LAN_Tcp_Client_State state, string message)
        {
            State = state;
            Message = message;
        }
    }

    public sealed class LAN_Tcp_Client_Error_EventArgs : EventArgs
    {
        public Exception Exception { get; }
        public string Message { get; }

        public LAN_Tcp_Client_Error_EventArgs(Exception exception, string message)
        {
            Exception = exception;
            Message = message;
        }
    }

    public sealed class LAN_Tcp_Client_Log_EventArgs : EventArgs
    {
        public string Message { get; }
        public DateTime Timestamp { get; }

        public LAN_Tcp_Client_Log_EventArgs(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
