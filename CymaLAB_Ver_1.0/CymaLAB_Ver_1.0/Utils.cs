using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CymaLAB_Ver_1._0
{
    public static class Utils
    {
        public static bool Validate_Rx_Packet(byte[] rxPacket)
        {
            if (rxPacket == null || rxPacket.Length != Constants.RX_BUFFER_SIZE)
            {
                return false;
            }

            if (rxPacket[1] != Constants.HEADER_1 || rxPacket[2] != Constants.HEADER_2 || rxPacket[3] != Constants.HEADER_3)
            {
                return false;
            }

            uint receivedChecksum = (uint)rxPacket[Constants.PAYLOAD_SIZE] + ((uint)rxPacket[Constants.PAYLOAD_SIZE + 1] << 8) + ((uint)rxPacket[Constants.PAYLOAD_SIZE + 2] << 16);

            uint calculatedChecksum = Calculate_Checksum24(rxPacket);

            return receivedChecksum == calculatedChecksum;
        }

        public static uint Calculate_Checksum24(byte[] packet)
        {
            if (packet == null || packet.Length < Constants.PAYLOAD_SIZE)
            {
                return 0;
            }

            uint checksum = 0;

            for (int i = 0; i < Constants.PAYLOAD_SIZE; i++)
            {
                checksum += packet[i];
            }

            return checksum & 0x00FFFFFFU;
        }

        public static byte Calculate_Checksum8(byte[] packet, int length)
        {
            if (packet == null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            if (length < 1 || length > packet.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            byte checksum = 0;

            for (int i = 0; i < length - 1; i++)
            {
                unchecked
                {
                    checksum += packet[i];
                }
            }

            return checksum;
        }

        public static short GetInt16LE(byte[] buffer, int startIndex)
        {
            ushort value = (ushort)(buffer[startIndex] | (buffer[startIndex + 1] << 8));

            return unchecked((short)value);
        }

        public static UInt16 GetUInt16LE(byte[] buffer, int startIndex)
        {
            ushort value = (ushort)(buffer[startIndex] | (buffer[startIndex + 1] << 8));

            return value;
        }

        public static void SetInt16LE(byte[] buffer, int startIndex, int value)
        {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)(value >> 8);
        }

        public static void SetInt32LE(byte[] buffer, int startIndex, int value)
        {
            buffer[startIndex] = (byte)value;
            buffer[startIndex + 1] = (byte)(value >> 8);
            buffer[startIndex + 2] = (byte)(value >> 16);
            buffer[startIndex + 3] = (byte)(value >> 24);
        }
    }
}
