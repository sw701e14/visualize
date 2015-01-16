namespace BikeVisualizer
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.standCheck = new System.Windows.Forms.CheckBox();
            this.movingCheck = new System.Windows.Forms.CheckBox();
            this.hotspotCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hsPointCheck = new System.Windows.Forms.CheckBox();
            this.hsFillCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // standCheck
            // 
            this.standCheck.AutoSize = true;
            this.standCheck.Location = new System.Drawing.Point(12, 12);
            this.standCheck.Name = "standCheck";
            this.standCheck.Size = new System.Drawing.Size(99, 17);
            this.standCheck.TabIndex = 0;
            this.standCheck.Text = "Standstill points";
            this.standCheck.UseVisualStyleBackColor = true;
            // 
            // movingCheck
            // 
            this.movingCheck.AutoSize = true;
            this.movingCheck.Location = new System.Drawing.Point(12, 35);
            this.movingCheck.Name = "movingCheck";
            this.movingCheck.Size = new System.Drawing.Size(102, 17);
            this.movingCheck.TabIndex = 1;
            this.movingCheck.Text = "\"Moving\" points";
            this.movingCheck.UseVisualStyleBackColor = true;
            // 
            // hotspotCheck
            // 
            this.hotspotCheck.AutoSize = true;
            this.hotspotCheck.Location = new System.Drawing.Point(12, 58);
            this.hotspotCheck.Name = "hotspotCheck";
            this.hotspotCheck.Size = new System.Drawing.Size(68, 17);
            this.hotspotCheck.TabIndex = 2;
            this.hotspotCheck.Text = "Hotspots";
            this.hotspotCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hsFillCheck);
            this.groupBox1.Controls.Add(this.hsPointCheck);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // hsPointCheck
            // 
            this.hsPointCheck.AutoSize = true;
            this.hsPointCheck.Checked = true;
            this.hsPointCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hsPointCheck.Location = new System.Drawing.Point(6, 19);
            this.hsPointCheck.Name = "hsPointCheck";
            this.hsPointCheck.Size = new System.Drawing.Size(55, 17);
            this.hsPointCheck.TabIndex = 0;
            this.hsPointCheck.Text = "Points";
            this.hsPointCheck.UseVisualStyleBackColor = true;
            // 
            // hsFillCheck
            // 
            this.hsFillCheck.AutoSize = true;
            this.hsFillCheck.Checked = true;
            this.hsFillCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hsFillCheck.Location = new System.Drawing.Point(6, 42);
            this.hsFillCheck.Name = "hsFillCheck";
            this.hsFillCheck.Size = new System.Drawing.Size(38, 17);
            this.hsFillCheck.TabIndex = 1;
            this.hsFillCheck.Text = "Fill";
            this.hsFillCheck.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 394);
            this.Controls.Add(this.hotspotCheck);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.movingCheck);
            this.Controls.Add(this.standCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox standCheck;
        private System.Windows.Forms.CheckBox movingCheck;
        private System.Windows.Forms.CheckBox hotspotCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox hsFillCheck;
        private System.Windows.Forms.CheckBox hsPointCheck;
    }
}