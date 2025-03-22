public interface IArbitruRepository : DataBaseRepository<long, Arbitru>
{
    Arbitru FindByUser(string username);
}
