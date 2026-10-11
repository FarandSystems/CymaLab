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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Command_Box));
            this.timer1 = new System.Windows.Forms.Timer();
            this.modeToolTip = new System.Windows.Forms.ToolTip();
            this.pictureBox_TestMode = new System.Windows.Forms.PictureBox();
            this.pictureBox_Firmware_Update = new System.Windows.Forms.PictureBox();
            this.pictureBox_Run = new System.Windows.Forms.PictureBox();
            this.pictureBox_Save = new System.Windows.Forms.PictureBox();
            this.pictureBox_load = new System.Windows.Forms.PictureBox();
            this.pictureBox_Connected = new System.Windows.Forms.PictureBox();
            this.pictureBox_Connection_Mode = new System.Windows.Forms.PictureBox();
            this.responsiveCommandLayout = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TestMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Firmware_Update)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Run)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_load)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connection_Mode)).BeginInit();
            this.responsiveCommandLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox_TestMode
            // 
            this.pictureBox_TestMode.AccessibleName = "Toggle test mode with Ctrl+click";
            this.pictureBox_TestMode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox_TestMode.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox_TestMode.Image = global::Command_Box.Properties.Resources.Mode_Normal;
            this.pictureBox_TestMode.Location = new System.Drawing.Point(7, 5);
            this.pictureBox_TestMode.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_TestMode.Name = "pictureBox_TestMode";
            this.pictureBox_TestMode.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_TestMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_TestMode.TabIndex = 7;
            this.pictureBox_TestMode.TabStop = false;
            this.pictureBox_TestMode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_TestMode_MouseClick);
            // 
            // pictureBox_Firmware_Update
            // 
            this.pictureBox_Firmware_Update.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Firmware_Update.Enabled = false;
            this.pictureBox_Firmware_Update.Image = global::Command_Box.Properties.Resources.Firmware_Update;
            this.pictureBox_Firmware_Update.Location = new System.Drawing.Point(147, 5);
            this.pictureBox_Firmware_Update.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Firmware_Update.Name = "pictureBox_Firmware_Update";
            this.pictureBox_Firmware_Update.Size = new System.Drawing.Size(53, 62);
            this.pictureBox_Firmware_Update.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Firmware_Update.TabIndex = 4;
            this.pictureBox_Firmware_Update.TabStop = false;
            this.pictureBox_Firmware_Update.Click += new System.EventHandler(this.pictureBox_Firmware_Update_Click);
            this.pictureBox_Firmware_Update.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Firmware_Update_MouseDown);
            this.pictureBox_Firmware_Update.MouseLeave += new System.EventHandler(this.pictureBox_Firmware_Update_MouseLeave);
            // 
            // pictureBox_Run
            // 
            this.pictureBox_Run.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Run.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Run.Image")));
            this.pictureBox_Run.Location = new System.Drawing.Point(204, 5);
            this.pictureBox_Run.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Run.Name = "pictureBox_Run";
            this.pictureBox_Run.Size = new System.Drawing.Size(53, 62);
            this.pictureBox_Run.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Run.TabIndex = 1;
            this.pictureBox_Run.TabStop = false;
            this.pictureBox_Run.Click += new System.EventHandler(this.pictureBox_Run_Click);
            this.pictureBox_Run.MouseEnter += new System.EventHandler(this.pictureBox_Run_MouseEnter);
            this.pictureBox_Run.MouseLeave += new System.EventHandler(this.pictureBox_Run_MouseLeave);
            // 
            // pictureBox_Save
            // 
            this.pictureBox_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Save.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Save.Image")));
            this.pictureBox_Save.Location = new System.Drawing.Point(261, 5);
            this.pictureBox_Save.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Save.Name = "pictureBox_Save";
            this.pictureBox_Save.Size = new System.Drawing.Size(53, 62);
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
            this.pictureBox_load.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_load.Image = global::Command_Box.Properties.Resources.Load_File_Inactive;
            this.pictureBox_load.Location = new System.Drawing.Point(318, 5);
            this.pictureBox_load.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_load.Name = "pictureBox_load";
            this.pictureBox_load.Size = new System.Drawing.Size(57, 62);
            this.pictureBox_load.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_load.TabIndex = 3;
            this.pictureBox_load.TabStop = false;
            this.pictureBox_load.Click += new System.EventHandler(this.pictureBox_load_Click);
            this.pictureBox_load.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_load_MouseDown);
            this.pictureBox_load.MouseEnter += new System.EventHandler(this.pictureBox_load_MouseEnter);
            this.pictureBox_load.MouseLeave += new System.EventHandler(this.pictureBox_load_MouseLeave);
            this.pictureBox_load.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_load_MouseUp);
            // 
            // pictureBox_Connected
            // 
            this.pictureBox_Connected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Connected.Image = global::Command_Box.Properties.Resources.NotConnected;
            this.pictureBox_Connected.Location = new System.Drawing.Point(33, 5);
            this.pictureBox_Connected.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Connected.Name = "pictureBox_Connected";
            this.pictureBox_Connected.Size = new System.Drawing.Size(53, 62);
            this.pictureBox_Connected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Connected.TabIndex = 0;
            this.pictureBox_Connected.TabStop = false;
            // 
            // pictureBox_Connection_Mode
            // 
            this.pictureBox_Connection_Mode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Connection_Mode.Image = global::Command_Box.Properties.Resources.Wifi;
            this.pictureBox_Connection_Mode.Location = new System.Drawing.Point(90, 5);
            this.pictureBox_Connection_Mode.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Connection_Mode.Name = "pictureBox_Connection_Mode";
            this.pictureBox_Connection_Mode.Size = new System.Drawing.Size(53, 62);
            this.pictureBox_Connection_Mode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Connection_Mode.TabIndex = 5;
            this.pictureBox_Connection_Mode.TabStop = false;
            this.pictureBox_Connection_Mode.Click += new System.EventHandler(this.pictureBox_Connection_Mode_Click);
            // 
            // responsiveCommandLayout
            // 
            this.responsiveCommandLayout.ColumnCount = 7;
            this.responsiveCommandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.responsiveCommandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.responsiveCommandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.responsiveCommandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.responsiveCommandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.responsiveCommandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.responsiveCommandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66665F));
            this.responsiveCommandLayout.Controls.Add(this.pictureBox_TestMode, 0, 0);
            this.responsiveCommandLayout.Controls.Add(this.pictureBox_Connected, 1, 0);
            this.responsiveCommandLayout.Controls.Add(this.pictureBox_Connection_Mode, 2, 0);
            this.responsiveCommandLayout.Controls.Add(this.pictureBox_load, 6, 0);
            this.responsiveCommandLayout.Controls.Add(this.pictureBox_Firmware_Update, 3, 0);
            this.responsiveCommandLayout.Controls.Add(this.pictureBox_Save, 5, 0);
            this.responsiveCommandLayout.Controls.Add(this.pictureBox_Run, 4, 0);
            this.responsiveCommandLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.responsiveCommandLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.responsiveCommandLayout.Location = new System.Drawing.Point(0, 0);
            this.responsiveCommandLayout.Margin = new System.Windows.Forms.Padding(0);
            this.responsiveCommandLayout.Name = "responsiveCommandLayout";
            this.responsiveCommandLayout.Padding = new System.Windows.Forms.Padding(3);
            this.responsiveCommandLayout.RowCount = 1;
            this.responsiveCommandLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.responsiveCommandLayout.Size = new System.Drawing.Size(380, 72);
            this.responsiveCommandLayout.TabIndex = 6;
            // 
            // Command_Box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.responsiveCommandLayout);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Command_Box";
            this.Size = new System.Drawing.Size(380, 72);
            this.Load += new System.EventHandler(this.Command_Box_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TestMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Firmware_Update)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Run)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_load)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Connection_Mode)).EndInit();
            this.responsiveCommandLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Connected;
        private System.Windows.Forms.PictureBox pictureBox_TestMode;
        private System.Windows.Forms.ToolTip modeToolTip;
        private System.Windows.Forms.PictureBox pictureBox_Run;
        private System.Windows.Forms.PictureBox pictureBox_Save;
        private System.Windows.Forms.PictureBox pictureBox_load;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox_Firmware_Update;
        private System.Windows.Forms.PictureBox pictureBox_Connection_Mode;
        private System.Windows.Forms.TableLayoutPanel responsiveCommandLayout;
    }
}
