using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Measurement_Mode_Control
{
    public partial class Measurement_Mode_Control: UserControl
    {
        string length_Text_Prev = "";
        string velocity_Text_Prev = "";

        private bool updatingControls;

        public event EventHandler Measurement_Mode_isChanged;

        public enum Measurement_Mode_Enum
        {
            Velocity_Calculation,
            Length_Calculation

        }

        private Measurement_Mode_Enum measurement_Mode = Measurement_Mode_Enum.Length_Calculation;
        public Measurement_Mode_Enum Measurement_Mode
        {
            get { return measurement_Mode; }

            set
            {
                measurement_Mode = value;
                UpdateModeSelection();
            }
        }

        private double velocity;
        private double length;

        public double Length
        {
            get { return length; }

            set
            {
                updatingControls = true;

                try
                {
                    length = value;
                    length_Text_Prev = value.ToString("0.00");
                    textBox_Sample_Length.Text = length_Text_Prev;
                }
                finally
                {
                    updatingControls = false;
                }
            }
        }

        public double Velocity
        {
            get { return velocity; }

            set
            {
                updatingControls = true;

                try
                {
                    velocity = value;
                    velocity_Text_Prev = value.ToString("0.00");
                    textBox_Sample_Velocity.Text = velocity_Text_Prev;
                }
                finally
                {
                    updatingControls = false;
                }
            }
        }


        public Measurement_Mode_Control()
        {
            InitializeComponent();
        }

        private void UpdateModeSelection()
        {
            bool calculateVelocity = measurement_Mode == Measurement_Mode_Enum.Velocity_Calculation;

            fancy_Lable_Control_Velocity._IsActive = calculateVelocity;
            fancy_Lable_Control_Length._IsActive = !calculateVelocity;

            // Calculating velocity requires a known sample length.
            groupBox_Sample_Length.Enabled = calculateVelocity;

            // Calculating length requires a known sample velocity.
            groupBox_Sample_Velocity.Enabled = !calculateVelocity;
        }

        private void label_Velocity_Click(object sender, EventArgs e)
        {

        }

        private void fancy_Lable_Control_Velocity_Fancy_Label_Clicked(object sender, EventArgs e)
        {
            Measurement_Mode = Measurement_Mode_Enum.Velocity_Calculation;

            Measurement_Mode_isChanged?.Invoke(this, EventArgs.Empty);
        }

        private void fancy_Lable_Control_Length_Fancy_Label_Clicked(object sender, EventArgs e)
        {
            Measurement_Mode = Measurement_Mode_Enum.Length_Calculation;

            Measurement_Mode_isChanged?.Invoke(this, EventArgs.Empty);
        }

        private void textBox_Sampel_Length_TextChanged(object sender, EventArgs e)
        {
            if (updatingControls)
                return;

            try
            {
                if(textBox_Sample_Length.Text == "")
                {
                    textBox_Sample_Length.Text = "0";
                }
                length = Convert.ToDouble(textBox_Sample_Length.Text);
                length_Text_Prev = textBox_Sample_Length.Text;

                if (Measurement_Mode_isChanged != null)
                {
                    Measurement_Mode_isChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {

                if(length_Text_Prev != "")
                {
                    textBox_Sample_Length.Text = length_Text_Prev;
                }
                else
                {
                    textBox_Sample_Length.Text = "0";
                }
            }
            
            textBox_Sample_Length.SelectionStart = textBox_Sample_Length.Text.Length;
            textBox_Sample_Length.SelectionLength = 0;
        }

        private void textBox_Sample_Velocity_TextChanged(object sender, EventArgs e)
        {
            if (updatingControls)
                return;

            try
            {
                if (textBox_Sample_Velocity.Text == "")
                {
                    textBox_Sample_Velocity.Text = "0";
                }
                velocity = Convert.ToDouble(textBox_Sample_Velocity.Text);
                velocity_Text_Prev = textBox_Sample_Velocity.Text;

                if (Measurement_Mode_isChanged != null)
                {
                    Measurement_Mode_isChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {

                if (velocity_Text_Prev != "")
                {
                    textBox_Sample_Velocity.Text = velocity_Text_Prev;
                }
                else
                {
                    textBox_Sample_Velocity.Text = "0";
                }
            }

            textBox_Sample_Velocity.SelectionStart = textBox_Sample_Velocity.Text.Length;
            textBox_Sample_Velocity.SelectionLength = 0;
        }

        private void fancy_Lable_Control_Length_Load(object sender, EventArgs e)
        {

        }
    }
}
