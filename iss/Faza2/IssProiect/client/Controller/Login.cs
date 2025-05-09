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
    public partial class Login : UserControl
    {
        IService _service;
        Form1 _form;

        public Login(IService service, Form1 form)
        {
            InitializeComponent();
            _form = form;
            _service = service;

            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            form.Text = "Login";
            form.Location = new Point(600, 200);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Login button clicked");

            var username = usernameField.Text;
            var password = PasswordEncrypt.Encrypt(passwordField.Text);

            try
            {
                var user = _form.Login(username, password);

                Console.WriteLine("User logged in: " + user.Username);
                _form.SwitchLogin(true);
                _form.setUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            usernameField.Text = "";
            passwordField.Text = "";
        }
    }
}
