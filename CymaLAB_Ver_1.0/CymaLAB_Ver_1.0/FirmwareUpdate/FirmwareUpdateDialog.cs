using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace CymaLAB_Ver_1._0
{
    public sealed class FirmwareUpdateDialog : Form
    {
        private readonly FirmwareUpdater updater;
        private readonly Communication communication;
        private readonly TextBox filePath = new TextBox { ReadOnly = true, Dock = DockStyle.Fill };
        private readonly Button browse = new Button { Text = "Browse...", AutoSize = true };
        private readonly ComboBox recoveryPort = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        private readonly Button refresh = new Button { Text = "Refresh ports", AutoSize = true };
        private readonly Button upload = new Button { Text = "Update firmware", AutoSize = true };
        private readonly Button close = new Button { Text = "Close", AutoSize = true };
        private readonly Label status = new Label { AutoSize = true, Dock = DockStyle.Fill };
        private readonly ProgressBar progress = new ProgressBar { Minimum = 0, Maximum = 100, Dock = DockStyle.Fill };
        private bool working;
        private bool transferCompleted;

        public FirmwareUpdateDialog(FirmwareUpdater updater, Communication communication)
        {
            this.updater = updater ?? throw new ArgumentNullException(nameof(updater));
            this.communication = communication ?? throw new ArgumentNullException(nameof(communication));
            Text = "USB firmware update";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(580, 285);
            AutoScaleMode = AutoScaleMode.Font;

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 2,
                RowCount = 7
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.Controls.Add(new Label { Text = "Application firmware (.bin, .hex or .ihx)", AutoSize = true }, 0, 0);
            layout.Controls.Add(filePath, 0, 1);
            layout.Controls.Add(browse, 1, 1);
            layout.Controls.Add(new Label { Text = "Recovery COM port (only used when the application is disconnected)", AutoSize = true }, 0, 2);
            layout.SetColumnSpan(layout.GetControlFromPosition(0, 2), 2);
            layout.Controls.Add(recoveryPort, 0, 3);
            layout.Controls.Add(refresh, 1, 3);
            layout.Controls.Add(progress, 0, 4);
            layout.SetColumnSpan(progress, 2);
            layout.Controls.Add(status, 0, 5);
            layout.SetColumnSpan(status, 2);
            layout.Controls.Add(upload, 0, 6);
            layout.Controls.Add(close, 1, 6);
            Controls.Add(layout);

            browse.Click += Browse_Click;
            refresh.Click += (sender, e) => RefreshPorts();
            upload.Click += Upload_Click;
            close.Click += (sender, e) => Close();
            updater.StatusChanged += Updater_StatusChanged;
            updater.ProgressChanged += Updater_ProgressChanged;
            communication.Connected += Communication_Connected;
            RefreshPorts();
            status.Text = "Choose the application image. Keep USB connected throughout the update.";
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Title = "Select application firmware",
                Filter = "Application firmware (*.bin;*.hex;*.ihx)|*.bin;*.hex;*.ihx",
                CheckFileExists = true,
                Multiselect = false
            })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    filePath.Text = dialog.FileName;
            }
        }

        private void RefreshPorts()
        {
            string selected = recoveryPort.SelectedItem as string;
            recoveryPort.Items.Clear();
            recoveryPort.Items.AddRange(SerialPort.GetPortNames());
            if (selected != null && recoveryPort.Items.Contains(selected))
                recoveryPort.SelectedItem = selected;
        }

        private async void Upload_Click(object sender, EventArgs e)
        {
            if (working) return;
            if (string.IsNullOrWhiteSpace(filePath.Text))
            {
                status.Text = "Select an application firmware file first.";
                return;
            }
            if (!communication.IsConnected && recoveryPort.SelectedItem == null)
            {
                status.Text = "Connect the USB application, or select its bootloader COM port for recovery.";
                return;
            }

            transferCompleted = false;
            progress.Value = 0;
            SetWorking(true);
            try
            {
                await updater.UpdateAsync(filePath.Text, recoveryPort.SelectedItem as string);
                transferCompleted = true;
                status.Text = communication.IsConnected
                    ? "Transfer acknowledged. The application is connected."
                    : "Transfer acknowledged. Waiting for the application to reconnect; reset the device if its bootloader requires it.";
            }
            catch (Exception ex)
            {
                status.Text = "Update failed. " + ex.Message;
                MessageBox.Show(this, ex.Message, "Firmware update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetWorking(false);
            }
        }

        private void SetWorking(bool value)
        {
            working = value;
            browse.Enabled = !value;
            refresh.Enabled = !value;
            recoveryPort.Enabled = !value;
            upload.Enabled = !value;
            close.Enabled = !value;
        }

        private void Updater_StatusChanged(string message) { OnUi(() => status.Text = message); }
        private void Updater_ProgressChanged(int percent) { OnUi(() => progress.Value = Math.Max(0, Math.Min(100, percent))); }
        private void Communication_Connected()
        {
            OnUi(() =>
            {
                if (transferCompleted)
                    status.Text = "Transfer acknowledged. The application is connected.";
            });
        }

        private void OnUi(Action action)
        {
            if (IsDisposed || Disposing || !IsHandleCreated) return;
            if (!InvokeRequired) { action(); return; }
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (!IsDisposed && !Disposing) action();
                }));
            }
            catch (InvalidOperationException) { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (working) e.Cancel = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            updater.StatusChanged -= Updater_StatusChanged;
            updater.ProgressChanged -= Updater_ProgressChanged;
            communication.Connected -= Communication_Connected;
            base.OnFormClosed(e);
        }
    }
}
