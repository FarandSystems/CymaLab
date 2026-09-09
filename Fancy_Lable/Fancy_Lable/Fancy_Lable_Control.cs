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

namespace Fancy_Lable
{
    public partial class Fancy_Lable_Control: UserControl
    {
        public event EventHandler Fancy_Label_Clicked;

        private bool _isToggleSwitch = true;
        public bool _IsToggleSwitch
        {
            get { return _isToggleSwitch; }
            set 
            { 
                _isToggleSwitch = value;
                
            }
        }


        private bool _isMouseEnter = false;

        bool _isActive = false;
        public bool _IsActive 
        { 
            get{ return _isActive;}
            set
            { 
                _isActive = value;
                Update_Fancy_Lable();
            }
        }

        private string fancy_lable_text = "Default Text";
        public string Fancy_lable_text
        {
            get { return fancy_lable_text; }
            set
            {
                fancy_lable_text = value;
                label_Fancy.Text = value;
            }
        }

        private Color active_Normal_Color = Color.LimeGreen;
        public Color Active_Normal_Color
        {
            get { return active_Normal_Color; }
            set {
                active_Normal_Color = value;
                Update_Fancy_Lable();
                }
        }

        private Color active_Highlight_Color = Color.Chartreuse;
        public Color Active_Highlight_Color
        {
            get { return active_Highlight_Color; }
            set
            {
                active_Highlight_Color = value;
                Update_Fancy_Lable();
            }
        }

        private Color inactive_Normal_Color = Color.DarkGray;
        public Color Inactive_Normal_Color
        {
            get { return inactive_Normal_Color; }
            set
            {
                inactive_Normal_Color = value;
                Update_Fancy_Lable();
            }
        }

        private Color inactive_Highlight_Color = Color.LightGray;
        public Color Inactive_Highlight_Color
        {
            get { return inactive_Highlight_Color; }
            set
            {
                inactive_Highlight_Color = value;
                Update_Fancy_Lable();
            }
        }

        private Font fancy_Lable_Font = new Font("Arial",12,FontStyle.Regular);
        public Font Fancy_Lable_Font
        {
            get { return fancy_Lable_Font; }
            set 
            {     fancy_Lable_Font = value;
                  Update_Fancy_Lable();
            }
        }

        public Fancy_Lable_Control()
        {
            InitializeComponent();
        }


        private void Update_Fancy_Lable()
        {
            label_Fancy.Font = Fancy_Lable_Font;
            label_Fancy.Text = fancy_lable_text;
            if (_isMouseEnter == true)
            {
                if (_isActive == true)
                {
                    
                    label_Fancy.ForeColor = active_Highlight_Color;
                }
                else
                {
                    
                    label_Fancy.ForeColor = inactive_Highlight_Color;
                }
            }
            else
            {
                if (_isActive == true)
                {
                    label_Fancy.ForeColor = active_Normal_Color;
                }
                else
                {
                    label_Fancy.ForeColor = inactive_Normal_Color;
                }
            }
        }

       

        private void UserControl1_Load(object sender, EventArgs e)
        {
            Update_Fancy_Lable();
           
        }

        private void label_Fancy_Click(object sender, EventArgs e)
        {
            if (_isToggleSwitch == true)
            {
                SoundPlayer Mybeep = new SoundPlayer(Properties.Resources.Electronic_Short_Beep_High_Loud_6);
                Mybeep.Play();
                _isActive = !_isActive;
                Update_Fancy_Lable();
                
            }

            if (Fancy_Label_Clicked != null)
            {
                Fancy_Label_Clicked(this, EventArgs.Empty);
            }

        }

        private void label_Fancy_MouseEnter(object sender, EventArgs e)
        {
            
            _isMouseEnter = true;
            Update_Fancy_Lable();
        }

        private void label_Fancy_MouseLeave(object sender, EventArgs e)
        {
            _isMouseEnter = false;
            Update_Fancy_Lable();
        }
    }
}
