using client.Controller;
using model;
using model.Enum;
using services.Interfaces;

namespace client
{
    public partial class Form1 : Form, IManageObserver
    {
        Login _loginWindow;

        private bool _triedLoggin = false;
        private IService _service = null;
        private User _currentUser;
        private UserType _currentType;

        AdminWindow _adminWindow;
        ProgrammerWindow _programmerWindow;
        TesterWindow _testerWindow;
        public Form1(IService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void setController(ControllerType type)
        {
            this.Controls.Clear();
            switch (type)
            {
                case ControllerType.LOGIN:
                    this.Controls.Add(_loginWindow);
                    return;
                case ControllerType.ADMIN:
                    this.Controls.Add(_adminWindow);
                    return;
                case ControllerType.PROGRAMMER:
                    this.Controls.Add(_programmerWindow);
                    _programmerWindow.populate();
                    return;
                case ControllerType.TESTER:
                    this.Controls.Add(_testerWindow);
                    return;
                default:
                    throw new ArgumentException("Invalid controller type");
            }
        }

        public User Login(string username,string password)
        {
            return _service.Login(username, password, this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _triedLoggin = false;
        }

        public void SwitchLogin(bool status)
        {
            _triedLoggin = status;
        }

        public void setUser(User user)
        {
            _currentUser = user;
            _currentType = user.Type;
            switch (_currentType)
            {
                case UserType.admin:
                    setController(ControllerType.ADMIN);
                    break;
                case UserType.programmer:
                    setController(ControllerType.PROGRAMMER);
                    break;
                case UserType.tester:
                    setController(ControllerType.TESTER);
                    break;
                default:
                    throw new ArgumentException("Invalid user type");
            }
        }

        public void setWindows(Login login, AdminWindow aw, ProgrammerWindow pw, TesterWindow tw)
        {
            _loginWindow = login;
            _adminWindow = aw;
            _programmerWindow = pw;
            _testerWindow = tw;

            setController(ControllerType.LOGIN);
        }

        public void BugsAdded(IEnumerable<Bug> bugs)
        {
            this.Invoke(delegate
            {
                if(_currentType == UserType.programmer)
                {
                    _programmerWindow.newBugs(bugs);
                }
            });
        }

        public void BugStatusChanged(Bug newBug)
        {
            this.Invoke(delegate
            {
                if(_currentType == UserType.programmer)
                {
                    _programmerWindow.bugStatusChanged(newBug);
                }
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_triedLoggin)
            {
                switch (_currentType)
                {
                    case UserType.admin:
                        _adminWindow.Logout();
                        break;
                    case UserType.programmer:
                        _programmerWindow.Logout();
                        break;
                    case UserType.tester:
                        _testerWindow.Logout();
                        break;
                    default:
                        throw new ArgumentException("Invalid user type");
                }
            }
        }

        internal User getUser()
        {
            return _currentUser;
        }
    }

    public enum ControllerType
    {
        LOGIN,
        ADMIN,
        PROGRAMMER,
        TESTER
    }
}
