using client.myObjects;

namespace client.Controller
{
    partial class ProgrammerWindow
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
            bugList = new ListViewEx();
            UnsolvedRadio = new RadioButton();
            workingRadio = new RadioButton();
            SolvedRadio = new RadioButton();
            changeStatusBugButton = new Button();
            logoutButton = new Button();
            descriptionButton = new Button();
            label1 = new Label();
            label2 = new Label();
            solvedCheck = new Button();
            SuspendLayout();
            // 
            // bugList
            // 
            bugList.Location = new Point(48, 84);
            bugList.MultiSelect = false;
            bugList.Name = "bugList";
            bugList.Size = new Size(530, 352);
            bugList.TabIndex = 0;
            bugList.UseCompatibleStateImageBehavior = false;
            bugList.ItemSelectionChanged += bugList_ItemSelectionChanged;
            // 
            // UnsolvedRadio
            // 
            UnsolvedRadio.AutoSize = true;
            UnsolvedRadio.Cursor = Cursors.Hand;
            UnsolvedRadio.Location = new Point(633, 150);
            UnsolvedRadio.Name = "UnsolvedRadio";
            UnsolvedRadio.Size = new Size(91, 24);
            UnsolvedRadio.TabIndex = 1;
            UnsolvedRadio.TabStop = true;
            UnsolvedRadio.Text = "Unsolved";
            UnsolvedRadio.UseVisualStyleBackColor = true;
            // 
            // workingRadio
            // 
            workingRadio.AutoSize = true;
            workingRadio.Cursor = Cursors.Hand;
            workingRadio.Location = new Point(633, 209);
            workingRadio.Name = "workingRadio";
            workingRadio.Size = new Size(106, 24);
            workingRadio.TabIndex = 2;
            workingRadio.TabStop = true;
            workingRadio.Text = "Working on";
            workingRadio.UseVisualStyleBackColor = true;
            // 
            // SolvedRadio
            // 
            SolvedRadio.AutoSize = true;
            SolvedRadio.Cursor = Cursors.Hand;
            SolvedRadio.Location = new Point(633, 264);
            SolvedRadio.Name = "SolvedRadio";
            SolvedRadio.Size = new Size(75, 24);
            SolvedRadio.TabIndex = 3;
            SolvedRadio.TabStop = true;
            SolvedRadio.Text = "Solved";
            SolvedRadio.UseVisualStyleBackColor = true;
            // 
            // changeStatusBugButton
            // 
            changeStatusBugButton.Cursor = Cursors.Hand;
            changeStatusBugButton.Location = new Point(633, 326);
            changeStatusBugButton.Name = "changeStatusBugButton";
            changeStatusBugButton.Size = new Size(117, 29);
            changeStatusBugButton.TabIndex = 4;
            changeStatusBugButton.Text = "Change Status";
            changeStatusBugButton.UseVisualStyleBackColor = true;
            changeStatusBugButton.Click += changeStatusBugButton_Click;
            // 
            // logoutButton
            // 
            logoutButton.Cursor = Cursors.Hand;
            logoutButton.Location = new Point(737, 23);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(108, 29);
            logoutButton.TabIndex = 5;
            logoutButton.Text = "LogOut";
            logoutButton.UseVisualStyleBackColor = true;
            logoutButton.Click += logoutButton_Click;
            // 
            // descriptionButton
            // 
            descriptionButton.Cursor = Cursors.Hand;
            descriptionButton.Location = new Point(385, 451);
            descriptionButton.Name = "descriptionButton";
            descriptionButton.Size = new Size(189, 29);
            descriptionButton.TabIndex = 6;
            descriptionButton.Text = "Bug Description";
            descriptionButton.UseVisualStyleBackColor = true;
            descriptionButton.Click += descriptionButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 48);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 7;
            label1.Text = "Bug List";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(633, 101);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 8;
            label2.Text = "Bug Status";
            // 
            // solvedCheck
            // 
            solvedCheck.Cursor = Cursors.Hand;
            solvedCheck.Location = new Point(48, 451);
            solvedCheck.Name = "solvedCheck";
            solvedCheck.Size = new Size(162, 29);
            solvedCheck.TabIndex = 9;
            solvedCheck.Text = "Hide Solved Bugs";
            solvedCheck.UseVisualStyleBackColor = true;
            solvedCheck.Click += solvedCheck_Click;
            // 
            // ProgrammerWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(solvedCheck);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(descriptionButton);
            Controls.Add(logoutButton);
            Controls.Add(changeStatusBugButton);
            Controls.Add(SolvedRadio);
            Controls.Add(workingRadio);
            Controls.Add(UnsolvedRadio);
            Controls.Add(bugList);
            Name = "ProgrammerWindow";
            Size = new Size(874, 498);
            Load += ProgrammerWindow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListViewEx bugList;
        private RadioButton UnsolvedRadio;
        private RadioButton workingRadio;
        private RadioButton SolvedRadio;
        private Button changeStatusBugButton;
        private Button logoutButton;
        private Button descriptionButton;
        private Label label1;
        private Label label2;
        private Button solvedCheck;
    }
}
