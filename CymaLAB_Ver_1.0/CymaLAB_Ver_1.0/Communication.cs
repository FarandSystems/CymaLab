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

        private volatile Enums.CommunicationTransport activeTransport = Enums.CommunicationTransport.None;

        private CancellationTokenSource wifiCancellation;
        private Task wifiTask;

        private volatile NetworkStream wifiStream;

        private readonly object wifiSendLock = new object();

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
                // Capture the socket before cancellation can close the client.
                Socket connectSocket = client.Client;

                using (CancellationTokenSource connectTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    connectTimeout.CancelAfter(Constants.WIFI_CONNECT_TIMEOUT_MS);

                    using (connectTimeout.Token.Register(() => client.Close()))
                    {
                        try
                        {
                            connectTimeout.Token.ThrowIfCancellationRequested();

                            await Task.Factory.FromAsync((AsyncCallback callback, object state) =>
                                connectSocket.BeginConnect(Constants.WIFI_SERVER_IP, Constants.WIFI_SERVER_PORT, callback, state),
                                connectSocket.EndConnect, null).ConfigureAwait(false);

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

        private async Task RunWifiAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await RunWifiSessionAsync(token).ConfigureAwait(false);

                    if (token.IsCancellationRequested)
                        break;

                    StatusChanged?.Invoke("Wi-Fi disconnected. Retrying in " + Constants.WIFI_RECONNECT_DELAY_MS / 1000.0 + " seconds...");

                    await Task.Delay(Constants.WIFI_RECONNECT_DELAY_MS, token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                // Switching transport or closing the application.
            }
        }

        private async Task RunWifiSessionAsync(CancellationToken token)
        {
            TcpClient client = null;

            try
            {
                StatusChanged?.Invoke("Connecting to Wi-Fi device...");

                WifiPacketBuffer receiveBuffer = new WifiPacketBuffer();

                client = await OpenWifiConnectionAsync(receiveBuffer, token).ConfigureAwait(false);

                token.ThrowIfCancellationRequested();

                using (token.Register(() => client.Close()))
                {
                    NetworkStream stream = client.GetStream();

                    stream.WriteTimeout = Constants.WIFI_WRITE_TIMEOUT_MS;
                    wifiStream = stream;

                    token.ThrowIfCancellationRequested();

                    isConnected = true;

                    StatusChanged?.Invoke("CymaLab Wi-Fi connected on " + Constants.WIFI_SERVER_IP + ":" + Constants.WIFI_SERVER_PORT);

                    Connected?.Invoke();

                    byte[] receivedBytes = new byte[Constants.WIFI_READ_BUFFER_SIZE];

                    while (true)
                    {
                        token.ThrowIfCancellationRequested();

                        // Also process bytes left over from detection.
                        byte[] packet;

                        while (receiveBuffer.TryReadPacket(out packet))
                        {
                            token.ThrowIfCancellationRequested();

                            PacketReceived?.Invoke(packet);
                        }

                        int receivedCount = await stream.ReadAsync(receivedBytes, 0, receivedBytes.Length, token).ConfigureAwait(false);

                        if (receivedCount == 0)
                        {
                            throw new IOException("The device closed the Wi-Fi connection.");
                        }

                        receiveBuffer.Append(receivedBytes, receivedCount);
                    }
                }
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                // Normal shutdown or transport change.
            }
            catch (Exception ex) when (ex is IOException || ex is SocketException || ex is TimeoutException || ex is ObjectDisposedException)
            {
                if (!token.IsCancellationRequested)
                {
                    StatusChanged?.Invoke("Wi-Fi connection ended: " + ex.Message);
                }
            }
            finally
            {
                wifiStream = null;

                if (client != null)
                    client.Close();

                MarkDisconnected();
            }
        }


        public void Start(Enums.CommunicationTransport transport)
        {
            if (transport != Enums.CommunicationTransport.Usb && transport != Enums.CommunicationTransport.Wifi)
            {
                throw new ArgumentOutOfRangeException(nameof(transport));
            }

            Stop();

            activeTransport = transport;

            if (transport == Enums.CommunicationTransport.Usb)
            {
                StatusChanged?.Invoke("Searching for USB device...");
                vcp.Start();
            }
            else
            {
                wifiCancellation = new CancellationTokenSource();

                CancellationToken token = wifiCancellation.Token;

                wifiTask = Task.Run(() => RunWifiAsync(token));
            }
        }

        public void Stop()
        {
            // Cancellation closes the Wi-Fi socket, interrupting pending I/O.
            if (wifiCancellation != null)
                wifiCancellation.Cancel();

            vcp.Stop();

            try
            {
                // Finish the old receiver before another connection can start.
                if (wifiTask != null)
                    wifiTask.GetAwaiter().GetResult();
            }
            finally
            {
                wifiTask = null;
                wifiStream = null;

                if (wifiCancellation != null)
                {
                    wifiCancellation.Dispose();
                    wifiCancellation = null;
                }

                MarkDisconnected();

                activeTransport = Enums.CommunicationTransport.None;
            }
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
                throw new InvalidOperationException("The device is not connected.");

            switch (activeTransport)
            {
                case Enums.CommunicationTransport.Usb:
                    vcp.Send(command);
                    break;

                case Enums.CommunicationTransport.Wifi:
                    lock (wifiSendLock)
                    {
                        NetworkStream stream = wifiStream;

                        if (!IsConnected || stream == null)
                        {
                            throw new InvalidOperationException("The Wi-Fi device is not connected.");
                        }

                        try
                        {
                            stream.Write(command, 0, command.Length);
                        }
                        catch
                        {
                            // Wake the receive loop so it handles connection loss.
                            stream.Close();
                            throw;
                        }
                    }
                    break;

                default:
                    throw new InvalidOperationException("No communication transport is active.");
            }
        }

        public void Dispose()
        {
            Stop();
            vcp.Dispose();
        }

        private void MarkDisconnected()
        {
            bool wasConnected = isConnected;

            isConnected = false;

            if (wasConnected)
                ConnectionLost?.Invoke();
        }

        private void Vcp_PortOpened(object sender, EventArgs e)
        {
            if (activeTransport != Enums.CommunicationTransport.Usb)
                return;

            // Every newly opened port must complete detection first.
            isConnected = false;

            vcp.FrameLength = Constants.COMMAND_FRAME_SIZE_BYTES;
        }

        private void Vcp_FrameReceived(object sender, VcpFrameReceivedEventArgs e)
        {
            if (activeTransport != Enums.CommunicationTransport.Usb)
                return;

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
            if (activeTransport != Enums.CommunicationTransport.Usb)
                return;

            vcp.FrameLength = Constants.RX_BUFFER_SIZE;

            isConnected = true;

            StatusChanged?.Invoke("CymaLab USB connected on " + vcp.CurrentPortName);

            Connected?.Invoke();
        }

        private void Vcp_StatusChanged(object sender, VcpStatusChangedEventArgs e)
        {
            if (activeTransport != Enums.CommunicationTransport.Usb)
                return;

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