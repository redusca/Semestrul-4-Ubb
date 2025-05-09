namespace client.Controller
{
    partial class TesterWindow
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
            bugTBox = new TextBox();
            descriptionBox = new RichTextBox();
            AddBugToList = new Button();
            AddBugs = new Button();
            logoutButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            bugList = new ListView();
            elimBug = new Button();
            SuspendLayout();
            // 
            // bugTBox
            // 
            bugTBox.Location = new Point(57, 49);
            bugTBox.Name = "bugTBox";
            bugTBox.Size = new Size(173, 27);
            bugTBox.TabIndex = 0;
            // 
            // descriptionBox
            // 
            descriptionBox.Location = new Point(57, 104);
            descriptionBox.Name = "descriptionBox";
            descriptionBox.Size = new Size(526, 299);
            descriptionBox.TabIndex = 1;
            descriptionBox.Text = "";
            // 
            // AddBugToList
            // 
            AddBugToList.Location = new Point(57, 409);
            AddBugToList.Name = "AddBugToList";
            AddBugToList.Size = new Size(135, 29);
            AddBugToList.TabIndex = 3;
            AddBugToList.Text = "Add Bug to List";
            AddBugToList.UseVisualStyleBackColor = true;
            AddBugToList.Click += AddBugToList_Click;
            // 
            // AddBugs
            // 
            AddBugs.Location = new Point(676, 410);
            AddBugs.Name = "AddBugs";
            AddBugs.Size = new Size(175, 29);
            AddBugs.TabIndex = 4;
            AddBugs.Text = "Add Bugs";
            AddBugs.UseVisualStyleBackColor = true;
            AddBugs.Click += AddBugs_Click;
            // 
            // logoutButton
            // 
            logoutButton.Location = new Point(784, 18);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(94, 29);
            logoutButton.TabIndex = 5;
            logoutButton.Text = "Log Out";
            logoutButton.UseVisualStyleBackColor = true;
            logoutButton.Click += logoutButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(57, 18);
            label1.Name = "label1";
            label1.Size = new Size(68, 20);
            label1.TabIndex = 6;
            label1.Text = "Bug Title";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(57, 79);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 7;
            label2.Text = "Description";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(676, 79);
            label3.Name = "label3";
            label3.Size = new Size(61, 20);
            label3.TabIndex = 8;
            label3.Text = "Bug List";
            // 
            // bugList
            // 
            bugList.Location = new Point(676, 104);
            bugList.MultiSelect = false;
            bugList.Name = "bugList";
            bugList.Size = new Size(175, 299);
            bugList.TabIndex = 9;
            bugList.UseCompatibleStateImageBehavior = false;
            // 
            // elimBug
            // 
            elimBug.Location = new Point(676, 445);
            elimBug.Name = "elimBug";
            elimBug.Size = new Size(175, 29);
            elimBug.TabIndex = 10;
            elimBug.Text = "Delete Bug from List";
            elimBug.UseVisualStyleBackColor = true;
            elimBug.Click += elimBug_Click;
            // 
            // TesterWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(elimBug);
            Controls.Add(bugList);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(logoutButton);
            Controls.Add(AddBugs);
            Controls.Add(AddBugToList);
            Controls.Add(descriptionBox);
            Controls.Add(bugTBox);
            Name = "TesterWindow";
            Size = new Size(919, 511);
            Load += TesterWindow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox bugTBox;
        private RichTextBox descriptionBox;
        private Button AddBugToList;
        private Button AddBugs;
        private Button logoutButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView bugList;
        private Button elimBug;
    }
}
