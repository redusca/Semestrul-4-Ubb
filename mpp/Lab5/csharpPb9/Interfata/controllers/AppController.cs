using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfata.controllers
{
    public partial class AppController : UserControl
    {
        Arbitru arbitru;
        ServiceRezultatAndParticipantAndProba serviceRPP;
        Form1 form1;
        private static readonly ILog log = LogManager.GetLogger("");

        public AppController(Arbitru arbitru, ServiceRezultatAndParticipantAndProba serviceRPP, Form1 form1)
        {
            log.Info("Creating AppController");
            InitializeComponent();
            this.arbitru = arbitru;
            this.serviceRPP = serviceRPP;
            this.form1 = form1;
            this.form1.Text = arbitru.Username;

            ArbitruLabel.Text = arbitru.Nume + " " + arbitru.Username + "\n" + arbitru.Id_proba;

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
                Name = "Proba Punctaj",
                HeaderText = "Proba Punctaj",
                DataPropertyName = "Proba Punctaj",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            probaView.Columns.Add(col);

            populateViews();
        }

        private void populateViews()
        {
            log.Info("Populating Views");
            participantCombo.Items.Clear();
            foreach (var par in serviceRPP.GetParticipantiFaraScor(arbitru.Id_proba))
            {
                participantCombo.Items.Add(par);
            }

            participantView.Rows.Clear();
            probaView.Rows.Clear();

            foreach (var par in serviceRPP.GetRezultate())
            {
                participantView.Rows.Add(par.Key, par.Value);
            }

            foreach (var par in serviceRPP.GetRezultateScorDescrescator(arbitru.Id_proba))
            {
                probaView.Rows.Add(par.Key, par.Value);
            }
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            form1.setController(ControllerType.LoginController);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            form1.deleteArbitru(arbitru.Id);
            form1.setController(ControllerType.LoginController);
            MessageBox.Show("Account deleted");
        }

        private void AddRezultat_Click(object sender, EventArgs e)
        {
            var DataRezultat = new DataSetRezultat
            {
                id_participant = ((Participant)participantCombo.SelectedItem).Id,
                id_proba = arbitru.Id_proba,
                punctaj = (long)numericUpDown1.Value
            };

            serviceRPP.SaveRezultat(DataRezultat);
            populateViews();
        }
    }
}
