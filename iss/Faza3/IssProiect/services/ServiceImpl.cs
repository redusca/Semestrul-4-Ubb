using model;
using model.Enum;
using persistence.repo.Interface;
using repository;
using services.Interfaces;

namespace services
{
    public class ServiceImpl : IService
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUserRepository _userRepository;

        private readonly IDictionary<string, IManageObserver> _loggedClients;

        public ServiceImpl(IBugRepository bugRepository, IUserRepository userRepository)
        {
            _bugRepository = bugRepository;
            _userRepository = userRepository;
            _loggedClients = new Dictionary<string, IManageObserver>();
        }

        private void NotifyUserOfNewBugs(IEnumerable<Bug> bugs)
        {
            foreach (var client in _loggedClients)
            {
                try
                {
                    IManageObserver observer = client.Value;
                    Task.Run(
                        () => observer.BugsAdded(bugs)
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void NotifyUsersOfBugStatusChange(Bug newBug)
        {
            foreach (var client in _loggedClients)
            {
                try
                {
                    IManageObserver observer = client.Value;
                    Task.Run(
                        () => observer.BugStatusChanged(newBug)
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void AddBugs(IEnumerable<Bug> bugs) { foreach (var bug in bugs) _bugRepository.Add(bug);  NotifyUserOfNewBugs(bugs); }

        public void ChangeBugStatus(Bug newBug)
        {
            Bug bug = _bugRepository.FindByBugNo(newBug.BugNo);
            if (bug == null)
                throw new Exception("Bug not found");
            bug.Status = newBug.Status;
            _bugRepository.Update(bug);

            NotifyUsersOfBugStatusChange(bug);
        }

        public void CreateUser(string username, string password, UserType type)
        {
            try
            {
                if (_userRepository.FindByUsername(username) != null)
                    throw new Exception("User already exists");
                _userRepository.Add(new User(username, password, type));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Bug> GetBugs() { return _bugRepository.FindAll(); }

        public User Login(string username, string password, IManageObserver client)
        {
            if (_loggedClients.ContainsKey(username))
            {
                throw new Exception("User already logged in");
            }
            User user = _userRepository.FindByUsername(username);
            if (user == null)
                throw new Exception("User not found");

            if (user.Password != password)
                throw new Exception("Invalid password");
            
            _loggedClients.Add(username, client);

            return user;
        }

        public void Logout(User user) { _loggedClients.Remove(user.Username); }
    }
}
