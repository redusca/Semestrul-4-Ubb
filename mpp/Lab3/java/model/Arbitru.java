package ro.mpp2024.model;

import java.util.Objects;

public class Arbitru extends Entity<Long> {
    private String nume;
    private String username;
    private String password;
    private String id_proba;

    public Arbitru() {
    }

    public Arbitru(Long id ,String nume, String username, String password, Proba proba) {
        setId(id);
        this.nume = nume;
        this.username = username;
        this.password = password;
        this.id_proba = proba.getId();
        proba.setArbitru(id);
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getId_proba() {
        return id_proba;
    }

    public void setId_proba(String id_proba) {
        this.id_proba = id_proba;
    }

    @Override
    public String toString() {
        return "Arbitru{" +
                "nume='" + nume + '\'' +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                ", id_proba=" + id_proba +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        Arbitru arbitru = (Arbitru) o;

        if (!Objects.equals(nume, arbitru.nume)) return false;
        if (!Objects.equals(username, arbitru.username)) return false;
        if (!Objects.equals(password, arbitru.password)) return false;
        return Objects.equals(id_proba, arbitru.id_proba);
    }

}
