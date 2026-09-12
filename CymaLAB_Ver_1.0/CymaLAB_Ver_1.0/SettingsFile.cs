using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace CymaLAB_Ver_1._0
{
    public static class SettingsFile
    {
        public static void Save(string filePath, AppSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            DateTime now = DateTime.Now;

            settings.Validate();

            using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(false)))
            {
                writer.WriteLine("Date = " + now.ToString("M/d/yyyy", CultureInfo.InvariantCulture));

                writer.WriteLine("Time = " + now.ToString("h:mm tt", CultureInfo.InvariantCulture));

                writer.WriteLine("Sample Length(cm) = " + Format(settings.SampleLengthCm, "0.00"));

                writer.WriteLine("Sample Velocity(m/Sec) = " + Format(settings.SampleVelocityMs, "0.00"));

                writer.WriteLine("Reference TOF (uSec.) = " + Format(settings.ReferenceTofUs));

                writer.WriteLine("Discard Time (uSec.) = " + Format(settings.DiscardTimeUs));

                writer.WriteLine("PreTrigger Time (uSec.) = " + Format(settings.PreTriggerTimeUs));

                writer.WriteLine("TOF Offset (uSec.) = " + Format(settings.TofOffsetUs));

                writer.WriteLine("Pulse Width (uSec.) = " + Format(settings.PulseWidthUs));

                writer.WriteLine("Power Filtering Active = " + settings.PowerFilteringActive);

                writer.WriteLine("Transducer Type = " + settings.TransducerType);

                writer.WriteLine("Filter Mode = " + settings.FilterMode);

                writer.WriteLine("Measurement Mode = " + settings.MeasurementMode);

                writer.WriteLine("Amplifier Gain = " + settings.AmplifierGain);


                writer.WriteLine("Transducer Power Level = " + settings.TransducerPowerLevel.ToString(CultureInfo.InvariantCulture));

                writer.WriteLine("Capture Averaging Count = " + settings.CaptureAveragingCount.ToString(CultureInfo.InvariantCulture));
            }
        }

        public static AppSettings Load(string filePath)
        {
            AppSettings settings = new AppSettings();

            // First run: no saved file yet, so use the defaults.
            if (!File.Exists(filePath))
                return settings;

            int lineNumber = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                int separatorIndex = line.IndexOf('=');

                if (separatorIndex < 0)
                    throw new FormatException("Missing '=' at settings line " + lineNumber);

                string name = line.Substring(0, separatorIndex).Trim();
                string value = line.Substring(separatorIndex + 1).Trim();

                try
                {
                    switch (name)
                    {
                        case "Date":
                        case "Time":
                            // Save timestamps are not operating settings.
                            break;

                        case "Sample Length(cm)":
                            settings.SampleLengthCm = ParseNumber(value);
                            break;

                        case "Sample Velocity(m/Sec)":
                            settings.SampleVelocityMs = ParseNumber(value);
                            break;

                        case "Reference TOF (uSec.)":
                            settings.ReferenceTofUs = ParseNumber(value);
                            break;

                        case "Discard Time (uSec.)":
                            settings.DiscardTimeUs = ParseNumber(value);
                            break;

                        case "PreTrigger Time (uSec.)":
                            settings.PreTriggerTimeUs = ParseNumber(value);
                            break;

                        case "TOF Offset (uSec.)":
                            settings.TofOffsetUs = ParseNumber(value);
                            break;

                        case "Pulse Width (uSec.)":
                            settings.PulseWidthUs = ParseNumber(value);
                            break;

                        case "Power Filtering Active":
                            settings.PowerFilteringActive = bool.Parse(value);
                            break;

                        case "Transducer Type":
                            settings.TransducerType = value;
                            break;

                        case "Filter Mode":
                            settings.FilterMode = value;
                            break;

                        case "Measurement Mode":
                            settings.MeasurementMode = value;
                            break;

                        case "Amplifier Gain":
                            settings.AmplifierGain = value;
                            break;

                        case "Tranducer Power Level": // Misspell at previous version so for parsing the previous versions this needed!
                        case "Transducer Power Level":
                            settings.TransducerPowerLevel =
                                int.Parse(value, CultureInfo.InvariantCulture);
                            break;

                        case "Capture Averaging Count":
                            settings.CaptureAveragingCount =
                                int.Parse(value, CultureInfo.InvariantCulture);
                            break;

                        default:
                            // Allow extra settings from another application version.
                            break;
                    }
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException)
                {
                    throw new FormatException("Invalid value for '" + name +"' at settings line " + lineNumber + ": " + value, ex);
                }
            }

            settings.Validate();

            return settings;
        }

        private static double ParseNumber(string value)
        {
            double number = double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);

            if (double.IsNaN(number) || double.IsInfinity(number))
                throw new FormatException("A finite number is required.");

            return number;
        }

        private static string Format(double value, string format = "G")
        {
            return value.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}