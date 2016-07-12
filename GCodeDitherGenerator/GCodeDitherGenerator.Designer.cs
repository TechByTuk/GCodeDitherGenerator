namespace GCodeDitherGenerator
{
    partial class GCodeDitherGenerator
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
            this.OriginalPictureBox = new System.Windows.Forms.PictureBox();
            this.OutputPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ifYouNeedHelpJustAskOrSomethingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DotsPerMmLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.NumOfDotsLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ProcessImageButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.PauseNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.GammaNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DotSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ContrastNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.BrightnessNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.OutputProgressBar = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.GenerateGCodeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PauseNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GammaNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotSizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalPictureBox
            // 
            this.OriginalPictureBox.Location = new System.Drawing.Point(12, 50);
            this.OriginalPictureBox.Name = "OriginalPictureBox";
            this.OriginalPictureBox.Size = new System.Drawing.Size(270, 270);
            this.OriginalPictureBox.TabIndex = 0;
            this.OriginalPictureBox.TabStop = false;
            // 
            // OutputPictureBox
            // 
            this.OutputPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputPictureBox.Location = new System.Drawing.Point(288, 50);
            this.OutputPictureBox.Name = "OutputPictureBox";
            this.OutputPictureBox.Size = new System.Drawing.Size(270, 302);
            this.OutputPictureBox.TabIndex = 1;
            this.OutputPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Original";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(722, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importImageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importImageToolStripMenuItem
            // 
            this.importImageToolStripMenuItem.Name = "importImageToolStripMenuItem";
            this.importImageToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importImageToolStripMenuItem.Text = "&Import Image";
            this.importImageToolStripMenuItem.Click += new System.EventHandler(this.importImageToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ifYouNeedHelpJustAskOrSomethingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // ifYouNeedHelpJustAskOrSomethingToolStripMenuItem
            // 
            this.ifYouNeedHelpJustAskOrSomethingToolStripMenuItem.Name = "ifYouNeedHelpJustAskOrSomethingToolStripMenuItem";
            this.ifYouNeedHelpJustAskOrSomethingToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.ifYouNeedHelpJustAskOrSomethingToolStripMenuItem.Text = "If you need help just ask or something";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.DotsPerMmLabel);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.NumOfDotsLabel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(564, 302);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 79);
            this.panel1.TabIndex = 5;
            // 
            // DotsPerMmLabel
            // 
            this.DotsPerMmLabel.AutoSize = true;
            this.DotsPerMmLabel.Location = new System.Drawing.Point(74, 42);
            this.DotsPerMmLabel.Name = "DotsPerMmLabel";
            this.DotsPerMmLabel.Size = new System.Drawing.Size(14, 13);
            this.DotsPerMmLabel.TabIndex = 4;
            this.DotsPerMmLabel.Text = "#";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Dots/mm:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 1;
            // 
            // NumOfDotsLabel
            // 
            this.NumOfDotsLabel.AutoSize = true;
            this.NumOfDotsLabel.Location = new System.Drawing.Point(74, 21);
            this.NumOfDotsLabel.Name = "NumOfDotsLabel";
            this.NumOfDotsLabel.Size = new System.Drawing.Size(14, 13);
            this.NumOfDotsLabel.TabIndex = 2;
            this.NumOfDotsLabel.Text = "#";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "#Dots:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Stats";
            // 
            // ProcessImageButton
            // 
            this.ProcessImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessImageButton.Location = new System.Drawing.Point(564, 34);
            this.ProcessImageButton.Name = "ProcessImageButton";
            this.ProcessImageButton.Size = new System.Drawing.Size(96, 23);
            this.ProcessImageButton.TabIndex = 6;
            this.ProcessImageButton.Text = "Process Image";
            this.ProcessImageButton.UseVisualStyleBackColor = true;
            this.ProcessImageButton.Click += new System.EventHandler(this.ProcessImageButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.PauseNumericUpDown);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.GammaNumericUpDown);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.DotSizeNumericUpDown);
            this.panel2.Controls.Add(this.ContrastNumericUpDown);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.BrightnessNumericUpDown);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(564, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(149, 204);
            this.panel2.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Algorithm";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Bayer Dithering",
            "Burkes Dithering",
            "Floyd Steinberg Dithering",
            "Jarvis Judice Ninke Dithering",
            "Ordered Dithering",
            "Sierra Dithering",
            "Stucki Dithering",
            "Tuk\'s Dither"});
            this.comboBox1.Location = new System.Drawing.Point(0, 160);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Pause:";
            // 
            // PauseNumericUpDown
            // 
            this.PauseNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PauseNumericUpDown.Location = new System.Drawing.Point(70, 113);
            this.PauseNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PauseNumericUpDown.Name = "PauseNumericUpDown";
            this.PauseNumericUpDown.Size = new System.Drawing.Size(77, 20);
            this.PauseNumericUpDown.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Gamma:";
            // 
            // GammaNumericUpDown
            // 
            this.GammaNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.GammaNumericUpDown.Location = new System.Drawing.Point(70, 69);
            this.GammaNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.GammaNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GammaNumericUpDown.Name = "GammaNumericUpDown";
            this.GammaNumericUpDown.Size = new System.Drawing.Size(77, 20);
            this.GammaNumericUpDown.TabIndex = 10;
            this.GammaNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Dot Def:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Contrast:";
            // 
            // DotSizeNumericUpDown
            // 
            this.DotSizeNumericUpDown.Location = new System.Drawing.Point(70, 91);
            this.DotSizeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DotSizeNumericUpDown.Name = "DotSizeNumericUpDown";
            this.DotSizeNumericUpDown.Size = new System.Drawing.Size(77, 20);
            this.DotSizeNumericUpDown.TabIndex = 8;
            this.DotSizeNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ContrastNumericUpDown
            // 
            this.ContrastNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ContrastNumericUpDown.Location = new System.Drawing.Point(70, 47);
            this.ContrastNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ContrastNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ContrastNumericUpDown.Name = "ContrastNumericUpDown";
            this.ContrastNumericUpDown.Size = new System.Drawing.Size(77, 20);
            this.ContrastNumericUpDown.TabIndex = 6;
            this.ContrastNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Brightness:";
            // 
            // BrightnessNumericUpDown
            // 
            this.BrightnessNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BrightnessNumericUpDown.Location = new System.Drawing.Point(70, 25);
            this.BrightnessNumericUpDown.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.BrightnessNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BrightnessNumericUpDown.Name = "BrightnessNumericUpDown";
            this.BrightnessNumericUpDown.Size = new System.Drawing.Size(77, 20);
            this.BrightnessNumericUpDown.TabIndex = 4;
            this.BrightnessNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Settings";
            // 
            // OutputProgressBar
            // 
            this.OutputProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputProgressBar.Location = new System.Drawing.Point(12, 358);
            this.OutputProgressBar.Name = "OutputProgressBar";
            this.OutputProgressBar.Size = new System.Drawing.Size(546, 23);
            this.OutputProgressBar.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GenerateGCodeButton
            // 
            this.GenerateGCodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateGCodeButton.Location = new System.Drawing.Point(564, 63);
            this.GenerateGCodeButton.Name = "GenerateGCodeButton";
            this.GenerateGCodeButton.Size = new System.Drawing.Size(96, 23);
            this.GenerateGCodeButton.TabIndex = 9;
            this.GenerateGCodeButton.Text = "Generate GCode";
            this.GenerateGCodeButton.UseVisualStyleBackColor = true;
            this.GenerateGCodeButton.Click += new System.EventHandler(this.GenerateGCodeButton_Click);
            // 
            // GCodeDitherGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 391);
            this.Controls.Add(this.GenerateGCodeButton);
            this.Controls.Add(this.OutputProgressBar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ProcessImageButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OutputPictureBox);
            this.Controls.Add(this.OriginalPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GCodeDitherGenerator";
            this.Text = "GCode Dither Generator";
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PauseNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GammaNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotSizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox OriginalPictureBox;
        private System.Windows.Forms.PictureBox OutputPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ifYouNeedHelpJustAskOrSomethingToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label NumOfDotsLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ProcessImageButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown DotSizeNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown ContrastNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown BrightnessNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar OutputProgressBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown GammaNumericUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown PauseNumericUpDown;
        private System.Windows.Forms.Button GenerateGCodeButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label DotsPerMmLabel;
        private System.Windows.Forms.Label label15;
    }
}

