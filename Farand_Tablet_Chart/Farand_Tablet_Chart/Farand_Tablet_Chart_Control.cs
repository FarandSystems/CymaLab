using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Farand_Tablet_Chart
{


    public partial class Farand_Tablet_Chart_Control : UserControl
    {
        private const string ChartAreaName = "MainArea";
        private const string SeriesName = "Signal";

        private readonly ConcurrentQueue<double[]> sampleQueue = new ConcurrentQueue<double[]>();
        private readonly List<double> sampleBuffer = new List<double>(4096);

        private System.Windows.Forms.Timer renderTimer;
        private System.Windows.Forms.Timer interactionTimer;

        private int visiblePoints = 720;
        private int maxStoredSamples = 50000;
        private int firstSampleIndex = 0;

        private bool drainAllQueuedBatchesPerTick = true;

        private bool autoFollowX = true;

        // Keep Y fixed for normalized data
        private bool autoScaleY = false;

        private double xMin = 0;
        private double xMax = 720;


        // Normalized signal useful range
        private double normalizedYMin = -1.0;
        private double normalizedYMax = 1.0;

        // Full visible Y range with margin.
        // User cannot pan or zoom-out beyond this range.
        private double defaultYMin = -1.2;
        private double defaultYMax = 1.2;

        private double yMin = -1.2;
        private double yMax = 1.2;

        // Maximum zoom-in limit.
        // Smaller value means user can zoom in more.
        private double minYVisibleSpan = 0.05;

        private const int XAxisPanHeightPx = 55;
        private const int YAxisPanWidthPx = 70;

        private PanMode activePanMode = PanMode.None;
        private Point panStartPoint;
        private Point panCurrentPoint;
        private bool panDirty = false;

        private double panStartXMin;
        private double panStartXMax;
        private double panStartYMin;
        private double panStartYMax;

        private ZoomButtonMode heldZoomButton = ZoomButtonMode.None;
        private ZoomButtonMode pendingSingleZoom = ZoomButtonMode.None;
        private bool zoomWasAppliedDuringHold = false;

        public event EventHandler<XAxisViewChangedEventArgs> XAxisViewChanged;

        public double XAxisMinimum
        {
            get { return xMin; }
        }

        public double XAxisMaximum
        {
            get { return xMax; }
        }

        public int XAxisStartIndex
        {
            get
            {
                int availableCount = GetAvailableXPointCount();
                int visibleCount = XAxisVisiblePointCount;

                int maxStartIndex = availableCount - visibleCount;

                int start = (int)Math.Floor(xMin - firstSampleIndex);

                return ClampInt(start, 0, maxStartIndex);
            }
        }

        public int XAxisVisiblePointCount
        {
            get
            {
                int availableCount = GetAvailableXPointCount();

                double span = xMax - xMin;

                if (span <= 0.0 || double.IsNaN(span) || double.IsInfinity(span))
                    return 1;

                int count = (int)Math.Round(span + 1.0);

                return ClampInt(count, 1, availableCount);
            }
        }

        public double XAxisZoomRatio
        {
            get
            {
                int availableCount = GetAvailableXPointCount();
                int visibleCount = XAxisVisiblePointCount;

                if (visibleCount <= 0)
                    return 1.0;

                double ratio = (double)availableCount / visibleCount;

                if (ratio < 1.0)
                    ratio = 1.0;

                return ratio;
            }
        }

        private XAxisViewChangedEventArgs BuildXAxisViewChangedEventArgs(XAxisViewChangeReason reason)
        {
            int availableCount = GetAvailableXPointCount();

            int visibleCount = XAxisVisiblePointCount;

            int maxStartIndex = availableCount - visibleCount;

            int startIndex = (int)Math.Floor(xMin - firstSampleIndex);
            startIndex = ClampInt(startIndex, 0, maxStartIndex);

            double zoomRatio = 1.0;

            if (visibleCount > 0)
                zoomRatio = (double)availableCount / visibleCount;

            if (zoomRatio < 1.0)
                zoomRatio = 1.0;

            return new XAxisViewChangedEventArgs(
                startIndex,
                visibleCount,
                zoomRatio,
                xMin,
                xMax,
                reason
            );
        }

        public Farand_Tablet_Chart_Control()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            CreateTimers();
            ConfigureChart();
            WireEvents();

            InitChartBehavior(
                renderFps: 24,
                interactionHz: 8,
                visiblePointCount: 720,
                maxStoredPointCount: 50000,
                autoStart: true
            );
        }

        // Call this from parent form after InitializeComponent() if you want different behavior.
        public void InitChartBehavior(
            int renderFps = 24,
            int interactionHz = 8,
            int visiblePointCount = 720,
            int maxStoredPointCount = 50000,
            bool autoStart = true,
            bool drainAllQueuedBatches = true,
            double fixedYMin = -1.2,
            double fixedYMax = 1.2,
            bool enableAutoScaleY = false)
        {
            if (renderFps < 1) renderFps = 1;
            if (interactionHz < 1) interactionHz = 1;
            if (visiblePointCount < 10) visiblePointCount = 10;
            if (maxStoredPointCount < visiblePointCount) maxStoredPointCount = visiblePointCount;

            visiblePoints = visiblePointCount;
            maxStoredSamples = maxStoredPointCount;
            drainAllQueuedBatchesPerTick = drainAllQueuedBatches;

            if (fixedYMax <= fixedYMin)
            {
                fixedYMin = -1.2;
                fixedYMax = 1.2;
            }

            defaultYMin = fixedYMin;
            defaultYMax = fixedYMax;

            SetFixedYRange( fixedYMin, fixedYMax );

            yMin = defaultYMin;
            yMax = defaultYMax;

            // For normalized data, keep this false.
            autoScaleY = enableAutoScaleY;

            renderTimer.Interval = Math.Max(1, 1000 / renderFps);
            interactionTimer.Interval = Math.Max(1, 1000 / interactionHz);

            if (autoStart)
            {
                StartChart();
            }
            else
            {
                StopChart();
            }
        }

        public void StartChart()
        {
            renderTimer.Start();
            interactionTimer.Start();
        }

        public void StopChart()
        {
            renderTimer.Stop();
            interactionTimer.Stop();
        }

        public void ClearChart()
        {
            while (sampleQueue.TryDequeue(out _)) { }

            sampleBuffer.Clear();
            firstSampleIndex = 0;

            xMin = 0;
            xMax = visiblePoints;
            yMin = defaultYMin;
            yMax = defaultYMax;

            autoFollowX = true;
            autoScaleY = false;

            UpdateChartPoints();
        }

        public void EnableAutoFollow()
        {
            autoFollowX = true;

            // Do not enable Y autoscale for normalized data.
            autoScaleY = false;
        }

        private void NotifyXAxisViewChanged(XAxisViewChangeReason reason)
        {
            EventHandler<XAxisViewChangedEventArgs> handler = XAxisViewChanged;

            if (handler == null)
                return;

            handler(this, BuildXAxisViewChangedEventArgs(reason));
        }

        // Main function to feed your chart.
        // You can call this from another thread.
        public void PushSamples(double[] values, bool copyInputBuffer = true)
        {
            if (values == null || values.Length == 0)
                return;

            if (copyInputBuffer)
            {
                double[] copy = new double[values.Length];
                Array.Copy(values, copy, values.Length);
                sampleQueue.Enqueue(copy);
            }
            else
            {
                sampleQueue.Enqueue(values);
            }
        }

        public void PushSample(double value)
        {
            sampleQueue.Enqueue(new double[] { value });
        }

        private void CreateTimers()
        {
            renderTimer = new System.Windows.Forms.Timer();
            interactionTimer = new System.Windows.Forms.Timer();

            renderTimer.Tick += RenderTimer_Tick;
            interactionTimer.Tick += InteractionTimer_Tick;
        }

        private void ConfigureChart()
        {
            chartMain.Series.Clear();
            chartMain.ChartAreas.Clear();
            chartMain.Legends.Clear();

            chartMain.AntiAliasing = AntiAliasingStyles.None;
            chartMain.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;

            ChartArea area = new ChartArea(ChartAreaName);
            Color grid_Color = Color.FromArgb(100,100,100);

            area.BorderDashStyle = ChartDashStyle.Solid;
            area.BorderColor = grid_Color;
            area.BorderWidth = 2;

            area.AxisX.MajorGrid.LineColor = grid_Color;
            area.AxisY.MajorGrid.LineColor = grid_Color;

            area.BackColor = Color.FromArgb(70, 70, 70);

            area.AxisX.LabelStyle.ForeColor = grid_Color;
            area.AxisY.LabelStyle.ForeColor = grid_Color;

            area.AxisX.MajorGrid.LineColor = grid_Color;
            area.AxisX.MinorGrid.LineColor = grid_Color;

            area.AxisY.MajorGrid.LineColor = grid_Color;
            area.AxisY.MinorGrid.LineColor = grid_Color;


            area.AxisX.LineColor = grid_Color;
            area.AxisY.LineColor = grid_Color;

            area.AxisX.MajorTickMark.LineColor = grid_Color;
            area.AxisY.MajorTickMark.LineColor = grid_Color;

            area.AxisX.LabelStyle.Format = "0";
            area.AxisY.LabelStyle.Format = "0.###";

            area.AxisX.LabelStyle.Font = new Font("Arial", 15, FontStyle.Bold);
            area.AxisY.LabelStyle.Font = new Font("Arial", 15, FontStyle.Bold);

            area.AxisX.Minimum = xMin;
            area.AxisX.Maximum = xMax;



            area.AxisX.IntervalType = DateTimeIntervalType.Number;
            area.AxisX.IntervalOffsetType = DateTimeIntervalType.Number;


            area.AxisY.Minimum = yMin;
            area.AxisY.Maximum = yMax;

            area.CursorX.IsUserEnabled = false;
            area.CursorY.IsUserEnabled = false;          


            chartMain.ChartAreas.Add(area);

            Series series = new Series(SeriesName);
            series.ChartArea = ChartAreaName;
            series.ChartType = SeriesChartType.FastLine;
            series.BorderWidth = 2;
            series.IsXValueIndexed = false;

            chartMain.Series.Add(series);
        }

        private void WireEvents()
        {
            chartMain.MouseDown += ChartMain_MouseDown;
            chartMain.MouseMove += ChartMain_MouseMove;
            chartMain.MouseUp += ChartMain_MouseUp;
            chartMain.MouseLeave += ChartMain_MouseLeave;

            // Fast double-click on chart area resets view
            chartMain.MouseDoubleClick += ChartMain_MouseDoubleClick;

            //BindZoomButton(btnZoomXIn, ZoomButtonMode.XIn);
            //BindZoomButton(btnZoomXOut, ZoomButtonMode.XOut);
            //BindZoomButton(btnZoomYIn, ZoomButtonMode.YIn);
            //BindZoomButton(btnZoomYOut, ZoomButtonMode.YOut);

        }

        private void ChartMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            HitTestResult hit = chartMain.HitTest(e.X, e.Y);

            bool doubleClickedInsideChartArea =
                hit.ChartElementType == ChartElementType.PlottingArea;

            if (!doubleClickedInsideChartArea)
                return;

            ResetView();
        }

        public void ResetView()
        {
            double oldXMin = xMin;
            double oldXMax = xMax;

            autoFollowX = true;
            autoScaleY = false;

            MoveXWindowToLatestSamples();

            yMin = defaultYMin;
            yMax = defaultYMax;

            UpdateChartPoints();

            bool xChanged =
                Math.Abs(xMin - oldXMin) > 0.000001 ||
                Math.Abs(xMax - oldXMax) > 0.000001;

            if (xChanged)
                NotifyXAxisViewChanged(XAxisViewChangeReason.Reset);
        }

        public void SetXView(double newXMin, double newXMax)
        {
            if (double.IsNaN(newXMin) || double.IsNaN(newXMax) ||
                double.IsInfinity(newXMin) || double.IsInfinity(newXMax))
                return;

            if (newXMax <= newXMin)
                return;

            xMin = newXMin;
            xMax = newXMax;

            autoFollowX = false;

            ClampXRangeToAvailableData();
            UpdateChartPoints();

            NotifyXAxisViewChanged(XAxisViewChangeReason.Programmatic);
        }

        private static int ClampInt(int value, int min, int max)
        {
            if (max < min)
                max = min;

            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        private int GetAvailableXPointCount()
        {
            if (sampleBuffer.Count > 0)
                return sampleBuffer.Count;

            return Math.Max(1, visiblePoints);
        }

        public void SetFixedYRange(double min, double max)
        {
            if (max <= min)
                return;

            normalizedYMin = min;
            normalizedYMax = max;

            double span = normalizedYMax - normalizedYMin;
            double margin = span * 0.10;

            defaultYMin = normalizedYMin - margin;
            defaultYMax = normalizedYMax + margin;

            yMin = defaultYMin;
            yMax = defaultYMax;

            minYVisibleSpan = span * 0.025;

            if (minYVisibleSpan <= 0.0)
                minYVisibleSpan = 0.05;

            autoScaleY = false;

            UpdateChartPoints();
        }

        private void BindZoomButton(Button button, ZoomButtonMode mode)
        {
            button.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                    BeginZoomHold(mode);
            };

            button.MouseUp += delegate
            {
                EndZoomHold(mode);
            };

            button.MouseLeave += delegate
            {
                EndZoomHold(mode);
            };
        }

        private void BeginZoomHold(ZoomButtonMode mode)
        {
            heldZoomButton = mode;
            zoomWasAppliedDuringHold = false;
        }

        private void EndZoomHold(ZoomButtonMode mode)
        {
            if (heldZoomButton != mode)
                return;

            if (!zoomWasAppliedDuringHold)
                pendingSingleZoom = mode;

            heldZoomButton = ZoomButtonMode.None;
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            bool receivedNewSamples = DrainSampleQueue();

            if (receivedNewSamples && autoFollowX)
            {
                MoveXWindowToLatestSamples();
            }

            if (autoScaleY)
            {
                AutoScaleVisibleY();
            }

            UpdateChartPoints();
        }

        private bool DrainSampleQueue()
        {
            bool received = false;
            int drainedBatchCount = 0;

            while (sampleQueue.TryDequeue(out double[] batch))
            {
                received = true;
                drainedBatchCount++;

                for (int i = 0; i < batch.Length; i++)
                {
                    sampleBuffer.Add(batch[i]);
                }

                TrimOldSamplesIfNeeded();

                if (!drainAllQueuedBatchesPerTick && drainedBatchCount >= 1)
                    break;
            }

            return received;
        }

        private void TrimOldSamplesIfNeeded()
        {
            int extra = sampleBuffer.Count - maxStoredSamples;

            if (extra <= 0)
                return;

            sampleBuffer.RemoveRange(0, extra);
            //firstSampleIndex += extra;

            ClampXRangeToAvailableData();
        }

        private static AxisGridInfo GetNiceAxisGrid(double axisMin, double axisMax, int targetGridCount)
        {
            AxisGridInfo result = new AxisGridInfo();

            if (targetGridCount < 2)
                targetGridCount = 8;

            if (double.IsNaN(axisMin) || double.IsNaN(axisMax) ||
                double.IsInfinity(axisMin) || double.IsInfinity(axisMax))
            {
                result.Interval = 1.0;
                result.Offset = 0.0;
                result.FirstGridLine = 0.0;
                return result;
            }

            if (axisMax < axisMin)
            {
                double temp = axisMin;
                axisMin = axisMax;
                axisMax = temp;
            }

            double span = axisMax - axisMin;

            if (span <= 0.0)
            {
                result.Interval = 1.0;
                result.Offset = 0.0;
                result.FirstGridLine = axisMin;
                return result;
            }

            double targetInterval = span / targetGridCount;

            double decade = Math.Pow(10.0, Math.Floor(Math.Log10(targetInterval)));
            double[] multipliers = { 1.0, 2.0, 5.0 };

            double bestInterval = decade;
            double bestError = double.MaxValue;

            for (int d = -1; d <= 1; d++)
            {
                double currentDecade = decade * Math.Pow(10.0, d);

                for (int i = 0; i < multipliers.Length; i++)
                {
                    double candidate = multipliers[i] * currentDecade;

                    if (candidate <= 0.0)
                        continue;

                    double error = Math.Abs(candidate - targetInterval);

                    if (error < bestError)
                    {
                        bestError = error;
                        bestInterval = candidate;
                    }
                }
            }

            // First clean grid line >= axisMin.
            double firstGridLine = Math.Ceiling(axisMin / bestInterval) * bestInterval;

            double eps = bestInterval * 1e-9;

            if (Math.Abs(firstGridLine - axisMin) < eps)
                firstGridLine = axisMin;

            double offset = firstGridLine - axisMin;

            if (Math.Abs(offset) < eps)
                offset = 0.0;

            if (Math.Abs(offset - bestInterval) < eps)
                offset = 0.0;

            result.Interval = bestInterval;
            result.Offset = offset;
            result.FirstGridLine = firstGridLine;

            return result;
        }

        private void ClampYRangeToNormalizedLimits()
        {
            double outerMin = defaultYMin;
            double outerMax = defaultYMax;

            if (outerMax <= outerMin)
            {
                outerMin = -1.2;
                outerMax = 1.2;
            }

            double outerSpan = outerMax - outerMin;
            double currentSpan = yMax - yMin;

            if (currentSpan <= 0.0 || double.IsNaN(currentSpan) || double.IsInfinity(currentSpan))
            {
                yMin = outerMin;
                yMax = outerMax;
                return;
            }

            // Do not allow zoom-in too much.
            if (currentSpan < minYVisibleSpan)
            {
                double center = (yMin + yMax) * 0.5;
                currentSpan = minYVisibleSpan;

                yMin = center - currentSpan * 0.5;
                yMax = center + currentSpan * 0.5;
            }

            // Do not allow zoom-out bigger than 1x full range.
            if (currentSpan >= outerSpan)
            {
                yMin = outerMin;
                yMax = outerMax;
                return;
            }

            // Do not allow panning below lower limit.
            if (yMin < outerMin)
            {
                yMin = outerMin;
                yMax = yMin + currentSpan;
            }

            // Do not allow panning above upper limit.
            if (yMax > outerMax)
            {
                yMax = outerMax;
                yMin = yMax - currentSpan;
            }

            // Final safety.
            if (yMin < outerMin)
                yMin = outerMin;

            if (yMax > outerMax)
                yMax = outerMax;
        }

        private void MoveXWindowToLatestSamples()
        {
            if (sampleBuffer.Count <= 0)
            {
                xMin = 0;
                xMax = visiblePoints;
                return;
            }

            double latestX = firstSampleIndex + sampleBuffer.Count - 1;

            xMax = latestX;
            xMin = xMax - visiblePoints + 1;

            if (xMin < firstSampleIndex)
                xMin = firstSampleIndex;

            if (xMax <= xMin)
                xMax = xMin + 1;
        }

        private void AutoScaleVisibleY()
        {
            if (sampleBuffer.Count <= 0)
            {
                yMin = -1;
                yMax = 1;
                return;
            }

            int startIndex = Math.Max(0, (int)Math.Floor(xMin - firstSampleIndex));
            int endIndex = Math.Min(sampleBuffer.Count - 1, (int)Math.Ceiling(xMax - firstSampleIndex));

            if (endIndex < startIndex)
            {
                yMin = -1;
                yMax = 1;
                return;
            }

            double min = double.MaxValue;
            double max = double.MinValue;

            for (int i = startIndex; i <= endIndex; i++)
            {
                double v = sampleBuffer[i];

                if (double.IsNaN(v) || double.IsInfinity(v))
                    continue;

                if (v < min) min = v;
                if (v > max) max = v;
            }

            if (min == double.MaxValue || max == double.MinValue)
            {
                yMin = -1;
                yMax = 1;
                return;
            }

            double span = max - min;
            double padding = span * 0.10;

            if (padding <= 0)
                padding = 1.0;

            yMin = min - padding;
            yMax = max + padding;
        }

        private void UpdateChartPoints()
        {
            if (chartMain.ChartAreas.Count == 0 || chartMain.Series.Count == 0)
                return;

            EnsureValidRanges();

            ChartArea area = chartMain.ChartAreas[ChartAreaName];
            Series series = chartMain.Series[SeriesName];

            series.Points.Clear();

            if (sampleBuffer.Count > 0)
            {
                int startIndex = Math.Max(0, (int)Math.Floor(xMin - firstSampleIndex));
                int endIndex = Math.Min(sampleBuffer.Count - 1, (int)Math.Ceiling(xMax - firstSampleIndex));

                for (int i = startIndex; i <= endIndex; i++)
                {
                    double x = firstSampleIndex + i;
                    double y = sampleBuffer[i];

                    if (!double.IsNaN(y) && !double.IsInfinity(y))
                        series.Points.AddXY(x, y);
                }
            }

            area.AxisX.Minimum = xMin;
            area.AxisX.Maximum = xMax;

            AxisGridInfo xGrid = GetNiceAxisGrid(xMin, xMax, 9);

            area.AxisX.IntervalType = DateTimeIntervalType.Number;
            area.AxisX.IntervalOffsetType = DateTimeIntervalType.Number;
            area.AxisX.Interval = xGrid.Interval;
            area.AxisX.IntervalOffset = xGrid.Offset;

            area.AxisY.Minimum = yMin;
            area.AxisY.Maximum = yMax;

            AxisGridInfo yGrid = GetNiceAxisGrid(yMin, yMax, 10);

            area.AxisY.IntervalType = DateTimeIntervalType.Number;
            area.AxisY.IntervalOffsetType = DateTimeIntervalType.Number;
            area.AxisY.Interval = yGrid.Interval;
            area.AxisY.IntervalOffset = yGrid.Offset;
        }

        private void EnsureValidRanges()
        {
            if (xMax <= xMin)
                xMax = xMin + 1;

            if (yMax <= yMin)
                yMax = yMin + 1;
        }

        private void InteractionTimer_Tick(object sender, EventArgs e)
        {
            if (activePanMode != PanMode.None && panDirty)
            {
                ApplyPan();
                panDirty = false;
                UpdateChartPoints();
            }

            if (heldZoomButton != ZoomButtonMode.None)
            {
                ApplyZoomButton(heldZoomButton);
                zoomWasAppliedDuringHold = true;
                UpdateChartPoints();
            }
            else if (pendingSingleZoom != ZoomButtonMode.None)
            {
                ApplyZoomButton(pendingSingleZoom);
                pendingSingleZoom = ZoomButtonMode.None;
                UpdateChartPoints();
            }
        }

        private void ChartMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            activePanMode = GetPanModeFromMousePosition(e.Location);

            panStartPoint = e.Location;
            panCurrentPoint = e.Location;

            panStartXMin = xMin;
            panStartXMax = xMax;
            panStartYMin = yMin;
            panStartYMax = yMax;

            panDirty = false;

            if (activePanMode == PanMode.X || activePanMode == PanMode.XY)
                autoFollowX = false;

            if (activePanMode == PanMode.Y || activePanMode == PanMode.XY)
                autoScaleY = false;

            chartMain.Capture = true;
            chartMain.Cursor = GetCursorForPanMode(activePanMode);
        }

        private void ChartMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (activePanMode != PanMode.None)
            {
                panCurrentPoint = e.Location;
                panDirty = true;
                return;
            }

            PanMode hoverMode = GetPanModeFromMousePosition(e.Location);
            chartMain.Cursor = GetCursorForPanMode(hoverMode);
        }

        private void ChartMain_MouseUp(object sender, MouseEventArgs e)
        {
            activePanMode = PanMode.None;
            panDirty = false;
            chartMain.Capture = false;
            chartMain.Cursor = Cursors.Default;
        }

        private void ChartMain_MouseLeave(object sender, EventArgs e)
        {
            if (activePanMode == PanMode.None)
                chartMain.Cursor = Cursors.Default;
        }

        private PanMode GetPanModeFromMousePosition(Point p)
        {
            bool onXAxisArea = p.Y >= chartMain.Height - XAxisPanHeightPx;
            bool onYAxisArea = p.X <= YAxisPanWidthPx;

            if (onXAxisArea && !onYAxisArea)
                return PanMode.X;

            if (onYAxisArea && !onXAxisArea)
                return PanMode.Y;

            if (onXAxisArea && onYAxisArea)
                return PanMode.XY;

            return PanMode.XY;
        }

        private System.Windows.Forms.Cursor GetCursorForPanMode(PanMode mode)
        {
            switch (mode)
            {
                case PanMode.X:
                    return System.Windows.Forms.Cursors.SizeWE;

                case PanMode.Y:
                    return System.Windows.Forms.Cursors.SizeNS;

                case PanMode.XY:
                    return System.Windows.Forms.Cursors.SizeAll;

                default:
                    return System.Windows.Forms.Cursors.Default;
            }
        }

        private void ApplyPan()
        {
            int plotWidth = Math.Max(1, chartMain.Width - YAxisPanWidthPx);
            int plotHeight = Math.Max(1, chartMain.Height - XAxisPanHeightPx);

            int dx = panCurrentPoint.X - panStartPoint.X;
            int dy = panCurrentPoint.Y - panStartPoint.Y;

            if (activePanMode == PanMode.X || activePanMode == PanMode.XY)
            {
                double oldXMin = xMin;
                double oldXMax = xMax;

                double xRange = panStartXMax - panStartXMin;
                double xDelta = -dx * xRange / plotWidth;

                xMin = panStartXMin + xDelta;
                xMax = panStartXMax + xDelta;

                ClampXRangeToAvailableData();

                bool xChanged =
                    Math.Abs(xMin - oldXMin) > 0.000001 ||
                    Math.Abs(xMax - oldXMax) > 0.000001;

                if (xChanged)
                    NotifyXAxisViewChanged(XAxisViewChangeReason.Pan);
            }

            if (activePanMode == PanMode.Y || activePanMode == PanMode.XY)
            {
                double yRange = panStartYMax - panStartYMin;
                double yDelta = dy * yRange / plotHeight;

                yMin = panStartYMin + yDelta;
                yMax = panStartYMax + yDelta;

                ClampYRangeToNormalizedLimits();
            }
        }

        private void ApplyZoomButton(ZoomButtonMode mode)
        {
            switch (mode)
            {
                case ZoomButtonMode.XIn:
                    autoFollowX = false;
                    ZoomX(0.80);
                    break;

                case ZoomButtonMode.XOut:
                    autoFollowX = false;
                    ZoomX(1.25);
                    break;

                case ZoomButtonMode.YIn:
                    autoScaleY = false;
                    ZoomY(0.80);
                    break;

                case ZoomButtonMode.YOut:
                    autoScaleY = false;
                    ZoomY(1.25);
                    break;

                case ZoomButtonMode.Reset:
                    ResetView();
                    break;
            }
        }

        private void ZoomX(double factor)
        {
            double oldXMin = xMin;
            double oldXMax = xMax;

            double center = (xMin + xMax) * 0.5;
            double range = xMax - xMin;

            double newRange = range * factor;

            if (newRange < 10)
                newRange = 10;

            double half = newRange * 0.5;

            xMin = center - half;
            xMax = center + half;

            ClampXRangeToAvailableData();

            bool xChanged =
                Math.Abs(xMin - oldXMin) > 0.000001 ||
                Math.Abs(xMax - oldXMax) > 0.000001;

            if (xChanged)
                NotifyXAxisViewChanged(XAxisViewChangeReason.Zoom);
        }

        private void ZoomY(double factor)
        {
            double center = (yMin + yMax) * 0.5;
            double range = yMax - yMin;

            double newRange = range * factor;

            if (newRange < minYVisibleSpan)
                newRange = minYVisibleSpan;

            double maxRange = defaultYMax - defaultYMin;

            // Do not zoom out more than full 1x normalized view with margin.
            if (newRange > maxRange)
                newRange = maxRange;

            double half = newRange * 0.5;

            yMin = center - half;
            yMax = center + half;

            ClampYRangeToNormalizedLimits();
        }

        private void ClampXRangeToAvailableData()
        {
            if (sampleBuffer.Count <= 0)
            {
                if (xMin < 0)
                {
                    double range = xMax - xMin;
                    xMin = 0;
                    xMax = xMin + range;
                }

                return;
            }

            double dataMin = firstSampleIndex;
            double dataMax = firstSampleIndex + sampleBuffer.Count - 1;
            double rangeX = xMax - xMin;

            if (rangeX <= 1)
                rangeX = 1;

            double dataSpan = dataMax - dataMin;

            if (dataSpan <= 1)
            {
                xMin = dataMin;
                xMax = dataMin + 1;
                return;
            }

            if (rangeX >= dataSpan)
            {
                xMin = dataMin;
                xMax = dataMax;
                return;
            }

            if (xMin < dataMin)
            {
                xMin = dataMin;
                xMax = xMin + rangeX;
            }

            if (xMax > dataMax)
            {
                xMax = dataMax;
                xMin = xMax - rangeX;
            }
        }



        private enum PanMode
        {
            None,
            X,
            Y,
            XY
        }

        private enum ZoomButtonMode
        {
            None,
            XIn,
            XOut,
            YIn,
            YOut,
            Reset
        }

        private struct AxisGridInfo
        {
            public double Interval;
            public double Offset;
            public double FirstGridLine;
        }

        private void btnZoomXIn_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox_ZoomXin_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            switch (p.Name)
            {
                case "pictureBox_ZoomXin":
                    BeginZoomHold(ZoomButtonMode.XIn);
                    break;

                case "pictureBox_ZoomXOut":
                    BeginZoomHold(ZoomButtonMode.XOut);
                    break;

                case "pictureBox_ZoomYIn":
                    BeginZoomHold(ZoomButtonMode.YIn);
                    break;

                case "pictureBox_ZoomYOut":
                    BeginZoomHold(ZoomButtonMode.YOut);
                    break;
            }
          
        }

        private void pictureBox_ZoomXin_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            switch (p.Name)
            {
                case "pictureBox_ZoomXin":
                    EndZoomHold(ZoomButtonMode.XIn);
                    break;

                case "pictureBox_ZoomXOut":
                    EndZoomHold(ZoomButtonMode.XOut);
                    break;

                case "pictureBox_ZoomYIn":
                    EndZoomHold(ZoomButtonMode.YIn);
                    break;

                case "pictureBox_ZoomYOut":
                    EndZoomHold(ZoomButtonMode.YOut);
                    break;
            }

        }

        private void pictureBox_ZoomXin_MouseLeave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            switch (p.Name)
            {
                case "pictureBox_ZoomXin":
                    EndZoomHold(ZoomButtonMode.XIn);
                    break;

                case "pictureBox_ZoomXOut":
                    EndZoomHold(ZoomButtonMode.XOut);
                    break;

                case "pictureBox_ZoomYIn":
                    EndZoomHold(ZoomButtonMode.YIn);
                    break;

                case "pictureBox_ZoomYOut":
                    EndZoomHold(ZoomButtonMode.YOut);
                    break;
            }

        }

        private void Farand_Tablet_Chart_Control_Load(object sender, EventArgs e)
        {

        }

        private void Farand_Tablet_Chart_Control_SizeChanged(object sender, EventArgs e)
        {
            int H0 = 5;
            int W0 = 50;
            int D0 = 100;
            int P0 = 50;

            pictureBox_ZoomYIn.Width = P0;
            pictureBox_ZoomYIn.Height = P0;

            pictureBox_ZoomYOut.Width = P0;
            pictureBox_ZoomYOut.Height = P0;

            pictureBox_ZoomXin.Width = P0;
            pictureBox_ZoomXin.Height = P0;

            pictureBox_ZoomXOut.Width = P0;
            pictureBox_ZoomXOut.Height = P0;

            int w = this.Width;
            int h = this.Height;
           

            chartMain.Top = H0;
            chartMain.Left = W0;
            chartMain.Width = w - W0 - H0;
            chartMain.Height = h - W0 - H0;
            
            pictureBox_ZoomXOut.Top = h - W0 + (W0 - P0) / 2;
            pictureBox_ZoomXOut.Left = W0 + (w - W0 - H0) / 2 - D0 / 2 - P0;
            pictureBox_ZoomXin.Top = h - W0 + (W0 - P0) / 2;
            pictureBox_ZoomXin.Left = W0 + (w - W0 - H0) / 2 + D0 / 2;

            pictureBox_ZoomYIn.Top = H0 + (h - W0 - H0) / 2 - D0 / 2 - P0;
            pictureBox_ZoomYIn.Left = (W0 - P0) / 2;
            pictureBox_ZoomYOut.Top = H0 + (h - W0 - H0) / 2 + D0 / 2;
            pictureBox_ZoomYOut.Left = (W0 - P0) / 2;
        }

        private void pictureBox_ZoomYOut_Click(object sender, EventArgs e)
        {

        }

        private void signal_Generator1_Simulated_Data_Choosen(object sender, EventArgs e)
        {

        }
    }

    public enum XAxisViewChangeReason
    {
        Pan,
        Zoom,
        Reset,
        Programmatic
    }

    public class XAxisViewChangedEventArgs : EventArgs
    {
        public int StartIndex { get; private set; }
        public int VisiblePointCount { get; private set; }
        public double ZoomRatio { get; private set; }
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public XAxisViewChangeReason Reason { get; private set; }

        public XAxisViewChangedEventArgs(
            int startIndex,
            int visiblePointCount,
            double zoomRatio,
            double xMin,
            double xMax,
            XAxisViewChangeReason reason)
        {
            StartIndex = startIndex;
            VisiblePointCount = visiblePointCount;
            ZoomRatio = zoomRatio;
            XMin = xMin;
            XMax = xMax;
            Reason = reason;
        }
    }


}