namespace client.Controller
{
    partial class Login
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
            usernameField = new TextBox();
            LoginButton = new Button();
            label1 = new Label();
            label2 = new Label();
            passwordField = new TextBox();
            SuspendLayout();
            // 
            // usernameField
            // 
            usernameField.Cursor = Cursors.IBeam;
            usernameField.Location = new Point(181, 51);
            usernameField.Name = "usernameField";
            usernameField.Size = new Size(201, 27);
            usernameField.TabIndex = 0;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(238, 203);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(94, 29);
            LoginButton.TabIndex = 2;
            LoginButton.Text = "login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(194, 28);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 3;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(194, 105);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // passwordField
            // 
            passwordField.Cursor = Cursors.IBeam;
            passwordField.Location = new Point(181, 138);
            passwordField.Name = "passwordField";
            passwordField.Size = new Size(201, 27);
            passwordField.TabIndex = 1;
            passwordField.UseSystemPasswordChar = true;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(passwordField);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LoginButton);
            Controls.Add(usernameField);
            Name = "Login";
            Size = new Size(578, 275);
            Load += Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox usernameField;
        private Button LoginButton;
        private Label label1;
        private Label label2;
        private TextBox passwordField;
    }
}
