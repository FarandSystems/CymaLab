using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filter_Control
{
    public partial class Filter_Control : UserControl
    {
        public enum Filter_Mode_Enum
        {
            No_Filter,
            Weak_BandPass,
            Strong_BandPass
        }

        public event EventHandler Filter_Mode_Changed;
        private Filter_Mode_Enum filter_Mode = Filter_Mode_Enum.No_Filter;
        public Filter_Mode_Enum Filter_Mode
        {
            get { return filter_Mode; }
            set { filter_Mode = value; }
        }
    
        public Filter_Control()
        {
            InitializeComponent();
        }

        private void Update_Filter_Icon()
        {
            if(Band_Pass_Filter_label._IsActive == true)
            {
                if (filter_Mode == Filter_Mode_Enum.Strong_BandPass)
                {
                    pictureBox_Filter_Icon.Image = Properties.Resources.Band_Pass_Filter_High;
                }
                else
                {
                    pictureBox_Filter_Icon.Image = Properties.Resources.Band_Pass_Filter_Low;
                }
            }
            else
            {
                pictureBox_Filter_Icon.Image = Properties.Resources.Band_Pass_Filter_Inactive;
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }


        private void pictureBox_Filter_Icon_Click(object sender, EventArgs e)
        {
            if(filter_Mode == Filter_Mode_Enum.Strong_BandPass)
            {
                filter_Mode = Filter_Mode_Enum.Weak_BandPass;
            }
            else if (filter_Mode == Filter_Mode_Enum.Weak_BandPass)
            {
                filter_Mode = Filter_Mode_Enum.Strong_BandPass;
            }

            Update_Filter_Icon();

            if (Filter_Mode_Changed != null)
            {
                Filter_Mode_Changed(this, EventArgs.Empty);
            }

        }

        private void Band_Pass_Filter_label_Fancy_Label_Clicked(object sender, EventArgs e)
        {
            if (Band_Pass_Filter_label._IsActive == true)
            {
                Band_Pass_Filter_label.Fancy_lable_text = "Band Pass Filter: ON";
                filter_Mode = Filter_Mode_Enum.Weak_BandPass;
            }
            else
            {
                Band_Pass_Filter_label.Fancy_lable_text = "Band Pass Filter: OFF";
                filter_Mode = Filter_Mode_Enum.No_Filter;
            }
            Update_Filter_Icon();

            if (Filter_Mode_Changed != null)
            {
                Filter_Mode_Changed(this, EventArgs.Empty);
            }

        }

        private void Center_Frequency_label_Load(object sender, EventArgs e)
        {

        }
    }
}
