using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace FirmwareTools
{
    public enum XModemChecksumMode
    {
        /// <summary>
        /// Normal XMODEM behavior: use CRC if the receiver sends 'C', otherwise use checksum if it sends NAK.
        /// </summary>
        ReceiverRequested,

        /// <summary>
        /// Force CRC-16/XMODEM after the receiver sends either 'C' or NAK.
        /// Use only if your bootloader expects CRC.
        /// </summary>
        ForceCrc,

        /// <summary>
        /// Force 8-bit checksum after the receiver sends either 'C' or NAK.
        /// Use only for old checksum-only bootloaders.
        /// </summary>
        ForceChecksum
    }

    public enum XModemFirmwareFormat
    {
        /// <summary>
        /// Detect file type from extension. .hex and .ihx are treated as Intel HEX. Everything else is binary.
        /// </summary>
        Auto,

        /// <summary>Raw binary image.</summary>
        Binary,

        /// <summary>Intel HEX image, converted to a flat binary image before transmission.</summary>
        IntelHex
    }

    public sealed class XModemOptions
    {
        /// <summary>
        /// 128 = classic XMODEM/SOH packets. 1024 = XMODEM-1K/STX packets.
        /// Tera Term's normal XMODEM CRC mode uses 128-byte packets.
        /// </summary>
        public int PacketSize { get; set; } = 128;

        // The updater may already have read the bootloader request.
        internal byte? InitialReceiverRequest { get; set; }

        public XModemChecksumMode ChecksumMode { get; set; } = XModemChecksumMode.ReceiverRequested;

        /// <summary>
        /// Auto = use file extension. .hex and .ihx are converted from Intel HEX to binary.
        /// .bin and other extensions are sent as raw binary.
        /// </summary>
        public XModemFirmwareFormat FirmwareFormat { get; set; } = XModemFirmwareFormat.Auto;

        /// <summary>
        /// Byte used to fill gaps between Intel HEX data records when creating the flat binary image.
        /// 0x00 matches the simple Python hex2bin behavior. Use 0xFF if your flash image should preserve erased gaps.
        /// </summary>
        public byte IntelHexGapFillByte { get; set; } = 0x00;

        /// <summary>
        /// Validate the Intel HEX line checksum before conversion.
        /// Recommended: keep true.
        /// </summary>
        public bool ValidateIntelHexChecksum { get; set; } = true;

        /// <summary>
        /// Safety limit for the loaded/converted firmware image.
        /// </summary>
        public long MaximumFirmwareSizeBytes { get; set; } = 64L * 1024L * 1024L;

        /// <summary>How long to wait for the bootloader to send 'C' or NAK.</summary>
        public int InitialHandshakeTimeoutMs { get; set; } = 60000;

        /// <summary>How long to wait for ACK/NAK after each packet.</summary>
        public int PacketResponseTimeoutMs { get; set; } = 10000;

        /// <summary>How long to wait for ACK after EOT.</summary>
        public int EotResponseTimeoutMs { get; set; } = 10000;

        /// <summary>Number of retransmissions before failing a packet or EOT.</summary>
        public int MaxRetries { get; set; } = 10;

        /// <summary>XMODEM pads the last packet. Tera Term uses 0x1A.</summary>
        public byte PaddingByte { get; set; } = 0x1A;

        /// <summary>Optional delay after each write. Usually keep this 0.</summary>
        public int InterPacketDelayMs { get; set; } = 0;

        /// <summary>Flush stale bootloader text or previous bytes before waiting for XMODEM request.</summary>
        public bool DiscardInputBeforeStart { get; set; } = true;
    }

    public sealed class XModemProgressEventArgs : EventArgs
    {
        public XModemProgressEventArgs(long bytesSent, long totalBytes, int packetNumber, int retryCount, bool finished)
        {
            BytesSent = bytesSent;
            TotalBytes = totalBytes;
            PacketNumber = packetNumber;
            RetryCount = retryCount;
            Finished = finished;
        }

        public long BytesSent { get; private set; }
        public long TotalBytes { get; private set; }
        public int PacketNumber { get; private set; }
        public int RetryCount { get; private set; }
        public bool Finished { get; private set; }
        public double Percent
        {
            get { return TotalBytes <= 0 ? 100.0 : (BytesSent * 100.0 / TotalBytes); }
        }
    }

    public sealed class XModemResult
    {
        public XModemResult(long bytesSent, int packetsSent, TimeSpan elapsed, bool usedCrc, int packetSize)
            : this(bytesSent, packetsSent, elapsed, usedCrc, packetSize, XModemFirmwareFormat.Binary, 0, false)
        {
        }

        public XModemResult(long bytesSent, int packetsSent, TimeSpan elapsed, bool usedCrc, int packetSize,
            XModemFirmwareFormat firmwareFormat, uint imageBaseAddress, bool hasImageBaseAddress)
        {
            BytesSent = bytesSent;
            PacketsSent = packetsSent;
            Elapsed = elapsed;
            UsedCrc = usedCrc;
            PacketSize = packetSize;
            FirmwareFormat = firmwareFormat;
            ImageBaseAddress = imageBaseAddress;
            HasImageBaseAddress = hasImageBaseAddress;
        }

        public long BytesSent { get; private set; }
        public int PacketsSent { get; private set; }
        public TimeSpan Elapsed { get; private set; }
        public bool UsedCrc { get; private set; }
        public int PacketSize { get; private set; }

        /// <summary>Detected/used firmware file format.</summary>
        public XModemFirmwareFormat FirmwareFormat { get; private set; }

        /// <summary>
        /// For Intel HEX files, this is the lowest address found in the HEX data records.
        /// The transmitted image starts from this address offset as a flat binary image.
        /// </summary>
        public uint ImageBaseAddress { get; private set; }

        public bool HasImageBaseAddress { get; private set; }
    }

    public sealed class XModemException : Exception
    {
        public XModemException(string message) : base(message) { }
        public XModemException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Sends a BIN or Intel HEX file using XMODEM over an already opened System.IO.Ports.SerialPort.
    /// The caller owns the serial port. Do not let other DataReceived handlers read from the port during upload.
    /// </summary>
    public sealed class XModemUploader
    {
        private const uint ApplicationStartAddress = 0x08010000U;
        private const uint ApplicationEndAddress = 0x08100000U;

        private const uint ApplicationRamStartAddress = 0x20000000U;
        private const uint ApplicationRamEndAddress = 0x20050000U;


        private const byte SOH = 0x01;
        private const byte STX = 0x02;
        private const byte EOT = 0x04;
        private const byte ACK = 0x06;
        private const byte NAK = 0x15;
        private const byte CAN = 0x18;
        private const byte CRC_REQUEST = 0x43; // 'C'

        private readonly SerialPort _serialPort;
        private readonly object _transferLock = new object();

        public XModemUploader(SerialPort serialPort)
        {
            if (serialPort == null)
                throw new ArgumentNullException(nameof(serialPort));

            _serialPort = serialPort;
        }

        public event EventHandler<XModemProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Uploads a firmware file. With default options, .hex/.ihx files are converted to binary automatically;
        /// all other files are sent as raw binary.
        /// </summary>
        public Task<XModemResult> UploadAsync(string binFilePath, XModemOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(delegate
            {
                return Upload(binFilePath, options, cancellationToken);
            }, cancellationToken);
        }

        /// <summary>
        /// Uploads a firmware file. With default options, .hex/.ihx files are converted to binary automatically;
        /// all other files are sent as raw binary.
        /// </summary>
        public XModemResult Upload(string binFilePath, XModemOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            options = options ?? new XModemOptions();
            ValidateOptions(options);

            if (string.IsNullOrWhiteSpace(binFilePath))
                throw new ArgumentException("Firmware file path is empty.", nameof(binFilePath));

            if (!File.Exists(binFilePath))
                throw new FileNotFoundException("Firmware file was not found.", binFilePath);

            if (!_serialPort.IsOpen)
                throw new InvalidOperationException("SerialPort must be opened before starting XMODEM upload.");

            lock (_transferLock)
            {
                return UploadLocked(binFilePath, options, cancellationToken);
            }
        }

        public static byte[] PrepareFirmware(string filePath, XModemOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            ValidateOptions(options);
            FirmwareImage image = LoadFirmwareImage(filePath, options);
            ValidateApplicationImage(image);
            return image.Data;
        }

        public Task<XModemResult> UploadPreparedAsync(byte[] image, XModemOptions options)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if (options == null) throw new ArgumentNullException(nameof(options));
            ValidateOptions(options);
            byte[] snapshot = (byte[])image.Clone();
            return Task.Run(() =>
            {
                lock (_transferLock)
                {
                    if (!_serialPort.IsOpen)
                        throw new InvalidOperationException("The bootloader port is not open.");
                    return UploadImageLocked(
                        new FirmwareImage(snapshot, XModemFirmwareFormat.Binary, 0, false),
                        options, CancellationToken.None);
                }
            });
        }

        private XModemResult UploadLocked(string firmwareFilePath, XModemOptions options, CancellationToken cancellationToken)
        {
            return UploadImageLocked(LoadFirmwareImage(firmwareFilePath, options), options, cancellationToken);
        }

        private XModemResult UploadImageLocked(FirmwareImage firmwareImage, XModemOptions options, CancellationToken cancellationToken)
        {
            ValidateApplicationImage(firmwareImage);

            long totalBytes = firmwareImage.Data.LongLength;
            long acknowledgedBytes = 0;
            int acknowledgedPackets = 0;
            int packetNumber = 1;
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (options.DiscardInputBeforeStart)
            {
                SafeDiscardInBuffer();
            }

            bool useCrc = WaitForReceiverRequest(options, cancellationToken);

            // Tera Term flushes extra startup bytes before sending the first packet.
            SafeDiscardInBuffer();

            byte[] data = new byte[options.PacketSize];

            using (MemoryStream file = new MemoryStream(firmwareImage.Data, false))
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    int dataBytesRead = ReadNextBlock(file, data, options.PacketSize);
                    if (dataBytesRead == 0)
                        break;

                    if (dataBytesRead < options.PacketSize)
                    {
                        for (int i = dataBytesRead; i < options.PacketSize; i++)
                            data[i] = options.PaddingByte;
                    }

                    byte[] packet = BuildPacket((byte)packetNumber, data, options.PacketSize, useCrc);
                    int retryCountUsed;
                    bool packetAcked = SendPacketAndWaitForAck(packet, packetNumber, options, cancellationToken, out retryCountUsed);

                    if (!packetAcked)
                    {
                        throw new TimeoutException("XMODEM packet " + packetNumber + " was not acknowledged after " +
                                                   (options.MaxRetries + 1) + " attempts.");
                    }

                    acknowledgedBytes += dataBytesRead;
                    acknowledgedPackets++;

                    OnProgress(new XModemProgressEventArgs(
                        acknowledgedBytes,
                        totalBytes,
                        packetNumber,
                        retryCountUsed,
                        false));

                    packetNumber = (packetNumber + 1) & 0xFF;
                }
            }

            SendEotAndWaitForAck(options, cancellationToken);

            stopwatch.Stop();

            OnProgress(new XModemProgressEventArgs(
                acknowledgedBytes,
                totalBytes,
                packetNumber,
                0,
                true));

            return new XModemResult(
                acknowledgedBytes,
                acknowledgedPackets,
                stopwatch.Elapsed,
                useCrc,
                options.PacketSize,
                firmwareImage.Format,
                firmwareImage.BaseAddress,
                firmwareImage.HasBaseAddress);
        }

        private static uint ReadUInt32LittleEndian(
    byte[] data,
    int offset)
        {
            return
                ((uint)data[offset + 0]) |
                ((uint)data[offset + 1] << 8) |
                ((uint)data[offset + 2] << 16) |
                ((uint)data[offset + 3] << 24);
        }

        private static void ValidateApplicationImage(
            FirmwareImage firmwareImage)
        {
            if (firmwareImage == null ||
                firmwareImage.Data == null)
            {
                throw new XModemException(
                    "The firmware image is invalid.");
            }

            if (firmwareImage.Data.Length < 8)
            {
                throw new XModemException(
                    "The firmware image is too small to contain an STM32 vector table.");
            }

            if (firmwareImage.Data.LongLength > 0x000F0000L)
            {
                throw new XModemException(
                    "The firmware is larger than the STM32 application region.");
            }

            /*
             * For Intel HEX, the lowest address must be the beginning
             * of the relocated main application.
             */
            if (firmwareImage.HasBaseAddress &&
                firmwareImage.BaseAddress != ApplicationStartAddress)
            {
                throw new XModemException(
                    "The Intel HEX file has the wrong base address." +
                    Environment.NewLine +
                    "Expected: 0x" +
                    ApplicationStartAddress.ToString("X8") +
                    Environment.NewLine +
                    "Found: 0x" +
                    firmwareImage.BaseAddress.ToString("X8") +
                    Environment.NewLine +
                    "Rebuild the main firmware with IROM1 start 0x08010000.");
            }

            uint initialStackPointer =
                ReadUInt32LittleEndian(
                    firmwareImage.Data,
                    0);

            uint resetVector =
                ReadUInt32LittleEndian(
                    firmwareImage.Data,
                    4);

            uint resetHandlerAddress =
                resetVector & 0xFFFFFFFEU;

            bool stackPointerValid =
                initialStackPointer >= ApplicationRamStartAddress &&
                initialStackPointer <= ApplicationRamEndAddress &&
                (initialStackPointer & 0x7U) == 0U;

            if (!stackPointerValid)
            {
                throw new XModemException(
                    "The firmware has an invalid initial stack pointer: 0x" +
                    initialStackPointer.ToString("X8"));
            }

            bool resetVectorValid =
                (resetVector & 1U) != 0U &&
                resetHandlerAddress >= ApplicationStartAddress &&
                resetHandlerAddress < ApplicationEndAddress;

            if (!resetVectorValid)
            {
                throw new XModemException(
                    "The firmware Reset_Handler is not linked inside the application region." +
                    Environment.NewLine +
                    "Reset vector: 0x" +
                    resetVector.ToString("X8") +
                    Environment.NewLine +
                    "Expected range: 0x08010000–0x080FFFFF.");
            }
        }

        private bool WaitForReceiverRequest(XModemOptions options, CancellationToken cancellationToken)
        {
            if (options.InitialReceiverRequest.HasValue)
            {
                byte request = options.InitialReceiverRequest.Value;
                options.InitialReceiverRequest = null;
                if (request != CRC_REQUEST && request != NAK)
                    throw new XModemException("Invalid initial XMODEM request.");
                if (options.ChecksumMode == XModemChecksumMode.ForceCrc) return true;
                if (options.ChecksumMode == XModemChecksumMode.ForceChecksum) return false;
                return request == CRC_REQUEST;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            int cancelCount = 0;

            while (stopwatch.ElapsedMilliseconds < options.InitialHandshakeTimeoutMs)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int b = ReadByteNonBlocking(100, cancellationToken);
                if (b < 0)
                    continue;

                if (b == CAN)
                {
                    cancelCount++;
                    if (cancelCount >= 2)
                        throw new XModemException("XMODEM receiver canceled transfer by sending CAN CAN.");
                    continue;
                }

                cancelCount = 0;

                if (b == CRC_REQUEST || b == NAK)
                {
                    if (options.ChecksumMode == XModemChecksumMode.ForceCrc)
                        return true;

                    if (options.ChecksumMode == XModemChecksumMode.ForceChecksum)
                        return false;

                    return b == CRC_REQUEST;
                }
            }

            throw new TimeoutException("Timed out waiting for XMODEM receiver request ('C' for CRC or NAK for checksum). Make sure the bootloader is already in receive mode.");
        }

        private bool SendPacketAndWaitForAck(byte[] packet, int packetNumber, XModemOptions options,
            CancellationToken cancellationToken, out int retryCountUsed)
        {
            for (int attempt = 0; attempt <= options.MaxRetries; attempt++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Remove stale 'C' or NAK bytes that may have arrived before this attempt.
                SafeDiscardInBuffer();

                WriteBytes(packet, 0, packet.Length, options, cancellationToken);

                int response = ReadAckOrNak(options.PacketResponseTimeoutMs, cancellationToken);
                if (response == ACK)
                {
                    retryCountUsed = attempt;
                    return true;
                }

                // NAK or timeout: resend the same packet.
                // Other bytes are ignored until timeout.
            }

            retryCountUsed = options.MaxRetries;
            return false;
        }

        private void SendEotAndWaitForAck(XModemOptions options, CancellationToken cancellationToken)
        {
            byte[] eot = new byte[] { EOT };

            for (int attempt = 0; attempt <= options.MaxRetries; attempt++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                SafeDiscardInBuffer();
                WriteBytes(eot, 0, 1, options, cancellationToken);

                int response = ReadAckOrNak(options.EotResponseTimeoutMs, cancellationToken);
                if (response == ACK)
                    return;

                // NAK or timeout: send EOT again.
            }

            throw new TimeoutException("XMODEM EOT was not acknowledged by the receiver.");
        }

        private int ReadAckOrNak(int timeoutMs, CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int cancelCount = 0;

            while (stopwatch.ElapsedMilliseconds < timeoutMs)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int b = ReadByteNonBlocking(100, cancellationToken);
                if (b < 0)
                    continue;

                if (b == CAN)
                {
                    cancelCount++;
                    if (cancelCount >= 2)
                        throw new XModemException("XMODEM receiver canceled transfer by sending CAN CAN.");
                    continue;
                }

                cancelCount = 0;

                if (b == ACK || b == NAK)
                    return b;

                // Ignore bootloader text, repeated 'C', or other bytes.
            }

            return -1;
        }

        private int ReadByteNonBlocking(int sliceTimeoutMs, CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (stopwatch.ElapsedMilliseconds < sliceTimeoutMs)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    if (_serialPort.BytesToRead > 0)
                        return _serialPort.ReadByte();
                }
                catch (TimeoutException)
                {
                    return -1;
                }

                Thread.Sleep(2);
            }

            return -1;
        }

        private void WriteBytes(byte[] buffer, int offset, int count, XModemOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _serialPort.Write(buffer, offset, count);

            try
            {
                _serialPort.BaseStream.Flush();
            }
            catch
            {
                // Some SerialPort implementations do not need or support explicit flushing.
            }

            if (options.InterPacketDelayMs > 0)
                Thread.Sleep(options.InterPacketDelayMs);
        }

        private static byte[] BuildPacket(byte blockNumber, byte[] data, int packetSize, bool useCrc)
        {
            int checkLength = useCrc ? 2 : 1;
            byte[] packet = new byte[3 + packetSize + checkLength];

            packet[0] = packetSize == 1024 ? STX : SOH;
            packet[1] = blockNumber;
            packet[2] = (byte)(0xFF - blockNumber);

            Buffer.BlockCopy(data, 0, packet, 3, packetSize);

            if (useCrc)
            {
                ushort crc = CalculateCrc16XModem(data, 0, packetSize);
                packet[3 + packetSize] = (byte)((crc >> 8) & 0xFF);
                packet[3 + packetSize + 1] = (byte)(crc & 0xFF);
            }
            else
            {
                packet[3 + packetSize] = CalculateChecksum(data, 0, packetSize);
            }

            return packet;
        }

        private static ushort CalculateCrc16XModem(byte[] buffer, int offset, int count)
        {
            ushort crc = 0x0000;

            for (int i = offset; i < offset + count; i++)
            {
                crc ^= (ushort)(buffer[i] << 8);

                for (int bit = 0; bit < 8; bit++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }

            return crc;
        }

        private static byte CalculateChecksum(byte[] buffer, int offset, int count)
        {
            int sum = 0;
            for (int i = offset; i < offset + count; i++)
                sum += buffer[i];

            return (byte)(sum & 0xFF);
        }

        private static int ReadNextBlock(Stream file, byte[] buffer, int count)
        {
            int totalRead = 0;

            while (totalRead < count)
            {
                int read = file.Read(buffer, totalRead, count - totalRead);
                if (read == 0)
                    break;

                totalRead += read;
            }

            return totalRead;
        }

        private static void ValidateOptions(XModemOptions options)
        {
            if (options.PacketSize != 128 && options.PacketSize != 1024)
                throw new ArgumentOutOfRangeException(nameof(options.PacketSize), "PacketSize must be 128 or 1024.");

            if (options.InitialHandshakeTimeoutMs <= 0)
                throw new ArgumentOutOfRangeException(nameof(options.InitialHandshakeTimeoutMs));

            if (options.PacketResponseTimeoutMs <= 0)
                throw new ArgumentOutOfRangeException(nameof(options.PacketResponseTimeoutMs));

            if (options.EotResponseTimeoutMs <= 0)
                throw new ArgumentOutOfRangeException(nameof(options.EotResponseTimeoutMs));

            if (options.MaxRetries < 0)
                throw new ArgumentOutOfRangeException(nameof(options.MaxRetries));

            if (options.MaximumFirmwareSizeBytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(options.MaximumFirmwareSizeBytes));
        }

        private void SafeDiscardInBuffer()
        {
            try
            {
                _serialPort.DiscardInBuffer();
            }
            catch
            {
                // Ignore if the port driver rejects a discard while the port is active.
            }
        }

        private void OnProgress(XModemProgressEventArgs e)
        {
            EventHandler<XModemProgressEventArgs> handler = ProgressChanged;
            if (handler != null)
                handler(this, e);
        }

        private static FirmwareImage LoadFirmwareImage(string filePath, XModemOptions options)
        {
            XModemFirmwareFormat format = ResolveFirmwareFormat(filePath, options.FirmwareFormat);

            if (format == XModemFirmwareFormat.IntelHex)
                return LoadIntelHexFirmwareImage(filePath, options);

            return LoadBinaryFirmwareImage(filePath, options);
        }

        private static XModemFirmwareFormat ResolveFirmwareFormat(string filePath, XModemFirmwareFormat requestedFormat)
        {
            if (requestedFormat == XModemFirmwareFormat.Binary || requestedFormat == XModemFirmwareFormat.IntelHex)
                return requestedFormat;

            string extension = Path.GetExtension(filePath);
            if (string.Equals(extension, ".hex", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(extension, ".ihx", StringComparison.OrdinalIgnoreCase))
            {
                return XModemFirmwareFormat.IntelHex;
            }

            return XModemFirmwareFormat.Binary;
        }

        private static FirmwareImage LoadBinaryFirmwareImage(string filePath, XModemOptions options)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Length <= 0)
                throw new XModemException("Firmware file is empty.");

            if (info.Length > options.MaximumFirmwareSizeBytes)
            {
                throw new XModemException("Firmware file is too large: " + info.Length +
                                          " bytes. Limit is " + options.MaximumFirmwareSizeBytes + " bytes.");
            }

            return new FirmwareImage(File.ReadAllBytes(filePath), XModemFirmwareFormat.Binary, 0, false);
        }

        private static FirmwareImage LoadIntelHexFirmwareImage(string filePath, XModemOptions options)
        {
            List<IntelHexDataRecord> records = new List<IntelHexDataRecord>();
            ulong minAddress = ulong.MaxValue;
            ulong maxAddress = 0;
            uint extendedBaseAddress = 0;
            bool eofSeen = false;
            int lineNumber = 0;

            foreach (string rawLine in File.ReadLines(filePath))
            {
                lineNumber++;
                string line = rawLine.Trim();

                if (line.Length == 0)
                    continue;

                if (!line.StartsWith(":", StringComparison.Ordinal))
                    throw new FormatException("Invalid Intel HEX line " + lineNumber + ": missing ':' start code.");

                if (line.Length < 11)
                    throw new FormatException("Invalid Intel HEX line " + lineNumber + ": line is too short.");

                int byteCount = ParseHexByte(line, 1, lineNumber);
                int expectedLength = 11 + (byteCount * 2);
                if (line.Length != expectedLength)
                {
                    throw new FormatException("Invalid Intel HEX line " + lineNumber +
                                              ": expected " + expectedLength + " characters but found " + line.Length + ".");
                }

                ushort address = ParseHexUShort(line, 3, lineNumber);
                int recordType = ParseHexByte(line, 7, lineNumber);

                byte[] data = new byte[byteCount];
                for (int i = 0; i < byteCount; i++)
                    data[i] = (byte)ParseHexByte(line, 9 + (i * 2), lineNumber);

                int checksum = ParseHexByte(line, 9 + (byteCount * 2), lineNumber);

                if (options.ValidateIntelHexChecksum && !IsIntelHexChecksumValid(byteCount, address, recordType, data, checksum))
                    throw new FormatException("Invalid Intel HEX checksum at line " + lineNumber + ".");

                if (eofSeen)
                    throw new FormatException("Invalid Intel HEX file: data found after EOF record at line " + lineNumber + ".");

                switch (recordType)
                {
                    case 0x00: // Data record
                        {
                            if (byteCount == 0)
                                break;

                            ulong absoluteAddress = (ulong)extendedBaseAddress + address;
                            ulong endAddress = absoluteAddress + (ulong)byteCount - 1UL;

                            if (endAddress > uint.MaxValue)
                                throw new FormatException("Intel HEX address is above 32-bit range at line " + lineNumber + ".");

                            records.Add(new IntelHexDataRecord((uint)absoluteAddress, data));
                            if (absoluteAddress < minAddress)
                                minAddress = absoluteAddress;
                            if (endAddress > maxAddress)
                                maxAddress = endAddress;
                            break;
                        }

                    case 0x01: // End Of File
                        {
                            if (byteCount != 0)
                                throw new FormatException("Invalid Intel HEX EOF record at line " + lineNumber + ".");

                            eofSeen = true;
                            break;
                        }

                    case 0x02: // Extended Segment Address
                        {
                            if (byteCount != 2)
                                throw new FormatException("Invalid Intel HEX extended segment address record at line " + lineNumber + ".");

                            uint segment = (uint)((data[0] << 8) | data[1]);
                            extendedBaseAddress = segment << 4;
                            break;
                        }

                    case 0x03: // Start Segment Address - not needed for flat binary conversion
                        break;

                    case 0x04: // Extended Linear Address
                        {
                            if (byteCount != 2)
                                throw new FormatException("Invalid Intel HEX extended linear address record at line " + lineNumber + ".");

                            uint upperAddress = (uint)((data[0] << 8) | data[1]);
                            extendedBaseAddress = upperAddress << 16;
                            break;
                        }

                    case 0x05: // Start Linear Address - not needed for flat binary conversion
                        break;

                    default:
                        throw new FormatException("Unsupported Intel HEX record type 0x" + recordType.ToString("X2") +
                                                  " at line " + lineNumber + ".");
                }
            }

            if (records.Count == 0 || minAddress == ulong.MaxValue)
                throw new XModemException("No data records were found in the Intel HEX file.");

            ulong imageSize = maxAddress - minAddress + 1UL;
            if (imageSize > (ulong)options.MaximumFirmwareSizeBytes)
            {
                throw new XModemException("Converted Intel HEX image is too large: " + imageSize +
                                          " bytes. Limit is " + options.MaximumFirmwareSizeBytes + " bytes.");
            }

            if (imageSize > int.MaxValue)
                throw new XModemException("Converted Intel HEX image is too large for this process.");

            byte[] image = new byte[(int)imageSize];
            if (options.IntelHexGapFillByte != 0x00)
            {
                for (int i = 0; i < image.Length; i++)
                    image[i] = options.IntelHexGapFillByte;
            }

            foreach (IntelHexDataRecord record in records)
            {
                int offset = checked((int)((ulong)record.Address - minAddress));
                Buffer.BlockCopy(record.Data, 0, image, offset, record.Data.Length);
            }

            return new FirmwareImage(image, XModemFirmwareFormat.IntelHex, (uint)minAddress, true);
        }

        private static bool IsIntelHexChecksumValid(int byteCount, ushort address, int recordType, byte[] data, int checksum)
        {
            int sum = byteCount;
            sum += (address >> 8) & 0xFF;
            sum += address & 0xFF;
            sum += recordType & 0xFF;

            for (int i = 0; i < data.Length; i++)
                sum += data[i];

            sum += checksum & 0xFF;
            return (sum & 0xFF) == 0;
        }

        private static int ParseHexByte(string line, int startIndex, int lineNumber)
        {
            if (startIndex < 0 || startIndex + 2 > line.Length)
                throw new FormatException("Invalid Intel HEX line " + lineNumber + ": unexpected end of line.");

            try
            {
                return Convert.ToByte(line.Substring(startIndex, 2), 16);
            }
            catch (Exception ex)
            {
                throw new FormatException("Invalid hex byte at line " + lineNumber + ", position " + startIndex + ".", ex);
            }
        }

        private static ushort ParseHexUShort(string line, int startIndex, int lineNumber)
        {
            if (startIndex < 0 || startIndex + 4 > line.Length)
                throw new FormatException("Invalid Intel HEX line " + lineNumber + ": unexpected end of line.");

            try
            {
                return Convert.ToUInt16(line.Substring(startIndex, 4), 16);
            }
            catch (Exception ex)
            {
                throw new FormatException("Invalid hex word at line " + lineNumber + ", position " + startIndex + ".", ex);
            }
        }

        private sealed class FirmwareImage
        {
            public FirmwareImage(byte[] data, XModemFirmwareFormat format, uint baseAddress, bool hasBaseAddress)
            {
                Data = data;
                Format = format;
                BaseAddress = baseAddress;
                HasBaseAddress = hasBaseAddress;
            }

            public byte[] Data { get; private set; }
            public XModemFirmwareFormat Format { get; private set; }
            public uint BaseAddress { get; private set; }
            public bool HasBaseAddress { get; private set; }
        }

        private sealed class IntelHexDataRecord
        {
            public IntelHexDataRecord(uint address, byte[] data)
            {
                Address = address;
                Data = data;
            }

            public uint Address { get; private set; }
            public byte[] Data { get; private set; }
        }
    }
}
