using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Advanced_Settings_Control
{
    public partial class Advanced_Settings_Control: UserControl
    {
        public event EventHandler Advanced_Settings_Changed;

        private bool _isMouseEnter = false;
        
        private bool _isMouseDown = false;

        private double reference_TOF_uSec;
        public double Reference_TOF_uSec
        {
            get { return reference_TOF_uSec; }
            set { reference_TOF_uSec = value; }
        }

        private double discard_Time_uSec;
        public double Discard_Time_uSec
        {
            get { return discard_Time_uSec; }
            set { discard_Time_uSec = value; }
        }
        public Advanced_Settings_Control()
        {
            InitializeComponent();
        }

        private void Update_Set_Pic()
        {
            if (_isMouseDown == false)
            {
                if (_isMouseEnter == false)
                {
                    pictureBox_Set.Image = Properties.Resources.Set_Key_Normal;
                }
                else
                {
                    pictureBox_Set.Image = Properties.Resources.Set_Key_Highlighted;
                }
            }
            else 
            {
                pictureBox_Set.Image = Properties.Resources.Set_Key_Inactive;
            }
            
        }

        private void pictureBox_Set_Click(object sender, EventArgs e)
        {
            try
            {
                reference_TOF_uSec = Convert.ToDouble(textBox_Reference.Text);
            }
            catch 
            {

                MessageBox.Show("Invalid input!" + "\r\n" + "Defualt value for Reference TOF will be loaded." , "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_Reference.Text = "100";
            }

            try
            {
                discard_Time_uSec = Convert.ToDouble(textBox_Discard.Text);
            }
            catch 
            {

                MessageBox.Show("Invalid input!" + "\r\n" + "Defualt value for Discard Time will be loaded.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_Discard.Text = "20";
            }
            
            if(Advanced_Settings_Changed != null)
            {
                Advanced_Settings_Changed(this, EventArgs.Empty);
            }

            
        }

        private void pictureBox_Set_MouseEnter(object sender, EventArgs e)
        {
            _isMouseEnter = true;

            Update_Set_Pic();
        }

        private void pictureBox_Set_MouseLeave(object sender, EventArgs e)
        {
            _isMouseEnter = false;

            Update_Set_Pic();
        }

        private void pictureBox_Set_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown = true;

            Update_Set_Pic();
        }

        private void pictureBox_Set_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;

            Update_Set_Pic();
        }
    }
}
