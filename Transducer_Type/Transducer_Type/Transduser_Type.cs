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

        private double piezo_frequency_kHz = 55;
        public double Piezo_frequency_kHz
        {
            get { return piezo_frequency_kHz; }

            set
            {
                if (value != 55.0)
                    throw new ArgumentOutOfRangeException(nameof(value), $"NA");

                piezo_frequency_kHz = value;

                fancy_Lable_Control_P_Type_55._IsActive = true;
            }
        }
        public Transducer_Type()
        {
            InitializeComponent();
        }

        private void fancy_Lable_Control_P_Type_55_Fancy_Label_Clicked(object sender, EventArgs e)
        {
            //fancy_Lable_Control_P_Type_55._IsActive = false;

            //Fancy_Lable.Fancy_Lable_Control F = (Fancy_Lable.Fancy_Lable_Control)sender;

            //F._IsActive = true;

            //switch (F.Name)
            //{
            //    case "fancy_Lable_Control_P_Type_55":
            //        piezo_frequency_kHz = 55;
            //        break;
            //}

            //if (piezo_frequency_Changed != null)
            //{
            //    piezo_frequency_Changed(this, EventArgs.Empty);
            //}

            Piezo_frequency_kHz = 55.0;

            piezo_frequency_Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
