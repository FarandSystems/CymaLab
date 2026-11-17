using System;
using System.Media;
using System.Windows.Forms;

namespace Signal_Strength_Control
{
    public partial class Signal_Strength_Control : UserControl
    {
        private readonly SoundPlayer selectionSound = new SoundPlayer(Properties.Resources.beep_07a);

        public int Reciever_Sensitivity
        {
            get { return amplifierSelector.Value; }
            set { amplifierSelector.Value = value; }
        }

        public int Transmitter_Power
        {
            get { return powerSelector.Value; }
            set { powerSelector.Value = value; }
        }

        public event EventHandler Signal_Intensity_isChanged;

        public Signal_Strength_Control()
        {
            InitializeComponent();
            powerSelector.ValueChanged += Selection_Changed;
            amplifierSelector.ValueChanged += Selection_Changed;
        }

        private void Selection_Changed(object sender, EventArgs e)
        {
            // Only user changes notify the main app; restoring settings stays silent.
            Signal_Intensity_isChanged?.Invoke(this, EventArgs.Empty);
            selectionSound.Play();
        }
    }
}
