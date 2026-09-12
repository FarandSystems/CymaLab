using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CymaLAB_Ver_1._0
{
    public static class Constants
    {
        // ============= Communication ============= //
        public const int RX_BUFFER_SIZE = 2048;
        public const int PAYLOAD_SIZE = 2045;

        public const byte HEADER_1 = 0xC3;
        public const byte HEADER_2 = 0x3C;
        public const byte HEADER_3 = 0xCC;

        public const int WIFI_CONNECT_TIMEOUT_MS = 4000;
        public const int WIFI_PACKET_TIMEOUT_MS = 5000;


        public static readonly byte[] DETECTION_PC_COMMAND =
        {
            0x00,
            0xAA,
            0x55,
            0x00,
            0x00,
            0x00,
            0x00,
            0xFF
        };

        public static readonly byte[] DETECTION_DEVICE_RESPONSE =
        {
            0x00,
            0xAA,
            0x55,
            0x55,
            0x00,
            0x00,
            0x00,
            0x54
        };
        // ==================================== //


        // ============= Commands ============= //
        public static readonly byte[] HEARTBEAT_COMMAND =
        {
            0xAA,
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0xAA
        };

        public const int MAX_PENDING_COMMANDS = 32;
        // ==================================== //
    }
}
