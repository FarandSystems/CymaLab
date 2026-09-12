using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Averaging_Control
{
    public partial class Averaging_Control: UserControl
    {
        public event EventHandler AveragingChanged;

        private int averaging_Captures_Count = 1;
        public int Averaging_Captures_Count
        {
            get { return averaging_Captures_Count; }

            set
            {
                if (value != 1 && value != 4 && value != 8 && value != 16 && value != 32)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                averaging_Captures_Count = value;

                UpdateSelection();
            }
        }

        public Averaging_Control()
        {
            InitializeComponent();
        }

        private void UpdateSelection()
        {
            fancy_Lable_Control_None._IsActive = averaging_Captures_Count == 1;

            fancy_Lable_Control4._IsActive = averaging_Captures_Count == 4;

            fancy_Lable_Control8._IsActive = averaging_Captures_Count == 8;

            fancy_Lable_Control16._IsActive = averaging_Captures_Count == 16;

            fancy_Lable_Control32._IsActive = averaging_Captures_Count == 32;
        }



        private void groupBox_Averaging_Enter(object sender, EventArgs e)
        {

        }

        private void fancy_Lable_Control_None_Fancy_Label_Clicked(object sender, EventArgs e)
        {
            fancy_Lable_Control_None._IsActive = false;
            fancy_Lable_Control4._IsActive = false;
            fancy_Lable_Control8._IsActive = false;
            fancy_Lable_Control16._IsActive = false;
            fancy_Lable_Control32._IsActive = false;

            Fancy_Lable.Fancy_Lable_Control F = (Fancy_Lable.Fancy_Lable_Control)sender;

            F._IsActive = true;


            switch (F.Name)
            {
                case "fancy_Lable_Control_None":

                    averaging_Captures_Count = 1;

                    break;

                case "fancy_Lable_Control4":

                    averaging_Captures_Count = 4;

                    break;

                case "fancy_Lable_Control8":

                    averaging_Captures_Count = 8;

                    break;

                case "fancy_Lable_Control16":

                    averaging_Captures_Count = 16;

                    break;

                case "fancy_Lable_Control32":

                    averaging_Captures_Count = 32;

                    break;
            }

            if (AveragingChanged != null)
            {
                AveragingChanged(this, EventArgs.Empty);
            }

        }
    }
}
