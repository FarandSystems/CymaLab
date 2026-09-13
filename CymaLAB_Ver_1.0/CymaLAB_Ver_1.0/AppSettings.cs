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

        public Filter_Control.Filter_Control.Filter_Mode_Enum FilterMode
        {
            get;
            set;
        } = Filter_Control.Filter_Control.Filter_Mode_Enum.No_Filter;

        public string TransducerType { get; set; } = "P";
        public double PiezoFrequencyKhz { get; set; } = 55.0;

        public string MeasurementMode { get; set; } = "Velocity";
        public int AmplifierGain { get; set; } = 1;

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

            if (TransducerType != "P")
                throw new System.ArgumentException("Transducer type must be P.");

            if (PiezoFrequencyKhz != Constants.PIEZO_FREQUENCY_KHZ)
                throw new System.ArgumentException($"The available transducer frequency is {Constants.PIEZO_FREQUENCY_KHZ} kHz.");

            if (!System.Enum.IsDefined(typeof(Filter_Control.Filter_Control.Filter_Mode_Enum), FilterMode))
            {
                throw new System.ArgumentException("Invalid filter mode.");
            }

            if (MeasurementMode != "Velocity" && MeasurementMode != "Length")
                throw new System.ArgumentException("Measurement mode must be Velocity or Length.");

            if (AmplifierGain < 1 || AmplifierGain > 4)
                throw new System.ArgumentException("Amplifier gain level must be between 1 and 4.");

        }
    }
}