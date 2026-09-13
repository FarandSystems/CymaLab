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

        public event Action<string> StatusChanged;

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

            vcp.FrameValidator = ValidateDetectionFrame;

            vcp.PortOpened += Vcp_PortOpened;
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
                isConnected = false;

            StatusChanged?.Invoke(e.Message);
        }
    }
}