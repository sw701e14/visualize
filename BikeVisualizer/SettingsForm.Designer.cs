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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hsFillCheck = new System.Windows.Forms.CheckBox();
            this.hsPointCheck = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bikePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStep = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.resetDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputNumberOfBikesBelowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bikeCountBox = new System.Windows.Forms.ToolStripTextBox();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotspotCheck = new BikeVisualizer.ColoredCheckbox();
            this.movingCheck = new BikeVisualizer.ColoredCheckbox();
            this.standCheck = new BikeVisualizer.ColoredCheckbox();
            this.mapsCheck = new System.Windows.Forms.CheckBox();
            this.simLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hsFillCheck);
            this.groupBox1.Controls.Add(this.hsPointCheck);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
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
            // hsPointCheck
            // 
            this.hsPointCheck.AutoSize = true;
            this.hsPointCheck.Checked = true;
            this.hsPointCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hsPointCheck.Location = new System.Drawing.Point(6, 19);
            this.hsPointCheck.Name = "hsPointCheck";
            this.hsPointCheck.Size = new System.Drawing.Size(57, 17);
            this.hsPointCheck.TabIndex = 0;
            this.hsPointCheck.Text = "Border";
            this.hsPointCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.bikePanel);
            this.groupBox2.Location = new System.Drawing.Point(12, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 135);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bikes";
            // 
            // bikePanel
            // 
            this.bikePanel.AutoScroll = true;
            this.bikePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bikePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.bikePanel.Location = new System.Drawing.Point(3, 16);
            this.bikePanel.Name = "bikePanel";
            this.bikePanel.Size = new System.Drawing.Size(184, 116);
            this.bikePanel.TabIndex = 11;
            this.bikePanel.WrapContents = false;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(6, 32);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(178, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start simulation";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.simLabel);
            this.groupBox3.Controls.Add(this.btnStep);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Location = new System.Drawing.Point(9, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(190, 90);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controls";
            // 
            // btnStep
            // 
            this.btnStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep.Enabled = false;
            this.btnStep.Location = new System.Drawing.Point(6, 61);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(178, 23);
            this.btnStep.TabIndex = 11;
            this.btnStep.Text = "Single step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(214, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // resetDataToolStripMenuItem
            // 
            this.resetDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputNumberOfBikesBelowToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.resetDataToolStripMenuItem.Name = "resetDataToolStripMenuItem";
            this.resetDataToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.resetDataToolStripMenuItem.Text = "Reset Data";
            // 
            // inputNumberOfBikesBelowToolStripMenuItem
            // 
            this.inputNumberOfBikesBelowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bikeCountBox});
            this.inputNumberOfBikesBelowToolStripMenuItem.Name = "inputNumberOfBikesBelowToolStripMenuItem";
            this.inputNumberOfBikesBelowToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.inputNumberOfBikesBelowToolStripMenuItem.Text = "Number of bikes:";
            // 
            // bikeCountBox
            // 
            this.bikeCountBox.Name = "bikeCountBox";
            this.bikeCountBox.Size = new System.Drawing.Size(100, 23);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Enabled = false;
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.resetToolStripMenuItem.Text = "Reset!";
            // 
            // hotspotCheck
            // 
            this.hotspotCheck.Checked = false;
            this.hotspotCheck.Color = System.Drawing.Color.Orange;
            this.hotspotCheck.Label = "Hotspots";
            this.hotspotCheck.Location = new System.Drawing.Point(12, 96);
            this.hotspotCheck.Name = "hotspotCheck";
            this.hotspotCheck.Size = new System.Drawing.Size(87, 17);
            this.hotspotCheck.TabIndex = 6;
            // 
            // movingCheck
            // 
            this.movingCheck.Checked = false;
            this.movingCheck.Color = System.Drawing.Color.Purple;
            this.movingCheck.Label = "\"Moving\" points";
            this.movingCheck.Location = new System.Drawing.Point(12, 73);
            this.movingCheck.Name = "movingCheck";
            this.movingCheck.Size = new System.Drawing.Size(150, 17);
            this.movingCheck.TabIndex = 5;
            // 
            // standCheck
            // 
            this.standCheck.Checked = false;
            this.standCheck.Color = System.Drawing.Color.Green;
            this.standCheck.Label = "Standstill points";
            this.standCheck.Location = new System.Drawing.Point(12, 50);
            this.standCheck.Name = "standCheck";
            this.standCheck.Size = new System.Drawing.Size(190, 17);
            this.standCheck.TabIndex = 4;
            // 
            // mapsCheck
            // 
            this.mapsCheck.AutoSize = true;
            this.mapsCheck.Checked = true;
            this.mapsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mapsCheck.Location = new System.Drawing.Point(12, 27);
            this.mapsCheck.Name = "mapsCheck";
            this.mapsCheck.Size = new System.Drawing.Size(89, 17);
            this.mapsCheck.TabIndex = 12;
            this.mapsCheck.Text = "Google Maps";
            this.mapsCheck.UseVisualStyleBackColor = true;
            // 
            // simLabel
            // 
            this.simLabel.AutoSize = true;
            this.simLabel.Location = new System.Drawing.Point(6, 16);
            this.simLabel.Name = "simLabel";
            this.simLabel.Size = new System.Drawing.Size(0, 13);
            this.simLabel.TabIndex = 12;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 414);
            this.Controls.Add(this.mapsCheck);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.hotspotCheck);
            this.Controls.Add(this.movingCheck);
            this.Controls.Add(this.standCheck);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(230, 1200);
            this.MinimumSize = new System.Drawing.Size(230, 448);
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox hsFillCheck;
        private System.Windows.Forms.CheckBox hsPointCheck;
        private ColoredCheckbox standCheck;
        private ColoredCheckbox movingCheck;
        private ColoredCheckbox hotspotCheck;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.FlowLayoutPanel bikePanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputNumberOfBikesBelowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox bikeCountBox;
        private System.Windows.Forms.CheckBox mapsCheck;
        private System.Windows.Forms.Label simLabel;
    }
}