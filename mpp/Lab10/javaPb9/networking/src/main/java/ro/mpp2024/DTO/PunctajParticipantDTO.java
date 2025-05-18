package ro.mpp2024.DTO;

import java.io.Serializable;

public class PunctajParticipantDTO implements Serializable {
    private Long idParticipant;
    private String nume,prenume;
    private int varsta;
    private long punctaj;

    public PunctajParticipantDTO() {
    }

    public PunctajParticipantDTO(long idParticipant, String nume, String prenume, int varsta, long punctaj) {
        this.idParticipant = idParticipant;
        this.nume = nume;
        this.prenume = prenume;
        this.varsta = varsta;
        this.punctaj = punctaj;
    }

    public Long getIdParticipant() {
        return idParticipant;
    }

    public String getNume() {
        return nume;
    }

    public String getPrenume() {
        return prenume;
    }

    public int getVarsta() {
        return varsta;
    }

    public long getPunctaj() {
        return punctaj;
    }

    @Override
    public String toString() {
        return "PunctajParticipantDTO{" +
                "idParticipant=" + idParticipant +
                ", nume='" + nume + '\'' +
                ", prenume='" + prenume + '\'' +
                ", varsta=" + varsta +
                ", punctaj=" + punctaj +
                '}';
    }
}
