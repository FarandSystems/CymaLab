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

        // ============= Processing ============= //

        public const double ADC_SAMPLE_RATE_HZ = 2400000.0;
        public const double MICROSECONDS_PER_SECOND = 1000000.0;
        public const double TOF_PREOFFSET_US = 100.0;

        public const double PIEZO_PHASE_SCALE = 0.01;



        // ====================================== //


        // ============= Rx Packet ============= //

        public const int SAMPLE_DATA_START = 8;
        public const int FRAME_SIZE_BYTES = 8;
        public const int SAMPLE_FRAME_COUNT = 240;

        public const int SAMPLES_PER_FRAME = 3;
        public const int SAMPLE_SIZE_BYTES = 2;
        public const int SAMPLE_DATA_OFFSET = 1;

        public const int CAPTURE_SAMPLE_COUNT = SAMPLE_FRAME_COUNT * SAMPLES_PER_FRAME;

        public const int RAW_TOF_OFFSET_INDEX = 1929;
        public const int FILTERED_TOF_OFFSET_INDEX = 1931;
        public const int AUTO_TOF_OFFSET_INDEX = 1933;
        public const int TOF_FILTER_MODE_OFFSET_INDEX = 1935;
        public const int PIEZO_FREQUENCY_OFFSET_INDEX = 1937;
        public const int PIEZO_PHASE_OFFSET_INDEX = 1939;
        public const int SIGNAL_PEAK_TIME_OFFSET_INDEX = 1941;
        public const int IDEAL_PEAK_TIME_OFFSET_INDEX = 1945;
        public const int DIAGNOSTIC_RAW_TOF_OFFSET_INDEX = 1947;
        public const int DIAGNOSTIC_FILTERED_TOF_OFFSET_INDEX = 1949;
        public const int PEAK_5_ADVANCE_OFFSET_INDEX = 1953;

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
