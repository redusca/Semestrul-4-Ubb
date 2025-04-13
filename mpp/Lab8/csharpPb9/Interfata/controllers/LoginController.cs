using Interfata.controllers;
using log4net;
using pass;
using services.services;


namespace Interfata
{
    public partial class LoginController : UserControl
    {
        private Form1 Form1;

        private static readonly ILog log = LogManager.GetLogger("");
        public LoginController(Form1 form1)
        {
            log.Info("LoginController created");
            this.Form1 = form1;
            InitializeComponent();

            Form1.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            
            Form1.Text = "Login";
            Form1.Location = new Point(800, 200);
        }

        /*
        private void SingInButton_MouseHover(object sender, EventArgs e)
        {
            SignInButton.ForeColor = Color.Purple;
            SignInButton.Font = new Font(SignInButton.Font, FontStyle.Underline);
        }

        private void SingInButton_MouseLeave(object sender, EventArgs e)
        {
            SignInButton.ForeColor = Color.Blue;
            SignInButton.Font = new Font(SignInButton.Font, FontStyle.Regular);
        }*/

        private void SignInButton_DoubleClick(object sender, EventArgs e)
        {
            Form1.setController(ControllerType.SingInController);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {

            log.Info("Login button clicked");

            var username = usernameTextBox.Text;
            var password = parolaTextBox.Text;

            try
            {
                var arbitru = Form1.login(username,pass.PasswordEncrypt.Encrypt(password));

                log.Info("Login successful");
                Form1.Swtichloggin(true);
                Form1.setArbitru(arbitru);
                Form1.setController(ControllerType.AppController);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginController_Load(object sender, EventArgs e)
        {
            usernameTextBox.Text = "";
            parolaTextBox.Text = "";
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {

        }
    }
}
