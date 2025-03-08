package ro.mpp2024.model;

import java.util.Objects;

public class Rezultat extends Entity<String> {
    private Long id_participant;
    private String id_proba;
    private Long numar_puncte;

    public Rezultat() {
    }

    public Rezultat(String id,Long id_participant, String id_proba, Long numar_puncte) {
        setId(id);
        this.id_participant = id_participant;
        this.id_proba = id_proba;
        this.numar_puncte = numar_puncte;
    }

    public Long getNumar_puncte() {
        return numar_puncte;
    }

    public void setNumar_puncte(Long numar_puncte) {
        this.numar_puncte = numar_puncte;
    }

    @Override
    public String toString() {
        return "Rezultat{" +
                "id_participant=" + id_participant +
                ", id_proba='" + id_proba + '\'' +
                ", numar_puncte=" + numar_puncte +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Rezultat rezultat)) return false;

        if (!Objects.equals(id_participant, rezultat.id_participant)) return false;
        if (!Objects.equals(id_proba, rezultat.id_proba)) return false;
        return Objects.equals(numar_puncte, rezultat.numar_puncte);
    }
}
