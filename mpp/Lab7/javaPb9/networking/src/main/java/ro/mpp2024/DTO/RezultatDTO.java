package ro.mpp2024.DTO;

import java.io.Serializable;

public class RezultatDTO implements Serializable {
    private String idProba;
    private Long idParticipant;
    private String numeParticipant;
    private String prenumeParticipant;
    private long puncte;

    public RezultatDTO() {
    }

    public RezultatDTO(String idProba, String numeParticipant, String prenumeParticipant, Long idParticipant, long puncte) {
        this.idProba = idProba;
        this.numeParticipant = numeParticipant;
        this.prenumeParticipant = prenumeParticipant;
        this.idParticipant = idParticipant;
        this.puncte = puncte;
    }

    public String getIdProba() {
        return idProba;
    }

    public Long getIdParticipant() {
        return idParticipant;
    }

    public long getPuncte() {
        return puncte;
    }

    public String getNumeParticipant() {
        return numeParticipant;
    }

    public String getPrenumeParticipant() {
        return prenumeParticipant;
    }

    @Override
    public String toString() {
        return "RezultatDTO{" +
                "idProba='" + idProba + '\'' +
                ", idParticipant=" + idParticipant +
                ", puncte=" + puncte +
                '}';
    }
}
