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

            set
            {
                reference_TOF_uSec = value;
                textBox_Reference.Text = value.ToString();
            }
        }

        private double discard_Time_uSec;
        public double Discard_Time_uSec
        {
            get { return discard_Time_uSec; }

            set
            {
                discard_Time_uSec = value;
                textBox_Discard.Text = value.ToString();
            }
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
            double reference;
            double discard;

            if (!double.TryParse(textBox_Reference.Text, out reference) || double.IsNaN(reference) || double.IsInfinity(reference))
            {
                MessageBox.Show("Enter a valid reference TOF.");
                return;
            }

            if (!double.TryParse(textBox_Discard.Text, out discard) || double.IsNaN(discard) || double.IsInfinity(discard) || discard < 0 || discard > 255)
            {
                MessageBox.Show("Discard time must be between 0 and 255 us.");
                return;
            }

            Reference_TOF_uSec = reference;
            Discard_Time_uSec = discard;

            Advanced_Settings_Changed?.Invoke(this, EventArgs.Empty);
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
