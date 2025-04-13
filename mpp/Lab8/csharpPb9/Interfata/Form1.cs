using csharpPb9.utils;
using Interfata.controllers;
using services.services;
using System.Configuration;
using System.Net.Http.Headers;

namespace Interfata
{
    public partial class Form1 : Form, IManageObserver
    {
        LoginController loginController;
        AppController userControl;

        private bool triedLoggin = false;

        IService service;

        public Form1(IService service)
        {
            InitializeComponent();
            this.service = service;
        }

        public void setController(ControllerType tip)
        {
            this.Controls.Clear();
            switch (tip)
            {
                case ControllerType.LoginController:
                    this.Controls.Add(loginController);
                    break;
                case ControllerType.AppController:
                    this.Controls.Add(userControl);
                    break;
                default:
                    throw new ArgumentException("Invalid controller type");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            triedLoggin = false;
        }

        public void Swtichloggin(bool status)
        {
            triedLoggin = status;
        }

        public void setArbitru(Arbitru arbitru)
        {
            userControl.setArbitru(arbitru);
            userControl.populateViews();
        }

        internal void setAppController(AppController appController)
        {
            this.userControl = appController;
        }

        internal void setLoginController(LoginController loginController)
        {
            this.loginController = loginController;
            setController(ControllerType.LoginController);
        }

        public void RezultatAdded(long idParticipant, string numeParticipant, string prenumeParticipant, string idproba, long punctaj)
        {
            this.Invoke(delegate
                {
                    userControl.RezultatAdded(idParticipant, numeParticipant, prenumeParticipant, idproba, punctaj);
                }
            );
        }

        internal Arbitru login(string username, string pass)
        {
            return service.login(username, pass, this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (triedLoggin)
                this.userControl.logOutFunction();
        }
    }
}
