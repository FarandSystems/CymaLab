using System;
using Auto_Detect_VCP_Control;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

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
        public event Action Connected;
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

        private async Task<TcpClient> OpenWifiConnectionAsync(WifiPacketBuffer receiveBuffer, CancellationToken cancellationToken)
        {
            if (receiveBuffer == null)
                throw new ArgumentNullException(nameof(receiveBuffer));

            receiveBuffer.Reset();

            TcpClient client = new TcpClient
            {
                NoDelay = true
            };

            try
            {
                // First establish the TCP connection.
                using (CancellationTokenSource connectTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    connectTimeout.CancelAfter(Constants.WIFI_CONNECT_TIMEOUT_MS);

                    using (connectTimeout.Token.Register(() => client.Close()))
                    {
                        try
                        {
                            connectTimeout.Token.ThrowIfCancellationRequested();

                            await client.ConnectAsync(Constants.WIFI_SERVER_IP, Constants.WIFI_SERVER_PORT).ConfigureAwait(false);

                            connectTimeout.Token.ThrowIfCancellationRequested();
                        }
                        catch (Exception ex) when (connectTimeout.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            throw new TimeoutException("The Wi-Fi TCP connection timed out.", ex);
                        }
                    }
                }

                NetworkStream stream = client.GetStream();

                // Then verify that the device responds to our protocol.
                using (CancellationTokenSource detectionTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    detectionTimeout.CancelAfter(Constants.WIFI_DETECTION_TIMEOUT_MS);

                    using (detectionTimeout.Token.Register(() => client.Close()))
                    {
                        try
                        {
                            byte[] detectionCommand = (byte[])Constants.DETECTION_PC_COMMAND.Clone();

                            await stream.WriteAsync(detectionCommand, 0, detectionCommand.Length, detectionTimeout.Token).ConfigureAwait(false);

                            byte[] receivedBytes = new byte[Constants.WIFI_READ_BUFFER_SIZE];

                            while (!receiveBuffer.DetectionComplete)
                            {
                                int receivedCount = await stream.ReadAsync(receivedBytes, 0, receivedBytes.Length, detectionTimeout.Token).ConfigureAwait(false);

                                if (receivedCount == 0)
                                {
                                    throw new IOException("The Wi-Fi connection closed during detection.");
                                }

                                receiveBuffer.Append(receivedBytes, receivedCount);

                                receiveBuffer.TryReadDetectionResponse();
                            }

                            detectionTimeout.Token.ThrowIfCancellationRequested();
                        }
                        catch (Exception ex) when (detectionTimeout.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            throw new TimeoutException("The device did not answer Wi-Fi detection.", ex);
                        }
                    }
                }

                cancellationToken.ThrowIfCancellationRequested();

                return client;
            }
            catch
            {
                client.Close();
                throw;
            }
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
            vcp.FrameLength = Constants.RX_BUFFER_SIZE;

            isConnected = true;

            StatusChanged?.Invoke("CymaLab USB connected on " + vcp.CurrentPortName);

            Connected?.Invoke();
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