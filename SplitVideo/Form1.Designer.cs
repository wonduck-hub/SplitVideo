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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(videoPartialCountUpDown);
            Controls.Add(multiThreadButton);
            Controls.Add(singleThreadButton);
            Controls.Add(selectButton);
            Name = "Form1";
            Text = "Split video";
            ((System.ComponentModel.ISupportInitialize)videoPartialCountUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button selectButton;
        private Button singleThreadButton;
        private Button multiThreadButton;
        private NumericUpDown videoPartialCountUpDown;
    }
}
