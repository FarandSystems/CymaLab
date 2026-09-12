using System;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace Auto_Detect_VCP_Control
{
    /*
     * Thin WinForms wrapper around VcpSerialService.
     *
     * The UserControl no longer owns SerialPort and no WinForms timer is used
     * for communication.
     */
    public partial class Auto_Detect_VCP_Control : UserControl
    {
        private readonly object _frameLock =
            new object();

        private readonly VcpSerialService _serialService;

        private byte[] _rxBytes =
            new byte[64];

        private bool _isMinimised;
        private bool _startVcpConnection;

        public Auto_Detect_VCP_Control()
        {
            InitializeComponent();

            timer_State_Machine.Stop();
            timer1.Stop();

            _serialService =
                new VcpSerialService();

            _serialService.FrameDecoder =
                Decode_0x1A;

            _serialService.PortOpened +=
                SerialService_PortOpened;

            _serialService.NormalOperationStarted +=
                SerialService_NormalOperationStarted;

            _serialService.ConnectionFailed +=
                SerialService_ConnectionFailed;

            _serialService.FrameReceived +=
                SerialService_FrameReceived;

            _serialService.StatusChanged +=
                SerialService_StatusChanged;

            Disposed +=
                Auto_Detect_VCP_Control_Disposed;
        }

        public enum Connection_Status_Enum
        {
            Disconnected,
            Connected,
            Ready_To_Retry,
            Connecting,
            Port_Opened
        }

        public event EventHandler Connection_Ready;
        public event EventHandler Connection_Failed;
        public event EventHandler Received_Data_Ready;
        public event EventHandler Normal_Operation_Starts;

        public Func<byte[], bool> FrameValidator
        {
            get
            {
                return
                    _serialService.FrameValidator;
            }
            set
            {
                _serialService.FrameValidator =
                    value;
            }
        }

        public SerialPort SerialPort
        {
            get 
            {
                if (_serialService != null)
                {
                    return _serialService.SerialPort;
                }
                return null;
            }
        }

        public bool FirmwareUpdateMode
        {
            get { return _serialService.FirmwareUpdateMode; }
            set { _serialService.FirmwareUpdateMode = value;}
        }

        public string CurrentPortName
        {
            get { return (_serialService.CurrentPortName); }
        }

        public bool IsOpen
        {
            get { return _serialService.IsOpen;}
        }

        public bool Is_Minimised
        {
            get { return _isMinimised; }
            set
            {
                _isMinimised = value;
                PostUi(Change_Size);
            }
        }

        public bool Is_Port_Open
        {
            get
            {
                return
                    _serialService.IsOpen;
            }
        }

        public int Rx_Byte_Count
        {
            get
            {
                return
                    _serialService.FrameLength;
            }
            set
            {
                _serialService.FrameLength =
                    value;

                lock (_frameLock)
                {
                    _rxBytes =
                        new byte[value];
                }
            }
        }

        public byte[] Rx_Bytes
        {
            get
            {
                lock (_frameLock)
                {
                    return
                        (byte[])_rxBytes.Clone();
                }
            }
            set
            {
                lock (_frameLock)
                {
                    _rxBytes =
                        value == null
                            ? new byte[0]
                            : (byte[])value.Clone();
                }
            }
        }

        public int Communication_Response
        {
            get
            {
                return
                    _serialService.ResponseByte;
            }
            set
            {
                _serialService.ResponseByte =
                    checked((byte)value);
            }
        }

        [Category("VCP Communication")]
        [Description("Complete startup command sent while probing each COM port. Set null to restore the generated command based on Start_Communication_Byte and Start_Communication_Byte_Index.")]
        public byte[] Command_Bytes
        {
            get
            {
                return
                    _serialService.StartupCommandBytes;
            }
            set
            {
                _serialService.StartupCommandBytes =
                    value;
            }
        }

        public int Communication_Response_Byte_Index
        {
            get
            {
                return
                    _serialService.ResponseByteIndex;
            }
            set
            {
                _serialService.ResponseByteIndex =
                    value;
            }
        }

        public int Start_Communication_Byte_Index
        {
            get
            {
                return
                    _serialService.StartupByteIndex;
            }
            set
            {
                _serialService.StartupByteIndex =
                    value;
            }
        }

        public int Start_Communication_Byte
        {
            get
            {
                return
                    _serialService.StartupByte;
            }
            set
            {
                _serialService.StartupByte =
                    checked((byte)value);
            }
        }

        public bool Start_VCP_Connection
        {
            get
            {
                return
                    _startVcpConnection;
            }
            set
            {
                _startVcpConnection =
                    value;

                if (value)
                {
                    _serialService.Start();
                }
                else
                {
                    _serialService.Stop();
                }
            }
        }

        public bool Close_Serialport
        {
            get { return false; }
            set
            {
                if (value)
                {
                    Close_Port();
                }
            }
        }

        public int Baud_Rate
        {
            get
            {
                return
                    _serialService.BaudRate;
            }
            set
            {
                _serialService.BaudRate =
                    value;
            }
        }

        public void Send_Data(
            byte[] txBytes)
        {
            _serialService.Send(
                txBytes);
        }

        public void Close_Port()
        {
            _startVcpConnection = false;
            _serialService.Stop();
        }

        public void Reconnect()
        {
            _startVcpConnection = true;
            _serialService.RequestReconnect();
        }

        private void SerialService_PortOpened(
            object sender,
            EventArgs e)
        {
            FireEvent(
                Connection_Ready);
        }

        private void SerialService_NormalOperationStarted(
            object sender,
            EventArgs e)
        {
            PostUi(
                delegate
                {
                    label_Status.Text =
                        "Connected!";

                    label_Connection.ForeColor =
                        Color.LimeGreen;
                });

            FireEvent(
                Normal_Operation_Starts);
        }

        private void SerialService_ConnectionFailed(
            object sender,
            EventArgs e)
        {
            PostUi(
                delegate
                {
                    label_Status.Text =
                        "Reconnect...";

                    label_Connection.ForeColor =
                        Color.Tomato;
                });

            FireEvent(
                Connection_Failed);
        }

        private void SerialService_FrameReceived(
            object sender,
            VcpFrameReceivedEventArgs e)
        {
            lock (_frameLock)
            {
                _rxBytes =
                    (byte[])e.Frame.Clone();
            }

            /*
             * Deliberately raised on the serial worker thread. Form1 can
             * validate and answer the MCU without waiting for the UI thread.
             */
            FireEvent(
                Received_Data_Ready);

            PostUi(
                delegate
                {
                    if (checkBox_Rx_Bytes.Checked)
                    {
                        Show_Received_Bytes(
                            e.Frame);
                    }

                    label_Connection.ForeColor =
                        label_Connection.ForeColor ==
                            Color.DimGray
                                ? Color.LimeGreen
                                : Color.DimGray;
                });
        }

        private void SerialService_StatusChanged(
            object sender,
            VcpStatusChangedEventArgs e)
        {
            PostUi(
                delegate
                {
                    label_Status.Text =
                        e.Message;

                    if (!string.IsNullOrWhiteSpace(
                            e.PortName))
                    {
                        comboBox_Available_Ports.Text =
                            e.PortName;
                    }

                    label_Connection.ForeColor =
                        e.IsOpen
                            ? Color.MediumTurquoise
                            : Color.Tomato;

                    label_State.Text =
                        e.IsOpen
                            ? "PORT_OPEN"
                            : "SCANNING";
                });
        }

        private void FireEvent(
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
                        this,
                        EventArgs.Empty);
                }
                catch
                {
                }
            }
        }

        private void PostUi(
            Action action)
        {
            if (action == null ||
                IsDisposed ||
                Disposing)
            {
                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(action);
                }
                catch (InvalidOperationException)
                {
                }

                return;
            }

            action();
        }

        private byte[] Decode_0x1A(byte[] codedBytes)
        {
            if (codedBytes == null)
            {
                return null;
            }

            byte[] decoded = (byte[])codedBytes.Clone();
            int packetCount = decoded.Length / 8;

            for (int packet = 0; packet < packetCount; packet++)
            {
                int baseIndex = packet * 8;

                byte encodedCodeByte = decoded[baseIndex];

                // Convert abc0defg back to 0abcdefg.
                byte codeByte = (byte)((encodedCodeByte & 0x0FU) | ((encodedCodeByte & 0xE0U) >> 1));

                for (int index = 0; index < 7; index++)
                {
                    if (((codeByte >> index) & 0x01U) != 0U)
                    {
                        decoded[baseIndex + index + 1] = 0x1A;
                    }
                }

                // Critical: this byte was zero when the MCU calculated the checksum.
                decoded[baseIndex] = 0x00;
            }

            return decoded;
        }


        private void Show_Received_Bytes(
            byte[] bytesToShow)
        {
            if (bytesToShow == null)
            {
                textBox_Rx_Bytes.Text =
                    string.Empty;

                return;
            }

            string[] values =
                new string[
                    bytesToShow.Length];

            for (int index = 0;
                 index < bytesToShow.Length;
                 index++)
            {
                values[index] =
                    "0x" +
                    bytesToShow[index]
                        .ToString("X2");
            }

            textBox_Rx_Bytes.Text =
                string.Join(
                    ", ",
                    values);
        }

        private void Change_Size()
        {
            if (_isMinimised)
            {
                Width = 15;
                Height = 15;
            }
            else
            {
                Width = 480;
                Height = 160;
            }
        }

        private void VCP_Control_Load(
            object sender,
            EventArgs e)
        {
            Change_Size();
        }

        private void panel_COM_LED_Click(
            object sender,
            EventArgs e)
        {
            if (ModifierKeys ==
                Keys.Control)
            {
                _isMinimised =
                    !_isMinimised;

                Change_Size();
            }
            else
            {
                Reconnect();
            }
        }

        private void label_Connection_Click(
            object sender,
            EventArgs e)
        {
            panel_COM_LED_Click(
                sender,
                e);
        }

        private void button_Start_Click(
            object sender,
            EventArgs e)
        {
            Start_VCP_Connection = true;
        }

        /*
         * Kept only because the existing Designer may still wire them.
         * Communication no longer runs from WinForms timers.
         */
        private void timer_State_Machine_Tick(
            object sender,
            EventArgs e)
        {
        }

        private void timer1_Tick(
            object sender,
            EventArgs e)
        {
        }

        private void panel_COM_LED_Paint(
            object sender,
            PaintEventArgs e)
        {
        }

        private void panel1_Paint(
            object sender,
            PaintEventArgs e)
        {
        }

        private void Auto_Detect_VCP_Control_Disposed(
            object sender,
            EventArgs e)
        {
            _serialService.Dispose();
        }
    }
}