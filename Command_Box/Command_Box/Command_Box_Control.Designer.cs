namespace Command_Box
{
    partial class Command_Box
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Command_Box));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox_Firmware_Update = new System.Windows.Forms.PictureBox();
            this.pictureBox_Run = new System.Windows.Forms.PictureBox();
            this.pictureBox_Save = new System.Windows.Forms.PictureBox();
            this.pictureBox_load = new System.Windows.Forms.PictureBox();
            this.pictureBox_Connected = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Firmware_Update)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Run)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_load)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connected)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox_Firmware_Update
            // 
            this.pictureBox_Firmware_Update.Image = global::Command_Box.Properties.Resources.Firmware_Update;
            this.pictureBox_Firmware_Update.Location = new System.Drawing.Point(68, 11);
            this.pictureBox_Firmware_Update.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Firmware_Update.Name = "pictureBox_Firmware_Update";
            this.pictureBox_Firmware_Update.Size = new System.Drawing.Size(48, 52);
            this.pictureBox_Firmware_Update.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Firmware_Update.TabIndex = 4;
            this.pictureBox_Firmware_Update.TabStop = false;
            this.pictureBox_Firmware_Update.Click += new System.EventHandler(this.pictureBox_Firmware_Update_Click);
            this.pictureBox_Firmware_Update.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Firmware_Update_MouseDown);
            this.pictureBox_Firmware_Update.MouseLeave += new System.EventHandler(this.pictureBox_Firmware_Update_MouseLeave);
            // 
            // pictureBox_Run
            // 
            this.pictureBox_Run.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Run.Image")));
            this.pictureBox_Run.Location = new System.Drawing.Point(131, 11);
            this.pictureBox_Run.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Run.Name = "pictureBox_Run";
            this.pictureBox_Run.Size = new System.Drawing.Size(48, 52);
            this.pictureBox_Run.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Run.TabIndex = 1;
            this.pictureBox_Run.TabStop = false;
            this.pictureBox_Run.Click += new System.EventHandler(this.pictureBox_Run_Click);
            this.pictureBox_Run.MouseEnter += new System.EventHandler(this.pictureBox_Run_MouseEnter);
            this.pictureBox_Run.MouseLeave += new System.EventHandler(this.pictureBox_Run_MouseLeave);
            // 
            // pictureBox_Save
            // 
            this.pictureBox_Save.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Save.Image")));
            this.pictureBox_Save.Location = new System.Drawing.Point(194, 11);
            this.pictureBox_Save.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Save.Name = "pictureBox_Save";
            this.pictureBox_Save.Size = new System.Drawing.Size(48, 52);
            this.pictureBox_Save.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Save.TabIndex = 2;
            this.pictureBox_Save.TabStop = false;
            this.pictureBox_Save.Click += new System.EventHandler(this.pictureBox_Save_Click);
            this.pictureBox_Save.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Save_MouseDown);
            this.pictureBox_Save.MouseEnter += new System.EventHandler(this.pictureBox_Save_MouseEnter);
            this.pictureBox_Save.MouseLeave += new System.EventHandler(this.pictureBox_Save_MouseLeave);
            this.pictureBox_Save.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Save_MouseUp);
            // 
            // pictureBox_load
            // 
            this.pictureBox_load.Image = global::Command_Box.Properties.Resources.Load_File_Inactive;
            this.pictureBox_load.Location = new System.Drawing.Point(257, 11);
            this.pictureBox_load.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_load.Name = "pictureBox_load";
            this.pictureBox_load.Size = new System.Drawing.Size(48, 52);
            this.pictureBox_load.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_load.TabIndex = 3;
            this.pictureBox_load.TabStop = false;
            this.pictureBox_load.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_load_MouseDown);
            this.pictureBox_load.MouseEnter += new System.EventHandler(this.pictureBox_load_MouseEnter);
            this.pictureBox_load.MouseLeave += new System.EventHandler(this.pictureBox_load_MouseLeave);
            this.pictureBox_load.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_load_MouseUp);
            // 
            // pictureBox_Connected
            // 
            this.pictureBox_Connected.Image = global::Command_Box.Properties.Resources.NotConnected;
            this.pictureBox_Connected.Location = new System.Drawing.Point(5, 11);
            this.pictureBox_Connected.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Connected.Name = "pictureBox_Connected";
            this.pictureBox_Connected.Size = new System.Drawing.Size(48, 52);
            this.pictureBox_Connected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Connected.TabIndex = 0;
            this.pictureBox_Connected.TabStop = false;
            // 
            // Command_Box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.pictureBox_Firmware_Update);
            this.Controls.Add(this.pictureBox_Run);
            this.Controls.Add(this.pictureBox_Save);
            this.Controls.Add(this.pictureBox_load);
            this.Controls.Add(this.pictureBox_Connected);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Command_Box";
            this.Size = new System.Drawing.Size(311, 72);
            this.Load += new System.EventHandler(this.Command_Box_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Firmware_Update)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Run)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_load)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Connected;
        private System.Windows.Forms.PictureBox pictureBox_Run;
        private System.Windows.Forms.PictureBox pictureBox_Save;
        private System.Windows.Forms.PictureBox pictureBox_load;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox_Firmware_Update;
    }
}
