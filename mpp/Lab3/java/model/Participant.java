package ro.mpp2024.model;

import java.util.Objects;

public class Participant extends Entity<Long> {
    private String nume,prenume;
    private int varsta;

    public Participant() {
    }

    public Participant(Long id,String nume, String prenume, int varsta) {
        setId(id);
        this.nume = nume;
        this.prenume = prenume;
        this.varsta = varsta;
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public String getPrenume() {
        return prenume;
    }

    public void setPrenume(String prenume) {
        this.prenume = prenume;
    }

    public int getVarsta() {
        return varsta;
    }

    public void setVarsta(int varsta) {
        this.varsta = varsta;
    }

    @Override
    public String toString() {
        return "Participant{" +
                "nume='" + nume + '\'' +
                ", prenume='" + prenume + '\'' +
                ", varsta=" + varsta +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Participant participant)) return false;

        if (!Objects.equals(nume, participant.nume)) return false;
        if (!Objects.equals(prenume, participant.prenume)) return false;
        return varsta == participant.varsta;
    }
}
