using model;
using model.Enum;

namespace services.Interfaces
{
    public interface IManageObserver
    {
        void BugsAdded(IEnumerable<Bug> bugs);
        void BugStatusChanged(Bug newBug);
    }
}
