using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Signal_Strength_Control
{
    public partial class Signal_Strength_Control: UserControl
    {
        SoundPlayer sp = new SoundPlayer(Properties.Resources.beep_07a);

        private int reciever_Sensitivity = 1;
        public int Reciever_Sensitivity
        {
            get { return reciever_Sensitivity; }
            set { reciever_Sensitivity = value; }
        }

        private int transmitter_Power = 1;
        public int Transmitter_Power
        {
            get { return transmitter_Power; }
            set { transmitter_Power = value; }
        }

        public event EventHandler Signal_Intensity_isChanged;
        public Signal_Strength_Control()
        {
            InitializeComponent();
        }

        private void Trun_Off_All_Receiver_LEDs()
        {
            pictureBox_Receiver1.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Receiver2.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Receiver3.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Receiver4.Image = Properties.Resources.LED2_Green_OFF;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Receiver1_Click(object sender, EventArgs e)
        {
            reciever_Sensitivity = 1;

            Trun_Off_All_Receiver_LEDs();
            pictureBox_Receiver1.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Receiver2_Click(object sender, EventArgs e)
        {
            reciever_Sensitivity = 2;

            Trun_Off_All_Receiver_LEDs();
            pictureBox_Receiver1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Receiver2.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Receiver3_Click(object sender, EventArgs e)
        {
            reciever_Sensitivity = 3;

            Trun_Off_All_Receiver_LEDs();
            pictureBox_Receiver1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Receiver2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Receiver3.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Receiver4_Click(object sender, EventArgs e)
        {
            reciever_Sensitivity = 4;

            Trun_Off_All_Receiver_LEDs();
            pictureBox_Receiver1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Receiver2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Receiver3.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Receiver4.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void Trun_Off_All_Transducer_LEDs()
        {
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Transducer3.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Transducer4.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Transducer5.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Transducer6.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Transducer7.Image = Properties.Resources.LED2_Green_OFF;
            pictureBox_Transducer8.Image = Properties.Resources.LED2_Green_OFF;
        }

        private void pictureBox_Transducer1_Click(object sender, EventArgs e)
        {
            transmitter_Power = 1;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Transducer2_Click(object sender, EventArgs e)
        {
            transmitter_Power = 2;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Transducer3_Click(object sender, EventArgs e)
        {
            transmitter_Power = 3;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer3.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Transducer4_Click(object sender, EventArgs e)
        {
            transmitter_Power = 4;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer3.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer4.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Transducer5_Click(object sender, EventArgs e)
        {
            transmitter_Power = 5;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer3.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer4.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer5.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Transducer6_Click(object sender, EventArgs e)
        {
            transmitter_Power = 6;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer3.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer4.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer5.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer6.Image = Properties.Resources.LED2_Green_ON;

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Transducer7_Click(object sender, EventArgs e)
        {
            transmitter_Power = 7;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer3.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer4.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer5.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer6.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer7.Image = Properties.Resources.LED2_Green_ON;
            

            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }

        private void pictureBox_Transducer8_Click(object sender, EventArgs e)
        {
            transmitter_Power = 8;

            Trun_Off_All_Transducer_LEDs();
            pictureBox_Transducer1.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer2.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer3.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer4.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer5.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer6.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer7.Image = Properties.Resources.LED2_Green_ON;
            pictureBox_Transducer8.Image = Properties.Resources.LED2_Green_ON;


            if (Signal_Intensity_isChanged != null)
            {
                Signal_Intensity_isChanged(this, EventArgs.Empty);
            }

            sp.Play();
        }
    }
}
