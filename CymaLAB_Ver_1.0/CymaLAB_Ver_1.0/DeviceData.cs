public sealed class DeviceData
{
    public int[] CapturedSignal { get; set; }

    public double TimeOfFlightUs { get; set; }
    public double FilteredTimeOfFlightUs { get; set; }
    public double AutoTimeOfFlightUs { get; set; }

    public int FilterTofMode { get; set; }

    public ushort PiezoFrequency { get; set; }
    public double PiezoPhase { get; set; }

    public short SignalPeakTimeUs { get; set; }
    public short IdealPeakTimeUs { get; set; }

    public short DiagnosticRawTofUs { get; set; }
    public short DiagnosticFilteredTofUs { get; set; }

    public short Peak5AdvanceUs { get; set; }
}