namespace Filter_Control
{
    partial class Filter_Control
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_Filter_Icon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Center_Frequency_label = new Fancy_Lable.Fancy_Lable_Control();
            this.Band_Pass_Filter_label = new Fancy_Lable.Fancy_Lable_Control();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Filter_Icon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Filter_Icon
            // 
            this.pictureBox_Filter_Icon.Image = global::Filter_Control.Properties.Resources.Band_Pass_Filter_Inactive;
            this.pictureBox_Filter_Icon.Location = new System.Drawing.Point(301, 15);
            this.pictureBox_Filter_Icon.Name = "pictureBox_Filter_Icon";
            this.pictureBox_Filter_Icon.Size = new System.Drawing.Size(82, 74);
            this.pictureBox_Filter_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Filter_Icon.TabIndex = 2;
            this.pictureBox_Filter_Icon.TabStop = false;
            this.pictureBox_Filter_Icon.Click += new System.EventHandler(this.pictureBox_Filter_Icon_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Center_Frequency_label);
            this.groupBox1.Controls.Add(this.Band_Pass_Filter_label);
            this.groupBox1.Controls.Add(this.pictureBox_Filter_Icon);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 99);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Mode";
            // 
            // Center_Frequency_label
            // 
            this.Center_Frequency_label._IsActive = false;
            this.Center_Frequency_label._IsToggleSwitch = false;
            this.Center_Frequency_label.Active_Highlight_Color = System.Drawing.Color.LimeGreen;
            this.Center_Frequency_label.Active_Normal_Color = System.Drawing.Color.DarkGreen;
            this.Center_Frequency_label.AutoSize = true;
            this.Center_Frequency_label.BackColor = System.Drawing.Color.Transparent;
            this.Center_Frequency_label.Fancy_Lable_Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Center_Frequency_label.Fancy_lable_text = "Center Frequency:  22kHz";
            this.Center_Frequency_label.Inactive_Highlight_Color = System.Drawing.Color.LightGray;
            this.Center_Frequency_label.Inactive_Normal_Color = System.Drawing.Color.DarkGray;
            this.Center_Frequency_label.Location = new System.Drawing.Point(16, 61);
            this.Center_Frequency_label.Name = "Center_Frequency_label";
            this.Center_Frequency_label.Size = new System.Drawing.Size(243, 33);
            this.Center_Frequency_label.TabIndex = 1;
            this.Center_Frequency_label.Load += new System.EventHandler(this.Center_Frequency_label_Load);
            // 
            // Band_Pass_Filter_label
            // 
            this.Band_Pass_Filter_label._IsActive = false;
            this.Band_Pass_Filter_label._IsToggleSwitch = true;
            this.Band_Pass_Filter_label.Active_Highlight_Color = System.Drawing.Color.Chartreuse;
            this.Band_Pass_Filter_label.Active_Normal_Color = System.Drawing.Color.LimeGreen;
            this.Band_Pass_Filter_label.AutoSize = true;
            this.Band_Pass_Filter_label.Fancy_Lable_Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Band_Pass_Filter_label.Fancy_lable_text = "Band Pass Filter:  OFF";
            this.Band_Pass_Filter_label.Inactive_Highlight_Color = System.Drawing.Color.LightGray;
            this.Band_Pass_Filter_label.Inactive_Normal_Color = System.Drawing.Color.DarkGray;
            this.Band_Pass_Filter_label.Location = new System.Drawing.Point(16, 32);
            this.Band_Pass_Filter_label.Name = "Band_Pass_Filter_label";
            this.Band_Pass_Filter_label.Size = new System.Drawing.Size(240, 26);
            this.Band_Pass_Filter_label.TabIndex = 0;
            this.Band_Pass_Filter_label.Fancy_Label_Clicked += new System.EventHandler(this.Band_Pass_Filter_label_Fancy_Label_Clicked);
            // 
            // Filter_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.groupBox1);
            this.Name = "Filter_Control";
            this.Size = new System.Drawing.Size(415, 105);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Filter_Icon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Fancy_Lable.Fancy_Lable_Control Band_Pass_Filter_label;
        private Fancy_Lable.Fancy_Lable_Control Center_Frequency_label;
        private System.Windows.Forms.PictureBox pictureBox_Filter_Icon;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
