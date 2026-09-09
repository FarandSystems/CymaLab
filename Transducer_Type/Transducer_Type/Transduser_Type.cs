using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transducer_Type
{
    public partial class Transducer_Type: UserControl
    {
        public event EventHandler piezo_frequency_Changed;

        private double piezo_frequency_kHz = 20;
        public double Piezo_frequency_kHz
        {
            get { return piezo_frequency_kHz; }
            set { piezo_frequency_kHz = value;}
        }
        public Transducer_Type()
        {
            InitializeComponent();
        }

        private void fancy_Lable_Control_P_Type_20_Fancy_Label_Clicked(object sender, EventArgs e)
        {
            fancy_Lable_Control_P_Type_20._IsActive = false;
            fancy_Lable_Control_P_Type_40._IsActive = false;

            Fancy_Lable.Fancy_Lable_Control F = (Fancy_Lable.Fancy_Lable_Control)sender;

            F._IsActive = true;

            switch (F.Name)
            {
                case "fancy_Lable_Control_P_Type_20":
                    piezo_frequency_kHz = 20;
                    break;

                case "fancy_Lable_Control_P_Type_40":
                    piezo_frequency_kHz = 40;
                    break;
            }

            if (piezo_frequency_Changed != null)
            {
                piezo_frequency_Changed(this, EventArgs.Empty);
            }
        }
    }
}
