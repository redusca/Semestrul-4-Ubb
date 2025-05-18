package ro.mpp2024.repository.interfaces;

import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Proba;

public interface IProbaRepository extends DataBaseRepository<String, Proba> {
        Arbitru getArbitru(String id);
}
