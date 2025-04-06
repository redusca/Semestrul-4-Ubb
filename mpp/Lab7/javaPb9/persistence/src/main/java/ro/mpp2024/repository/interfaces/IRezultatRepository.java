package ro.mpp2024.repository.interfaces;

import ro.mpp2024.model.Participant;
import ro.mpp2024.model.Rezultat;

import java.util.Map;

public interface IRezultatRepository extends DataBaseRepository<Long, Rezultat> {
    Map<Participant, Long> listaParticipantiPuncteAlfabetic();

    Map<Participant, Long> listaParticiapntPuncteProbaDescrescator(String id_proba);
}
