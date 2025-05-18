package ro.mpp2024.repository.interfaces;

import ro.mpp2024.model.Entity;

import java.sql.ResultSet;
import java.sql.SQLException;

public interface DataBaseRepository<Id,E extends Entity<Id>> extends Repository<Id, E> {

    E createEntityFromResultSet(ResultSet resultSet) throws SQLException;
}
