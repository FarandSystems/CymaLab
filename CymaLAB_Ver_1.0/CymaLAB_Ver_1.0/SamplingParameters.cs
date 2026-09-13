using System;

namespace CymaLAB_Ver_1._0
{
    public sealed class SamplingParameters
    {
        public int DownSampleRatio { get; private set; } = Constants.DEFAULT_DOWNSAMPLE_RATIO;

        public int StartIndex { get; private set; } = Constants.DEFAULT_SAMPLE_START_INDEX;

        public double SampleIntervalUs
        {
            get
            {
                return DownSampleRatio * Constants.MICROSECONDS_PER_SECOND / Constants.ADC_SAMPLE_RATE_HZ;
            }
        }

        public double StartTimeUs
        {
            get
            {
                return StartIndex * Constants.MICROSECONDS_PER_SECOND / Constants.ADC_SAMPLE_RATE_HZ;
            }
        }

        public bool SetTimeWindow(double minimumUs, double maximumUs)
        {
            if (double.IsNaN(minimumUs) || double.IsInfinity(minimumUs) || double.IsNaN(maximumUs) || double.IsInfinity(maximumUs) || minimumUs < 0 || maximumUs <= minimumUs)
            {
                throw new ArgumentException("The time window must have a nonnegative start " + "and an end greater than its start.");
            }

            double samplesPerUs = Constants.ADC_SAMPLE_RATE_HZ / Constants.MICROSECONDS_PER_SECOND;

            double requestedStart = minimumUs * samplesPerUs;

            int newStartIndex = (int)Math.Min(requestedStart, Constants.MAX_SAMPLE_START_INDEX);

            double requestedRatio = (maximumUs - minimumUs) * samplesPerUs / Constants.CAPTURE_SAMPLE_COUNT;

            // Same nearest-integer rule as the previous application.
            int newRatio = (int)Math.Max(Constants.MIN_DOWNSAMPLE_RATIO, Math.Min(Constants.MAX_DOWNSAMPLE_RATIO, Math.Floor(requestedRatio + 0.5)));

            bool changed = newStartIndex != StartIndex || newRatio != DownSampleRatio;

            StartIndex = newStartIndex;
            DownSampleRatio = newRatio;

            return changed;
        }
    }
}