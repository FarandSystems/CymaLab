using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Auto_Detect_VCP_Control
{
    public sealed class VcpFrameReceivedEventArgs : EventArgs
    {
        public VcpFrameReceivedEventArgs(byte[] frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException("frame");
            }

            Frame = frame;
        }

        public byte[] Frame { get; private set; }
    }

    public sealed class VcpStatusChangedEventArgs : EventArgs
    {
        public VcpStatusChangedEventArgs(
            string message,
            string portName,
            bool isOpen)
        {
            Message = message ?? string.Empty;
            PortName = portName ?? string.Empty;
            IsOpen = isOpen;
        }

        public string Message { get; private set; }
        public string PortName { get; private set; }
        public bool IsOpen { get; private set; }
    }

    /*
     * Non-UI owner of SerialPort.
     *
     * All COM scanning, reconnecting, startup probing, blocking reads,
     * buffering, frame resynchronization and writes run independently of the
     * WinForms message thread.
     */
    public sealed class VcpSerialService : IDisposable
    {
        private const int ScanRetryMs = 1000;
        private const int StartupTimeoutMs = 2500;
        private const int StartupProbePeriodMs = 250;
        private const int ReadTimeoutMs = 100;
        private const int ReadChunkLength = 512;

        private readonly object _portLock =
            new object();

        private readonly object _writeLock =
            new object();

        private readonly object _bufferLock =
            new object();

        private readonly object _startupCommandLock =
            new object();

        private readonly AutoResetEvent _wakeEvent =
            new AutoResetEvent(false);

        private readonly List<byte> _receiveBuffer =
            new List<byte>(4096);

        private Thread _workerThread;
        private SerialPort _serialPort;

        public SerialPort SerialPort
        {
            get { return _serialPort; }
        }

        private volatile bool _firmwareUpdateMode;
        private volatile bool _running;
        private volatile bool _reconnectRequested;
        private volatile bool _disposed;

        private int _baudRate = 38400;
        private int _frameLength = 64;
        private int _startupByteIndex = 3;
        private int _responseByteIndex = 3;

        private byte _startupByte = 0x55;
        private byte _responseByte = 0x55;
        private byte[] _startupCommandBytes;

        public event EventHandler PortOpened;
        public event EventHandler NormalOperationStarted;
        public event EventHandler ConnectionFailed;
        public event EventHandler<VcpFrameReceivedEventArgs> FrameReceived;
        public event EventHandler<VcpStatusChangedEventArgs> StatusChanged;

        /*
         * Decoder receives one raw physical frame.
         * Validator receives the decoded candidate. When it returns false,
         * the service discards one byte and tests the next candidate.
         */
        public Func<byte[], byte[]> FrameDecoder { get; set; }
        public Func<byte[], bool> FrameValidator { get; set; }

        public int BaudRate
        {
            get
            {
                return Volatile.Read(
                    ref _baudRate);
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "value");
                }

                int previous =
                    Interlocked.Exchange(
                        ref _baudRate,
                        value);

                if (previous != value &&
                    _running)
                {
                    RequestReconnect();
                }
            }
        }

        public bool FirmwareUpdateMode
        {
            get
            {
                return _firmwareUpdateMode;
            }
            set
            {
                if (_firmwareUpdateMode == value)
                {
                    return;
                }

                _firmwareUpdateMode = value;

                if (value)
                {
                    /*
                     * Stop the normal protocol and release the COM port.
                     * The XMODEM uploader will open its own SerialPort.
                     */
                    _reconnectRequested = true;
                    CloseCurrentPort();
                }
                else
                {
                    /*
                     * Allow the normal worker to scan and reconnect again.
                     */
                    _reconnectRequested = false;
                }

                _wakeEvent.Set();
            }
        }

        public int FrameLength
        {
            get
            {
                return Volatile.Read(
                    ref _frameLength);
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "value");
                }

                Interlocked.Exchange(
                    ref _frameLength,
                    value);

                ClearReceiveBuffer();
            }
        }

        public int StartupByteIndex
        {
            get
            {
                return Volatile.Read(
                    ref _startupByteIndex);
            }
            set
            {
                if (value < 0 ||
                    value >= 31)
                {
                    throw new ArgumentOutOfRangeException(
                        "value");
                }

                Interlocked.Exchange(
                    ref _startupByteIndex,
                    value);
            }
        }

        public byte StartupByte
        {
            get { return _startupByte; }
            set { _startupByte = value; }
        }

        public int ResponseByteIndex
        {
            get
            {
                return Volatile.Read(
                    ref _responseByteIndex);
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "value");
                }

                Interlocked.Exchange(
                    ref _responseByteIndex,
                    value);
            }
        }

        public byte ResponseByte
        {
            get { return _responseByte; }
            set { _responseByte = value; }
        }

        public byte[] StartupCommandBytes
        {
            get
            {
                lock (_startupCommandLock)
                {
                    if (_startupCommandBytes != null)
                    {
                        return
                            (byte[])_startupCommandBytes.Clone();
                    }
                }

                return
                    BuildGeneratedStartupCommand();
            }
            set
            {
                if (value != null &&
                    value.Length == 0)
                {
                    throw new ArgumentException(
                        "The startup command cannot be empty.",
                        "value");
                }

                lock (_startupCommandLock)
                {
                    _startupCommandBytes =
                        value == null
                            ? null
                            : (byte[])value.Clone();
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                lock (_portLock)
                {
                    return
                        _serialPort != null &&
                        _serialPort.IsOpen;
                }
            }
        }

        public string CurrentPortName
        {
            get
            {
                lock (_portLock)
                {
                    return
                        _serialPort == null
                            ? string.Empty
                            : _serialPort.PortName;
                }
            }
        }

        public void Start()
        {
            ThrowIfDisposed();

            if (_running)
            {
                _wakeEvent.Set();
                return;
            }

            _running = true;
            _reconnectRequested = false;

            _workerThread =
                new Thread(WorkerLoop);

            _workerThread.IsBackground = true;
            _workerThread.Name =
                "ORCA VCP Serial Worker";

            _workerThread.Start();
        }

        public void Stop()
        {
            _running = false;
            _reconnectRequested = true;

            CloseCurrentPort();
            _wakeEvent.Set();

            Thread worker =
                _workerThread;

            if (worker != null &&
                worker.IsAlive &&
                worker != Thread.CurrentThread)
            {
                worker.Join(1500);
            }

            _workerThread = null;
        }

        public void RequestReconnect()
        {
            if (!_running)
            {
                Start();
                return;
            }

            _reconnectRequested = true;

            /*
             * Closing interrupts any pending SerialPort.Read().
             */
            CloseCurrentPort();
            _wakeEvent.Set();
        }

        public void Send(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(
                    "data");
            }

            if (data.Length == 0)
            {
                return;
            }

            if (FirmwareUpdateMode)
            {
                throw new InvalidOperationException(
                    "Normal VCP communication is paused during firmware update.");
            }

            SerialPort port;

            lock (_portLock)
            {
                port = _serialPort;
            }

            if (port == null ||
                !port.IsOpen)
            {
                throw new IOException(
                    "The VCP port is not open.");
            }

            try
            {
                lock (_writeLock)
                {
                    if (!port.IsOpen)
                    {
                        throw new IOException(
                            "The VCP port closed before write.");
                    }

                    port.Write(
                        data,
                        0,
                        data.Length);
                }
            }
            catch
            {
                RequestReconnect();
                throw;
            }
        }

        private void WorkerLoop()
        {
            while (_running)
            {
                /*
                 * During firmware update, the normal VCP service must not
                 * open, read, write, or probe any COM port.
                 */
                if (FirmwareUpdateMode)
                {
                    CloseCurrentPort();
                    WaitForWake(250);
                    continue;
                }

                _reconnectRequested = false;

                string[] portNames;

                try
                {
                    portNames =
                        SerialPort.GetPortNames();

                    Array.Sort(
                        portNames,
                        StringComparer.OrdinalIgnoreCase);
                }
                catch (Exception ex)
                {
                    RaiseStatus(
                        "COM enumeration failed: " +
                        ex.Message,
                        string.Empty,
                        false);

                    WaitForWake(
                        ScanRetryMs);

                    continue;
                }

                if (portNames.Length == 0)
                {
                    RaiseStatus(
                        "No COM ports available.",
                        string.Empty,
                        false);

                    WaitForWake(
                        ScanRetryMs);

                    continue;
                }

                bool openedAnyPort = false;

                foreach (string portName in portNames)
                {
                    if (!_running || FirmwareUpdateMode)
                    {
                        break;
                    }

                    if (!TryOpenPort(portName))
                    {
                        continue;
                    }

                    if (FirmwareUpdateMode)
                    {
                        CloseCurrentPort();
                        break;
                    }

                    openedAnyPort = true;

                    SafeRaise(
                        PortOpened);

                    bool normalStarted =
                        RunStartupHandshake();

                    if (normalStarted &&
                        _running &&
                        !_reconnectRequested)
                    {
                        SafeRaise(
                            NormalOperationStarted);

                        ReadNormalTraffic();

                        if (_running)
                        {
                            SafeRaise(
                                ConnectionFailed);
                        }
                    }

                    CloseCurrentPort();

                    if (_reconnectRequested)
                    {
                        _reconnectRequested = false;
                        break;
                    }
                }

                if (!_running)
                {
                    break;
                }

                if (!openedAnyPort)
                {
                    RaiseStatus(
                        "No usable COM port found.",
                        string.Empty,
                        false);
                }

                WaitForWake(
                    ScanRetryMs);
            }

            CloseCurrentPort();
        }

        private bool TryOpenPort(
            string portName)
        {
            SerialPort port =
                new SerialPort();

            try
            {
                port.PortName = portName;
                port.BaudRate = BaudRate;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Handshake = Handshake.None;
                port.ReadTimeout = ReadTimeoutMs;
                port.WriteTimeout = 1000;
                port.ReadBufferSize = 4096;
                port.WriteBufferSize = 4096;

                port.Open();
                port.DiscardInBuffer();
                port.DiscardOutBuffer();

                ClearReceiveBuffer();

                lock (_portLock)
                {
                    _serialPort = port;
                }

                RaiseStatus(
                    portName + " opened.",
                    portName,
                    true);

                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    port.Dispose();
                }
                catch
                {
                }

                RaiseStatus(
                    "Cannot open " +
                    portName +
                    ": " +
                    ex.Message,
                    portName,
                    false);

                return false;
            }
        }

        private bool RunStartupHandshake()
        {
            long deadline =
                Stopwatch.GetTimestamp() +
                MillisecondsToTicks(
                    StartupTimeoutMs);

            long nextProbe = 0L;

            while (_running &&
                   !FirmwareUpdateMode &&
                   !_reconnectRequested &&
                   IsOpen &&
                   Stopwatch.GetTimestamp() < deadline)
            {
                long now =
                    Stopwatch.GetTimestamp();

                if (nextProbe == 0L ||
                    now >= nextProbe)
                {
                    try
                    {
                        Send(
                            BuildStartupCommand());
                    }
                    catch
                    {
                        return false;
                    }

                    nextProbe =
                        now +
                        MillisecondsToTicks(
                            StartupProbePeriodMs);
                }

                bool startupAcknowledged;

                if (!ReadAndProcess(
                        true,
                        out startupAcknowledged))
                {
                    return false;
                }

                if (startupAcknowledged)
                {
                    RaiseStatus(
                        "ORCA startup acknowledgment received.",
                        CurrentPortName,
                        true);

                    return true;
                }
            }

            return false;
        }

        private void ReadNormalTraffic()
        {
            while (_running &&
                   !FirmwareUpdateMode &&
                   !_reconnectRequested &&
                   IsOpen)
            {
                bool ignored;

                if (!ReadAndProcess(
                        false,
                        out ignored))
                {
                    return;
                }
            }
        }

        private bool ReadAndProcess(
            bool detectStartupAck,
            out bool startupAck)
        {
            startupAck = false;

            if (FirmwareUpdateMode)
            {
                return false;
            }

            SerialPort port;

            lock (_portLock)
            {
                port = _serialPort;
            }

            if (port == null ||
                !port.IsOpen)
            {
                return false;
            }

            byte[] chunk =
                new byte[ReadChunkLength];

            try
            {
                int bytesToRead =
                    port.BytesToRead;

                if (bytesToRead <= 0)
                {
                    bytesToRead = 1;
                }

                bytesToRead =
                    Math.Min(
                        bytesToRead,
                        chunk.Length);

                int received =
                    port.Read(
                        chunk,
                        0,
                        bytesToRead);

                if (received <= 0)
                {
                    return true;
                }

                lock (_bufferLock)
                {
                    for (int index = 0;
                         index < received;
                         index++)
                    {
                        _receiveBuffer.Add(
                            chunk[index]);
                    }
                }

                return ExtractFrames(
                    detectStartupAck,
                    out startupAck);
            }
            catch (TimeoutException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        private bool ExtractFrames(
            bool detectStartupAck,
            out bool startupAck)
        {
            startupAck = false;

            while (_running &&
                   !_reconnectRequested)
            {
                int frameLength =
                    FrameLength;

                byte[] rawCandidate;

                lock (_bufferLock)
                {
                    if (_receiveBuffer.Count <
                        frameLength)
                    {
                        return true;
                    }

                    rawCandidate =
                        _receiveBuffer
                            .GetRange(
                                0,
                                frameLength)
                            .ToArray();
                }

                byte[] decodedCandidate =
                    DecodeCandidate(
                        rawCandidate);

                bool valid =
                    ValidateCandidate(
                        decodedCandidate,
                        frameLength);

                lock (_bufferLock)
                {
                    if (valid)
                    {
                        _receiveBuffer.RemoveRange(
                            0,
                            frameLength);
                    }
                    else
                    {
                        /*
                         * Lost/inserted byte recovery.
                         */
                        _receiveBuffer.RemoveAt(0);
                    }
                }

                if (!valid)
                {
                    continue;
                }

                SafeRaise(
                    FrameReceived,
                    new VcpFrameReceivedEventArgs(
                        decodedCandidate));

                if (detectStartupAck &&
                    IsStartupAcknowledgment(
                        decodedCandidate))
                {
                    startupAck = true;
                    return true;
                }
            }

            return true;
        }

        private byte[] DecodeCandidate(
            byte[] rawCandidate)
        {
            try
            {
                Func<byte[], byte[]> decoder =
                    FrameDecoder;

                return
                    decoder == null
                        ? rawCandidate
                        : decoder(
                            rawCandidate);
            }
            catch
            {
                return null;
            }
        }

        private bool ValidateCandidate(
            byte[] candidate,
            int frameLength)
        {
            if (candidate == null ||
                candidate.Length !=
                    frameLength)
            {
                return false;
            }

            Func<byte[], bool> validator =
                FrameValidator;

            if (validator == null)
            {
                return true;
            }

            try
            {
                return validator(
                    candidate);
            }
            catch
            {
                return false;
            }
        }

        private bool IsStartupAcknowledgment(
            byte[] frame)
        {
            int index =
                ResponseByteIndex;

            return
                frame != null &&
                index >= 0 &&
                index + 1 <
                    frame.Length &&
                frame[index] ==
                    ResponseByte &&
                frame[index + 1] ==
                    ResponseByte;
        }

        private byte[] BuildStartupCommand()
        {
            lock (_startupCommandLock)
            {
                if (_startupCommandBytes != null)
                {
                    return
                        (byte[])_startupCommandBytes.Clone();
                }
            }

            return
                BuildGeneratedStartupCommand();
        }

        private byte[] BuildGeneratedStartupCommand()
        {
            byte[] command =
                new byte[32];

            int index =
                StartupByteIndex;

            command[index] =
                StartupByte;

            command[index + 1] =
                StartupByte;

            byte checksum = 0;

            for (int byteIndex = 0;
                 byteIndex <
                 command.Length - 1;
                 byteIndex++)
            {
                checksum +=
                    command[byteIndex];
            }

            command[31] =
                checksum;

            return command;
        }

        private void CloseCurrentPort()
        {
            SerialPort port;

            lock (_portLock)
            {
                port = _serialPort;
                _serialPort = null;
            }

            if (port == null)
            {
                return;
            }

            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
            }
            catch
            {
            }

            try
            {
                port.Dispose();
            }
            catch
            {
            }

            ClearReceiveBuffer();

            RaiseStatus(
                "VCP port closed.",
                string.Empty,
                false);
        }

        private void ClearReceiveBuffer()
        {
            lock (_bufferLock)
            {
                _receiveBuffer.Clear();
            }
        }

        private static long MillisecondsToTicks(
            int milliseconds)
        {
            return checked(
                (long)Math.Ceiling(
                    milliseconds *
                    (double)Stopwatch.Frequency /
                    1000.0));
        }

        private void WaitForWake(
            int milliseconds)
        {
            _wakeEvent.WaitOne(
                milliseconds);
        }

        private void RaiseStatus(
            string message,
            string portName,
            bool isOpen)
        {
            SafeRaise(
                StatusChanged,
                new VcpStatusChangedEventArgs(
                    message,
                    portName,
                    isOpen));
        }

        private static void SafeRaise(
            EventHandler handler)
        {
            if (handler == null)
            {
                return;
            }

            foreach (Delegate subscriber in
                     handler.GetInvocationList())
            {
                try
                {
                    ((EventHandler)subscriber)(
                        null,
                        EventArgs.Empty);
                }
                catch
                {
                }
            }
        }

        private static void SafeRaise<TEventArgs>(
            EventHandler<TEventArgs> handler,
            TEventArgs args)
            where TEventArgs : EventArgs
        {
            if (handler == null)
            {
                return;
            }

            foreach (Delegate subscriber in
                     handler.GetInvocationList())
            {
                try
                {
                    ((EventHandler<TEventArgs>)subscriber)(
                        null,
                        args);
                }
                catch
                {
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(
                    GetType().FullName);
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            Stop();
            _wakeEvent.Dispose();
        }
    }
}