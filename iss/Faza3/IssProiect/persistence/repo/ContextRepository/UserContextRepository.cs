using model;
using repository;

namespace persistence.repo.ContextRepository
{
    public class UserContextRepository : IUserRepository
    {
        Context _context;
        public UserContextRepository(Context context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            User u = FindById(id);
            _context.Users.Remove(u);
            _context.SaveChanges();
        }

        public IEnumerable<User> FindAll()
        {
            return _context.Users.ToList();
        }

        public User FindById(long id)
        {
            return _context.Users.Find(id);
        }

        public User FindByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
