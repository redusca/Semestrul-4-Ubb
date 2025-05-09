using model;

namespace persistence.repo.Interface
{
    public interface IBugRepository
    {
        void Add(Bug bug);
        void Update(Bug bug);
        void Delete(long bugNo);
        Bug FindByBugNo(long bugNo);
        IEnumerable<Bug> FindByStatus(string status);
        IEnumerable<Bug> FindAll();
    }
}
