using csharpPb9.utils;
using Interfata.controllers;
using System.Configuration;
using System.Net.Http.Headers;

namespace Interfata
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        LoginController loginController;
        SignInController signInController;

        Arbitru arbitru;

        ServiceArbitru serviceArbitru;
        ServiceRezultatAndParticipantAndProba serviceRPP;

        public Form1(ServiceArbitru serviceArbitru,ServiceRezultatAndParticipantAndProba serviceRPP)
        {
            InitializeComponent();

            this.serviceArbitru = serviceArbitru;
            this.serviceRPP = serviceRPP;

            setController(ControllerType.LoginController);
        }

        public void setController(ControllerType tip)
        {
            this.Controls.Clear();
            switch (tip)
            {
                case ControllerType.LoginController:
                    this.Controls.Add(new LoginController(serviceArbitru, this));
                    break;
                case ControllerType.SingInController:
                    this.Controls.Add(new SignInController(serviceArbitru, this));
                    break;
                case ControllerType.AppController:
                    this.Controls.Add(new AppController(arbitru, serviceRPP, this));
                    break;
            }
        }

        public void RemoveController()
        {
            this.Controls.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        internal void setArbitru(Arbitru e)
        {
            arbitru = e;
        }

        internal void deleteArbitru(long id)
        {
            serviceArbitru.deleteArbitru(id);
        }
    }
}
