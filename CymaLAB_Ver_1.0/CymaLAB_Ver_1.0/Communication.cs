using System;
using Auto_Detect_VCP_Control;

namespace CymaLAB_Ver_1._0
{
    public sealed class Communication
    {
        private readonly VcpSerialService vcp;
        private volatile bool isConnected;

        public bool IsConnected
        {
            get { return isConnected; }
        }

        public event Action ConnectionLost;
        public event Action<string> StatusChanged;
        public event Action<byte[]> PacketReceived;

        public Communication()
        {
            vcp = new VcpSerialService
            {
                BaudRate = Constants.USB_BAUD_RATE,

                FrameLength = Constants.COMMAND_FRAME_SIZE_BYTES,

                StartupCommandBytes = (byte[])Constants.DETECTION_PC_COMMAND.Clone(),

                ResponseByteIndex = Constants.DETECTION_RESPONSE_INDEX,

                ResponseByte = Constants.DETECTION_RESPONSE_VALUE
            };

            vcp.FrameDecoder = Utils.DecodeUsbData;
            vcp.FrameValidator = ValidateReceivedData;

            vcp.PortOpened += Vcp_PortOpened;
            vcp.FrameReceived += Vcp_FrameReceived;
            vcp.NormalOperationStarted += Vcp_NormalOperationStarted;
            vcp.StatusChanged += Vcp_StatusChanged;
        }

        public void Start()
        {
            vcp.Start();
        }

        public void Stop()
        {
            vcp.Stop();
            isConnected = false;
        }

        public void Send(byte[] command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.Length != Constants.COMMAND_FRAME_SIZE_BYTES)
            {
                throw new ArgumentException("A command must contain exactly one frame.", nameof(command));
            }

            if (!IsConnected)
            {
                throw new InvalidOperationException("The device is not connected.");
            }

            vcp.Send(command);
        }

        public void Dispose()
        {
            vcp.Dispose();
            isConnected = false;
        }

        private void Vcp_PortOpened(object sender, EventArgs e)
        {
            // Every newly opened port must complete detection first.
            isConnected = false;

            vcp.FrameLength = Constants.COMMAND_FRAME_SIZE_BYTES;
        }

        private void Vcp_FrameReceived(object sender, VcpFrameReceivedEventArgs e)
        {
            if (!isConnected)
                return;

            if (e.Frame.Length != Constants.RX_BUFFER_SIZE)
                return;

            // Already decoded and validated by VcpSerialService.
            PacketReceived?.Invoke(e.Frame);
        }

        private bool ValidateReceivedData(byte[] data)
        {
            if (data == null)
                return false;

            if (data.Length == Constants.COMMAND_FRAME_SIZE_BYTES)
                return ValidateDetectionFrame(data);

            if (data.Length == Constants.RX_BUFFER_SIZE)
                return Utils.Validate_Rx_Packet(data);

            return false;
        }

        private bool ValidateDetectionFrame(byte[] frame)
        {
            byte[] expected = Constants.DETECTION_DEVICE_RESPONSE;

            if (frame == null || frame.Length != expected.Length)
                return false;

            for (int index = 0; index < expected.Length; index++)
            {
                if (frame[index] != expected[index])
                    return false;
            }

            return true;
        }

        private void Vcp_NormalOperationStarted(object sender, EventArgs e)
        {
            // The service calls this only after the handshake succeeds.
            vcp.FrameLength = Constants.RX_BUFFER_SIZE;

            isConnected = true;

            StatusChanged?.Invoke("CymaLab USB connected on " + vcp.CurrentPortName);
        }

        private void Vcp_StatusChanged(object sender, VcpStatusChangedEventArgs e)
        {
            if (!e.IsOpen)
            {
                bool wasConnected = isConnected;

                isConnected = false;

                if (wasConnected)
                    ConnectionLost?.Invoke();
            }

            StatusChanged?.Invoke(e.Message);
        }
    }
}