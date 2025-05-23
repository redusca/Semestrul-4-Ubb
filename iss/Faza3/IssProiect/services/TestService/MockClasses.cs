using model.Enum;
using model;
using persistence.repo.Interface;
using repository;
using services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services.TestService
{
    public class MockBugRepository : IBugRepository
    {
        private readonly List<Bug> _bugs = new List<Bug>();
        private long _nextId = 1;

        public void Add(Bug bug)
        {
            bug.BugNo = _nextId++;
            _bugs.Add(bug);
        }

        public void Delete(long bugNo)
        {
            var bug = FindByBugNo(bugNo);
            if (bug != null)
                _bugs.Remove(bug);
        }

        public IEnumerable<Bug> FindAll()
        {
            return _bugs.ToList();
        }

        public Bug FindByBugNo(long bugNo)
        {
            return _bugs.FirstOrDefault(b => b.BugNo == bugNo);
        }

        public IEnumerable<Bug> FindByStatus(string status)
        {
            return _bugs.Where(b => b.Status.ToString() == status).ToList();
        }

        public void Update(Bug bug)
        {
            var existingBug = FindByBugNo(bug.BugNo);
            if (existingBug != null)
            {
                existingBug.Description = bug.Description;
                existingBug.Status = bug.Status;
            }
        }
    }

    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private long _nextId = 1;

        public MockUserRepository()
        {
            // Add some default test users
            Add(new User("admin", "admin123", UserType.admin));
            Add(new User("tester1", "test123", UserType.tester));
            Add(new User("programmer1", "prog123", UserType.programmer));
        }

        public void Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public void Delete(long id)
        {
            var user = FindById(id);
            if (user != null)
                _users.Remove(user);
        }

        public IEnumerable<User> FindAll()
        {
            return _users.ToList();
        }

        public User FindById(long id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User FindByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public void Update(User user)
        {
            var existingUser = FindById(user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Type = user.Type;
            }
        }
    }

    public class MockObserver : IManageObserver
    {
        public List<IEnumerable<Bug>> BugsAddedNotifications { get; } = new List<IEnumerable<Bug>>();
        public List<Bug> BugStatusChangedNotifications { get; } = new List<Bug>();

        public void BugsAdded(IEnumerable<Bug> bugs)
        {
            BugsAddedNotifications.Add(bugs);
            Console.WriteLine($"Observer notified: {bugs.Count()} bugs added");
        }

        public void BugStatusChanged(Bug newBug)
        {
            BugStatusChangedNotifications.Add(newBug);
            Console.WriteLine($"Observer notified: Bug {newBug.BugNo} status changed to {newBug.Status}");
        }
    }
}
