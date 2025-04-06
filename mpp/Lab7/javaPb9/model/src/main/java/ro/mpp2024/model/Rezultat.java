package ro.mpp2024.model;

import java.util.Objects;

public class Rezultat extends Entity<Long> {
    private Participant participant;
    private Proba proba;
    private Long numar_puncte;

    public Rezultat() {
    }

    public Rezultat(Long id,Participant participant, Proba proba, Long numar_puncte) {
        setId(id);
        this.participant = participant;
        this.proba = proba;
        this.numar_puncte = numar_puncte;
    }

    public Long getNumar_puncte() {
        return numar_puncte;
    }

    public void setNumar_puncte(Long numar_puncte) {
        this.numar_puncte = numar_puncte;
    }

    public Participant getParticipant() {
        return participant;
    }

    public void setParticipant(Participant participant) {
        this.participant = participant;
    }

    public Proba getProba() {
        return proba;
    }

    public void setProba(Proba proba) {
        this.proba = proba;
    }

    @Override
    public String toString() {
        return "Rezultat{" +
                "participant=" + participant +
                ", proba=" + proba +
                ", numar_puncte=" + numar_puncte +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Rezultat rezultat)) return false;

        if (!Objects.equals(participant, rezultat.participant)) return false;
        if (!Objects.equals(proba, rezultat.proba)) return false;
        return Objects.equals(numar_puncte, rezultat.numar_puncte);
    }
}
