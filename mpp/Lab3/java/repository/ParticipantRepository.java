package ro.mpp2024.repository;

import ro.mpp2024.model.Participant;
import ro.mpp2024.repository.interfaces.DataBaseRepository;

import java.sql.ResultSet;

public class ParticipantRepository implements DataBaseRepository<Long, Participant> {

    @Override
    public Participant createEntityFromResultSet(ResultSet resultSet) {
        return null;
    }

    @Override
    public Iterable<Participant> findAll() {
        return null;
    }

    @Override
    public void save(Participant entity) {

    }

    @Override
    public Participant delete(Long aLong) {
        return null;
    }

    @Override
    public Participant update(Long aLong, Participant new_entity) {
        return null;
    }
}
