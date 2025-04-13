namespace Interfata
{
    partial class LoginController
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
            Username = new Label();
            Parola = new Label();
            usernameTextBox = new TextBox();
            parolaTextBox = new TextBox();
            LoginButton = new Button();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            SuspendLayout();
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Location = new Point(72, 85);
            Username.Name = "Username";
            Username.Size = new Size(75, 40);
            Username.TabIndex = 0;
            Username.Text = "Username\r\n\r\n";
            // 
            // Parola
            // 
            Parola.AutoSize = true;
            Parola.Location = new Point(72, 138);
            Parola.Name = "Parola";
            Parola.Size = new Size(50, 20);
            Parola.TabIndex = 1;
            Parola.Text = "Parola\r\n";
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = Color.Moccasin;
            usernameTextBox.BorderStyle = BorderStyle.FixedSingle;
            usernameTextBox.CharacterCasing = CharacterCasing.Lower;
            usernameTextBox.Location = new Point(59, 108);
            usernameTextBox.MaxLength = 30;
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(209, 27);
            usernameTextBox.TabIndex = 2;
            // 
            // parolaTextBox
            // 
            parolaTextBox.BackColor = Color.Moccasin;
            parolaTextBox.BorderStyle = BorderStyle.FixedSingle;
            parolaTextBox.Location = new Point(59, 161);
            parolaTextBox.MaxLength = 30;
            parolaTextBox.Name = "parolaTextBox";
            parolaTextBox.Size = new Size(209, 27);
            parolaTextBox.TabIndex = 3;
            parolaTextBox.UseSystemPasswordChar = true;
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.Wheat;
            LoginButton.Location = new Point(108, 225);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(94, 29);
            LoginButton.TabIndex = 4;
            LoginButton.Text = "Login\r\n";
            LoginButton.UseVisualStyleBackColor = false;
            LoginButton.Click += LoginButton_Click;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // LoginController
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Bisque;
            Controls.Add(LoginButton);
            Controls.Add(parolaTextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(Parola);
            Controls.Add(Username);
            Name = "LoginController";
            Size = new Size(320, 340);
            Load += LoginController_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Username;
        private Label Parola;
        private TextBox usernameTextBox;
        private TextBox parolaTextBox;
        private Button LoginButton;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
    }
}
