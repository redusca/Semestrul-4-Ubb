namespace client.Controller
{
    partial class AdminWindow
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
            adminRadio = new RadioButton();
            programmerRadio = new RadioButton();
            testerRadio = new RadioButton();
            UsernameField = new TextBox();
            passwordField = new TextBox();
            label1 = new Label();
            label2 = new Label();
            addUserButton = new Button();
            LogOutButton = new Button();
            SuspendLayout();
            // 
            // adminRadio
            // 
            adminRadio.AutoSize = true;
            adminRadio.Checked = true;
            adminRadio.Cursor = Cursors.Hand;
            adminRadio.Location = new Point(396, 112);
            adminRadio.Name = "adminRadio";
            adminRadio.Size = new Size(74, 24);
            adminRadio.TabIndex = 0;
            adminRadio.TabStop = true;
            adminRadio.Text = "Admin";
            adminRadio.UseVisualStyleBackColor = true;
            // 
            // programmerRadio
            // 
            programmerRadio.AutoSize = true;
            programmerRadio.Cursor = Cursors.Hand;
            programmerRadio.Location = new Point(396, 186);
            programmerRadio.Name = "programmerRadio";
            programmerRadio.Size = new Size(113, 24);
            programmerRadio.TabIndex = 1;
            programmerRadio.Text = "Programmer";
            programmerRadio.UseVisualStyleBackColor = true;
            // 
            // testerRadio
            // 
            testerRadio.AutoSize = true;
            testerRadio.Cursor = Cursors.Hand;
            testerRadio.Location = new Point(396, 258);
            testerRadio.Name = "testerRadio";
            testerRadio.Size = new Size(69, 24);
            testerRadio.TabIndex = 2;
            testerRadio.Text = "Tester";
            testerRadio.UseVisualStyleBackColor = true;
            // 
            // UsernameField
            // 
            UsernameField.Location = new Point(55, 132);
            UsernameField.Name = "UsernameField";
            UsernameField.Size = new Size(224, 27);
            UsernameField.TabIndex = 3;
            // 
            // passwordField
            // 
            passwordField.Location = new Point(55, 227);
            passwordField.Name = "passwordField";
            passwordField.Size = new Size(218, 27);
            passwordField.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 91);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 5;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 190);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 6;
            label2.Text = "Password";
            // 
            // addUserButton
            // 
            addUserButton.Location = new Point(396, 338);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(191, 29);
            addUserButton.TabIndex = 7;
            addUserButton.Text = "Add new User";
            addUserButton.UseVisualStyleBackColor = true;
            addUserButton.Click += addUserButton_Click;
            // 
            // LogOutButton
            // 
            LogOutButton.Location = new Point(618, 28);
            LogOutButton.Name = "LogOutButton";
            LogOutButton.Size = new Size(88, 29);
            LogOutButton.TabIndex = 8;
            LogOutButton.Text = "LogOut";
            LogOutButton.UseVisualStyleBackColor = true;
            LogOutButton.Click += LogOutButton_Click;
            // 
            // AdminWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(LogOutButton);
            Controls.Add(addUserButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passwordField);
            Controls.Add(UsernameField);
            Controls.Add(testerRadio);
            Controls.Add(programmerRadio);
            Controls.Add(adminRadio);
            Name = "AdminWindow";
            Size = new Size(739, 425);
            Load += AdminWindow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton adminRadio;
        private RadioButton programmerRadio;
        private RadioButton testerRadio;
        private TextBox UsernameField;
        private TextBox passwordField;
        private Label label1;
        private Label label2;
        private Button addUserButton;
        private Button LogOutButton;
    }
}
