namespace LAN_Tcp_Client_Communication_Component
{
    partial class LAN_Tcp_Client_Communication_Component
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Ip = new System.Windows.Forms.TextBox();
            this.pictureBox_Connection = new System.Windows.Forms.PictureBox();
            this.timerConnectionStatus = new System.Windows.Forms.Timer();
            this.timerIpReconnect = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connection)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(36, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address: ";
            // 
            // textBox_Ip
            // 
            this.textBox_Ip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.textBox_Ip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.textBox_Ip.Location = new System.Drawing.Point(106, 5);
            this.textBox_Ip.Name = "textBox_Ip";
            this.textBox_Ip.Size = new System.Drawing.Size(100, 20);
            this.textBox_Ip.TabIndex = 2;
            this.textBox_Ip.TextChanged += new System.EventHandler(this.textBox_Ip_TextChanged);
            // 
            // pictureBox_Connection
            // 
            this.pictureBox_Connection.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox_Connection.Image = global::LAN_Tcp_Client_Communication_Component.Properties.Resources.Connection_Fail;
            this.pictureBox_Connection.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Connection.Name = "pictureBox_Connection";
            this.pictureBox_Connection.Size = new System.Drawing.Size(30, 30);
            this.pictureBox_Connection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Connection.TabIndex = 0;
            this.pictureBox_Connection.TabStop = false;
            this.pictureBox_Connection.Click += new System.EventHandler(this.pictureBox_Connection_Click);
            // 
            // timerConnectionStatus
            // 
            this.timerConnectionStatus.Enabled = true;
            this.timerConnectionStatus.Interval = 120;
            this.timerConnectionStatus.Tick += new System.EventHandler(this.timerConnectionStatus_Tick);
            // 
            // timerIpReconnect
            // 
            this.timerIpReconnect.Interval = 1000;
            this.timerIpReconnect.Tick += new System.EventHandler(this.timerIpReconnect_Tick);
            // 
            // LAN_Tcp_Client_Communication_Component
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.textBox_Ip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox_Connection);
            this.Name = "LAN_Tcp_Client_Communication_Component";
            this.Size = new System.Drawing.Size(215, 30);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Connection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Ip;
        private System.Windows.Forms.Timer timerConnectionStatus;
        private System.Windows.Forms.Timer timerIpReconnect;
    }
}
