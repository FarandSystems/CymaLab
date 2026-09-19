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
            this.sampleLengthLayout = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Sample_Velocity = new System.Windows.Forms.TextBox();
            this.groupBox_Sample_Velocity = new System.Windows.Forms.GroupBox();
            this.label_Sample_Velocity = new System.Windows.Forms.Label();
            this.sampleVelocityLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_Measurement_Mode = new System.Windows.Forms.GroupBox();
            this.fancy_Lable_Control_Length = new Fancy_Lable.Fancy_Lable_Control();
            this.fancy_Lable_Control_Velocity = new Fancy_Lable.Fancy_Lable_Control();
            this.responsiveMeasurementLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_Sample_Length.SuspendLayout();
            this.sampleLengthLayout.SuspendLayout();
            this.groupBox_Sample_Velocity.SuspendLayout();
            this.sampleVelocityLayout.SuspendLayout();
            this.groupBox_Measurement_Mode.SuspendLayout();
            this.responsiveMeasurementLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Sample_Length
            // 
            this.groupBox_Sample_Length.Controls.Add(this.sampleLengthLayout);
            this.groupBox_Sample_Length.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Sample_Length.Location = new System.Drawing.Point(8, 36);
            this.groupBox_Sample_Length.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.groupBox_Sample_Length.Name = "groupBox_Sample_Length";
            this.groupBox_Sample_Length.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Sample_Length.Size = new System.Drawing.Size(360, 47);
            this.groupBox_Sample_Length.TabIndex = 1;
            this.groupBox_Sample_Length.TabStop = false;
            // 
            // label_Sample_Length
            // 
            this.label_Sample_Length.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Sample_Length.AutoSize = true;
            this.label_Sample_Length.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Sample_Length.ForeColor = System.Drawing.Color.White;
            this.label_Sample_Length.Location = new System.Drawing.Point(6, 3);
            this.label_Sample_Length.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Sample_Length.Name = "label_Sample_Length";
            this.label_Sample_Length.Size = new System.Drawing.Size(148, 18);
            this.label_Sample_Length.TabIndex = 3;
            this.label_Sample_Length.Text = "Sample Length (cm)";
            // 
            // textBox_Sample_Length
            // 
            this.textBox_Sample_Length.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Sample_Length.Location = new System.Drawing.Point(242, 4);
            this.textBox_Sample_Length.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Sample_Length.Name = "textBox_Sample_Length";
            this.textBox_Sample_Length.Size = new System.Drawing.Size(108, 26);
            this.textBox_Sample_Length.TabIndex = 2;
            this.textBox_Sample_Length.TextChanged += new System.EventHandler(this.textBox_Sampel_Length_TextChanged);
            // 
            // sampleLengthLayout
            // 
            this.sampleLengthLayout.ColumnCount = 2;
            this.sampleLengthLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.sampleLengthLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.sampleLengthLayout.Controls.Add(this.label_Sample_Length, 0, 0);
            this.sampleLengthLayout.Controls.Add(this.textBox_Sample_Length, 1, 0);
            this.sampleLengthLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleLengthLayout.Location = new System.Drawing.Point(2, 21);
            this.sampleLengthLayout.Margin = new System.Windows.Forms.Padding(0);
            this.sampleLengthLayout.Name = "sampleLengthLayout";
            this.sampleLengthLayout.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.sampleLengthLayout.RowCount = 1;
            this.sampleLengthLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sampleLengthLayout.Size = new System.Drawing.Size(356, 24);
            this.sampleLengthLayout.TabIndex = 0;
            // 
            // textBox_Sample_Velocity
            // 
            this.textBox_Sample_Velocity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Sample_Velocity.Location = new System.Drawing.Point(242, 4);
            this.textBox_Sample_Velocity.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Sample_Velocity.Name = "textBox_Sample_Velocity";
            this.textBox_Sample_Velocity.Size = new System.Drawing.Size(108, 26);
            this.textBox_Sample_Velocity.TabIndex = 2;
            this.textBox_Sample_Velocity.TextChanged += new System.EventHandler(this.textBox_Sample_Velocity_TextChanged);
            // 
            // groupBox_Sample_Velocity
            // 
            this.groupBox_Sample_Velocity.Controls.Add(this.sampleVelocityLayout);
            this.groupBox_Sample_Velocity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Sample_Velocity.Location = new System.Drawing.Point(8, 113);
            this.groupBox_Sample_Velocity.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.groupBox_Sample_Velocity.Name = "groupBox_Sample_Velocity";
            this.groupBox_Sample_Velocity.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Sample_Velocity.Size = new System.Drawing.Size(360, 49);
            this.groupBox_Sample_Velocity.TabIndex = 3;
            this.groupBox_Sample_Velocity.TabStop = false;
            // 
            // label_Sample_Velocity
            // 
            this.label_Sample_Velocity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Sample_Velocity.AutoSize = true;
            this.label_Sample_Velocity.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Sample_Velocity.Location = new System.Drawing.Point(6, 4);
            this.label_Sample_Velocity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Sample_Velocity.Name = "label_Sample_Velocity";
            this.label_Sample_Velocity.Size = new System.Drawing.Size(159, 18);
            this.label_Sample_Velocity.TabIndex = 3;
            this.label_Sample_Velocity.Text = "Sample Velocity (m/s)";
            // 
            // sampleVelocityLayout
            // 
            this.sampleVelocityLayout.ColumnCount = 2;
            this.sampleVelocityLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.sampleVelocityLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.sampleVelocityLayout.Controls.Add(this.label_Sample_Velocity, 0, 0);
            this.sampleVelocityLayout.Controls.Add(this.textBox_Sample_Velocity, 1, 0);
            this.sampleVelocityLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleVelocityLayout.Location = new System.Drawing.Point(2, 21);
            this.sampleVelocityLayout.Margin = new System.Windows.Forms.Padding(0);
            this.sampleVelocityLayout.Name = "sampleVelocityLayout";
            this.sampleVelocityLayout.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.sampleVelocityLayout.RowCount = 1;
            this.sampleVelocityLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sampleVelocityLayout.Size = new System.Drawing.Size(356, 26);
            this.sampleVelocityLayout.TabIndex = 0;
            // 
            // groupBox_Measurement_Mode
            // 
            this.groupBox_Measurement_Mode.Controls.Add(this.responsiveMeasurementLayout);
            this.groupBox_Measurement_Mode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Measurement_Mode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Measurement_Mode.ForeColor = System.Drawing.Color.White;
            this.groupBox_Measurement_Mode.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Measurement_Mode.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Measurement_Mode.Name = "groupBox_Measurement_Mode";
            this.groupBox_Measurement_Mode.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Measurement_Mode.Size = new System.Drawing.Size(380, 191);
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
            this.fancy_Lable_Control_Length.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fancy_Lable_Control_Length.AutoSize = true;
            this.fancy_Lable_Control_Length.Fancy_Lable_Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fancy_Lable_Control_Length.Fancy_lable_text = "Length Measument";
            this.fancy_Lable_Control_Length.Inactive_Highlight_Color = System.Drawing.Color.DarkGray;
            this.fancy_Lable_Control_Length.Inactive_Normal_Color = System.Drawing.Color.Gray;
            this.fancy_Lable_Control_Length.Location = new System.Drawing.Point(10, 90);
            this.fancy_Lable_Control_Length.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fancy_Lable_Control_Length.Name = "fancy_Lable_Control_Length";
            this.fancy_Lable_Control_Length.Size = new System.Drawing.Size(143, 18);
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
            this.fancy_Lable_Control_Velocity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fancy_Lable_Control_Velocity.AutoSize = true;
            this.fancy_Lable_Control_Velocity.Fancy_Lable_Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fancy_Lable_Control_Velocity.Fancy_lable_text = "Velocity Measurement";
            this.fancy_Lable_Control_Velocity.Inactive_Highlight_Color = System.Drawing.Color.DarkGray;
            this.fancy_Lable_Control_Velocity.Inactive_Normal_Color = System.Drawing.Color.Gray;
            this.fancy_Lable_Control_Velocity.Location = new System.Drawing.Point(10, 13);
            this.fancy_Lable_Control_Velocity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fancy_Lable_Control_Velocity.Name = "fancy_Lable_Control_Velocity";
            this.fancy_Lable_Control_Velocity.Size = new System.Drawing.Size(164, 18);
            this.fancy_Lable_Control_Velocity.TabIndex = 5;
            this.fancy_Lable_Control_Velocity.Fancy_Label_Clicked += new System.EventHandler(this.fancy_Lable_Control_Velocity_Fancy_Label_Clicked);
            // 
            // responsiveMeasurementLayout
            // 
            this.responsiveMeasurementLayout.ColumnCount = 1;
            this.responsiveMeasurementLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.responsiveMeasurementLayout.Controls.Add(this.fancy_Lable_Control_Velocity, 0, 0);
            this.responsiveMeasurementLayout.Controls.Add(this.groupBox_Sample_Length, 0, 1);
            this.responsiveMeasurementLayout.Controls.Add(this.fancy_Lable_Control_Length, 0, 2);
            this.responsiveMeasurementLayout.Controls.Add(this.groupBox_Sample_Velocity, 0, 3);
            this.responsiveMeasurementLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.responsiveMeasurementLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.responsiveMeasurementLayout.Location = new System.Drawing.Point(2, 21);
            this.responsiveMeasurementLayout.Margin = new System.Windows.Forms.Padding(0);
            this.responsiveMeasurementLayout.Name = "responsiveMeasurementLayout";
            this.responsiveMeasurementLayout.Padding = new System.Windows.Forms.Padding(8, 8, 8, 4);
            this.responsiveMeasurementLayout.RowCount = 4;
            this.responsiveMeasurementLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.responsiveMeasurementLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.responsiveMeasurementLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.responsiveMeasurementLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.responsiveMeasurementLayout.Size = new System.Drawing.Size(376, 168);
            this.responsiveMeasurementLayout.TabIndex = 0;
            // 
            // Measurement_Mode_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.groupBox_Measurement_Mode);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Measurement_Mode_Control";
            this.Size = new System.Drawing.Size(380, 191);
            this.groupBox_Sample_Length.ResumeLayout(false);
            this.sampleLengthLayout.ResumeLayout(false);
            this.sampleLengthLayout.PerformLayout();
            this.groupBox_Sample_Velocity.ResumeLayout(false);
            this.sampleVelocityLayout.ResumeLayout(false);
            this.sampleVelocityLayout.PerformLayout();
            this.groupBox_Measurement_Mode.ResumeLayout(false);
            this.responsiveMeasurementLayout.ResumeLayout(false);
            this.responsiveMeasurementLayout.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel responsiveMeasurementLayout;
        private System.Windows.Forms.TableLayoutPanel sampleLengthLayout;
        private System.Windows.Forms.TableLayoutPanel sampleVelocityLayout;
    }
}
