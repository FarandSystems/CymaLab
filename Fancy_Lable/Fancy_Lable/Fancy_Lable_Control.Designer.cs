namespace Fancy_Lable
{
    partial class Fancy_Lable_Control
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
            this.label_Fancy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Fancy
            // 
            this.label_Fancy.AutoSize = true;
            this.label_Fancy.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Fancy.Location = new System.Drawing.Point(0, 0);
            this.label_Fancy.Name = "label_Fancy";
            this.label_Fancy.Size = new System.Drawing.Size(114, 23);
            this.label_Fancy.TabIndex = 0;
            this.label_Fancy.Text = "Default Text";
            this.label_Fancy.Click += new System.EventHandler(this.label_Fancy_Click);
            this.label_Fancy.MouseEnter += new System.EventHandler(this.label_Fancy_MouseEnter);
            this.label_Fancy.MouseLeave += new System.EventHandler(this.label_Fancy_MouseLeave);
            // 
            // Fancy_Lable_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label_Fancy);
            this.Name = "Fancy_Lable_Control";
            this.Size = new System.Drawing.Size(117, 28);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Fancy;
    }
}
