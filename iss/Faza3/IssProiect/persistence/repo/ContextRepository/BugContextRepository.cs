using model;
using persistence.repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.repo.ContextRepository
{
    public class BugContextRepository : IBugRepository
    {
        private Context _context;

        public BugContextRepository(Context context)
        {
            _context = context;
        }

        public void Add(Bug bug)
        {
            _context.Bugs.Add(bug);
            _context.SaveChanges();
        }

        public void Delete(long bugNo)
        {
            _context.Bugs.Remove(FindByBugNo(bugNo));
            _context.SaveChanges();
        }

        public IEnumerable<Bug> FindAll()
        {
           return [.. _context.Bugs];
        }

        public Bug FindByBugNo(long bugNo)
        {
            return _context.Bugs.FirstOrDefault(b => b.BugNo == bugNo);
        }

        public IEnumerable<Bug> FindByStatus(string status)
        {
            return [.. _context.Bugs.Where(b => b.Status.ToString() == status)];
        }

        public void Update(Bug bug)
        {
            _context.Update(bug);
            _context.SaveChanges();
        }
    }
}
