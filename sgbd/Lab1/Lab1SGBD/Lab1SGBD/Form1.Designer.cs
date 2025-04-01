namespace Lab1SGBD
{
    partial class Form1
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
            this.GamePublishers = new System.Windows.Forms.DataGridView();
            this.GameView = new System.Windows.Forms.DataGridView();
            this.Nume = new System.Windows.Forms.TextBox();
            this.gen = new System.Windows.Forms.TextBox();
            this.playerAnuali = new System.Windows.Forms.NumericUpDown();
            this.launchDate = new System.Windows.Forms.DateTimePicker();
            this.nrCopii = new System.Windows.Forms.NumericUpDown();
            this.delete = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.price = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.devTeam = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.platformaBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GamePublishers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GameView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerAnuali)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrCopii)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.price)).BeginInit();
            this.SuspendLayout();
            // 
            // GamePublishers
            // 
            this.GamePublishers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GamePublishers.Location = new System.Drawing.Point(12, 41);
            this.GamePublishers.Name = "GamePublishers";
            this.GamePublishers.RowHeadersVisible = false;
            this.GamePublishers.RowHeadersWidth = 51;
            this.GamePublishers.RowTemplate.Height = 24;
            this.GamePublishers.Size = new System.Drawing.Size(1071, 304);
            this.GamePublishers.TabIndex = 1;
            this.GamePublishers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GamePublishers_CellContentClick);
            // 
            // GameView
            // 
            this.GameView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GameView.Location = new System.Drawing.Point(12, 385);
            this.GameView.Name = "GameView";
            this.GameView.RowHeadersVisible = false;
            this.GameView.RowHeadersWidth = 51;
            this.GameView.RowTemplate.Height = 24;
            this.GameView.Size = new System.Drawing.Size(1071, 304);
            this.GameView.TabIndex = 2;
            this.GameView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GameView_CellContentClick);
            // 
            // Nume
            // 
            this.Nume.Location = new System.Drawing.Point(1125, 96);
            this.Nume.Name = "Nume";
            this.Nume.Size = new System.Drawing.Size(200, 22);
            this.Nume.TabIndex = 3;
            // 
            // gen
            // 
            this.gen.Location = new System.Drawing.Point(1125, 161);
            this.gen.Name = "gen";
            this.gen.Size = new System.Drawing.Size(200, 22);
            this.gen.TabIndex = 4;
            // 
            // playerAnuali
            // 
            this.playerAnuali.Location = new System.Drawing.Point(1125, 387);
            this.playerAnuali.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.playerAnuali.Name = "playerAnuali";
            this.playerAnuali.Size = new System.Drawing.Size(200, 22);
            this.playerAnuali.TabIndex = 5;
            // 
            // launchDate
            // 
            this.launchDate.CustomFormat = "dd-MMM , yyyy";
            this.launchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.launchDate.Location = new System.Drawing.Point(1125, 234);
            this.launchDate.MaxDate = new System.DateTime(2029, 12, 25, 23, 59, 59, 0);
            this.launchDate.Name = "launchDate";
            this.launchDate.Size = new System.Drawing.Size(200, 22);
            this.launchDate.TabIndex = 6;
            // 
            // nrCopii
            // 
            this.nrCopii.Location = new System.Drawing.Point(1125, 313);
            this.nrCopii.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nrCopii.Name = "nrCopii";
            this.nrCopii.Size = new System.Drawing.Size(200, 22);
            this.nrCopii.TabIndex = 7;
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(1125, 515);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 8;
            this.delete.Text = "delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(1250, 515);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 9;
            this.update.Text = "update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(1125, 563);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(200, 23);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "New VideoGame";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(1125, 461);
            this.price.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(200, 22);
            this.price.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1122, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nume";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1122, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Gen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1122, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Data Lansarii";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1122, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1122, 368);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 16);
            this.label5.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1122, 442);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Pret";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1122, 294);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "Numar copii Vandute";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1122, 368);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "Playeri Anuali";
            // 
            // devTeam
            // 
            this.devTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.devTeam.FormattingEnabled = true;
            this.devTeam.Location = new System.Drawing.Point(1125, 623);
            this.devTeam.Name = "devTeam";
            this.devTeam.Size = new System.Drawing.Size(200, 24);
            this.devTeam.Sorted = true;
            this.devTeam.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1122, 604);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "Developer Team";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1122, 667);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Platforma";
            // 
            // platformaBox
            // 
            this.platformaBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.platformaBox.FormattingEnabled = true;
            this.platformaBox.Location = new System.Drawing.Point(1125, 686);
            this.platformaBox.Name = "platformaBox";
            this.platformaBox.Size = new System.Drawing.Size(200, 24);
            this.platformaBox.Sorted = true;
            this.platformaBox.TabIndex = 22;
            this.platformaBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(511, 357);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 25;
            this.label11.Text = "Tabela Fiu";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(511, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 16);
            this.label12.TabIndex = 26;
            this.label12.Text = "Tabela Parinte";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 747);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.platformaBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.devTeam);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.price);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.update);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.nrCopii);
            this.Controls.Add(this.launchDate);
            this.Controls.Add(this.playerAnuali);
            this.Controls.Add(this.gen);
            this.Controls.Add(this.Nume);
            this.Controls.Add(this.GameView);
            this.Controls.Add(this.GamePublishers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GamePublishers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GameView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerAnuali)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrCopii)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.price)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView GamePublishers;
        private System.Windows.Forms.DataGridView GameView;
        private System.Windows.Forms.TextBox Nume;
        private System.Windows.Forms.TextBox gen;
        private System.Windows.Forms.NumericUpDown playerAnuali;
        private System.Windows.Forms.DateTimePicker launchDate;
        private System.Windows.Forms.NumericUpDown nrCopii;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.NumericUpDown price;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox devTeam;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox platformaBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}

