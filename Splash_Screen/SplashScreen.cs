using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Splash_Screen
{
    public sealed class SplashOptions
    {
        public int Width { get; set; } = 800;
        public Color StatusColor { get; set; } = Color.FromArgb(32, 32, 32);
        public string InitialStatus { get; set; } = "Preparing application";
        // Positions are relative to the artwork's height (0 to 1).
        public float StatusPosition { get; set; } = 0.87f;
        public float DotsPosition { get; set; } = 0.94f;
    }

    /// <summary>
    /// An independently animated splash. Create and dispose on the application's
    /// STA thread; Report may be called from any thread. Artwork is cloned.
    /// </summary>
    public sealed class SplashScreen : IDisposable, IProgress<string>
    {
        private readonly Thread thread;
        private SplashWindow window;
        private int disposed;

        private SplashScreen(Image artwork, SplashOptions options)
        {
            if (artwork == null) throw new ArgumentNullException(nameof(artwork));
            if (options.Width <= 0 || options.StatusPosition < 0 || options.StatusPosition > 1
                || options.DotsPosition < 0 || options.DotsPosition > 1)
                throw new ArgumentOutOfRangeException(nameof(options));

            Image copy = new Bitmap(artwork);
            // Snapshot options so subsequent caller changes cannot race the UI.
            var snapshot = new SplashOptions
            {
                Width = options.Width, StatusColor = options.StatusColor,
                InitialStatus = options.InitialStatus,
                StatusPosition = options.StatusPosition, DotsPosition = options.DotsPosition
            };
            Exception startupError = null;
            bool shown = false;
            using (var ready = new ManualResetEventSlim())
            {
                thread = new Thread(() =>
                {
                    try
                    {
                        using (copy)
                        using (var form = new SplashWindow(copy, snapshot))
                        {
                            window = form;
                            form.Shown += (sender, args) =>
                            {
                                form.Update();
                                form.Activate();
                                shown = true;
                                ready.Set();
                            };
                            Application.Run(form);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!shown) { startupError = ex; ready.Set(); }
                        else System.Diagnostics.Debug.WriteLine(ex);
                    }
                });
                thread.Name = "Splash screen UI";
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                ready.Wait();
            }
            if (startupError != null)
            {
                thread.Join();
                throw new InvalidOperationException("Could not display the splash screen.", startupError);
            }
        }

        public static SplashScreen Show(Image artwork, SplashOptions options = null)
        {
            return new SplashScreen(artwork, options ?? new SplashOptions());
        }

        public void Report(string status)
        {
            if (Volatile.Read(ref disposed) != 0) return;
            try
            {
                window.Invoke((Action)(() => window.SetStatus(status ?? string.Empty)));
            }
            catch (InvalidOperationException) when (Volatile.Read(ref disposed) != 0) { }
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0) return;
            if (thread.IsAlive && !window.IsDisposed)
            {
                try { window.BeginInvoke((Action)(() => { window.AllowClose = true; window.Close(); })); }
                catch (InvalidOperationException) { }
            }
            if (Thread.CurrentThread != thread) thread.Join();
        }

        /// <summary>
        /// Call from the main form's Shown event on its UI thread. Transfer
        /// foreground ownership before closing the splash, then activate the app.
        /// </summary>
        public void Complete(Form mainWindow)
        {
            if (mainWindow == null) throw new ArgumentNullException(nameof(mainWindow));
            if (mainWindow.IsDisposed || !mainWindow.Visible || mainWindow.InvokeRequired)
                throw new InvalidOperationException("Complete must run on the visible main window's UI thread.");

            IntPtr mainHandle = mainWindow.Handle;
            if (Volatile.Read(ref disposed) == 0)
            {
                // Closing an active window first can activate a different app.
                // Hand off while the splash still owns the foreground instead.
                window.Invoke((Action)(() => SetForegroundWindow(mainHandle)));
            }
            Dispose();
            mainWindow.BringToFront();
            mainWindow.Activate();
            SetForegroundWindow(mainHandle);
            // Finish activation after the initial Show/Shown message sequence.
            mainWindow.BeginInvoke((Action)(() =>
            {
                if (!mainWindow.IsDisposed)
                {
                    mainWindow.BringToFront();
                    mainWindow.Activate();
                    SetForegroundWindow(mainWindow.Handle);
                }
            }));
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr windowHandle);

        private sealed class SplashWindow : Form
        {
            private readonly Image artwork;
            private readonly SplashOptions options;
            private readonly System.Windows.Forms.Timer animation;
            private readonly Font statusFont;
            private string status;
            private int activeDot;
            public bool AllowClose { get; set; }

            public SplashWindow(Image artwork, SplashOptions options)
            {
                this.artwork = artwork;
                this.options = options;
                status = options.InitialStatus;
                FormBorderStyle = FormBorderStyle.None;
                StartPosition = FormStartPosition.CenterScreen;
                ShowInTaskbar = false;
                ControlBox = false;
                AutoScaleMode = AutoScaleMode.None;
                DoubleBuffered = true;
                Text = "Preparing application";
                var area = Screen.FromPoint(Cursor.Position).WorkingArea;
                double scale = Math.Min(options.Width / (double)artwork.Width,
                    Math.Min(area.Width * 0.85 / artwork.Width, area.Height * 0.85 / artwork.Height));
                ClientSize = new Size((int)(artwork.Width * scale), (int)(artwork.Height * scale));
                statusFont = new Font("Segoe UI", Math.Max(9f, ClientSize.Height * 0.032f), FontStyle.Regular, GraphicsUnit.Pixel);
                animation = new System.Windows.Forms.Timer { Interval = 180 };
                animation.Tick += (sender, args) => { activeDot = (activeDot + 1) % 3; Invalidate(); };
                animation.Start();
            }

            public void SetStatus(string text)
            {
                status = text;
                AccessibleDescription = text;
                Invalidate();
                Update();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImage(artwork, ClientRectangle);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(options.StatusColor))
                using (var format = new StringFormat { Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap })
                {
                    float textHeight = ClientSize.Height * 0.07f;
                    e.Graphics.DrawString(status, statusFont, brush,
                        new RectangleF(20, ClientSize.Height * options.StatusPosition - textHeight / 2,
                            ClientSize.Width - 40, textHeight), format);
                    float diameter = Math.Max(5, ClientSize.Height * 0.018f);
                    float spacing = diameter * 2.4f;
                    for (int i = 0; i < 3; i++)
                    {
                        brush.Color = Color.FromArgb(i == activeDot ? 230 : 65, options.StatusColor);
                        e.Graphics.FillEllipse(brush, ClientSize.Width / 2f + (i - 1) * spacing - diameter / 2,
                            ClientSize.Height * options.DotsPosition - diameter / 2, diameter, diameter);
                    }
                }
            }

            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                base.OnFormClosing(e);
                if (e.CloseReason == CloseReason.UserClosing && !AllowClose) e.Cancel = true;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing) { animation.Dispose(); statusFont.Dispose(); }
                base.Dispose(disposing);
            }
        }
    }
}
