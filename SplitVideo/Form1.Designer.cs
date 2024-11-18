namespace SplitVideo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            selectButton = new Button();
            singleThreadButton = new Button();
            multiThreadButton = new Button();
            videoPartialCountUpDown = new NumericUpDown();
            progressBar = new ProgressBar();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)videoPartialCountUpDown).BeginInit();
            SuspendLayout();
            // 
            // selectButton
            // 
            selectButton.Location = new Point(713, 12);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(75, 23);
            selectButton.TabIndex = 0;
            selectButton.Text = "select";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += selectButton_Click;
            // 
            // singleThreadButton
            // 
            singleThreadButton.Location = new Point(635, 81);
            singleThreadButton.Name = "singleThreadButton";
            singleThreadButton.Size = new Size(153, 23);
            singleThreadButton.TabIndex = 1;
            singleThreadButton.Text = "single thread";
            singleThreadButton.UseVisualStyleBackColor = true;
            singleThreadButton.Click += singleThreadButton_Click;
            // 
            // multiThreadButton
            // 
            multiThreadButton.Location = new Point(635, 110);
            multiThreadButton.Name = "multiThreadButton";
            multiThreadButton.Size = new Size(153, 23);
            multiThreadButton.TabIndex = 2;
            multiThreadButton.Text = "multi thread";
            multiThreadButton.UseVisualStyleBackColor = true;
            // 
            // videoPartialCountUpDown
            // 
            videoPartialCountUpDown.Location = new Point(483, 12);
            videoPartialCountUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            videoPartialCountUpDown.Name = "videoPartialCountUpDown";
            videoPartialCountUpDown.Size = new Size(120, 23);
            videoPartialCountUpDown.TabIndex = 3;
            videoPartialCountUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 253);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(776, 23);
            progressBar.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(431, 14);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 5;
            label1.Text = "threads";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(progressBar);
            Controls.Add(videoPartialCountUpDown);
            Controls.Add(multiThreadButton);
            Controls.Add(singleThreadButton);
            Controls.Add(selectButton);
            Name = "Form1";
            Text = "Split video";
            ((System.ComponentModel.ISupportInitialize)videoPartialCountUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button selectButton;
        private Button singleThreadButton;
        private Button multiThreadButton;
        private NumericUpDown videoPartialCountUpDown;
        private ProgressBar progressBar;
        private Label label1;
    }
}
