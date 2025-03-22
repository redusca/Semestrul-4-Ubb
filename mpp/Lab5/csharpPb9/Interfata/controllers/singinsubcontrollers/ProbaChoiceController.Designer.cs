namespace Interfata.controllers.singinsubcontrollers
{
    partial class ProbaChoiceController
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
            NumeProba = new Label();
            button1 = new Button();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // NumeProba
            // 
            NumeProba.AutoSize = true;
            NumeProba.Location = new Point(34, 0);
            NumeProba.Name = "NumeProba";
            NumeProba.Size = new Size(48, 20);
            NumeProba.TabIndex = 0;
            NumeProba.Text = "Proba";
            // 
            // button1
            // 
            button1.BackColor = Color.DarkTurquoise;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(245, 22);
            button1.Name = "button1";
            button1.Size = new Size(56, 29);
            button1.TabIndex = 1;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.Turquoise;
            comboBox1.DropDownHeight = 100;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.ItemHeight = 20;
            comboBox1.Location = new Point(34, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(205, 28);
            comboBox1.Sorted = true;
            comboBox1.TabIndex = 2;
            // 
            // ProbaChoiceController
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.PaleTurquoise;
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(NumeProba);
            Name = "ProbaChoiceController";
            Size = new Size(320, 62);
            Load += ProbaChoiceController_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label NumeProba;
        private Button button1;
        public ComboBox comboBox1;
    }
}
