package ro.mpp2024.DTO;

import java.io.Serializable;

public class UserDTO implements Serializable {
    private Long id;
    private String nume;
    private String username;
    private String password;
    private String id_proba;

    public UserDTO() {
    }

    public UserDTO(Long id,String nume, String username, String password, String probaId) {
        this.id = id;
        this.nume = nume;
        this.username = username;
        this.password = password;
        this.id_proba = probaId;
    }

    public UserDTO(String nume, String username, String password, String probaId) {
        id = -1L;
        this.nume = nume;
        this.username = username;
        this.password = password;
        this.id_proba = probaId;
    }

    public Long getId() {
        return id;
    }

    public String getNume() {
        return nume;
    }

    public String getUsername() {
        return username;
    }

    public String getPassword() {
        return password;
    }

    public String getProbaId() {
        return id_proba;
    }

    @Override
    public String toString() {
        return "UserDTO{" +
                "nume='" + nume + '\'' +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                ", probaId='" + id_proba + '\'' +
                '}';
    }
}
