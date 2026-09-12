using Hlk_Wifi_Wrapper_Component;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            propertyGridWrapper.SelectedObject = hlkWifiWrapper1;

            hlkWifiWrapper1.PacketCompleted += HlkWifiWrapper1_PacketCompleted;

            LoadDefaultValuesFromComponent();
        }

        // ============================================================
        // Load / sync UI
        // ============================================================

        private void LoadDefaultValuesFromComponent()
        {
            txtIp.Text = hlkWifiWrapper1.ServerIp;

            numPort.Value = ClampDecimal(
                hlkWifiWrapper1.ServerPort,
                numPort.Minimum,
                numPort.Maximum
            );

            numFramesPerPacket.Value = ClampDecimal(
                hlkWifiWrapper1.FramesPerPacket,
                numFramesPerPacket.Minimum,
                numFramesPerPacket.Maximum
            );

            chkAutoConnectOnLoad.Checked = hlkWifiWrapper1.AutoConnectOnLoad;
            chkAutoHeartbeat.Checked = hlkWifiWrapper1.AutoHeartbeatEnabled;
            chkChartUpdate.Checked = hlkWifiWrapper1.EnableChartUpdate;
            chkLogUpdate.Checked = hlkWifiWrapper1.EnableLogUpdate;
            chkStatsUpdate.Checked = hlkWifiWrapper1.EnableStatsUpdate;
            chkSuspendUiWhenMinimized.Checked = hlkWifiWrapper1.SuspendUiWorkWhenMinimized;

            numDownSampleRatio.Value = ClampDecimal(
                hlkWifiWrapper1.DownSampleRatio,
                numDownSampleRatio.Minimum,
                numDownSampleRatio.Maximum
            );

            numStartIndex.Value = ClampDecimal(
                hlkWifiWrapper1.StartIndex,
                numStartIndex.Minimum,
                numStartIndex.Maximum
            );

            numPreTriggerTime.Value = ClampDecimal(
                hlkWifiWrapper1.PreTriggerTime,
                numPreTriggerTime.Minimum,
                numPreTriggerTime.Maximum
            );

            numLpfMode.Value = ClampDecimal(
                hlkWifiWrapper1.LpfMode,
                numLpfMode.Minimum,
                numLpfMode.Maximum
            );

            numLpfFrequency.Value = ClampDecimal(
                hlkWifiWrapper1.LpfFrequency,
                numLpfFrequency.Minimum,
                numLpfFrequency.Maximum
            );

            numHpfMode.Value = ClampDecimal(
                hlkWifiWrapper1.HpfMode,
                numHpfMode.Minimum,
                numHpfMode.Maximum
            );

            numHpfFrequency.Value = ClampDecimal(
                hlkWifiWrapper1.HpfFrequency,
                numHpfFrequency.Minimum,
                numHpfFrequency.Maximum
            );

            numPulseWidth.Value = ClampDecimal(
                hlkWifiWrapper1.PulseWidth,
                numPulseWidth.Minimum,
                numPulseWidth.Maximum
            );

            numMeasurementMode.Value = ClampDecimal(
                hlkWifiWrapper1.MeasurementMode,
                numMeasurementMode.Minimum,
                numMeasurementMode.Maximum
            );

            numSampleLength.Value = ClampDecimal(
                hlkWifiWrapper1.SampleLength,
                numSampleLength.Minimum,
                numSampleLength.Maximum
            );

            numVelocity.Value = ClampDecimal(
                hlkWifiWrapper1.Velocity,
                numVelocity.Minimum,
                numVelocity.Maximum
            );

            UpdateConnectionLabel();
            UpdateLastCommandLabel();
        }

        private void propertyGridWrapper_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            LoadDefaultValuesFromComponent();

            AppendLog("PropertyGrid value changed: " + e.ChangedItem.Label);
        }

        // ============================================================
        // Packet received event
        // ============================================================

        private void HlkWifiWrapper1_PacketCompleted(object sender, HLK_Wifi_PacketCompleted_EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => HlkWifiWrapper1_PacketCompleted(sender, e)));
                return;
            }

            UpdateConnectionLabel();

            lblPacketInfo.Text =
                $"Packet={e.PacketCounter} | " +
                $"MCU={NullableToString(e.McuPacketCounter)} | " +
                $"Samples={e.Samples.Length} | " +
                $"First={e.FirstSample} | " +
                $"Last={e.LastSample} | " +
                $"Continuity={e.ContinuityOk} | " +
                $"Time={e.Timestamp:HH:mm:ss.fff}";

            txtSamplesPreview.Text = MakeSamplesPreview(e.Samples, 60);

            AppendLog(
                $"PACKET RX: Packet={e.PacketCounter}, " +
                $"MCU={NullableToString(e.McuPacketCounter)}, " +
                $"Samples={e.Samples.Length}, " +
                $"First={e.FirstSample}, Last={e.LastSample}, " +
                $"Continuity={e.ContinuityOk}"
            );
        }

        // ============================================================
        // Connection / options
        // ============================================================

        private void btnApplyConnection_Click(object sender, EventArgs e)
        {
            try
            {
                hlkWifiWrapper1.ServerIp = txtIp.Text.Trim();
                hlkWifiWrapper1.ServerPort = (int)numPort.Value;
                hlkWifiWrapper1.FramesPerPacket = (int)numFramesPerPacket.Value;

                propertyGridWrapper.Refresh();

                UpdateConnectionLabel();

                AppendLog("Connection/protocol properties applied.");
            }
            catch (Exception ex)
            {
                AppendLog("Apply connection failed: " + ex.Message);
                MessageBox.Show(ex.Message, "Apply Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApplyOptions_Click(object sender, EventArgs e)
        {
            try
            {
                hlkWifiWrapper1.AutoConnectOnLoad = chkAutoConnectOnLoad.Checked;
                hlkWifiWrapper1.AutoHeartbeatEnabled = chkAutoHeartbeat.Checked;
                hlkWifiWrapper1.EnableChartUpdate = chkChartUpdate.Checked;
                hlkWifiWrapper1.EnableLogUpdate = chkLogUpdate.Checked;
                hlkWifiWrapper1.EnableStatsUpdate = chkStatsUpdate.Checked;
                hlkWifiWrapper1.SuspendUiWorkWhenMinimized = chkSuspendUiWhenMinimized.Checked;

                propertyGridWrapper.Refresh();

                AppendLog("Wrapper options applied.");
            }
            catch (Exception ex)
            {
                AppendLog("Apply options failed: " + ex.Message);
                MessageBox.Show(ex.Message, "Apply Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartReconnect_Click(object sender, EventArgs e)
        {
            btnApplyConnection_Click(sender, e);

            hlkWifiWrapper1.StartAutoReconnect();

            UpdateConnectionLabel();

            AppendLog("StartAutoReconnect called.");
        }

        private void btnStopReconnect_Click(object sender, EventArgs e)
        {
            hlkWifiWrapper1.StopAutoReconnect();

            UpdateConnectionLabel();

            AppendLog("StopAutoReconnect called.");
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            await hlkWifiWrapper1.DisconnectAsync();

            UpdateConnectionLabel();

            AppendLog("DisconnectAsync completed.");
        }

        // ============================================================
        // Heartbeat
        // ============================================================

        private async void btnHeartbeatAsync_Click(object sender, EventArgs e)
        {
            await SafeCommandAsync(
                async () => await hlkWifiWrapper1.Send_HeartbeatAsync(),
                "Heartbeat async"
            );
        }

        private void btnHeartbeatFireForget_Click(object sender, EventArgs e)
        {
            hlkWifiWrapper1.Send_Heartbeat();

            UpdateLastCommandLabel();

            AppendLog("Heartbeat non-async/fire-and-forget called.");
        }

        // ============================================================
        // Sampling
        // ============================================================

        private async void btnSendSamplingAsync_Click(object sender, EventArgs e)
        {
            await SafeCommandAsync(
                async () =>
                {
                    await hlkWifiWrapper1.Set_Sampling_ParametersAsync(
                        (int)numDownSampleRatio.Value,
                        (int)numStartIndex.Value,
                        (int)numPreTriggerTime.Value
                    );
                },
                "Sampling async"
            );
        }

        private void btnSendSamplingFireForget_Click(object sender, EventArgs e)
        {
            hlkWifiWrapper1.Set_Sampling_Parameters(
                (int)numDownSampleRatio.Value,
                (int)numStartIndex.Value,
                (int)numPreTriggerTime.Value
            );

            UpdateLastCommandLabel();

            AppendLog("Sampling non-async/fire-and-forget called.");
        }

        private async void btnSendConfiguredSampling_Click(object sender, EventArgs e)
        {
            CopyCommandInputsToProperties();

            await SafeCommandAsync(
                async () => await hlkWifiWrapper1.Send_Configured_Sampling_ParametersAsync(),
                "Configured sampling async"
            );
        }

        // ============================================================
        // Filtering
        // ============================================================

        private async void btnSendFilteringAsync_Click(object sender, EventArgs e)
        {
            await SafeCommandAsync(
                async () =>
                {
                    await hlkWifiWrapper1.Set_Filtering_ParametersAsync(
                        (int)numLpfMode.Value,
                        (int)numLpfFrequency.Value,
                        (int)numHpfMode.Value,
                        (int)numHpfFrequency.Value
                    );
                },
                "Filtering async"
            );
        }

        private void btnSendFilteringFireForget_Click(object sender, EventArgs e)
        {
            hlkWifiWrapper1.Set_Filtering_Parameters(
                (int)numLpfMode.Value,
                (int)numLpfFrequency.Value,
                (int)numHpfMode.Value,
                (int)numHpfFrequency.Value
            );

            UpdateLastCommandLabel();

            AppendLog("Filtering non-async/fire-and-forget called.");
        }

        private async void btnSendConfiguredFiltering_Click(object sender, EventArgs e)
        {
            CopyCommandInputsToProperties();

            await SafeCommandAsync(
                async () => await hlkWifiWrapper1.Send_Configured_Filtering_ParametersAsync(),
                "Configured filtering async"
            );
        }

        // ============================================================
        // Pulse
        // ============================================================

        private async void btnSendPulseAsync_Click(object sender, EventArgs e)
        {
            await SafeCommandAsync(
                async () =>
                {
                    await hlkWifiWrapper1.Set_Pulse_ParametersAsync(
                        (int)numPulseWidth.Value
                    );
                },
                "Pulse async"
            );
        }

        private void btnSendPulseFireForget_Click(object sender, EventArgs e)
        {
            hlkWifiWrapper1.Set_Pulse_Parameters(
                (int)numPulseWidth.Value
            );

            UpdateLastCommandLabel();

            AppendLog("Pulse non-async/fire-and-forget called.");
        }

        private async void btnSendConfiguredPulse_Click(object sender, EventArgs e)
        {
            CopyCommandInputsToProperties();

            await SafeCommandAsync(
                async () => await hlkWifiWrapper1.Send_Configured_Pulse_ParametersAsync(),
                "Configured pulse async"
            );
        }

        // ============================================================
        // Measurement
        // ============================================================

        private async void btnSendMeasurementAsync_Click(object sender, EventArgs e)
        {
            await SafeCommandAsync(
                async () =>
                {
                    await hlkWifiWrapper1.Set_Measurment_OptionsAsync(
                        (int)numMeasurementMode.Value,
                        (int)numSampleLength.Value,
                        (int)numVelocity.Value
                    );
                },
                "Measurement async"
            );
        }

        private void btnSendMeasurementFireForget_Click(object sender, EventArgs e)
        {
            hlkWifiWrapper1.Set_Measurment_Options(
                (int)numMeasurementMode.Value,
                (int)numSampleLength.Value,
                (int)numVelocity.Value
            );

            UpdateLastCommandLabel();

            AppendLog("Measurement non-async/fire-and-forget called.");
        }

        private async void btnSendConfiguredMeasurement_Click(object sender, EventArgs e)
        {
            CopyCommandInputsToProperties();

            await SafeCommandAsync(
                async () => await hlkWifiWrapper1.Send_Configured_Measurment_OptionsAsync(),
                "Configured measurement async"
            );
        }

        // ============================================================
        // Helpers
        // ============================================================

        private async Task SafeCommandAsync(Func<Task> command, string name)
        {
            try
            {
                await command();

                UpdateLastCommandLabel();

                AppendLog(name + " sent successfully.");
            }
            catch (Exception ex)
            {
                AppendLog(name + " failed: " + ex.Message);
                MessageBox.Show(ex.Message, name + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyCommandInputsToProperties()
        {
            hlkWifiWrapper1.DownSampleRatio = (int)numDownSampleRatio.Value;
            hlkWifiWrapper1.StartIndex = (int)numStartIndex.Value;
            hlkWifiWrapper1.PreTriggerTime = (int)numPreTriggerTime.Value;

            hlkWifiWrapper1.LpfMode = (int)numLpfMode.Value;
            hlkWifiWrapper1.LpfFrequency = (int)numLpfFrequency.Value;
            hlkWifiWrapper1.HpfMode = (int)numHpfMode.Value;
            hlkWifiWrapper1.HpfFrequency = (int)numHpfFrequency.Value;

            hlkWifiWrapper1.PulseWidth = (int)numPulseWidth.Value;

            hlkWifiWrapper1.MeasurementMode = (int)numMeasurementMode.Value;
            hlkWifiWrapper1.SampleLength = (int)numSampleLength.Value;
            hlkWifiWrapper1.Velocity = (int)numVelocity.Value;

            propertyGridWrapper.Refresh();
        }

        private void UpdateConnectionLabel()
        {
            lblConnection.Text =
                "Connection: " + hlkWifiWrapper1.TcpState +
                " | Connected=" + hlkWifiWrapper1.IsConnected +
                " | IP=" + hlkWifiWrapper1.ServerIp +
                " | Port=" + hlkWifiWrapper1.ServerPort;
        }

        private void UpdateLastCommandLabel()
        {
            byte[] frame = hlkWifiWrapper1.LastCommandFrame;

            if (frame == null)
            {
                lblLastCommand.Text = "Last CMD: -";
                return;
            }

            lblLastCommand.Text = "Last CMD: " + BitConverter.ToString(frame);
        }

        private void AppendLog(string text)
        {
            if (txtLog == null)
                return;

            if (txtLog.TextLength > 50000)
                txtLog.Clear();

            txtLog.AppendText(
                DateTime.Now.ToString("HH:mm:ss.fff") +
                "  " +
                text +
                Environment.NewLine
            );
        }

        private string MakeSamplesPreview(double[] samples, int maxCount)
        {
            if (samples == null || samples.Length == 0)
                return "";

            int count = Math.Min(samples.Length, maxCount);

            string text = "";

            for (int i = 0; i < count; i++)
            {
                text += samples[i].ToString("0");

                if (i < count - 1)
                    text += ", ";
            }

            if (samples.Length > count)
            {
                text += Environment.NewLine;
                text += "... " + (samples.Length - count) + " more samples";
            }

            return text;
        }

        private static string NullableToString(uint? value)
        {
            if (value.HasValue)
                return value.Value.ToString();

            return "-";
        }

        private static decimal ClampDecimal(decimal value, decimal min, decimal max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                await hlkWifiWrapper1.DisconnectAsync();
            }
            catch
            {
                // Ignore close errors.
            }
        }
    }
}