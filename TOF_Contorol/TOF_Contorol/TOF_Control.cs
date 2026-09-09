using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOF_Contorol
{
    public partial class TOF_Control: UserControl
    {
        string key_State_Increase = "Idle";
        string key_State_Decrease = "Idle";

        int time_Counter_Increase = 0;
        int time_Counter_Decrease = 0;

        public event EventHandler TOF_Changed;

        public event EventHandler Close_Button_Clicked;

        private bool increase_MouseEnter = false;

        private bool increase_MouseDown = false;

        private bool decrease_MouseEnter = false;

        private bool decrease_MouseDown = false;


        private bool auto_TOF = false;
        public bool Auto_TOF
        {
            get { return auto_TOF; }
            set 
            {
                auto_TOF = value;
                Update_Key_Pic();
                Update_Increase_Pic();
                Update_Decrease_Pic();
            }
        }

        private bool tOF_Stable = false;
        public bool TOF_Stable
        {
            get { return tOF_Stable; }
            set 
            {
                tOF_Stable = value;

                Update_TOF_Pic();
            }
        }

        private double tOF_mSec = 0.1 ;
        public double TOF_mSec
        {
            get { return tOF_mSec; }
            set { tOF_mSec = value; }
        }

        private double sample_Length_cm = 30;
        public double Sample_Length_cm
        {
            get { return sample_Length_cm; }
            set 
            {
                sample_Length_cm = value;

                Update_Measurement_Result();
            }
        }

        private double sample_Velocity_m_Sec = 3000;
        public double Sample_Velocity_m_Sec
        {
            get { return sample_Velocity_m_Sec; }
            set 
            {
                sample_Velocity_m_Sec = value;

                Update_Measurement_Result();
            }
        }
        public TOF_Control()
        {
            InitializeComponent();
        }

        private void Update_Key_Pic()
        {
            if (auto_TOF == false)
            {
                pictureBox_TOF_Key.Image = Properties.Resources.TOF_Key_Manual;
            }
            else
            {
                pictureBox_TOF_Key.Image = Properties.Resources.TOF_Key_Auto;

            }
        }

        private void Update_Increase_Pic()
        {
            if(auto_TOF == true)
            {
                pictureBox_TOF_Increase.Image = Properties.Resources.Increase_TOF_Inactive;
            }
            else
            {
                if(increase_MouseEnter == false || increase_MouseDown == false)
                {
                    pictureBox_TOF_Increase.Image = Properties.Resources.Increase_TOF_Active;
                }
                else
                {
                    pictureBox_TOF_Increase.Image = Properties.Resources.Increase_TOF_Active_Highlight;
                }

            }
        }

        private void Update_Decrease_Pic()
        {
            if (auto_TOF == true)
            {
                pictureBox_TOF_Decrease.Image = Properties.Resources.Decrease_TOF_Inactive;
            }
            else
            {
                if(decrease_MouseEnter == false || decrease_MouseDown == false)
                {
                    pictureBox_TOF_Decrease.Image = Properties.Resources.Decrease_TOF_Active;
                }
                else
                {
                    pictureBox_TOF_Decrease.Image = Properties.Resources.Decrease_TOF_Active_Highlight;
                }
            }
        }

        private void Update_Time_Of_Flight()
        {
            label_Time_Of_Flight.Text = "Time Of Flight (mSec) = " + Math.Round(TOF_mSec, 3);

            if (TOF_Changed != null)
            {
                TOF_Changed(this, EventArgs.Empty);
            }
        }

        private void pictureBox_TOF_Key_Click(object sender, EventArgs e)
        {
         
            auto_TOF = !auto_TOF;
            Update_Key_Pic();
            Update_Increase_Pic();
            Update_Decrease_Pic();
       
        }

        private void pictureBox_TOF_Increase_Click(object sender, EventArgs e)
        {
            if(auto_TOF == false)
            {
                tOF_mSec += 0.01;

            }
            Update_Increase_Pic();
            Update_Time_Of_Flight();
            
        }

        private void pictureBox_TOF_Decrease_Click(object sender, EventArgs e)
        {
            if(auto_TOF == false)
            {
                tOF_mSec -= 0.01;

            }
            Update_Decrease_Pic();
            Update_Time_Of_Flight();
            
        }

        private void pictureBox_TOF_Increase_MouseEnter(object sender, EventArgs e)
        {
            increase_MouseEnter = true;
            Update_Increase_Pic();
        }

        private void pictureBox_TOF_Increase_MouseLeave(object sender, EventArgs e)
        {
            increase_MouseEnter = false;
            Update_Increase_Pic();
        }

        private void pictureBox_TOF_Decrease_MouseEnter(object sender, EventArgs e)
        {
            decrease_MouseEnter = true;
            Update_Decrease_Pic();
        }

        private void pictureBox_TOF_Decrease_MouseLeave(object sender, EventArgs e)
        {
            decrease_MouseEnter = false;
            Update_Decrease_Pic();
        }

        private void TOF_Control_Load(object sender, EventArgs e)
        {
            timer_Increase.Enabled = true;
            timer_Decrease.Enabled = true;
            Update_Key_Pic();
            Update_Increase_Pic();
            Update_Decrease_Pic();
            Update_Measurement_Result();
            Update_Time_Of_Flight();
        }

        private void pictureBox_TOF_Increase_MouseDown(object sender, MouseEventArgs e)
        {
           increase_MouseDown = true;

            Update_Increase_Pic();
        }

        private void pictureBox_TOF_Increase_MouseUp(object sender, MouseEventArgs e)
        {
            increase_MouseDown = false;

            Update_Increase_Pic();
        }

        private void pictureBox_TOF_Decrease_MouseDown(object sender, MouseEventArgs e)
        {
            decrease_MouseDown = true;

            Update_Decrease_Pic();
        }

        private void pictureBox_TOF_Decrease_MouseUp(object sender, MouseEventArgs e)
        {
            decrease_MouseDown = false;

            Update_Decrease_Pic();
        }

        private void timer_Increase_Tick(object sender, EventArgs e)
        {
            switch (key_State_Increase)
            {

                case "Idle":
                    if(increase_MouseDown == true && auto_TOF == false)
                    {
                        key_State_Increase = "Wait";
                        time_Counter_Increase = 0;
                    }

                    break;

                case "Wait":
                    time_Counter_Increase++;
                    if(time_Counter_Increase == 10)
                    {
                        key_State_Increase = "Change";
                    }

                    if (increase_MouseDown == false)
                    {
                        key_State_Increase = "Idle";
                    }

                    break;

                case "Change":
                    tOF_mSec += 0.01;
                    Update_Time_Of_Flight();
                    time_Counter_Increase++;

                    if (time_Counter_Increase == 30)
                    {
                        key_State_Increase = "Fast_Change";
                    }

                    if (increase_MouseDown == false)
                    {
                        key_State_Increase = "Idle";
                    }

                    break;

                case "Fast_Change":
                    tOF_mSec += 0.1;
                    Update_Time_Of_Flight();
                    if(increase_MouseDown == false)
                    {
                        key_State_Increase = "Idle";
                    }
                    break;
            }
        }

        private void timer_Decrease_Tick(object sender, EventArgs e)
        {
            switch (key_State_Decrease)
            {

                case "Idle":
                    if (decrease_MouseDown == true && auto_TOF == false)
                    {
                        key_State_Decrease = "Wait";
                        time_Counter_Decrease = 0;
                    }

                    break;

                case "Wait":
                    time_Counter_Decrease++;
                    if (time_Counter_Decrease == 10)
                    {
                        key_State_Decrease = "Change";
                    }

                    if (decrease_MouseDown == false)
                    {
                        key_State_Decrease = "Idle";
                    }

                    break;

                case "Change":
                    tOF_mSec -= 0.01;
                    Update_Time_Of_Flight();
                    time_Counter_Decrease++;

                    if (time_Counter_Decrease == 30)
                    {
                        key_State_Decrease = "Fast_Change";
                    }

                    if (decrease_MouseDown == false)
                    {
                        key_State_Decrease = "Idle";
                    }

                    break;

                case "Fast_Change":
                    tOF_mSec -= 0.1;
                    Update_Time_Of_Flight();
                    if (decrease_MouseDown == false)
                    {
                        key_State_Decrease = "Idle";
                    }
                    break;
            }
        }

        private void Update_Measurement_Result()
        {
            label_Measurement_Result.Text = "L (cm) = " + sample_Length_cm.ToString() + ",  V (m/Sec) = " + sample_Velocity_m_Sec.ToString();
        }

        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            if(Close_Button_Clicked != null)
            {
                Close_Button_Clicked(this, EventArgs.Empty);
            }
        }

        private void Update_TOF_Pic()
        {
            if(tOF_Stable == false)
            {
                pictureBox_TOF_Light.Image = Properties.Resources.LED2_Green_OFF;
            }
            else
            {
                pictureBox_TOF_Light.Image = Properties.Resources.LED2_Green_ON;

            }
        }
    }
}
