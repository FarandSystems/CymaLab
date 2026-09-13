namespace Advanced_Settings_Control
{
    partial class Advanced_Settings_Control
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
            this.groupBox_Advanced_Settings = new System.Windows.Forms.GroupBox();
            this.pictureBox_Set = new System.Windows.Forms.PictureBox();
            this.textBox_Discard = new System.Windows.Forms.TextBox();
            this.textBox_Reference = new System.Windows.Forms.TextBox();
            this.label_Discard = new System.Windows.Forms.Label();
            this.label_Reference = new System.Windows.Forms.Label();
            this.groupBox_Advanced_Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Set)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_Advanced_Settings
            // 
            this.groupBox_Advanced_Settings.Controls.Add(this.pictureBox_Set);
            this.groupBox_Advanced_Settings.Controls.Add(this.textBox_Discard);
            this.groupBox_Advanced_Settings.Controls.Add(this.textBox_Reference);
            this.groupBox_Advanced_Settings.Controls.Add(this.label_Discard);
            this.groupBox_Advanced_Settings.Controls.Add(this.label_Reference);
            this.groupBox_Advanced_Settings.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Advanced_Settings.ForeColor = System.Drawing.Color.White;
            this.groupBox_Advanced_Settings.Location = new System.Drawing.Point(2, 2);
            this.groupBox_Advanced_Settings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_Advanced_Settings.Name = "groupBox_Advanced_Settings";
            this.groupBox_Advanced_Settings.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_Advanced_Settings.Size = new System.Drawing.Size(300, 101);
            this.groupBox_Advanced_Settings.TabIndex = 0;
            this.groupBox_Advanced_Settings.TabStop = false;
            this.groupBox_Advanced_Settings.Text = "Advanced Settings";
            // 
            // pictureBox_Set
            // 
            this.pictureBox_Set.Image = global::Advanced_Settings_Control.Properties.Resources.Set_Key_Normal;
            this.pictureBox_Set.Location = new System.Drawing.Point(243, 36);
            this.pictureBox_Set.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox_Set.Name = "pictureBox_Set";
            this.pictureBox_Set.Size = new System.Drawing.Size(48, 47);
            this.pictureBox_Set.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Set.TabIndex = 4;
            this.pictureBox_Set.TabStop = false;
            this.pictureBox_Set.Click += new System.EventHandler(this.pictureBox_Set_Click);
            this.pictureBox_Set.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Set_MouseDown);
            this.pictureBox_Set.MouseEnter += new System.EventHandler(this.pictureBox_Set_MouseEnter);
            this.pictureBox_Set.MouseLeave += new System.EventHandler(this.pictureBox_Set_MouseLeave);
            this.pictureBox_Set.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Set_MouseUp);
            // 
            // textBox_Discard
            // 
            this.textBox_Discard.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Discard.Location = new System.Drawing.Point(159, 66);
            this.textBox_Discard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_Discard.Name = "textBox_Discard";
            this.textBox_Discard.Size = new System.Drawing.Size(76, 26);
            this.textBox_Discard.TabIndex = 3;
            // 
            // textBox_Reference
            // 
            this.textBox_Reference.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Reference.Location = new System.Drawing.Point(159, 27);
            this.textBox_Reference.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_Reference.Name = "textBox_Reference";
            this.textBox_Reference.Size = new System.Drawing.Size(76, 26);
            this.textBox_Reference.TabIndex = 2;
            // 
            // label_Discard
            // 
            this.label_Discard.AutoSize = true;
            this.label_Discard.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Discard.Location = new System.Drawing.Point(6, 70);
            this.label_Discard.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Discard.Name = "label_Discard";
            this.label_Discard.Size = new System.Drawing.Size(147, 17);
            this.label_Discard.TabIndex = 1;
            this.label_Discard.Text = "Discard Time (uSec.)";
            // 
            // label_Reference
            // 
            this.label_Reference.AutoSize = true;
            this.label_Reference.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Reference.Location = new System.Drawing.Point(6, 27);
            this.label_Reference.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Reference.Name = "label_Reference";
            this.label_Reference.Size = new System.Drawing.Size(162, 17);
            this.label_Reference.TabIndex = 0;
            this.label_Reference.Text = "Reference TOF (uSec.)";
            // 
            // Advanced_Settings_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.groupBox_Advanced_Settings);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Advanced_Settings_Control";
            this.Size = new System.Drawing.Size(311, 110);
            this.groupBox_Advanced_Settings.ResumeLayout(false);
            this.groupBox_Advanced_Settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Set)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Advanced_Settings;
        private System.Windows.Forms.PictureBox pictureBox_Set;
        private System.Windows.Forms.TextBox textBox_Discard;
        private System.Windows.Forms.TextBox textBox_Reference;
        private System.Windows.Forms.Label label_Discard;
        private System.Windows.Forms.Label label_Reference;
    }
}
