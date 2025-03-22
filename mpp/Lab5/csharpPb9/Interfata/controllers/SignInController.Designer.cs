namespace Interfata.controllers
{
    partial class SignInController
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
            Nume = new Label();
            Username = new Label();
            label3 = new Label();
            NumeBox = new TextBox();
            usernameBox = new TextBox();
            parolaBox = new TextBox();
            SignInButton = new Button();
            LogInText = new Label();
            label5 = new Label();
            Punct = new Label();
            parolaBox2 = new TextBox();
            Parola2 = new Label();
            SuspendLayout();
            // 
            // Nume
            // 
            Nume.AutoSize = true;
            Nume.Location = new Point(33, 31);
            Nume.Name = "Nume";
            Nume.Size = new Size(49, 20);
            Nume.TabIndex = 2;
            Nume.Text = "Nume";
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Location = new Point(33, 96);
            Username.Name = "Username";
            Username.Size = new Size(75, 20);
            Username.TabIndex = 3;
            Username.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 169);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 4;
            label3.Text = "Parola";
            // 
            // NumeBox
            // 
            NumeBox.BackColor = Color.LightSkyBlue;
            NumeBox.BorderStyle = BorderStyle.FixedSingle;
            NumeBox.Location = new Point(33, 54);
            NumeBox.MaxLength = 30;
            NumeBox.Name = "NumeBox";
            NumeBox.Size = new Size(231, 27);
            NumeBox.TabIndex = 5;
            // 
            // usernameBox
            // 
            usernameBox.BackColor = Color.LightSkyBlue;
            usernameBox.BorderStyle = BorderStyle.FixedSingle;
            usernameBox.Location = new Point(33, 130);
            usernameBox.MaxLength = 30;
            usernameBox.Name = "usernameBox";
            usernameBox.Size = new Size(231, 27);
            usernameBox.TabIndex = 6;
            // 
            // parolaBox
            // 
            parolaBox.BackColor = Color.LightSkyBlue;
            parolaBox.BorderStyle = BorderStyle.FixedSingle;
            parolaBox.Location = new Point(33, 202);
            parolaBox.MaxLength = 30;
            parolaBox.Name = "parolaBox";
            parolaBox.Size = new Size(231, 27);
            parolaBox.TabIndex = 7;
            parolaBox.UseSystemPasswordChar = true;
            // 
            // SignInButton
            // 
            SignInButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SignInButton.BackColor = Color.Turquoise;
            SignInButton.FlatStyle = FlatStyle.Flat;
            SignInButton.Location = new Point(33, 344);
            SignInButton.Name = "SignInButton";
            SignInButton.Size = new Size(94, 29);
            SignInButton.TabIndex = 8;
            SignInButton.Text = "Sign In";
            SignInButton.UseVisualStyleBackColor = false;
            SignInButton.Click += SignInButton_Click;
            // 
            // LogInText
            // 
            LogInText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LogInText.AutoSize = true;
            LogInText.Location = new Point(35, 376);
            LogInText.Name = "LogInText";
            LogInText.Size = new Size(151, 20);
            LogInText.TabIndex = 9;
            LogInText.Text = "You have an account?";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.ForeColor = Color.Blue;
            label5.Location = new Point(192, 376);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 10;
            label5.Text = "Log In";
            label5.DoubleClick += label5_DoubleClick;
            label5.MouseLeave += label5_MouseLeave;
            label5.MouseHover += label5_MouseHover;
            // 
            // Punct
            // 
            Punct.AutoSize = true;
            Punct.Location = new Point(0, 324);
            Punct.Name = "Punct";
            Punct.Size = new Size(17, 20);
            Punct.TabIndex = 0;
            Punct.Text = "P";
            Punct.Visible = false;
            // 
            // parolaBox2
            // 
            parolaBox2.BackColor = Color.LightSkyBlue;
            parolaBox2.BorderStyle = BorderStyle.FixedSingle;
            parolaBox2.Location = new Point(34, 287);
            parolaBox2.MaxLength = 30;
            parolaBox2.Name = "parolaBox2";
            parolaBox2.Size = new Size(232, 27);
            parolaBox2.TabIndex = 12;
            parolaBox2.UseSystemPasswordChar = true;
            // 
            // Parola2
            // 
            Parola2.AutoSize = true;
            Parola2.Location = new Point(33, 249);
            Parola2.Name = "Parola2";
            Parola2.Size = new Size(101, 20);
            Parola2.TabIndex = 13;
            Parola2.Text = "Rescrie Parola";
            // 
            // SignInController
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            BackColor = Color.PaleTurquoise;
            Controls.Add(Parola2);
            Controls.Add(parolaBox2);
            Controls.Add(Punct);
            Controls.Add(label5);
            Controls.Add(LogInText);
            Controls.Add(SignInButton);
            Controls.Add(parolaBox);
            Controls.Add(usernameBox);
            Controls.Add(NumeBox);
            Controls.Add(label3);
            Controls.Add(Username);
            Controls.Add(Nume);
            Name = "SignInController";
            Size = new Size(320, 405);
            Load += SignInController_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Nume;
        private Label Username;
        private Label label3;
        private TextBox NumeBox;
        private TextBox usernameBox;
        private TextBox parolaBox;
        private Button SignInButton;
        private Label LogInText;
        private Label label5;
        private Label Punct;
        private TextBox parolaBox2;
        private Label Parola2;
    }
}
