using log4net;


namespace Interfata
{
    public partial class LoginController : UserControl
    {
        private ServiceArbitru service;
        private Form1 Form1;
        private static readonly ILog log = LogManager.GetLogger("");
        public LoginController(ServiceArbitru service, Form1 form1)
        {
            log.Info("LoginController created");
            this.service = service;
            this.Form1 = form1;
            InitializeComponent();

            Form1.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            
            Form1.Text = "Login";
            Form1.Location = new Point(800, 200);
        }

        private void SingInButton_MouseHover(object sender, EventArgs e)
        {
            SignInButton.ForeColor = Color.Purple;
            SignInButton.Font = new Font(SignInButton.Font, FontStyle.Underline);
        }

        private void SingInButton_MouseLeave(object sender, EventArgs e)
        {
            SignInButton.ForeColor = Color.Blue;
            SignInButton.Font = new Font(SignInButton.Font, FontStyle.Regular);
        }

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
                var arbitru = service.FindArbitru(username, password);
                if (arbitru == null)
                {
                    log.Info("Login unsuccessful");
                    MessageBox.Show("Username is incorrect");
                    return;
                }
                if (arbitru.Id == -1)
                {
                    log.Info("Login unsuccessful");
                    MessageBox.Show("Password is incorrect");
                    return;
                }
                //!!!!!!!!!!!!!!!!!!!
                log.Info("Login successful");
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

        }

        private void SignInButton_Click(object sender, EventArgs e)
        {

        }
    }
}
