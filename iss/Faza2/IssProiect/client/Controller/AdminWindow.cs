using model.Enum;
using persistence.utils;
using services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client.Controller
{
    public partial class AdminWindow : UserControl
    {
        IService _service;
        Form1 _form;
        public AdminWindow(IService service, Form1 form)
        {
            InitializeComponent();
            _service = service;
            _form = form;
        }

        internal void Logout()
        {
            _form.SwitchLogin(false);
            _service.Logout(_form.getUser());
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            _form.setController(ControllerType.LOGIN);

            UsernameField.Text = "";
            passwordField.Text = "";

            adminRadio.Checked = true;
            programmerRadio.Checked = false;
            testerRadio.Checked = false;

            Logout();
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            if(UsernameField.Text == "" || passwordField.Text == "")
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                UserType type;
                if (adminRadio.Checked)
                    type = UserType.admin;
                else if (programmerRadio.Checked)
                    type = UserType.programmer;
                else if (testerRadio.Checked)
                    type = UserType.tester;
                else
                    throw new Exception("No user type selected");

                _service.CreateUser(UsernameField.Text, PasswordEncrypt.Encrypt(passwordField.Text), type);

                MessageBox.Show("User added successfully!");
            }
            catch (Exception er)
            {
                Console.WriteLine(er.StackTrace);
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
