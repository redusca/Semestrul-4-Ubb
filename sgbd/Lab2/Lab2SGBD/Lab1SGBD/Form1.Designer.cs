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
            this.parentView = new System.Windows.Forms.DataGridView();
            this.childView = new System.Windows.Forms.DataGridView();
            this.delete = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ancor = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.parentView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.childView)).BeginInit();
            this.SuspendLayout();
            // 
            // parentView
            // 
            this.parentView.AllowUserToResizeRows = false;
            this.parentView.AllowUserToAddRows = false;
            this.parentView.AllowUserToDeleteRows = false;
            this.parentView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.parentView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.parentView.Location = new System.Drawing.Point(12, 41);
            this.parentView.Name = "parentView";
            this.parentView.ReadOnly = true;
            this.parentView.RowHeadersVisible = false;
            this.parentView.RowHeadersWidth = 51;
            this.parentView.RowTemplate.Height = 24;
            this.parentView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.parentView.Size = new System.Drawing.Size(1071, 304);
            this.parentView.TabIndex = 1;
            this.parentView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LineSelect);
            // 
            // childView
            // 
            this.childView.AllowUserToResizeRows = false;
            this.childView.AllowUserToAddRows = false;
            this.childView.AllowUserToDeleteRows = false;
            this.childView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.childView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.childView.Location = new System.Drawing.Point(12, 385);
            this.childView.Name = "childView";
            this.childView.ReadOnly = true;
            this.childView.RowHeadersVisible = false;
            this.childView.RowHeadersWidth = 51;
            this.childView.RowTemplate.Height = 24;
            this.childView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.childView.Size = new System.Drawing.Size(1071, 304);
            this.childView.TabIndex = 2;
            this.childView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LineSelectInChild);
            // 
            // delete
            // 
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(629, 712);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(132, 23);
            this.delete.TabIndex = 8;
            this.delete.Text = "delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // update
            // 
            this.update.Enabled = false;
            this.update.Location = new System.Drawing.Point(951, 712);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(132, 23);
            this.update.TabIndex = 9;
            this.update.Text = "update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(791, 712);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(135, 23);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Add";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.insert_Click);
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
            // ancor
            // 
            this.ancor.AutoSize = true;
            this.ancor.Location = new System.Drawing.Point(1125, 40);
            this.ancor.Name = "ancor";
            this.ancor.Size = new System.Drawing.Size(15, 16);
            this.ancor.TabIndex = 27;
            this.ancor.Text = "p";
            this.ancor.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 747);
            this.Controls.Add(this.ancor);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.update);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.childView);
            this.Controls.Add(this.parentView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.parentView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.childView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView parentView;
        private System.Windows.Forms.DataGridView childView;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label ancor;
    }
}

