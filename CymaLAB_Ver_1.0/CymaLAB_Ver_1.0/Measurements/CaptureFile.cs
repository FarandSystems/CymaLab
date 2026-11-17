using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Farand_Tablet_Chart;

namespace CymaLAB_Ver_1._0
{
    internal sealed class SavedCapture
    {
        public CaptureSnapshot Chart { get; set; }
        public DateTimeOffset SavedAt { get; set; }
        public string Source { get; set; }
        public int DownSampleRatio { get; set; }
    }

    internal static class CaptureFile
    {
        private const string Signature = "# CymaLab capture v1";
        private const string ColumnHeader = "Time_us\tAmplitude";
        private const int MaximumSamples = 1000000;
        private const int MaximumFileSizeBytes = 64 * 1024 * 1024;

        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        public static void Save(string path, SavedCapture capture)
        {
            Validate(capture);

            using (var writer = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                var chart = capture.Chart;

                // Store capture metadata and view limits before the sample rows.
                writer.WriteLine(Signature);

                writer.WriteLine("# SavedAt: " + capture.SavedAt.ToString("O", Invariant));
                writer.WriteLine("# Source: " + capture.Source);

                writer.WriteLine("# DownSampleRatio: " + capture.DownSampleRatio.ToString(Invariant));

                WriteNumber(writer, "StartTimeUs", chart.StartTimeUs);
                WriteNumber(writer, "SampleIntervalUs", chart.SampleIntervalUs);

                WriteNumber(writer, "XMinimumUs", chart.XMinimum);
                WriteNumber(writer, "XMaximumUs", chart.XMaximum);

                WriteNumber(writer, "YMinimum", chart.YMinimum);
                WriteNumber(writer, "YMaximum", chart.YMaximum);

                writer.WriteLine("# SampleCount: " + chart.Samples.Length.ToString(Invariant));

                writer.WriteLine(ColumnHeader);

                for (int i = 0; i < chart.Samples.Length; i++)
                {
                    double time = chart.StartTimeUs + i * chart.SampleIntervalUs;
                    writer.WriteLine(time.ToString("R", Invariant) + "\t" + chart.Samples[i].ToString("R", Invariant));
                }
            }
        }

        public static SavedCapture Load(string path)
        {
            if (new FileInfo(path).Length > MaximumFileSizeBytes)
                throw new FormatException("The capture file is too large.");

            using (var reader = new StreamReader(path, Encoding.UTF8, true))
            {
                if (reader.ReadLine() != Signature)
                    throw new FormatException("This is not a supported CymaLab capture file.");

                var headers = ReadHeaders(reader);
                var capture = ReadMetadata(headers);

                // Check metadata first, then validate again with the loaded amplitudes.
                Validate(capture);
                ReadSamples(reader, capture.Chart);
                Validate(capture);

                return capture;
            }
        }

        private static Dictionary<string, string> ReadHeaders(TextReader reader)
        {
            var headers = new Dictionary<string, string>(StringComparer.Ordinal);
            string line;

            while ((line = reader.ReadLine()) != null && line.StartsWith("# ", StringComparison.Ordinal))
            {
                int separator = line.IndexOf(':');

                if (separator < 3)
                    throw new FormatException("Invalid or duplicate capture header.");

                string name = line.Substring(2, separator - 2);

                if (headers.ContainsKey(name))
                    throw new FormatException("Invalid or duplicate capture header.");

                headers.Add(name, line.Substring(separator + 1).Trim());
            }

            if (line != ColumnHeader)
                throw new FormatException("Expected Time_us and Amplitude columns.");

            return headers;
        }

        private static SavedCapture ReadMetadata(Dictionary<string, string> headers)
        {
            int count = ParseInteger(headers, "SampleCount");

            if (count < 1 || count > MaximumSamples)
                throw new FormatException("Invalid sample count.");

            DateTimeOffset savedAt;

            string timestamp = Required(headers, "SavedAt");

            if (!DateTimeOffset.TryParseExact(timestamp, "O", Invariant, DateTimeStyles.None, out savedAt))
                throw new FormatException("Invalid saved timestamp.");

            return new SavedCapture
            {
                SavedAt = savedAt,
                Source = Required(headers, "Source"),
                DownSampleRatio = ParseInteger(headers, "DownSampleRatio"),
                Chart = new CaptureSnapshot
                {
                    Samples = new double[count],
                    StartTimeUs = ParseNumber(Required(headers, "StartTimeUs")),
                    SampleIntervalUs = ParseNumber(Required(headers, "SampleIntervalUs")),
                    XMinimum = ParseNumber(Required(headers, "XMinimumUs")),
                    XMaximum = ParseNumber(Required(headers, "XMaximumUs")),
                    YMinimum = ParseNumber(Required(headers, "YMinimum")),
                    YMaximum = ParseNumber(Required(headers, "YMaximum"))
                }
            };
        }

