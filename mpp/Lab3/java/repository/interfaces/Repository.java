package ro.mpp2024.repository.interfaces;

import ro.mpp2024.model.Entity;

public interface Repository<Id,E extends Entity<Id>> {

    Iterable<E> findAll();

    void save(E entity);

    E delete(Id id);

    E update(Id id,E new_entity);
}
