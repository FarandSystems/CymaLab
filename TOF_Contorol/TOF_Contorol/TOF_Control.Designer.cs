namespace TOF_Contorol
{
    partial class TOF_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TOF_Control));
            this.label_Time_Of_Flight = new System.Windows.Forms.Label();
            this.label_Measurement_Result = new System.Windows.Forms.Label();
            this.label_Range = new System.Windows.Forms.Label();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.pictureBox_TOF_Light = new System.Windows.Forms.PictureBox();
            this.pictureBox_TOF_Increase = new System.Windows.Forms.PictureBox();
            this.pictureBox_TOF_Key = new System.Windows.Forms.PictureBox();
            this.pictureBox_TOF_Decrease = new System.Windows.Forms.PictureBox();
            this.timer_Increase = new System.Windows.Forms.Timer(this.components);
            this.timer_Decrease = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Light)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Increase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Key)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Decrease)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Time_Of_Flight
            // 
            this.label_Time_Of_Flight.AutoSize = true;
            this.label_Time_Of_Flight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Time_Of_Flight.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Time_Of_Flight.ForeColor = System.Drawing.Color.White;
            this.label_Time_Of_Flight.Location = new System.Drawing.Point(807, 10);
            this.label_Time_Of_Flight.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label_Time_Of_Flight.Name = "label_Time_Of_Flight";
            this.label_Time_Of_Flight.Size = new System.Drawing.Size(566, 34);
            this.label_Time_Of_Flight.TabIndex = 4;
            this.label_Time_Of_Flight.Text = "Time Of Flight";
            // 
            // label_Measurement_Result
            // 
            this.label_Measurement_Result.AutoSize = true;
            this.label_Measurement_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Measurement_Result.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Measurement_Result.ForeColor = System.Drawing.Color.White;
            this.label_Measurement_Result.Location = new System.Drawing.Point(807, 54);
            this.label_Measurement_Result.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label_Measurement_Result.Name = "label_Measurement_Result";
            this.label_Measurement_Result.Size = new System.Drawing.Size(566, 34);
            this.label_Measurement_Result.TabIndex = 5;
            this.label_Measurement_Result.Text = "Measurement Result";
            // 
            // label_Range
            // 
            this.label_Range.AutoSize = true;
            this.label_Range.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Range.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Range.ForeColor = System.Drawing.Color.White;
            this.label_Range.Location = new System.Drawing.Point(304, 44);
            this.label_Range.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label_Range.Name = "label_Range";
            this.label_Range.Size = new System.Drawing.Size(487, 44);
            this.label_Range.TabIndex = 6;
            this.label_Range.Text = "Range:";
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Close.Image = global::TOF_Contorol.Properties.Resources.Close;
            this.pictureBox_Close.Location = new System.Drawing.Point(1379, 3);
            this.pictureBox_Close.Name = "pictureBox_Close";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox_Close, 2);
            this.pictureBox_Close.Size = new System.Drawing.Size(138, 82);
            this.pictureBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Close.TabIndex = 8;
            this.pictureBox_Close.TabStop = false;
            this.pictureBox_Close.Click += new System.EventHandler(this.pictureBox_Close_Click);
            // 
            // pictureBox_TOF_Light
            // 
            this.pictureBox_TOF_Light.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_TOF_Light.Image = global::TOF_Contorol.Properties.Resources.LED2_Green_OFF;
            this.pictureBox_TOF_Light.Location = new System.Drawing.Point(767, 10);
            this.pictureBox_TOF_Light.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pictureBox_TOF_Light.Name = "pictureBox_TOF_Light";
            this.pictureBox_TOF_Light.Size = new System.Drawing.Size(24, 25);
            this.pictureBox_TOF_Light.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_TOF_Light.TabIndex = 3;
            this.pictureBox_TOF_Light.TabStop = false;
            // 
            // pictureBox_TOF_Increase
            // 
            this.pictureBox_TOF_Increase.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_TOF_Increase.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_TOF_Increase.Image")));
            this.pictureBox_TOF_Increase.Location = new System.Drawing.Point(144, 0);
            this.pictureBox_TOF_Increase.Name = "pictureBox_TOF_Increase";
            this.pictureBox_TOF_Increase.Size = new System.Drawing.Size(64, 82);
            this.pictureBox_TOF_Increase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_TOF_Increase.TabIndex = 2;
            this.pictureBox_TOF_Increase.TabStop = false;
            this.pictureBox_TOF_Increase.Click += new System.EventHandler(this.pictureBox_TOF_Increase_Click);
            this.pictureBox_TOF_Increase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_TOF_Increase_MouseDown);
            this.pictureBox_TOF_Increase.MouseEnter += new System.EventHandler(this.pictureBox_TOF_Increase_MouseEnter);
            this.pictureBox_TOF_Increase.MouseLeave += new System.EventHandler(this.pictureBox_TOF_Increase_MouseLeave);
            this.pictureBox_TOF_Increase.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_TOF_Increase_MouseUp);
            // 
            // pictureBox_TOF_Key
            // 
            this.pictureBox_TOF_Key.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_TOF_Key.Image = global::TOF_Contorol.Properties.Resources.TOF_Key_Manual;
            this.pictureBox_TOF_Key.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_TOF_Key.Name = "pictureBox_TOF_Key";
            this.pictureBox_TOF_Key.Size = new System.Drawing.Size(208, 82);
            this.pictureBox_TOF_Key.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_TOF_Key.TabIndex = 1;
            this.pictureBox_TOF_Key.TabStop = false;
            this.pictureBox_TOF_Key.Click += new System.EventHandler(this.pictureBox_TOF_Key_Click);
            // 
            // pictureBox_TOF_Decrease
            // 
            this.pictureBox_TOF_Decrease.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox_TOF_Decrease.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_TOF_Decrease.Image")));
            this.pictureBox_TOF_Decrease.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_TOF_Decrease.Margin = new System.Windows.Forms.Padding(3, 3, 1000, 3);
            this.pictureBox_TOF_Decrease.Name = "pictureBox_TOF_Decrease";
            this.pictureBox_TOF_Decrease.Size = new System.Drawing.Size(64, 82);
            this.pictureBox_TOF_Decrease.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_TOF_Decrease.TabIndex = 0;
            this.pictureBox_TOF_Decrease.TabStop = false;
            this.pictureBox_TOF_Decrease.Click += new System.EventHandler(this.pictureBox_TOF_Decrease_Click);
            this.pictureBox_TOF_Decrease.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_TOF_Decrease_MouseDown);
            this.pictureBox_TOF_Decrease.MouseEnter += new System.EventHandler(this.pictureBox_TOF_Decrease_MouseEnter);
            this.pictureBox_TOF_Decrease.MouseLeave += new System.EventHandler(this.pictureBox_TOF_Decrease_MouseLeave);
            this.pictureBox_TOF_Decrease.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_TOF_Decrease_MouseUp);
            // 
            // timer_Increase
            // 
            this.timer_Increase.Tick += new System.EventHandler(this.timer_Increase_Tick);
            // 
            // timer_Decrease
            // 
            this.timer_Decrease.Tick += new System.EventHandler(this.timer_Decrease_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_Close, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_Measurement_Result, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_TOF_Light, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_Range, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_Time_Of_Flight, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1520, 88);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox_TOF_Increase);
            this.panel1.Controls.Add(this.pictureBox_TOF_Decrease);
            this.panel1.Controls.Add(this.pictureBox_TOF_Key);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(23, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(208, 82);
            this.panel1.TabIndex = 9;
            // 
            // TOF_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(700, 88);
            this.Name = "TOF_Control";
            this.Size = new System.Drawing.Size(1520, 88);
            this.Load += new System.EventHandler(this.TOF_Control_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Light)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Increase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Key)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TOF_Decrease)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_TOF_Decrease;
        private System.Windows.Forms.PictureBox pictureBox_TOF_Key;
        private System.Windows.Forms.PictureBox pictureBox_TOF_Increase;
        private System.Windows.Forms.PictureBox pictureBox_TOF_Light;
        private System.Windows.Forms.Label label_Time_Of_Flight;
        private System.Windows.Forms.Label label_Measurement_Result;
        private System.Windows.Forms.Label label_Range;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.Timer timer_Increase;
        private System.Windows.Forms.Timer timer_Decrease;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}
