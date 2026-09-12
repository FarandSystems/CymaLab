namespace CymaLAB_Ver_1._0
{
    public static class PacketParser
    {
        public static DeviceData Parse(byte[] packet)
        {
            if (!Utils.Validate_Rx_Packet(packet))
                return null;

            DeviceData data = new DeviceData();

            data.CapturedSignal = new int[Constants.CAPTURE_SAMPLE_COUNT];

            int sampleIndex = 0;


            // Signal
            for (int frame = 0; frame < Constants.SAMPLE_FRAME_COUNT; frame++)
            {
                int frameStart = Constants.SAMPLE_DATA_START + frame * Constants.FRAME_SIZE_BYTES;

                for (int sample = 0; sample < Constants.SAMPLES_PER_FRAME; sample++)
                {
                    int offset = frameStart + Constants.SAMPLE_DATA_OFFSET + sample * Constants.SAMPLE_SIZE_BYTES;

                    data.CapturedSignal[sampleIndex++] = Utils.GetInt16LE(packet, offset);
                }
            }

            // TOF
            data.TimeOfFlightUs = ReadTofUs(packet, Constants.RAW_TOF_OFFSET_INDEX);
            data.FilteredTimeOfFlightUs = ReadTofUs(packet, Constants.FILTERED_TOF_OFFSET_INDEX);
            data.AutoTimeOfFlightUs = ReadTofUs(packet, Constants.AUTO_TOF_OFFSET_INDEX);
            data.FilterTofMode = packet[Constants.TOF_FILTER_MODE_OFFSET_INDEX];

            // Piezo Freq and phase
            data.PiezoFrequency = Utils.GetUInt16LE(packet, Constants.PIEZO_FREQUENCY_OFFSET_INDEX);
            data.PiezoPhase = Utils.GetInt16LE(packet, Constants.PIEZO_PHASE_OFFSET_INDEX) * Constants.PIEZO_PHASE_SCALE;

            data.SignalPeakTimeUs = Utils.GetInt16LE(packet, Constants.SIGNAL_PEAK_TIME_OFFSET_INDEX);

            data.IdealPeakTimeUs = Utils.GetInt16LE(packet, Constants.IDEAL_PEAK_TIME_OFFSET_INDEX);

            data.DiagnosticRawTofUs = Utils.GetInt16LE(packet, Constants.DIAGNOSTIC_RAW_TOF_OFFSET_INDEX);

            data.DiagnosticFilteredTofUs = Utils.GetInt16LE(packet, Constants.DIAGNOSTIC_FILTERED_TOF_OFFSET_INDEX);

            data.Peak5AdvanceUs = Utils.GetInt16LE(packet, Constants.PEAK_5_ADVANCE_OFFSET_INDEX);

            return data;
        }



        private static double ReadTofUs(byte[] packet, int offset)
        {
            short sampleCount = Utils.GetInt16LE(packet, offset);

            return sampleCount * Constants.MICROSECONDS_PER_SECOND / Constants.ADC_SAMPLE_RATE_HZ - Constants.TOF_PREOFFSET_US;
        }
    }
}