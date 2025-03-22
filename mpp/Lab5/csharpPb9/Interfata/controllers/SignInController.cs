using Interfata.controllers.singinsubcontrollers;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfata.controllers
{
    public partial class SignInController : UserControl
    {
        private ProbaChoiceController ProbaChoiceController;
        private ProbaCreateController ProbaCreateController;
        private ServiceArbitru service;
        private Form1 Form1;
        bool choice = false;
        private static readonly ILog log = LogManager.GetLogger("");

        public SignInController(ServiceArbitru service, Form1 form1)
        {
            log.Info("SignInController created");
            this.service = service;
            this.Form1 = form1;
            
            InitializeComponent();

            Form1.Location = new Point(800, 200);
            Form1.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Form1.Text = "Sign In";
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            Form1.setController(ControllerType.LoginController);
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Purple;
            label5.Font = new Font(label5.Font, FontStyle.Underline);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Blue;
            label5.Font = new Font(label5.Font, FontStyle.Regular);
        }

        private void SignInController_Load(object sender, EventArgs e)
        {

            ProbaChoiceController = new ProbaChoiceController(service, this);
            ProbaChoiceController.Location = Punct.Location;
            ProbaCreateController = new ProbaCreateController(service, this);
            ProbaCreateController.Location = Punct.Location;
            this.Height += ProbaChoiceController.Height;
            Controls.Add(ProbaChoiceController);
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            DataSets dataSetArbitru = new DataSets();
            dataSetArbitru.Nume = NumeBox.Text;
            dataSetArbitru.Username = usernameBox.Text;
            dataSetArbitru.Password = parolaBox.Text;

            if(dataSetArbitru.Password != parolaBox2.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            if (!choice)
            {

                try
                {
                    var item = ProbaChoiceController.getProba();
                    dataSetArbitru.Id_proba = item.Id;
                    service.SaveArbitru(dataSetArbitru);
                    service.setProbaArbitru(item.Id,
                        service.FindArbitru(dataSetArbitru.Username, dataSetArbitru.Password).Id);

                    MessageBox.Show("Account Created");
                    Form1.setController(ControllerType.LoginController);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                try
                {
                    var proba = ProbaCreateController.getProbaSet();
                    service.SaveArbitruPlusProba(dataSetArbitru, proba);

                    MessageBox.Show("Account Created");
                    Form1.setController(ControllerType.LoginController);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        internal void SwitchController()
        {
            if (choice)
            { 
                Controls.Remove(ProbaCreateController);
                this.Height -= ProbaCreateController.Height;
                this.Height += ProbaChoiceController.Height;
                Controls.Add(ProbaChoiceController);
            }
            else
            {
                Controls.Remove(ProbaChoiceController);
                this.Height -= ProbaChoiceController.Height;
                this.Height += ProbaCreateController.Height;
                Controls.Add(ProbaCreateController);
            }
            choice = !choice;
        }
    }
}
