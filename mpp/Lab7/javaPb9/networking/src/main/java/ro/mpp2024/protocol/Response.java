package ro.mpp2024.protocol;

import ro.mpp2024.DTO.PunctajParticipantDTO;
import ro.mpp2024.DTO.RezultatDTO;
import ro.mpp2024.DTO.UserDTO;

import java.io.Serializable;
import java.util.Arrays;

public class Response implements Serializable {
    private ResponseType type;
    private String errormessage;
    private UserDTO user;
    private RezultatDTO rezultat;
    private PunctajParticipantDTO[] punctaje;
    private PunctajParticipantDTO[] participanti;

    public Response() {
    }

    public ResponseType getType() {
        return type;
    }

    public void setType(ResponseType type) {
        this.type = type;
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
                "type=" + type +
                ", errormessage='" + errormessage + '\'' +
                ", user=" + user +
                ", rezultat=" + rezultat +
                ", punctaje=" + Arrays.toString(punctaje) +
                '}';
    }
}
