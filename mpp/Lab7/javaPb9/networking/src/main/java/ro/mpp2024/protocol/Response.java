package ro.mpp2024.protocol;

import ro.mpp2024.DTO.PunctajParticipantDTO;
import ro.mpp2024.DTO.RezultatDTO;
import ro.mpp2024.DTO.UserDTO;

import java.io.Serializable;
import java.util.Arrays;

public class Response implements Serializable {
    private ResponseType typeJava;
    private int typeCsharp;
    private String errormessage;
    private UserDTO user;
    private RezultatDTO rezultat;
    private PunctajParticipantDTO[] punctaje;
    private PunctajParticipantDTO[] participanti;

    public Response() {
    }

    public ResponseType getTypeJava() {
        return typeJava;
    }

    public void setTypeJava(ResponseType type) {
        this.typeJava = type;
    }

    public int getTypeCsharp() {
        return CharpTranslateType.toNumber(typeJava);
    }

    public void setTypeCsharp(int typeCsharp) {
        this.typeCsharp = typeCsharp;
    }

    public String getErrormessage() {
        return errormessage;
    }

    public void setErrormessage(String errormessage) {
        this.errormessage = errormessage;
    }

    public UserDTO getUser() {
        return user;
    }

    public void setUser(UserDTO user) {
        this.user = user;
    }

    public PunctajParticipantDTO[] getPunctaje() {
        return punctaje;
    }

    public void setPunctaje(PunctajParticipantDTO[] punctaje) {
        this.punctaje = punctaje;
    }

    public RezultatDTO getRezultat() {
        return rezultat;
    }

    public void setRezultat(RezultatDTO rezultat) {
        this.rezultat = rezultat;
    }

    public PunctajParticipantDTO[] getParticipanti() {
        return participanti;
    }

    public void setParticipanti(PunctajParticipantDTO[] participanti) {
        this.participanti = participanti;
    }

    @Override
    public String toString() {
        return "Response{" +
                "type=" + typeJava +
                ", errormessage='" + errormessage + '\'' +
                ", user=" + user +
                ", rezultat=" + rezultat +
                ", punctaje=" + Arrays.toString(punctaje) +
                '}';
    }
}
