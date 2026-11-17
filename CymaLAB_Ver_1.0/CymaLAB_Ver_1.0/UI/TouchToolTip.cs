using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CymaLAB_Ver_1._0
{
    internal sealed class TouchToolTip : Component, IMessageFilter
    {
        private readonly Control target;
        private readonly Func<string> getText;
        private readonly ToolTip popup = new ToolTip { OwnerDraw = true, ShowAlways = true };
        private readonly Timer holdTimer = new Timer { Interval = 600 };
        private readonly Font textFont = new Font("Segoe UI", 11F);
        private Point pressLocation;
        private string displayedText;
        private bool popupVisible;

        public TouchToolTip(IContainer container, Control target, Func<string> getText)
        {
            container.Add(this);
            this.target = target;
            this.getText = getText;

            target.MouseDown += Target_MouseDown;
            target.MouseUp += Target_EndHold;
            target.MouseLeave += Target_EndHold;
            target.MouseCaptureChanged += Target_EndHold;
            target.MouseMove += Target_MouseMove;
            target.EnabledChanged += Target_StateChanged;
            target.VisibleChanged += Target_StateChanged;
            holdTimer.Tick += HoldTimer_Tick;
            popup.Popup += Popup_Size;
            popup.Draw += Popup_Draw;


            Application.AddMessageFilter(this);
        }

        private void Target_MouseDown(object sender, MouseEventArgs e)
        {
            Hide();

            if (e.Button != MouseButtons.Left || (Control.ModifierKeys & Keys.Control) != Keys.None)
                return;

            pressLocation = e.Location;
            holdTimer.Start();
        }

        private void Target_EndHold(object sender, EventArgs e)
        {
            // Releasing cancels a pending hold, but leaves a visible tip readable.
            holdTimer.Stop();
        }

        private void Target_MouseMove(object sender, MouseEventArgs e)
        {
            Size tolerance = SystemInformation.DragSize;

            if (Math.Abs(e.X - pressLocation.X) > tolerance.Width / 2
                || Math.Abs(e.Y - pressLocation.Y) > tolerance.Height / 2)
                holdTimer.Stop();
        }

        private void HoldTimer_Tick(object sender, EventArgs e)
        {
            holdTimer.Stop();

            if (!target.Visible || !target.Enabled || !target.IsHandleCreated)
                return;

            displayedText = getText();

            popup.Show(displayedText, target, 0, target.Height + 10, 5000);

            popupVisible = true;
        }

        private void Popup_Size(object sender, PopupEventArgs e)
        {
            Size textSize = TextRenderer.MeasureText(displayedText, textFont, new Size(300, 0), TextFormatFlags.WordBreak);
            e.ToolTipSize = new Size(textSize.Width + 28, textSize.Height + 24);
        }

        private void Popup_Draw(object sender, DrawToolTipEventArgs e)
        {
            using (var background = new SolidBrush(Color.FromArgb(40, 40, 40)))
            using (var border = new Pen(Color.Goldenrod))
            {
                e.Graphics.FillRectangle(background, e.Bounds);
                e.Graphics.DrawRectangle(border, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);
            }

            Rectangle textArea = Rectangle.Inflate(e.Bounds, -14, -12);

            TextRenderer.DrawText(e.Graphics, displayedText, textFont, textArea, Color.White, TextFormatFlags.WordBreak);
        }

        public void Hide()
        {
            holdTimer.Stop();

            if (popupVisible)
            {
                popup.Hide(target);
                popupVisible = false;
            }
        }

        private void Target_StateChanged(object sender, EventArgs e)
        {
            if (!target.Enabled || !target.Visible)
                Hide();
        }

        public bool PreFilterMessage(ref Message message)
        {
            // Dismiss on the next tap/click anywhere, including another control.
            if (message.Msg == 0x0201 || message.Msg == 0x0204 || message.Msg == 0x0207
                || message.Msg == 0x0246 || message.Msg == 0x00A1 || message.Msg == 0x0100)
                Hide();

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Application.RemoveMessageFilter(this);
                target.MouseDown -= Target_MouseDown;
                target.MouseUp -= Target_EndHold;
                target.MouseLeave -= Target_EndHold;
                target.MouseCaptureChanged -= Target_EndHold;
                target.MouseMove -= Target_MouseMove;
                target.EnabledChanged -= Target_StateChanged;
                target.VisibleChanged -= Target_StateChanged;
                holdTimer.Dispose();
                popup.Dispose();
                textFont.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
