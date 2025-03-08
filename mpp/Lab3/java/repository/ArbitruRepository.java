package ro.mpp2024.repository;

import ro.mpp2024.model.Arbitru;
import ro.mpp2024.repository.interfaces.DataBaseRepository;

import java.sql.ResultSet;

public class ArbitruRepository implements DataBaseRepository<Long, Arbitru> {

    @Override
    public Arbitru createEntityFromResultSet( ResultSet resultSet) {
        return null;
    }

    @Override
    public Iterable<Arbitru> findAll() {
        return null;
    }

    @Override
    public void save(Arbitru entity) {

    }

    @Override
    public Arbitru delete(Long aLong) {
        return null;
    }

    @Override
    public Arbitru update(Long aLong, Arbitru new_entity) {
        return null;
    }

}