        private static void ReadSamples(TextReader reader, CaptureSnapshot chart)
        {
            for (int i = 0; i < chart.Samples.Length; i++)
            {
                string row = reader.ReadLine();

                string[] columns = row == null ? new string[0] : row.Split('\t');

                if (columns.Length != 2)
                    throw new FormatException("Missing or invalid sample row " + (i + 1).ToString(Invariant) + ".");

                double time = ParseNumber(columns[0]);

                // Verify uniform sampling, allowing for floating-point rounding.
                double expectedTime = chart.StartTimeUs + i * chart.SampleIntervalUs;
                double tolerance = Math.Max(1E-9, Math.Abs(expectedTime) * 1E-12);

                if (Math.Abs(time - expectedTime) > tolerance)
                    throw new FormatException("Sample times do not match the capture interval.");

                chart.Samples[i] = ParseNumber(columns[1]);
            }

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    throw new FormatException("Unexpected data after the final sample.");
            }
        }

        private static void Validate(SavedCapture capture)
        {
            if (capture == null || capture.Chart == null || capture.Chart.Samples == null)
                throw new FormatException("Missing capture data.");

            var chart = capture.Chart;
            double endTime = chart.StartTimeUs + chart.Samples.Length * chart.SampleIntervalUs;

            bool invalidSource = string.IsNullOrWhiteSpace(capture.Source)
                || capture.Source.IndexOfAny(new[] { '\r', '\n' }) >= 0;


            bool invalidSampling = chart.Samples.Length < 1 || chart.Samples.Length > MaximumSamples
                || capture.DownSampleRatio < 1 || !IsFinite(chart.StartTimeUs) || chart.StartTimeUs < 0
                || !IsFinite(chart.SampleIntervalUs) || chart.SampleIntervalUs <= 0 || !IsFinite(endTime);


            bool invalidView = !IsFinite(chart.XMinimum) || chart.XMinimum < 0
                || !IsFinite(chart.XMaximum) || chart.XMaximum <= chart.XMinimum
                || !IsFinite(chart.YMinimum) || !IsFinite(chart.YMaximum) || chart.YMaximum <= chart.YMinimum;

            if (invalidSource || invalidSampling || invalidView)
                throw new FormatException("Invalid capture metadata.");

            double lower = chart.YMinimum;
            double upper = chart.YMaximum;

            foreach (double sample in chart.Samples)
            {
                if (!IsFinite(sample))
                    throw new FormatException("Capture contains an invalid amplitude.");

                lower = Math.Min(lower, sample);
                upper = Math.Max(upper, sample);
            }

            // Ensure the chart can calculate its padded range without overflow.
            double margin = (upper - lower) * 0.1;

            if (!IsFinite(margin) || !IsFinite(lower - margin) || !IsFinite(upper + margin) || endTime <= chart.StartTimeUs)
                throw new FormatException("Capture ranges exceed the supported numeric precision.");
        }


        private static bool IsFinite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);


        private static void WriteNumber(TextWriter writer, string name, double value)
        {
            writer.WriteLine("# " + name + ": " + value.ToString("R", Invariant));
        }

        private static string Required(Dictionary<string, string> headers, string name)
        {
            string value;

            if (!headers.TryGetValue(name, out value))
                throw new FormatException("Missing header: " + name);

            return value;
        }

        private static double ParseNumber(string text)
        {
            double value;

            if (!double.TryParse(text, NumberStyles.Float, Invariant, out value) || !IsFinite(value))
                throw new FormatException("Invalid numeric value in capture file.");

            return value;
        }

        private static int ParseInteger(Dictionary<string, string> headers, string name)
        {
            int value;

            if (!int.TryParse(Required(headers, name), NumberStyles.None, Invariant, out value))
                throw new FormatException("Invalid integer header: " + name);

            return value;
        }
    }
}
