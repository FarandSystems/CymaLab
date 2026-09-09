namespace Measurement_Mode_Control
{
    partial class Measurement_Mode_Control
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
            this.groupBox_Sample_Length = new System.Windows.Forms.GroupBox();
            this.label_Sample_Length = new System.Windows.Forms.Label();
            this.textBox_Sample_Length = new System.Windows.Forms.TextBox();
            this.textBox_Sample_Velocity = new System.Windows.Forms.TextBox();
            this.groupBox_Sample_Velocity = new System.Windows.Forms.GroupBox();
            this.label_Sample_Velocity = new System.Windows.Forms.Label();
            this.groupBox_Measurement_Mode = new System.Windows.Forms.GroupBox();
            this.fancy_Lable_Control_Length = new Fancy_Lable.Fancy_Lable_Control();
            this.fancy_Lable_Control_Velocity = new Fancy_Lable.Fancy_Lable_Control();
            this.groupBox_Sample_Length.SuspendLayout();
            this.groupBox_Sample_Velocity.SuspendLayout();
            this.groupBox_Measurement_Mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Sample_Length
            // 
            this.groupBox_Sample_Length.Controls.Add(this.label_Sample_Length);
            this.groupBox_Sample_Length.Controls.Add(this.textBox_Sample_Length);
            this.groupBox_Sample_Length.Location = new System.Drawing.Point(16, 51);
            this.groupBox_Sample_Length.Name = "groupBox_Sample_Length";
            this.groupBox_Sample_Length.Size = new System.Drawing.Size(338, 58);
            this.groupBox_Sample_Length.TabIndex = 1;
            this.groupBox_Sample_Length.TabStop = false;
            // 
            // label_Sample_Length
            // 
            this.label_Sample_Length.AutoSize = true;
            this.label_Sample_Length.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Sample_Length.ForeColor = System.Drawing.Color.White;
            this.label_Sample_Length.Location = new System.Drawing.Point(6, 22);
            this.label_Sample_Length.Name = "label_Sample_Length";
            this.label_Sample_Length.Size = new System.Drawing.Size(187, 23);
            this.label_Sample_Length.TabIndex = 3;
            this.label_Sample_Length.Text = "Sample Length (cm)";
            // 
            // textBox_Sample_Length
            // 
            this.textBox_Sample_Length.Location = new System.Drawing.Point(232, 18);
            this.textBox_Sample_Length.Name = "textBox_Sample_Length";
            this.textBox_Sample_Length.Size = new System.Drawing.Size(100, 30);
            this.textBox_Sample_Length.TabIndex = 2;
            this.textBox_Sample_Length.TextChanged += new System.EventHandler(this.textBox_Sampel_Length_TextChanged);
            // 
            // textBox_Sample_Velocity
            // 
            this.textBox_Sample_Velocity.Location = new System.Drawing.Point(232, 18);
            this.textBox_Sample_Velocity.Name = "textBox_Sample_Velocity";
            this.textBox_Sample_Velocity.Size = new System.Drawing.Size(100, 30);
            this.textBox_Sample_Velocity.TabIndex = 2;
            this.textBox_Sample_Velocity.TextChanged += new System.EventHandler(this.textBox_Sample_Velocity_TextChanged);
            // 
            // groupBox_Sample_Velocity
            // 
            this.groupBox_Sample_Velocity.Controls.Add(this.label_Sample_Velocity);
            this.groupBox_Sample_Velocity.Controls.Add(this.textBox_Sample_Velocity);
            this.groupBox_Sample_Velocity.Location = new System.Drawing.Point(16, 148);
            this.groupBox_Sample_Velocity.Name = "groupBox_Sample_Velocity";
            this.groupBox_Sample_Velocity.Size = new System.Drawing.Size(338, 56);
            this.groupBox_Sample_Velocity.TabIndex = 3;
            this.groupBox_Sample_Velocity.TabStop = false;
            // 
            // label_Sample_Velocity
            // 
            this.label_Sample_Velocity.AutoSize = true;
            this.label_Sample_Velocity.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Sample_Velocity.Location = new System.Drawing.Point(6, 19);
            this.label_Sample_Velocity.Name = "label_Sample_Velocity";
            this.label_Sample_Velocity.Size = new System.Drawing.Size(202, 23);
            this.label_Sample_Velocity.TabIndex = 3;
            this.label_Sample_Velocity.Text = "Sample Velocity (m/s)";
            // 
            // groupBox_Measurement_Mode
            // 
            this.groupBox_Measurement_Mode.Controls.Add(this.fancy_Lable_Control_Length);
            this.groupBox_Measurement_Mode.Controls.Add(this.fancy_Lable_Control_Velocity);
            this.groupBox_Measurement_Mode.Controls.Add(this.groupBox_Sample_Length);
            this.groupBox_Measurement_Mode.Controls.Add(this.groupBox_Sample_Velocity);
            this.groupBox_Measurement_Mode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Measurement_Mode.ForeColor = System.Drawing.Color.White;
            this.groupBox_Measurement_Mode.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Measurement_Mode.Name = "groupBox_Measurement_Mode";
            this.groupBox_Measurement_Mode.Size = new System.Drawing.Size(400, 219);
            this.groupBox_Measurement_Mode.TabIndex = 5;
            this.groupBox_Measurement_Mode.TabStop = false;
            this.groupBox_Measurement_Mode.Text = "Measurement Mode";
            // 
            // fancy_Lable_Control_Length
            // 
            this.fancy_Lable_Control_Length._IsActive = false;
            this.fancy_Lable_Control_Length._IsToggleSwitch = true;
            this.fancy_Lable_Control_Length.Active_Highlight_Color = System.Drawing.Color.Chartreuse;
            this.fancy_Lable_Control_Length.Active_Normal_Color = System.Drawing.Color.LimeGreen;
            this.fancy_Lable_Control_Length.AutoSize = true;
            this.fancy_Lable_Control_Length.Fancy_Lable_Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fancy_Lable_Control_Length.Fancy_lable_text = "Length Measument";
            this.fancy_Lable_Control_Length.Inactive_Highlight_Color = System.Drawing.Color.DarkGray;
            this.fancy_Lable_Control_Length.Inactive_Normal_Color = System.Drawing.Color.Gray;
            this.fancy_Lable_Control_Length.Location = new System.Drawing.Point(6, 114);
            this.fancy_Lable_Control_Length.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.fancy_Lable_Control_Length.Name = "fancy_Lable_Control_Length";
            this.fancy_Lable_Control_Length.Size = new System.Drawing.Size(196, 37);
            this.fancy_Lable_Control_Length.TabIndex = 6;
            this.fancy_Lable_Control_Length.Fancy_Label_Clicked += new System.EventHandler(this.fancy_Lable_Control_Length_Fancy_Label_Clicked);
            this.fancy_Lable_Control_Length.Load += new System.EventHandler(this.fancy_Lable_Control_Length_Load);
            // 
            // fancy_Lable_Control_Velocity
            // 
            this.fancy_Lable_Control_Velocity._IsActive = true;
            this.fancy_Lable_Control_Velocity._IsToggleSwitch = true;
            this.fancy_Lable_Control_Velocity.Active_Highlight_Color = System.Drawing.Color.Chartreuse;
            this.fancy_Lable_Control_Velocity.Active_Normal_Color = System.Drawing.Color.LimeGreen;
            this.fancy_Lable_Control_Velocity.AutoSize = true;
            this.fancy_Lable_Control_Velocity.Fancy_Lable_Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fancy_Lable_Control_Velocity.Fancy_lable_text = "Velocity Measurement";
            this.fancy_Lable_Control_Velocity.Inactive_Highlight_Color = System.Drawing.Color.DarkGray;
            this.fancy_Lable_Control_Velocity.Inactive_Normal_Color = System.Drawing.Color.Gray;
            this.fancy_Lable_Control_Velocity.Location = new System.Drawing.Point(6, 21);
            this.fancy_Lable_Control_Velocity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.fancy_Lable_Control_Velocity.Name = "fancy_Lable_Control_Velocity";
            this.fancy_Lable_Control_Velocity.Size = new System.Drawing.Size(223, 36);
            this.fancy_Lable_Control_Velocity.TabIndex = 5;
            this.fancy_Lable_Control_Velocity.Fancy_Label_Clicked += new System.EventHandler(this.fancy_Lable_Control_Velocity_Fancy_Label_Clicked);
            // 
            // Measurement_Mode_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.groupBox_Measurement_Mode);
            this.Name = "Measurement_Mode_Control";
            this.Size = new System.Drawing.Size(415, 235);
            this.groupBox_Sample_Length.ResumeLayout(false);
            this.groupBox_Sample_Length.PerformLayout();
            this.groupBox_Sample_Velocity.ResumeLayout(false);
            this.groupBox_Sample_Velocity.PerformLayout();
            this.groupBox_Measurement_Mode.ResumeLayout(false);
            this.groupBox_Measurement_Mode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_Sample_Length;
        private System.Windows.Forms.Label label_Sample_Length;
        private System.Windows.Forms.TextBox textBox_Sample_Length;
        private System.Windows.Forms.TextBox textBox_Sample_Velocity;
        private System.Windows.Forms.GroupBox groupBox_Sample_Velocity;
        private System.Windows.Forms.Label label_Sample_Velocity;
        private System.Windows.Forms.GroupBox groupBox_Measurement_Mode;
        private Fancy_Lable.Fancy_Lable_Control fancy_Lable_Control_Length;
        private Fancy_Lable.Fancy_Lable_Control fancy_Lable_Control_Velocity;
    }
}
