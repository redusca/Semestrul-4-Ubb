namespace client.Controller.helpWindows
{
    partial class Description
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
            Title = new Label();
            Time = new Label();
            richTextBox1 = new RichTextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Location = new Point(12, 23);
            Title.Name = "Title";
            Title.Size = new Size(50, 20);
            Title.TabIndex = 0;
            Title.Text = "label1";
            // 
            // Time
            // 
            Time.AutoSize = true;
            Time.Location = new Point(12, 60);
            Time.Name = "Time";
            Time.Size = new Size(50, 20);
            Time.TabIndex = 1;
            Time.Text = "label2";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 123);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(281, 206);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 100);
            label3.Name = "label3";
            label3.Size = new Size(85, 20);
            label3.TabIndex = 3;
            label3.Text = "Description";
            // 
            // Description
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(332, 353);
            Controls.Add(label3);
            Controls.Add(richTextBox1);
            Controls.Add(Time);
            Controls.Add(Title);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Description";
            Text = "Description";
            TopMost = true;
            Load += Description_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Title;
        private Label Time;
        private RichTextBox richTextBox1;
        private Label label3;
    }
}