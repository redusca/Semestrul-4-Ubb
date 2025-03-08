
public interface DataBaseRepository<ID, E> : IRepository<ID, E> where E : Entity<ID>
{
    void LoadData();
}
