public interface IRepository<ID, E> where E : Entity<ID>
{
    IEnumerable<E> FindAll();

    E FindOne(ID id);

    void Save(E entity);

    E Delete(ID id);

    E Update(ID id,E new_entity);
}
