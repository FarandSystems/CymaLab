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

        public const int USB_BAUD_RATE = 38400;

        public const int DETECTION_RESPONSE_INDEX = 2;
        public const byte DETECTION_RESPONSE_VALUE = 0x55;

        public const int USB_ENCODING_FLAGS_INDEX = 0;
        public const int USB_FRAME_DATA_START_INDEX = 1;

        public const int USB_FRAME_DATA_BYTE_COUNT =
            FRAME_SIZE_BYTES - USB_FRAME_DATA_START_INDEX;

        public const byte USB_ESCAPED_BYTE = 0x1A;
        public const byte USB_DECODED_FLAGS_VALUE = 0x00;

        public const byte USB_FLAGS_LOW_MASK = 0x0F;
        public const byte USB_FLAGS_HIGH_MASK = 0xE0;
        public const int USB_FLAGS_HIGH_SHIFT = 1;


        public const string WIFI_SERVER_IP = "192.168.16.254";
        public const int WIFI_SERVER_PORT = 5000;

        public const int WIFI_READ_BUFFER_SIZE = 4096;
        public const int WIFI_DETECTION_TIMEOUT_MS = 2000;


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

        public static readonly double[] PULSE_WIDTHS_US =
        {
            1, 2, 5, 7, 10, 15, 20, 25
        };

        public const double PIEZO_FREQUENCY_KHZ = 55.0;



        public const int MIN_DOWNSAMPLE_RATIO = 1;
        public const int MAX_DOWNSAMPLE_RATIO = 10;

        public const double PRETRIGGER_COUNTS_PER_US = 2.0;

        public const double MAX_PRETRIGGER_US = ushort.MaxValue / PRETRIGGER_COUNTS_PER_US;


        public static readonly byte[] AMPLIFIER_GAIN_CHANNELS =
        {
            0, 1, 2, 3
        };


        public static readonly int[] AVERAGING_COUNTS =
        {
            1, 4, 8, 16, 32
        };

        public const int TOF_FILTER_STABLE_MODE = 1;

        public const double FILTER_FREQUENCY_SCALE = 100.0;

        public const double WEAK_FILTER_RATIO = 10.0;
        public const double STRONG_FILTER_RATIO = 4.0;

        public const int DEFAULT_DOWNSAMPLE_RATIO = 10;
        public const int DEFAULT_SAMPLE_START_INDEX = 0;

        public const double CENTIMETERS_PER_METER = 100.0;

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

        public const int COMMAND_FRAME_SIZE_BYTES = 8;

        public const int COMMAND_ID_INDEX = 0;
        public const int COMMAND_VALUE_INDEX = 1;

        public const int COMMAND_CHECKSUM_INDEX = COMMAND_FRAME_SIZE_BYTES - 1;

        public const int FILTER_LPF_ENABLED_INDEX = 1;
        public const int FILTER_LPF_FREQUENCY_INDEX = 2;

        public const int FILTER_HPF_ENABLED_INDEX = 4;
        public const int FILTER_HPF_FREQUENCY_INDEX = 5;

        public const int SAMPLING_DOWNSAMPLE_INDEX = 1;
        public const int SAMPLING_START_INDEX = 2;
        public const int SAMPLING_PRETRIGGER_INDEX = 4;
        public const int SAMPLING_DISCARD_INDEX = 6;
        public const int MAX_SAMPLE_START_INDEX = 3600;
        public const int AMPLIFIER_GAIN_CHANNEL_INDEX = 1;
        public const int AVERAGING_COUNT_INDEX = 1;

        public const int MEASUREMENT_MODE_INDEX = 1;
        public const int MEASUREMENT_LENGTH_INDEX = 2;
        public const int MEASUREMENT_VELOCITY_INDEX = 4;
        public const int MEASUREMENT_OPTIONS_INDEX = 6;


        public const byte MEASUREMENT_LENGTH_MODE = 0;
        public const byte MEASUREMENT_VELOCITY_MODE = 1;

        // Retained from the previous command implementation.
        public const byte MEASUREMENT_OPTIONS_VALUE = 0x07;

        public const int SIGNAL_PULSE_WIDTH_INDEX = 1;
        public const int SIGNAL_AMPLIFIER_GAIN_INDEX = 5;
        public const int SIGNAL_OPTIONS_INDEX = 6;

        public const byte SIGNAL_OPTIONS_VALUE = 0x07;

        public const int MAX_PENDING_COMMANDS = 32;
        // ==================================== //


        // ================ UI ================ //

        public const int HARDWARE_DISPLAY_INTERVAL_MS = 125;

        // ==================================== //
    }
}
