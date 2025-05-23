using model;
using model.Enum;
using System.Configuration.Internal;

namespace services.Interfaces
{
    public interface IService
    {
        User Login(string username, string password, IManageObserver client);
        void Logout(User user);
        void CreateUser(string username, string password, UserType type);
        void AddBugs(IEnumerable<Bug> bugs);
        void ChangeBugStatus(Bug newBug);
        IEnumerable<Bug> GetBugs();
    }
}
