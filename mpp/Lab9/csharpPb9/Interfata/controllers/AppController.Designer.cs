namespace Interfata.controllers
{
    partial class AppController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppController));
            ArbitruLabel = new Label();
            Puncte = new Label();
            numericUpDown1 = new NumericUpDown();
            participantCombo = new ComboBox();
            RezultatLabel = new Label();
            logOut = new Button();
            AddRezultat = new Button();
            participantView = new DataGridView();
            ParticipantLabel = new Label();
            probaView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)participantView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)probaView).BeginInit();
            SuspendLayout();
            // 
            // ArbitruLabel
            // 
            ArbitruLabel.AutoSize = true;
            ArbitruLabel.BackColor = Color.Peru;
            ArbitruLabel.Location = new Point(38, 27);
            ArbitruLabel.Name = "ArbitruLabel";
            ArbitruLabel.Size = new Size(50, 20);
            ArbitruLabel.TabIndex = 0;
            ArbitruLabel.Text = "label1";
            // 
            // Puncte
            // 
            Puncte.AutoSize = true;
            Puncte.Location = new Point(595, 251);
            Puncte.Name = "Puncte";
            Puncte.Size = new Size(98, 20);
            Puncte.TabIndex = 3;
            Puncte.Text = "NumarPuncte";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Location = new Point(595, 286);
            numericUpDown1.Maximum = new decimal(new int[] { 400, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 4;
            // 
            // participantCombo
            // 
            participantCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            participantCombo.FormattingEnabled = true;
            participantCombo.ItemHeight = 20;
            participantCombo.Location = new Point(595, 199);
            participantCombo.MaxDropDownItems = 1;
            participantCombo.Name = "participantCombo";
            participantCombo.Size = new Size(151, 28);
            participantCombo.Sorted = true;
            participantCombo.TabIndex = 5;
            participantCombo.SelectedIndexChanged += participantCombo_SelectedIndexChanged;
            // 
            // RezultatLabel
            // 
            RezultatLabel.AutoSize = true;
            RezultatLabel.BackColor = Color.OrangeRed;
            RezultatLabel.Location = new Point(595, 120);
            RezultatLabel.Name = "RezultatLabel";
            RezultatLabel.Size = new Size(63, 20);
            RezultatLabel.TabIndex = 7;
            RezultatLabel.Text = "Rezultat";
            // 
            // logOut
            // 
            logOut.Location = new Point(736, 18);
            logOut.Name = "logOut";
            logOut.Size = new Size(94, 29);
            logOut.TabIndex = 8;
            logOut.Text = "Log Out";
            logOut.UseVisualStyleBackColor = true;
            logOut.Click += logOut_Click;
            // 
            // AddRezultat
            // 
            AddRezultat.Location = new Point(595, 346);
            AddRezultat.Name = "AddRezultat";
            AddRezultat.Size = new Size(108, 29);
            AddRezultat.TabIndex = 10;
            AddRezultat.Text = "AddRezultat";
            AddRezultat.UseVisualStyleBackColor = true;
            AddRezultat.Click += AddRezultat_Click;
            // 
            // participantView
            // 
            participantView.BackgroundColor = Color.Silver;
            participantView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            participantView.Location = new Point(38, 76);
            participantView.Name = "participantView";
            participantView.RowHeadersWidth = 51;
            participantView.Size = new Size(537, 299);
            participantView.TabIndex = 11;
            // 
            // ParticipantLabel
            // 
            ParticipantLabel.AutoSize = true;
            ParticipantLabel.Location = new Point(595, 164);
            ParticipantLabel.Name = "ParticipantLabel";
            ParticipantLabel.Size = new Size(79, 20);
            ParticipantLabel.TabIndex = 14;
            ParticipantLabel.Text = "Participant";
            // 
            // probaView
            // 
            probaView.BackgroundColor = Color.Silver;
            probaView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            probaView.Location = new Point(38, 401);
            probaView.Name = "probaView";
            probaView.RowHeadersWidth = 51;
            probaView.Size = new Size(537, 292);
            probaView.TabIndex = 15;
            // 
            // AppController
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            Controls.Add(probaView);
            Controls.Add(ParticipantLabel);
            Controls.Add(participantView);
            Controls.Add(AddRezultat);
            Controls.Add(logOut);
            Controls.Add(RezultatLabel);
            Controls.Add(participantCombo);
            Controls.Add(numericUpDown1);
            Controls.Add(Puncte);
            Controls.Add(ArbitruLabel);
            Name = "AppController";
            Size = new Size(859, 730);
            Load += AppController_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)participantView).EndInit();
            ((System.ComponentModel.ISupportInitialize)probaView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ArbitruLabel;
        private Label Puncte;
        private NumericUpDown numericUpDown1;
        private ComboBox participantCombo;
        private Label RezultatLabel;
        private Button logOut;
        private Button AddRezultat;
        private DataGridView participantView;
        private Label ParticipantLabel;
        private DataGridView probaView;
    }
}
