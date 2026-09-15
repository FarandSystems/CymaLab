using FirmwareTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CymaLAB_Ver_1._0
{
    public sealed class FirmwareUpdater
    {
        private const int BootloaderWaitMs = 20000;
        private const int BootloaderProbeMs = 1200;
        private const int PortPollMs = 200;
        private const int ResetSettleMs = 1000;
        private const int BootloaderBaudRate = 115200;
        private const int SerialReadTimeoutMs = 100;
        private const int SerialWriteTimeoutMs = 10000;
        private const int XmodemPacketSize = 1024;
        private const int XmodemMaxRetries = 30;
        private const byte CrcRequest = 0x43;
        private const long ApplicationSizeBytes = 0x000F0000;

        private readonly Communication communication;
        private readonly Commands commands;
        private int busy;

        public bool IsBusy { get { return Volatile.Read(ref busy) != 0; } }
        public event Action<string> StatusChanged;
        public event Action<int> ProgressChanged;

        public FirmwareUpdater(Communication communication, Commands commands)
        {
            this.communication = communication ?? throw new ArgumentNullException(nameof(communication));
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        // Call from the UI thread. A recovery port is used only when the normal
        // USB application is disconnected and the user selected that port.
        public async Task UpdateAsync(string filePath, string recoveryPortName = null)
        {
            if (Interlocked.CompareExchange(ref busy, 1, 0) != 0)
                throw new InvalidOperationException("A firmware update is already running.");

            bool restartUsb = false;
            try
            {
                bool requestBootloader = communication.IsConnected;
                if (requestBootloader && communication.ActiveTransport != Enums.CommunicationTransport.Usb)
                    throw new InvalidOperationException("Firmware update requires USB.");

                string applicationPort = requestBootloader
                    ? communication.CurrentUsbPortName : recoveryPortName;
                if (string.IsNullOrWhiteSpace(applicationPort))
                    throw new InvalidOperationException("Connect through USB or select the bootloader COM port.");

                var options = new XModemOptions
                {
                    PacketSize = XmodemPacketSize,
                    ChecksumMode = XModemChecksumMode.ForceCrc,
                    FirmwareFormat = XModemFirmwareFormat.Auto,
                    MaximumFirmwareSizeBytes = ApplicationSizeBytes,
                    IntelHexGapFillByte = 0xFF,
                    DiscardInputBeforeStart = false,
                    MaxRetries = XmodemMaxRetries
                };

                Report("Checking firmware file...");
                // Keep the validated bytes in memory: the upload cannot read
                // a different file if the original is changed during reset.
                byte[] image = await Task.Run(() => XModemUploader.PrepareFirmware(filePath, options));

                if (requestBootloader && (!communication.IsConnected ||
                    communication.ActiveTransport != Enums.CommunicationTransport.Usb ||
                    !string.Equals(communication.CurrentUsbPortName, applicationPort, StringComparison.OrdinalIgnoreCase)))
                    throw new IOException("The USB connection changed while checking the firmware. Try again.");

                string[] portsBeforeReset = SerialPort.GetPortNames();
                restartUsb = true;
                try
                {
                    if (requestBootloader)
                    {
                        Report("Requesting bootloader...");
                        if (commands.CaptureInProgress)
                            commands.StopCapture();
                        commands.EnterBootloader();
                    }
                }
                finally
                {
                    // Release the normal reader and stop automatic detection
                    // before opening any bootloader port.
                    communication.Stop();
                    commands.Reset();
                }

                if (requestBootloader)
                    await Task.Delay(ResetSettleMs);

                Report("Waiting for USB bootloader...");
                using (SerialPort port = await Task.Run(() => OpenBootloaderPort(
                    applicationPort, portsBeforeReset, requestBootloader)))
                {
                    Report("Uploading on " + port.PortName + "...");
                    options.InitialReceiverRequest = CrcRequest;
                    var uploader = new XModemUploader(port);
                    uploader.ProgressChanged += (sender, e) =>
                    {
                        // Reserve 100% for the final EOT acknowledgement.
                        int percent = e.Finished ? 100 : Math.Min(99, (int)e.Percent);
                        ProgressChanged?.Invoke(percent);
                    };
                    await uploader.UploadPreparedAsync(image, options);
                }

                Report("Transfer acknowledged. Waiting for the application to reconnect...");
            }
            finally
            {
                try
                {
                    if (restartUsb)
                        communication.Start(Enums.CommunicationTransport.Usb);
                }
                finally
                {
                    Interlocked.Exchange(ref busy, 0);
                }
            }
        }

        private SerialPort OpenBootloaderPort(string preferredPort, string[] previousPorts, bool allowNewPort)
        {
            var previous = new HashSet<string>(previousPorts, StringComparer.OrdinalIgnoreCase);
            var timer = Stopwatch.StartNew();
            Exception lastError = null;

            while (timer.ElapsedMilliseconds < BootloaderWaitMs)
            {
                string[] current = SerialPort.GetPortNames();
                var candidates = new List<string>();
                if (current.Contains(preferredPort, StringComparer.OrdinalIgnoreCase))
                    candidates.Add(preferredPort);

                if (allowNewPort)
                {
                    string[] added = current.Where(p => !previous.Contains(p)).ToArray();
                    if (added.Length > 1)
                        throw new IOException("Several new COM ports appeared. Retry in recovery mode and select the bootloader port explicitly.");
                    foreach (string name in added)
                        if (!candidates.Contains(name, StringComparer.OrdinalIgnoreCase))
                            candidates.Add(name);
                }

                foreach (string name in candidates)
                {
                    var port = new SerialPort(name, BootloaderBaudRate, Parity.None, 8, StopBits.One)
                    {
                        Handshake = Handshake.None,
                        DtrEnable = true,
                        RtsEnable = false,
                        ReadTimeout = SerialReadTimeoutMs,
                        WriteTimeout = SerialWriteTimeoutMs
                    };
                    bool accepted = false;
                    try
                    {
                        port.Open();
                        var probe = Stopwatch.StartNew();
                        while (probe.ElapsedMilliseconds < BootloaderProbeMs && timer.ElapsedMilliseconds < BootloaderWaitMs)
                        {
                            int value;
                            try { value = port.ReadByte(); }
                            catch (TimeoutException) { continue; }
                            if (value == CrcRequest)
                            {
                                // Pass this consumed request to the uploader.
                                accepted = true;
                                return port;
                            }
                        }
                    }
                    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
                    {
                        lastError = ex;
                    }
                    finally
                    {
                        if (!accepted) port.Dispose();
                    }
                }
                Thread.Sleep(PortPollMs);
            }
            throw new TimeoutException("No XMODEM CRC request arrived from the bootloader. Check the USB connection and installed bootloader, then retry in recovery mode.", lastError);
        }

        private void Report(string message) { StatusChanged?.Invoke(message); }
    }
}
