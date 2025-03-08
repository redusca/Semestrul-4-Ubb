package ro.mpp2024.repository;

import ro.mpp2024.model.Rezultat;
import ro.mpp2024.repository.interfaces.DataBaseRepository;

import java.sql.ResultSet;

public class RezultatRepository implements DataBaseRepository<String, Rezultat> {

    @Override
    public Rezultat createEntityFromResultSet(ResultSet resultSet) {
        return null;
    }

    @Override
    public Iterable<Rezultat> findAll() {
        return null;
    }

    @Override
    public void save(Rezultat entity) {

    }

    @Override
    public Rezultat delete(String s) {
        return null;
    }

    @Override
    public Rezultat update(String s, Rezultat new_entity) {
        return null;
    }
}
