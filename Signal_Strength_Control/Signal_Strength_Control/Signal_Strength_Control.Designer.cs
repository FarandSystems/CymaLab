namespace Signal_Strength_Control
{
    partial class Signal_Strength_Control
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel selectorLayout;
        private System.Windows.Forms.GroupBox groupBox_SignalIntensity;
        private LevelSelector powerSelector;
        private LevelSelector amplifierSelector;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                selectionSound.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.selectorLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_SignalIntensity = new System.Windows.Forms.GroupBox();
            this.powerSelector = new LevelSelector();
            this.amplifierSelector = new LevelSelector();
            this.selectorLayout.SuspendLayout();
            this.groupBox_SignalIntensity.SuspendLayout();
            this.SuspendLayout();
            this.powerSelector.Caption = "Transmitter power";
            this.powerSelector.Maximum = 8;
            this.powerSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.powerSelector.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.powerSelector.Name = "powerSelector";
            this.powerSelector.SetButtonNames("button_PowerDecrease", "button_PowerIncrease");
            this.amplifierSelector.Caption = "Amplifier gain";
            this.amplifierSelector.Maximum = 4;
            this.amplifierSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.amplifierSelector.Margin = new System.Windows.Forms.Padding(0);
            this.amplifierSelector.Name = "amplifierSelector";
            this.amplifierSelector.SetButtonNames("button_AmplifierDecrease", "button_AmplifierIncrease");
            this.selectorLayout.ColumnCount = 1;
            this.selectorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.selectorLayout.RowCount = 2;
            this.selectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.selectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.selectorLayout.Controls.Add(this.powerSelector, 0, 0);
            this.selectorLayout.Controls.Add(this.amplifierSelector, 0, 1);
            this.selectorLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectorLayout.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.selectorLayout.Name = "selectorLayout";
            this.groupBox_SignalIntensity.Controls.Add(this.selectorLayout);
            this.groupBox_SignalIntensity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_SignalIntensity.Font = new System.Drawing.Font("Arial", 12F);
            this.groupBox_SignalIntensity.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.groupBox_SignalIntensity.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_SignalIntensity.Name = "groupBox_SignalIntensity";
            this.groupBox_SignalIntensity.TabStop = false;
            this.groupBox_SignalIntensity.Text = "Signal intensity";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.Controls.Add(this.groupBox_SignalIntensity);
            this.MinimumSize = new System.Drawing.Size(248, 132);
            this.Name = "Signal_Strength_Control";
            this.Size = new System.Drawing.Size(387, 141);
            this.selectorLayout.ResumeLayout(false);
            this.groupBox_SignalIntensity.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
