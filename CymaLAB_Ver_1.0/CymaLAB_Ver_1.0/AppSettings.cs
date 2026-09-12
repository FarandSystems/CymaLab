namespace CymaLAB_Ver_1._0
{
    public sealed class AppSettings
    {
        public double SampleLengthCm { get; set; } = 30.0;
        public double SampleVelocityMs { get; set; } = 3000.0;

        public double ReferenceTofUs { get; set; } = 62.5;
        public double DiscardTimeUs { get; set; } = 50.0;
        public double PreTriggerTimeUs { get; set; } = 50.0;
        public double TofOffsetUs { get; set; } = 0.0;

        public double PulseWidthUs { get; set; } = 10.0;

        public bool PowerFilteringActive { get; set; } = false;

        public string TransducerType { get; set; } = "P";
        public string FilterMode { get; set; } = "OFF";
        public string MeasurementMode { get; set; } = "Velocity";
        public string AmplifierGain { get; set; } = "Low";

        public int TransducerPowerLevel { get; set; } = 5;
        public int CaptureAveragingCount { get; set; } = 16;


        public void Validate()
        {
            if (SampleLengthCm <= 0 || SampleLengthCm > short.MaxValue)
                throw new System.ArgumentException("Sample length must be greater than 0 and at most 32767 cm.");

            if (SampleVelocityMs <= 0 || SampleVelocityMs > short.MaxValue)
                throw new System.ArgumentException("Sample velocity must be greater than 0 and at most 32767 m/s.");

            if (DiscardTimeUs < 0 || DiscardTimeUs > byte.MaxValue)
                throw new System.ArgumentException("Discard time must be between 0 and 255 us.");

            if (PreTriggerTimeUs < 0 || PreTriggerTimeUs > 32767.5)
                throw new System.ArgumentException("Pretrigger time must be between 0 and 32767.5 us.");

            if (TransducerPowerLevel < 1 || TransducerPowerLevel > 8)
                throw new System.ArgumentException("Transducer power level must be between 1 and 8.");

            if (CaptureAveragingCount != 1 && CaptureAveragingCount != 4 && CaptureAveragingCount != 8 && CaptureAveragingCount != 16 && CaptureAveragingCount != 32)
                throw new System.ArgumentException("Capture averaging count must be 1, 4, 8, 16 or 32.");

            if (TransducerType != "P" && TransducerType != "S")
                throw new System.ArgumentException("Transducer type must be P or S.");

            if (FilterMode != "ON" && FilterMode != "OFF")
                throw new System.ArgumentException("Filter mode must be ON or OFF.");

            if (MeasurementMode != "Velocity" && MeasurementMode != "Length")
                throw new System.ArgumentException("Measurement mode must be Velocity or Length.");

            if (AmplifierGain != "Low" && AmplifierGain != "High")
                throw new System.ArgumentException("Amplifier gain must be Low or High.");
        }
    }
}