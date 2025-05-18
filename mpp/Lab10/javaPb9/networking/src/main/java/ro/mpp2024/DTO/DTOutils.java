package ro.mpp2024.DTO;

import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;
import ro.mpp2024.model.Rezultat;

import java.security.KeyPair;
import java.util.LinkedHashMap;
import java.util.Map;

public class DTOutils {
    public static Arbitru getFromDTO(UserDTO arbitruDTO) {
        return new Arbitru(
                arbitruDTO.getId(),
                arbitruDTO.getNume(),
                arbitruDTO.getUsername(),
                arbitruDTO.getPassword(),
                arbitruDTO.getProbaId()
        );
    }

    public static UserDTO getDTO(Arbitru arbitru) {
        return new UserDTO(
                arbitru.getId(),
                arbitru.getNume(),
                arbitru.getUsername(),
                arbitru.getPassword(),
                arbitru.getId_proba()
        );
    }

    public static RezultatDTO getDTO(Rezultat rezultat) {
        return new RezultatDTO(
                rezultat.getProba().getId(),
                rezultat.getParticipant().getNume(),
                rezultat.getParticipant().getPrenume(),
                rezultat.getParticipant().getId(),
                rezultat.getNumar_puncte()
        );
    }

    public static PunctajParticipantDTO[] getDTO(Map<Participant, Long> punctajParticipant) {
        PunctajParticipantDTO[] punctaje = new PunctajParticipantDTO[punctajParticipant.size()];
        int i = 0;
        for (Map.Entry<Participant, Long> entry : punctajParticipant.entrySet()) {
            punctaje[i++] = new PunctajParticipantDTO(
                    entry.getKey().getId(),
                    entry.getKey().getNume(),
                    entry.getKey().getPrenume(),
                    entry.getKey().getVarsta(),
                    entry.getValue()
            );
        }
        return punctaje;
    }

    public static PunctajParticipantDTO[] getDTOParticipanti(Iterable<Participant> participanti) {
        int size=0;
        for (Participant participant : participanti)
            size++;

        PunctajParticipantDTO[] punctaje = new PunctajParticipantDTO[size];
        int i = 0;
        for (Participant participant : participanti) {
            punctaje[i++] = new PunctajParticipantDTO(
                    participant.getId(),
                    participant.getNume(),
                    participant.getPrenume(),
                    participant.getVarsta(),
                    0
            );
        }
        return punctaje;
    }

    public static Map<Participant, Long> getParticiapntiFromDTO(PunctajParticipantDTO[] punctaje) {
        Map<Participant, Long> participanti = new LinkedHashMap<>();
        for (PunctajParticipantDTO punctaj : punctaje) {
            participanti.put(
                    new Participant(
                            punctaj.getIdParticipant(),
                            punctaj.getNume(),
                            punctaj.getPrenume(),
                            punctaj.getVarsta()),
                    punctaj.getPunctaj());
        }
        return participanti;
    }
}
