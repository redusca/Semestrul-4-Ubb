
using System.Data;

public interface DataBaseRepository<ID, E> : IRepository<ID, E> where E : Entity<ID>
{
    E fromSettoEntity(IDataReader read);
}
