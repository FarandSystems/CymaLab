using System;
using System.Windows.Forms;

namespace Test_App
{
    public partial class Form1 : Form
    {
        private readonly Timer dataTimer;

        private const int SamplesPerPacket = 720;
        private const int PacketRateHz = 8;

        private const double SimulatedSampleRate =
            SamplesPerPacket * PacketRateHz;

        private int packetCounter = 0;

        private double phase = 0.0;
        private double packetPhaseOffset = 0.0;

        private readonly Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            farandChart.InitChartBehavior(
                                    renderFps: 8,
                                    interactionHz: 8,
                                    visiblePointCount: 720,
                                    maxStoredPointCount: 720,
                                    autoStart: true,
                                    drainAllQueuedBatches: true,
                                    fixedYMin: -1,
                                    fixedYMax: 1,
                                    enableAutoScaleY: false
                                );

            farandChart.XAxisViewChanged += FarandChart_XAxisViewChanged;

            dataTimer = new Timer();
            dataTimer.Interval = 1000 / PacketRateHz; // 125 ms
            dataTimer.Tick += DataTimer_Tick;

            dataTimer.Start();

            lblInfo.Text = "Running...";
        }

        private void FarandChart_XAxisViewChanged(object sender, Farand_Tablet_Chart.XAxisViewChangedEventArgs e)
        {
            lblInfo.Text =
                "X View Changed" +
                " | Reason: " + e.Reason +
                " | StartIndex: " + e.StartIndex +
                " | VisiblePoints: " + e.VisiblePointCount +
                " | ZoomRatio: " + e.ZoomRatio.ToString("0.00") +
                " | XMin: " + e.XMin.ToString("0.00") +
                " | XMax: " + e.XMax.ToString("0.00");
        }

        private void DataTimer_Tick(object sender, EventArgs e)
        {
            double[] packet = BuildSinePacket();

            farandChart.PushSamples(packet, copyInputBuffer: false);

            packetCounter++;

            //lblInfo.Text =
            //    "Packets: " + packetCounter +
            //    " | Packet rate: 8 Hz" +
            //    " | Samples per packet: 720" +
            //    " | Simulated Fs: " + SimulatedSampleRate.ToString("0") + " Hz";
        }

        private double[] BuildSinePacket()
        {
            double[] buffer = new double[SamplesPerPacket];

            double f_Hz = 2e3;

            double phaseStep = 5e-2;

            phase += phaseStep;

            for (int i = 0; i < buffer.Length; i++)
            {

                double t = 1e-6 * i;
                double noise = 0.03 * (random.NextDouble() - 0.5);

                buffer[i] = Math.Sin(2 * 3.14f * f_Hz * t + phase) + noise;
            }

            return buffer;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            dataTimer.Start();
            farandChart.StartChart();

            lblInfo.Text = "Running...";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            dataTimer.Stop();
            farandChart.StopChart();

            lblInfo.Text = "Stopped";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataTimer.Stop();

            packetCounter = 0;
            phase = 0.0;
            packetPhaseOffset = 0.0;

            farandChart.ClearChart();

            lblInfo.Text = "Cleared";

            dataTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dataTimer.Stop();
            dataTimer.Dispose();

            base.OnFormClosing(e);
        }

        private void farandChart_Load(object sender, EventArgs e)
        {

        }
    }
}