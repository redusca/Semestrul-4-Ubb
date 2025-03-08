package ro.mpp2024.repository;

import ro.mpp2024.model.Proba;
import ro.mpp2024.repository.interfaces.DataBaseRepository;

import java.sql.ResultSet;

public class ProbaRepository implements DataBaseRepository<String, Proba> {
    @Override
    public Proba createEntityFromResultSet(ResultSet resultSet) {
        return null;
    }

    @Override
    public Iterable<Proba> findAll() {
        return null;
    }

    @Override
    public void save(Proba entity) {

    }

    @Override
    public Proba delete(String s) {
        return null;
    }

    @Override
    public Proba update(String s, Proba new_entity) {
        return null;
    }
}

