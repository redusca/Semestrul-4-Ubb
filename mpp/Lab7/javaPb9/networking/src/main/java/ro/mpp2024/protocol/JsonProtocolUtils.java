package ro.mpp2024.protocol;

import ro.mpp2024.DTO.DTOutils;
import ro.mpp2024.DTO.RezultatDTO;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;
import ro.mpp2024.model.Rezultat;

import java.util.Map;

public class JsonProtocolUtils {
    public static Response createLoggedResponse(Arbitru arbitru) {
        Response response = new Response();
        response.setType(ResponseType.LOGGED_ACC);
        response.setUser(DTOutils.getDTO(arbitru));
        return response;
    }

    public static Response createErrorResponse(String errorMessage) {
        Response response = new Response();
        response.setType(ResponseType.ERROR);
        response.setErrormessage(errorMessage);
        return response;
    }

    public static Response createRezultateResponse(Map<Participant, Long> participants) {
        Response response = new Response();
        response.setType(ResponseType.REZULTATE);
        response.setPunctaje(DTOutils.getDTO(participants));
        return response;
    }

    public static Response createAllRezultateResponse(Map<Participant, Long> participants) {
        Response response = new Response();
        response.setType(ResponseType.ALL_REZULTATE);
        response.setPunctaje(DTOutils.getDTO(participants));
        return response;
    }

    public static Response createParticipantiResponse(Iterable<Participant> participanti) {
        Response response = new Response();
        response.setType(ResponseType.PARTICIPANTI);
        response.setParticipanti(DTOutils.getDTOParticipanti(participanti));
        return response;
    }

    public static Response createOkResponse() {
        Response response = new Response();
        response.setType(ResponseType.OK);
        return response;
    }

    public static Response createRezultatAddedResponse(RezultatDTO rezultat) {
        Response response = new Response();
        response.setType(ResponseType.REZULTAT_ADDED);
        response.setRezultat(rezultat);
        return response;
    }

    public static Request createLoginRequest(Arbitru arbitru) {
        Request request = new Request();
        request.setType(RequestType.LOGIN);
        request.setUser(DTOutils.getDTO(arbitru));
        return request;
    }

    public static Request createAddRezultatRequest(Rezultat rezultat) {
        Request request = new Request();
        request.setType(RequestType.ADD_REZULTAT);
        request.setRezultat(DTOutils.getDTO(rezultat));
        return request;
    }

    public static Request createGetRezultateRequest() {
        Request request = new Request();
        request.setType(RequestType.GET_REZULTATE);
        return request;
    }

    public static Request createGetAllRezultateRequest() {
        Request request = new Request();
        request.setType(RequestType.GET_ALL_REZULTATE);
        return request;
    }

    public static Request createGetParticipantiRequest() {
        Request request = new Request();
        request.setType(RequestType.GET_PARTICIPANTI);
        return request;
    }

    public static Request createLogoutRequest(Arbitru arbitru) {
        Request request = new Request();
        request.setUser(DTOutils.getDTO(arbitru));
        request.setType(RequestType.LOGOUT);
        return request;
    }
}
