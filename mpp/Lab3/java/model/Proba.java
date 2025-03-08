package ro.mpp2024.model;

import java.util.Objects;

public class Proba extends Entity<String> {
    private String nume;
    private Long id_arbitru;
    private Categorie categorie;

    public Proba() {
    }

    public Proba(String id,String nume, Categorie categorie) {
        setId(id);
        this.nume = nume;
        this.categorie = categorie;
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public Long getArbitru() {
        return id_arbitru;
    }

    public void setArbitru(Long arbitru) {
        this.id_arbitru = arbitru;
    }

    public Categorie getCategorie() {
        return categorie;
    }

    public void setCategorie(Categorie categorie) {
        this.categorie = categorie;
    }

    @Override
    public String toString() {
        return "Proba{" +
                "nume='" + nume + '\'' +
                ", arbitru=" + id_arbitru +
                ", categorie=" + categorie +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Proba proba)) return false;

        if (!Objects.equals(nume, proba.nume)) return false;
        if (!Objects.equals(id_arbitru, proba.id_arbitru)) return false;
        return categorie == proba.categorie;
    }

}
