using log4net;
using services.services;

namespace Interfata.controllers
{
    public partial class AppController : UserControl
    {
        Arbitru arbitru;
        IService serviceRPP;
        Form1 form1;
        private static readonly ILog log = LogManager.GetLogger("");

        public AppController(IService serviceRPP, Form1 form1)
        {
            log.Info("Creating AppController");
            InitializeComponent();
            this.serviceRPP = serviceRPP;
            this.form1 = form1;

            var col = new DataGridViewTextBoxColumn
            {
                Name = "Participant",
                HeaderText = "Participant",
                DataPropertyName = "Participant",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            participantView.Columns.Add(col);

            col = new DataGridViewTextBoxColumn
            {
                Name = "Scor Total",
                HeaderText = "Scor Total",
                DataPropertyName = "Scor Total",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            participantView.Columns.Add(col);

            col = new DataGridViewTextBoxColumn
            {
                Name = "Participant2",
                HeaderText = "Participant",
                DataPropertyName = "Participant2",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            probaView.Columns.Add(col);
            col = new DataGridViewTextBoxColumn
            {
                Name = "ProbaPunctaj",
                HeaderText = "Proba Punctaj",
                DataPropertyName = "ProbaPunctaj",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            probaView.Columns.Add(col);
        }

        public void setArbitru(Arbitru arbitru)
        {
            this.arbitru = arbitru;
            ArbitruLabel.Text = arbitru.Nume + " " + arbitru.Username + "\n" + arbitru.Id_proba;
        }

        public void populateViews()
        {
            log.Info("Populating Views");
            participantCombo.Items.Clear();

            participantView.Rows.Clear();
            probaView.Rows.Clear();

            foreach (var par in serviceRPP.getParticipantiAlfabtic())
            {
                participantView.Rows.Add(par.Key, par.Value);
            }

            var participanti = serviceRPP.getParticipantiPuncteDesc(arbitru.Id_proba);
            foreach (var par in participanti)
            {
                probaView.Rows.Add(par.Key, par.Value);
            }

            var keys = new List<long>();
            foreach (var key in participanti.Keys)
            {
                keys.Add(key.Id);
            }

            foreach (var par in serviceRPP.GetParticipants())
            {
                if (!keys.Contains(par.Id))
                {
                    participantCombo.Items.Add(par);
                }

            }
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            form1.setController(ControllerType.LoginController);

            participantCombo.Items.Clear();

            participantView.Rows.Clear();
            probaView.Rows.Clear();

            logOutFunction();
        }

        public void logOutFunction()
        {
            form1.Swtichloggin(false);
            serviceRPP.logutOut(arbitru, null);
        }

        private void AddRezultat_Click(object sender, EventArgs e)
        {
            if (participantCombo.SelectedItem == null)
            {
                MessageBox.Show("Selectati un participant");
                return;
            }
            var particapnt = (Participant)participantCombo.SelectedItem;
            participantCombo.Items.Remove(participantCombo.SelectedItem);

            serviceRPP.addRezultat(
                particapnt.Id,
                particapnt.Nume,
                particapnt.Prenume,
                arbitru.Id_proba,
                (long)numericUpDown1.Value
            );

            probaView.Rows.Add(particapnt.Nume + " " + particapnt.Prenume, numericUpDown1.Value);

            probaView.Sort(new RowComparer(SortOrder.Descending, "ProbaPunctaj"));
        }

        private void participantCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AppController_Load(object sender, EventArgs e)
        {
            // populateViews();
        }

        public void RezultatAdded(long idParticipant, string numeParticipant, string prenumeParticipant, string idproba, long punctaj)
        { 

            bool participantExists = false;
            foreach (DataGridViewRow row in participantView.Rows)
            {
                if (row.Cells["Participant"].Value != null && row.Cells["Participant"].Value.ToString() == numeParticipant + " " + prenumeParticipant)
                {
                    participantExists = true;
                    long currentPunctaj = (long)row.Cells["Scor Total"].Value;
                    row.Cells["Scor Total"].Value = currentPunctaj + punctaj;
                    break;
                }
            }
            if (!participantExists)
            {
                participantView.Rows.Add(numeParticipant + " " + prenumeParticipant, punctaj);
            }

            participantView.Sort(new StringComparer(SortOrder.Ascending, "Participant"));
        }

        private class StringComparer : System.Collections.IComparer
        {
            private SortOrder sortOrder;
            private string columnName;
            public StringComparer(SortOrder sortOrder, string columnName)
            {
                this.sortOrder = sortOrder;
                this.columnName = columnName;
            }
            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = x as DataGridViewRow;
                DataGridViewRow row2 = y as DataGridViewRow;
                string value1 = row1.Cells[columnName].Value.ToString();
                string value2 = row2.Cells[columnName].Value.ToString();
                int result = value1.CompareTo(value2);
                if (sortOrder == SortOrder.Descending)
                {
                    result = -result;
                }
                return result;
            }
        }

        private class RowComparer : System.Collections.IComparer
        {
            private SortOrder sortOrder;
            private string columnName;

            public RowComparer(SortOrder sortOrder, string columnName)
            {
                this.sortOrder = sortOrder;
                this.columnName = columnName;
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = x as DataGridViewRow;
                DataGridViewRow row2 = y as DataGridViewRow;

                long value1 = Convert.ToInt64(row1.Cells[columnName].Value);
                long value2 = Convert.ToInt64(row2.Cells[columnName].Value);

                int result = value1.CompareTo(value2);

                if (sortOrder == SortOrder.Descending)
                {
                    result = -result;
                }

                return result;
            }
        }
    }
}

