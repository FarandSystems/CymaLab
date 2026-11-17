using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Signal_Strength_Control
{
    internal sealed class LevelSelector : UserControl
    {
        private static readonly Color Accent = Color.LimeGreen;
        private readonly LevelButton decrease = new LevelButton(false);
        private readonly LevelButton increase = new LevelButton(true);
        private readonly Font captionFont = new Font("Segoe UI", 11F);
        private int value = 1;
        private int maximum = 8;
        private string caption = "Level";

        public event EventHandler ValueChanged;

        public string Caption
        {
            get { return caption; }
            set { caption = value ?? "Level"; UpdateSelection(); }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(value));
                maximum = value;
                this.value = Math.Min(this.value, maximum);
                UpdateSelection();
            }
        }

        public int Value
        {
            get { return value; }
            set
            {
                if (value < 1 || value > maximum) throw new ArgumentOutOfRangeException(nameof(value));
                this.value = value;
                UpdateSelection();
            }
        }

        public LevelSelector()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            TabStop = false;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(decrease);
            Controls.Add(increase);
            decrease.Click += (sender, e) => ChangeValue(-1);
            increase.Click += (sender, e) => ChangeValue(1);
            UpdateSelection();
        }

        internal void SetButtonNames(string decreaseName, string increaseName)
        {
            decrease.Name = decreaseName;
            increase.Name = increaseName;
        }

        private void ChangeValue(int delta)
        {
            int next = Math.Max(1, Math.Min(maximum, value + delta));
            if (next == value) return;
            Value = next;
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateSelection()
        {
            decrease.Enabled = value > 1;
            increase.Enabled = value < maximum;
            decrease.AccessibleName = "Decrease " + caption.ToLowerInvariant();
            increase.AccessibleName = "Increase " + caption.ToLowerInvariant();
            AccessibleName = caption + ", level " + value + " of " + maximum;
            Invalidate();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            int side = Math.Min(44, Math.Max(1, Height - 6));
            int top = Math.Min(Height - side, (Height - side) / 2 + 4);
            decrease.SetBounds(8, top, side, side);
            increase.SetBounds(Width - side - 8, top, side, side);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 2 || Height < 2) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int meterLeft = decrease.Right + 14;
            int meterWidth = Math.Max(1, increase.Left - meterLeft - 14);
            TextRenderer.DrawText(e.Graphics, caption, captionFont,
                new Rectangle(meterLeft, Math.Max(0, Height / 2 - 33), meterWidth, 24), Color.FromArgb(224, 224, 224),
                TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter);

            // The meter is a readout; large buttons make level changes precise on touch screens.
            if (meterWidth < maximum * 5) return;
            float segmentWidth = (meterWidth - (maximum - 1) * 3F) / maximum;
            for (int i = 0; i < maximum; i++)
            {
                using (Brush segment = new SolidBrush(i < value && Enabled ? Accent : Color.FromArgb(96, 96, 96)))
                    e.Graphics.FillRectangle(segment, meterLeft + i * (segmentWidth + 3), decrease.Top + decrease.Height / 2F - 3.5F, segmentWidth, 7);
            }
        }

        internal static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = Math.Max(1, Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height)));
            var path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                captionFont.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    internal sealed class LevelButton : Button
    {
        private readonly bool increases;
        private readonly Stopwatch pressDuration = new Stopwatch();
        private bool hovered;
        private bool pressed;

        public LevelButton(bool increases)
        {
            this.increases = increases;
            Text = increases ? "+" : "−";
            Cursor = Cursors.Hand;
            FlatStyle = FlatStyle.Flat;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pressed = true;
                pressDuration.Restart();
            }
            base.OnMouseDown(e);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            pressed = false;
            pressDuration.Reset();
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            // A hold belongs to the touch tooltip and must not change the setting on release.
            if (!Enabled || (pressDuration.IsRunning && pressDuration.ElapsedMilliseconds >= 600)) return;
            base.OnClick(e);
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); hovered = true; Invalidate(); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); hovered = false; Invalidate(); }
        protected override void OnEnabledChanged(EventArgs e) { base.OnEnabledChanged(e); Invalidate(); }
        protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); Invalidate(); }
        protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width < 2 || Height < 2) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color fill = !Enabled ? Color.FromArgb(64, 64, 64)
                : pressed ? Color.FromArgb(96, 96, 96)
                : hovered ? Color.FromArgb(88, 88, 88) : Color.FromArgb(64, 64, 64);
            using (GraphicsPath shape = LevelSelector.RoundedRectangle(new Rectangle(0, 0, Width - 1, Height - 1), 9))
            using (Brush background = new SolidBrush(fill))
            using (Pen border = new Pen(Focused ? Color.Chartreuse : Color.FromArgb(112, 112, 112)))
            using (Pen glyph = new Pen(Enabled ? Color.FromArgb(242, 242, 244) : Color.FromArgb(101, 104, 111), 2F))
            {
                e.Graphics.FillPath(background, shape);
                e.Graphics.DrawPath(border, shape);
                int x = Width / 2;
                int y = Height / 2;
                e.Graphics.DrawLine(glyph, x - 7, y, x + 7, y);
                if (increases) e.Graphics.DrawLine(glyph, x, y - 7, x, y + 7);
            }
        }
    }
}
