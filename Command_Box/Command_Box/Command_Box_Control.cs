using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Command_Box
{
    public partial class Command_Box: UserControl
    {
        private bool _isMouseEnter_Load = false;

        private bool _isMouseDown_Load = false;

        private  bool _isMouseEnter_Save = false;

        private bool _isMouseDown_Save = false;

        private bool connection_Light = false;

        private bool _isMouseEnter_Run = false;

        public event EventHandler LiveKey_Clicked;
        public event EventHandler TestModeToggleRequested;

        public event EventHandler Save_Clicked;

        public event EventHandler Load_Clicked;

        public event EventHandler Firmware_Update_Clicked;

        private bool _isConnected = false;
        private bool isTestMode;

        public bool IsTestMode
        {
            get { return isTestMode; }
            set
            {
                isTestMode = value;
                Update_TestMode_Pic();
                Update_Live_Pic();
            }
        }

        public bool _IsConnected
        {
            get { return _isConnected; }
            set 
            { 
                _isConnected = value;
                if (_isConnected == true)
                {
                    timer1.Enabled = true;
                }

                Update_Live_Pic();
                Update_Save_Pic();
                Update_Load_Pic();
            }
        }

        private CommunicationModeEnum communicationMode = CommunicationModeEnum.Wifi;

        public CommunicationModeEnum CommunicationMode
        {
            get { return communicationMode; }
            set
            {
                if (!Enum.IsDefined(typeof(CommunicationModeEnum), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                communicationMode = value;

                UpdateCommunicationModeDisplay();
            }
        }

        public event EventHandler CommunicationModeChanged;

        private bool _isLive = true;
        public bool _IsLive
        {
            get { return _isLive; }
            set 
            {
                _isLive = value;
                Update_Live_Pic();
                Update_Save_Pic();
                Update_Load_Pic();
            }
        }

        private string save_Path;
        public string Save_Path
        {
            get { return save_Path; }
            set { save_Path = value; }
        }

        private string load_Path;
        public string Load_Path
        {
            get { return load_Path; }
            set { load_Path = value; }
        }

        public Command_Box()
        {
            InitializeComponent();
            Update_TestMode_Pic();
            Update_Save_Pic();
            Update_Load_Pic();
        }

        private void Update_TestMode_Pic()
        {
            pictureBox_TestMode.Image = isTestMode ? Properties.Resources.Mode_Test : Properties.Resources.Mode_Normal;
            //modeToolTip.SetToolTip(pictureBox_TestMode, isTestMode
            //    ? "Test mode — Ctrl+click to switch to normal mode"
            //    : "Normal mode — Ctrl+click to switch to test mode");
        }

        private void pictureBox_TestMode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Control) == Keys.Control)
                TestModeToggleRequested?.Invoke(this, EventArgs.Empty);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_isConnected == true)
            {
                connection_Light = !connection_Light;
                if (connection_Light == true)
                {
                    pictureBox_Connected.Image = Properties.Resources.Connected_Inactive;

                }
                else
                {
                    pictureBox_Connected.Image = Properties.Resources.Connected;
                }
            }
            else
            {
                pictureBox_Connected.Image= Properties.Resources.NotConnected;
                timer1.Enabled = false;
            }
            
        }

        private void Update_Live_Pic()
        {
            if (_isConnected || isTestMode)
            {
                if (_isLive == true)
                {
                    if (_isMouseEnter_Run == true)
                    {
                        pictureBox_Run.Image = Properties.Resources.Pause_Active;
                    }
                    else
                    {
                        pictureBox_Run.Image = Properties.Resources.Pause;
                    }

                }
                else
                {
                    if (_isMouseEnter_Run == true)
                    {
                        pictureBox_Run.Image = Properties.Resources.Run_Active;
                    }
                    else
                    {
                        pictureBox_Run.Image = Properties.Resources.Run;
                    }
                }
            }
            else
            {
                pictureBox_Run.Image = Properties.Resources.Run_Inactive;
            }
        }

        private void pictureBox_Run_Click(object sender, EventArgs e)
        {
            if (!_isConnected && !isTestMode)
                return;

            LiveKey_Clicked?.Invoke(this, EventArgs.Empty);
        }

        private void pictureBox_Run_MouseEnter(object sender, EventArgs e)
        {
            _isMouseEnter_Run = true;

            Update_Live_Pic();

        }

        private void pictureBox_Run_MouseLeave(object sender, EventArgs e)
        {
            _isMouseEnter_Run =false;

            Update_Live_Pic();
        }

        private void Update_Save_Pic()
        {
            pictureBox_Save.Enabled = !_isLive;
            if (_isLive)
            {
                _isMouseEnter_Save = false;
                _isMouseDown_Save = false;
                pictureBox_Save.Image = Properties.Resources.Save_File_Inactive;
                return;
            }

            pictureBox_Save.Image = _isMouseEnter_Save && !_isMouseDown_Save
                ? Properties.Resources.Save_File_Active
                : Properties.Resources.Save_File;
        }
        private void UpdateCommunicationModeDisplay()
        {
            bool useUsb = communicationMode == CommunicationModeEnum.USB;

            pictureBox_Connection_Mode.Image = useUsb ? Properties.Resources.Usb : Properties.Resources.Wifi;

            pictureBox_Firmware_Update.Enabled = useUsb;
        }


        private void pictureBox_Save_MouseEnter(object sender, EventArgs e)
        {
            _isMouseEnter_Save = true;

            Update_Save_Pic();
        }

        private void pictureBox_Save_MouseLeave(object sender, EventArgs e)
        {
            _isMouseEnter_Save = false;

            Update_Save_Pic();

        }

        private void pictureBox_Save_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown_Save = true;

            Update_Save_Pic();
        }


        private void pictureBox_Save_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown_Save = false;
            Update_Save_Pic();
        }
        private void Update_Load_Pic()
        {
            pictureBox_load.Enabled = !_isLive;
            if (_isLive)
            {
                _isMouseEnter_Load = false;
                _isMouseDown_Load = false;
                pictureBox_load.Image = Properties.Resources.Load_File_Inactive;
                return;
            }

            pictureBox_load.Image = _isMouseEnter_Load && !_isMouseDown_Load
                ? Properties.Resources.Load_File_Active
                : Properties.Resources.Load_File;
        }
        private void pictureBox_load_MouseEnter(object sender, EventArgs e)
        {
            _isMouseEnter_Load = true;

            Update_Load_Pic();
        }

        private void pictureBox_load_MouseLeave(object sender, EventArgs e)
        {
            _isMouseEnter_Load = false;

            Update_Load_Pic();
        }

        private void pictureBox_load_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown_Load = true;

            Update_Load_Pic();
        }

        private void pictureBox_load_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown_Load = false;
            Update_Load_Pic();
        }
        private void pictureBox_Save_Click(object sender, EventArgs e)
        {
            if (!_isLive && pictureBox_Save.Enabled)
                Save_Clicked?.Invoke(this, EventArgs.Empty);
        }

        private void pictureBox_load_Click(object sender, EventArgs e)
        {
            if (!_isLive && pictureBox_load.Enabled)
                Load_Clicked?.Invoke(this, EventArgs.Empty);
        }

        private void Command_Box_Load(object sender, EventArgs e)
        {
            UpdateCommunicationModeDisplay();
        }

        private void pictureBox_Firmware_Update_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_Firmware_Update.Image = Properties.Resources.Firmware_Update;
        }

        private void pictureBox_Firmware_Update_Click(object sender, EventArgs e)
        {
            if (Firmware_Update_Clicked != null)
            {
                Firmware_Update_Clicked(this, new EventArgs());
            }
        }

        private void pictureBox_Firmware_Update_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_Firmware_Update.Image = Properties.Resources.Firmware_Update_Highlighted;
        }

        private void SelectCommunicationMode(CommunicationModeEnum mode)
        {
            if (communicationMode == mode)
                return;

            CommunicationMode = mode;

            CommunicationModeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void pictureBox_Connection_Mode_Click(object sender, EventArgs e)
        {
            if (communicationMode == CommunicationModeEnum.USB)
            {
                SelectCommunicationMode(CommunicationModeEnum.Wifi);

            }
            else
            {
                SelectCommunicationMode(CommunicationModeEnum.USB);
            }

            UpdateCommunicationModeDisplay();
        }
    }

    public enum CommunicationModeEnum
    {
        USB,
        Wifi
    }
}
