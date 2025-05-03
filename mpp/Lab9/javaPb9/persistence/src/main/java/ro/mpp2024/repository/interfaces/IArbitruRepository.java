package ro.mpp2024.repository.interfaces;

import ro.mpp2024.model.Arbitru;

public interface IArbitruRepository extends DataBaseRepository<Long, Arbitru> {
    Arbitru findByUser(String username,String password);
}
