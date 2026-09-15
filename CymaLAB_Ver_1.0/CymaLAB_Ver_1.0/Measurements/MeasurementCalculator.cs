using System;

namespace CymaLAB_Ver_1._0
{
    public static class MeasurementCalculator
    {
        public static double CalculateVelocity(double lengthCm, double tofUs)
        {
            ValidatePositive(lengthCm, nameof(lengthCm));
            ValidatePositive(tofUs, nameof(tofUs));

            double lengthM = lengthCm / Constants.CENTIMETERS_PER_METER;

            double timeSeconds = tofUs / Constants.MICROSECONDS_PER_SECOND;

            return lengthM / timeSeconds;
        }

        public static double CalculateLength(double velocityMs, double tofUs)
        {
            ValidatePositive(velocityMs, nameof(velocityMs));
            ValidatePositive(tofUs, nameof(tofUs));

            double timeSeconds = tofUs / Constants.MICROSECONDS_PER_SECOND;

            return velocityMs * timeSeconds * Constants.CENTIMETERS_PER_METER;
        }

        private static void ValidatePositive(double value, string parameterName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
    }
}