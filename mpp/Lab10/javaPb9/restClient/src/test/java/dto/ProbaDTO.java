package dto;

public class ProbaDTO {
    private String nume;
    private String categorie;

    public ProbaDTO() {
    }

    public ProbaDTO(String nume, String categorie) {
        this.nume = nume;
        this.categorie = categorie;
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public String getCategorie() {
        return categorie;
    }

    public void setCategorie(String categorie) {
        this.categorie = categorie;
    }

    @Override
    public String toString() {
        return "dto.ProbaDTO{" +
                "nume='" + nume + '\'' +
                ", categorie='" + categorie + '\'' +
                '}';
    }
}
