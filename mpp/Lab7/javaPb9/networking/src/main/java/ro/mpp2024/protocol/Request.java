package ro.mpp2024.protocol;

import ro.mpp2024.DTO.PunctajParticipantDTO;
import ro.mpp2024.DTO.RezultatDTO;
import ro.mpp2024.DTO.UserDTO;
import ro.mpp2024.model.Rezultat;

import java.util.Arrays;

public class Request {
    private RequestType type;
    private UserDTO user;
    private RezultatDTO rezultat;


    public Request() {}

    public RequestType getType() {
        return type;
    }

    public void setType(RequestType type) {
        this.type = type;
    }

    public RezultatDTO getRezultat() {
        return rezultat;
    }

    public void setRezultat(RezultatDTO rezultat) {
        this.rezultat = rezultat;
    }

    public UserDTO getUser() {
        return user;
    }

    public void setUser(UserDTO user) {
        this.user = user;
    }

    public RezultatDTO getRezultatDTO() {
        return rezultat;
    }

    public void setRezultatDTO(RezultatDTO rezultat) {
        this.rezultat = rezultat;
    }

    @Override
    public String toString() {
        return "Request{" +
                "type=" + type +
                ", user=" + user +
                ", rezultat=" + rezultat +
                '}';
    }
}
