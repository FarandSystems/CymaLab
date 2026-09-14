using System;
using System.Collections.Generic;

namespace CymaLAB_Ver_1._0
{
    public sealed class WifiPacketBuffer
    {
        private readonly List<byte> buffer = new List<byte>(Constants.WIFI_READ_BUFFER_SIZE);

        public bool DetectionComplete { get; private set; }

        public void Reset()
        {
            buffer.Clear();
            DetectionComplete = false;
        }

        public void Append(byte[] bytes, int count)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (count < 0 || count > bytes.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (int index = 0; index < count; index++)
                buffer.Add(bytes[index]);
        }

        public bool TryReadDetectionResponse()
        {
            if (DetectionComplete)
                return false;

            byte[] expected = Constants.DETECTION_DEVICE_RESPONSE;

            int lastStart = buffer.Count - expected.Length;

            for (int start = 0; start <= lastStart; start++)
            {
                bool matches = true;

                for (int index = 0; index < expected.Length; index++)
                {
                    if (buffer[start + index] != expected[index])
                    {
                        matches = false;
                        break;
                    }
                }

                if (!matches)
                    continue;

                buffer.RemoveRange(0, start + expected.Length);
                DetectionComplete = true;

                return true;
            }

            // Preserve a possible partial response for the next read.
            int keepCount = expected.Length - 1;

            if (buffer.Count > keepCount)
                buffer.RemoveRange(0, buffer.Count - keepCount);

            return false;
        }

        public bool TryReadPacket(out byte[] packet)
        {
            packet = null;

            if (!DetectionComplete)
                return false;

            int packetSize = Constants.RX_BUFFER_SIZE;
            int lastStart = buffer.Count - packetSize;

            for (int start = 0; start <= lastStart; start++)
            {
                byte[] candidate = buffer.GetRange(start, packetSize).ToArray();

                if (!Utils.Validate_Rx_Packet(candidate))
                    continue;

                buffer.RemoveRange(0, start + packetSize);
                packet = candidate;

                return true;
            }

            // All complete candidates failed validation.
            // Retain the tail that could begin an incomplete packet.
            int examinedCount = lastStart + 1;

            if (examinedCount > 0)
                buffer.RemoveRange(0, examinedCount);

            return false;
        }
    }
}