package ro.mpp2024.protocol;

import ro.mpp2024.DTO.RezultatDTO;
import ro.mpp2024.DTO.UserDTO;

import java.io.Serializable;

public class Request implements Serializable {
    private RequestType typeJava;
    private int typeCsharp;
    private UserDTO user;
    private RezultatDTO rezultat;


    public Request() {}

    public RequestType getTypeJava() {
        return typeJava;
    }

    public void setTypeJava(RequestType type) {
        this.typeJava = type;
    }

    public int getTypeCsharp() {
        return CharpTranslateType.toNumber(typeJava);
    }

    public void setTypeCsharp(int typeCsharp) {
        this.typeCsharp = typeCsharp;
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
                "type=" + typeJava +
                ", user=" + user +
                ", rezultat=" + rezultat +
                '}';
    }
}
