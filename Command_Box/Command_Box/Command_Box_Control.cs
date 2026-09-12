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

        public event EventHandler Save_Clicked;

        public event EventHandler Load_Clicked;

        public event EventHandler Firmware_Update_Clicked;

        private bool _isConnected = false;
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
            set {string save_Path = value; }
        }

        private string load_Path;
        public string Load_Path
        {
            get { return load_Path; }
            set { string load_Path = value;}
        }

        public Command_Box()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

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
            if (_isConnected == true)
            {
                if (_isLive == true)
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
                else
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
            }
            else
            {
                pictureBox_Run.Image = Properties.Resources.Run_Inactive;
               // pictureBox_Run.Image = Properties.Resources.Pause_Inactive;
            }
        }

        private void pictureBox_Run_Click(object sender, EventArgs e)
        {
            _isLive = !_isLive;

            Update_Live_Pic();
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
            if(_isConnected == true)
            {
                if(_isLive == true)
                {
                    pictureBox_Save.Image = Properties.Resources.Save_File_Inactive; // ok
                }
                else
                {
                    if(_isMouseEnter_Save == true)
                    {
                        if(_isMouseDown_Save == true)
                        {
                            pictureBox_Save.Image = Properties.Resources.Save_File;
                        }
                        else
                        {
                            pictureBox_Save.Image = Properties.Resources.Save_File_Active;
                        }
                        
                    }
                    else
                    {
                        pictureBox_Save.Image= Properties.Resources.Save_File;
                    }
                }
            }
            else
            {
                if(_isLive == true)
                {
                    if (_isMouseEnter_Save == true)
                    {
                        if (_isMouseDown_Save == true)
                        {
                            pictureBox_Save.Image = Properties.Resources.Save_File;
                        }
                        else
                        {
                            pictureBox_Save.Image = Properties.Resources.Save_File_Active;
                        }

                    }
                    else
                    {
                        pictureBox_Save.Image = Properties.Resources.Save_File;
                    }
                }

            }
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

            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                save_Path = saveFileDialog1.FileName;

                if(Save_Clicked != null)
                {
                    Save_Clicked(this, new EventArgs());
                }
            }
        }


        private void Update_Load_Pic()
        {
            if (_isConnected == true)
            {
                if (_isLive == true)
                {
                    pictureBox_load.Image = Properties.Resources.Load_File_Inactive; // ok
                }
                else
                {
                    if (_isMouseEnter_Load == true)
                    {
                        if (_isMouseDown_Load == true)
                        {
                            pictureBox_load.Image = Properties.Resources.Load_File;
                        }
                        else
                        {
                            pictureBox_load.Image = Properties.Resources.Load_File_Active;
                        }

                    }
                    else
                    {
                        pictureBox_load.Image = Properties.Resources.Load_File;
                    }
                }
            }
            else
            {
                if (_isLive == true)
                {
                    if (_isMouseEnter_Load == true)
                    {
                        if (_isMouseDown_Load == true)
                        {
                            pictureBox_load.Image = Properties.Resources.Load_File;
                        }
                        else
                        {
                            pictureBox_load.Image = Properties.Resources.Load_File_Active;
                        }

                    }
                    else
                    {
                        pictureBox_load.Image = Properties.Resources.Load_File;
                    }
                }

            }
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

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                load_Path = openFileDialog1.FileName;

                if(Load_Clicked != null)
                {
                    Load_Clicked(this, new EventArgs());
                }
            }
        }

        private void pictureBox_Save_Click(object sender, EventArgs e)
        {

        }

        private void Command_Box_Load(object sender, EventArgs e)
        {

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
    }
}
