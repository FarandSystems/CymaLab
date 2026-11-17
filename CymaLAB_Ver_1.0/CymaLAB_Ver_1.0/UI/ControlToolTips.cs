using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CymaLAB_Ver_1._0
{
    internal sealed class ControlToolTips : IDisposable
    {
        private readonly Control root;
        private readonly Container components = new Container();
        private readonly HashSet<Control> registered = new HashSet<Control>();
        private readonly Func<Control, string> getDynamicText;

        private static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
        {
            { "button_PowerDecrease", "Decrease transmitter power by one level." },
            { "button_PowerIncrease", "Increase transmitter power by one level." },
            { "button_AmplifierDecrease", "Decrease amplifier gain by one level." },
            { "button_AmplifierIncrease", "Increase amplifier gain by one level." },
            { "pictureBox_Run", "Run or pause the current capture." },
            { "pictureBox_Save", "Save the paused chart capture and its current view to a text file." },
            { "pictureBox_load", "Load a saved chart capture for review. Run returns to live data." },
            { "pictureBox_Firmware_Update", "Open the USB firmware update window." },
            { "pictureBox_Connection_Mode", "Switch between USB and Wi-Fi communication." },
            { "pictureBox_Close", "Close the application." },
            { "pictureBox_Minimize", "Minimize the application." },
            { "pictureBox_TOF_Increase", "Increase the manual time of flight by 10 microseconds." },
            { "pictureBox_TOF_Decrease", "Decrease the manual time of flight by 10 microseconds." },
            { "pictureBox_TOF_Key", "Switch between automatic and manual time of flight." },
            { "pictureBox_Set", "Apply the reference time of flight and discard time." },
            { "pictureBox_Filter_Icon", "Cycle through no filter, weak band-pass and strong band-pass filtering." },
            { "pictureBox_ZoomXin", "Zoom in on the chart time axis." },
            { "pictureBox_ZoomXOut", "Zoom out on the chart time axis." },
            { "pictureBox_ZoomYIn", "Zoom in on the chart amplitude axis." },
            { "pictureBox_ZoomYOut", "Zoom out on the chart amplitude axis." },
            { "chartMain", "Drag the chart axes to pan the view. Use the zoom buttons to change the time or amplitude range." },
            { "textBox_Sample_Length", "Enter the known sample length in centimeters." },
            { "textBox_Sample_Velocity", "Enter the known sound velocity in meters per second." },
            { "textBox_Reference", "Enter the reference time of flight in microseconds." },
            { "textBox_Discard", "Enter the discard time from 0 to 255 microseconds." },
            { "checkBox_use_Simulated", "Switch between the test signal generator and normal device operation." },
            { "numericUpDown_Amplitude", "Adjust the simulated signal amplitude." },
            { "numericUpDown_T_Delay", "Adjust the simulated signal delay in microseconds." },
            { "numericUpDown_T_Rise", "Adjust the simulated signal rise time in microseconds." },
            { "numericUpDown_T1", "Adjust the simulated signal envelope duration." },
            { "numericUpDown_F", "Adjust the simulated signal frequency in kilohertz." },
            { "numericUpDown_Noise_Intensity", "Adjust the simulated noise intensity." },
            { "numericUpDown_Down_Sample_Ratio", "Adjust the test signal downsample ratio." },
            { "numericUpDown_Start_Index", "Adjust the starting sample of the test capture." },
            { "numericUpDown_Delay_Jitter", "Adjust the simulated delay jitter." },
            { "firmwareBrowse", "Choose an application firmware file (.bin, .hex or .ihx)." },
            { "firmwareRecoveryPort", "Select the bootloader COM port for recovery when disconnected." },
            { "firmwareRefresh", "Refresh the available COM ports." },
            { "firmwareUpload", "Upload the selected firmware to the USB device." },
            { "firmwareClose", "Close the firmware update window." }
        };

        public ControlToolTips(Control root, Func<Control, string> getDynamicText = null)
        {
            this.root = root;
            this.getDynamicText = getDynamicText;

            root.Disposed += Root_Disposed;

            Register(root);
        }

        private void Register(Control control)
        {
            if (!registered.Add(control)) return;

            // Exclude the two Ctrl+click information icons throughout the tree.
            if (control.Name != "pictureBox_TestMode" && control.Name != "pictureBox_Info"
                && GetDescription(control) != null)
                new TouchToolTip(components, control, () => getDynamicText?.Invoke(control) ?? GetDescription(control));

            control.ControlAdded += Control_Added;

            foreach (Control child in control.Controls)
                Register(child);
        }

        private static string GetDescription(Control control)
        {
            if (control.Parent is UpDownBase)
                return GetDescription(control.Parent);

            string description;

            if (Descriptions.TryGetValue(control.Name, out description)) return description;

            if (control.Name.StartsWith("pictureBox_Transducer", StringComparison.Ordinal))
                return "Select transmitter power level " + control.Name.Substring("pictureBox_Transducer".Length) + ".";

            if (control.Name.StartsWith("pictureBox_Receiver", StringComparison.Ordinal))
                return "Select receiver sensitivity level " + control.Name.Substring("pictureBox_Receiver".Length) + ".";

            // The label inside each custom selector receives its touch events.
            if (control.Name == "label_Fancy")
            {
                string selector = control.Parent?.Name ?? "";

                if (selector == "fancy_Lable_Control_Length") return "Measure sample length using the known sound velocity.";

                if (selector == "fancy_Lable_Control_Velocity") return "Measure sound velocity using the known sample length.";

                if (selector == "fancy_Lable_Control_None") return "Turn capture averaging off.";

                Match averaging = Regex.Match(selector, "^fancy_Lable_Control(4|8|16|32)$");

                if (averaging.Success) return "Average " + averaging.Groups[1].Value + " captures.";

                return "Select " + control.Text + ".";
            }

            if (control is ButtonBase || control is ComboBox || control is NumericUpDown
                || control is TrackBar || control is TextBoxBase && !((TextBoxBase)control).ReadOnly)
            {
                string name = string.IsNullOrWhiteSpace(control.Text) || control is TextBoxBase || control is NumericUpDown
                    ? Regex.Replace(control.Name, "^(numericUpDown|textBox|comboBox|checkBox|button|trackBar)_?", "").Replace('_', ' ')
                    : control.Text.Replace("&", "");

                return "Adjust " + name + ".";
            }

            return null;
        }

        private void Control_Added(object sender, ControlEventArgs e) { Register(e.Control); }
        private void Root_Disposed(object sender, EventArgs e) { Dispose(); }

        public void Dispose()
        {
            root.Disposed -= Root_Disposed;

            foreach (Control control in registered)
                control.ControlAdded -= Control_Added;

            registered.Clear();
            components.Dispose();
        }
    }
}
