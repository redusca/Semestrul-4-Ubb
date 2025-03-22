namespace Interfata.controllers.singinsubcontrollers
{
    partial class ProbaCreateController
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
            SelectButton = new Button();
            ProbaText = new Label();
            numeText = new TextBox();
            alergatRadio = new RadioButton();
            ciclismRadio = new RadioButton();
            inotRadio = new RadioButton();
            NameLabel = new Label();
            SuspendLayout();
            // 
            // SelectButton
            // 
            SelectButton.BackColor = Color.DeepSkyBlue;
            SelectButton.FlatStyle = FlatStyle.Popup;
            SelectButton.Location = new Point(177, 10);
            SelectButton.Name = "SelectButton";
            SelectButton.Size = new Size(94, 29);
            SelectButton.TabIndex = 0;
            SelectButton.Text = "Select";
            SelectButton.UseVisualStyleBackColor = false;
            SelectButton.Click += SelectButton_Click;
            // 
            // ProbaText
            // 
            ProbaText.AutoSize = true;
            ProbaText.Location = new Point(34, 14);
            ProbaText.Name = "ProbaText";
            ProbaText.Size = new Size(80, 20);
            ProbaText.TabIndex = 1;
            ProbaText.Text = "Add Proba";
            // 
            // numeText
            // 
            numeText.Location = new Point(34, 69);
            numeText.Name = "numeText";
            numeText.Size = new Size(182, 27);
            numeText.TabIndex = 2;
            // 
            // alergatRadio
            // 
            alergatRadio.AutoSize = true;
            alergatRadio.Checked = true;
            alergatRadio.Location = new Point(43, 115);
            alergatRadio.Name = "alergatRadio";
            alergatRadio.Size = new Size(77, 24);
            alergatRadio.TabIndex = 3;
            alergatRadio.TabStop = true;
            alergatRadio.Text = "alergat";
            alergatRadio.UseVisualStyleBackColor = true;
            // 
            // ciclismRadio
            // 
            ciclismRadio.AutoSize = true;
            ciclismRadio.Location = new Point(43, 145);
            ciclismRadio.Name = "ciclismRadio";
            ciclismRadio.Size = new Size(75, 24);
            ciclismRadio.TabIndex = 4;
            ciclismRadio.Text = "ciclism";
            ciclismRadio.UseVisualStyleBackColor = true;
            // 
            // inotRadio
            // 
            inotRadio.AutoSize = true;
            inotRadio.Location = new Point(43, 175);
            inotRadio.Name = "inotRadio";
            inotRadio.Size = new Size(56, 24);
            inotRadio.TabIndex = 5;
            inotRadio.Text = "inot";
            inotRadio.UseVisualStyleBackColor = true;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(43, 46);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(92, 20);
            NameLabel.TabIndex = 6;
            NameLabel.Text = "Nume Proba";
            // 
            // ProbaCreateController
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.PaleTurquoise;
            Controls.Add(NameLabel);
            Controls.Add(inotRadio);
            Controls.Add(ciclismRadio);
            Controls.Add(alergatRadio);
            Controls.Add(numeText);
            Controls.Add(ProbaText);
            Controls.Add(SelectButton);
            Name = "ProbaCreateController";
            Size = new Size(320, 207);
            Load += ProbaCreateController_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SelectButton;
        private Label ProbaText;
        private TextBox numeText;
        private RadioButton alergatRadio;
        private RadioButton ciclismRadio;
        private RadioButton inotRadio;
        private Label NameLabel;
    }
}
