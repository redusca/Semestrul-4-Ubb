using model;
using System.Collections.Generic;

namespace repository
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        void Delete(long id);
        User FindById(long id);
        User FindByUsername(string username);
        IEnumerable<User> FindAll();
    }
}