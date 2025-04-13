package ro.mpp2024.protocol;

import ro.mpp2024.DTO.DTOutils;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;
import ro.mpp2024.model.Rezultat;

import java.util.Map;

public class JsonProtocolUtils {
    public static Response createLoggedResponse(Arbitru arbitru) {
        Response response = new Response();
        response.setTypeJava(ResponseType.LOGGED_ACC);
        response.setTypeCsharp(CharpTranslateType.toNumber(ResponseType.LOGGED_ACC));
        response.setUser(DTOutils.getDTO(arbitru));
        return response;
    }

    public static Response createErrorResponse(String errorMessage) {
        Response response = new Response();
        response.setTypeJava(ResponseType.ERROR);
        response.setTypeCsharp(CharpTranslateType.toNumber(ResponseType.ERROR));
        response.setErrormessage(errorMessage);
        return response;
    }

    public static Response createRezultateResponse(Map<Participant, Long> participants) {
        Response response = new Response();
        response.setTypeJava(ResponseType.REZULTATE);
        response.setTypeCsharp(CharpTranslateType.toNumber(ResponseType.REZULTATE));
        response.setPunctaje(DTOutils.getDTO(participants));
        return response;
    }

    public static Response createAllRezultateResponse(Map<Participant, Long> participants) {
        Response response = new Response();
        response.setTypeJava(ResponseType.ALL_REZULTATE);
        response.setTypeCsharp(CharpTranslateType.toNumber(ResponseType.ALL_REZULTATE));
        response.setPunctaje(DTOutils.getDTO(participants));
        return response;
    }

    public static Response createParticipantiResponse(Iterable<Participant> participanti) {
        Response response = new Response();
        response.setTypeJava(ResponseType.PARTICIPANTI);
        response.setTypeCsharp(CharpTranslateType.toNumber(ResponseType.PARTICIPANTI));
        response.setParticipanti(DTOutils.getDTOParticipanti(participanti));
        return response;
    }

    public static Response createOkResponse() {
        Response response = new Response();
        response.setTypeJava(ResponseType.OK);
        response.setTypeCsharp(CharpTranslateType.toNumber(ResponseType.OK));
        return response;
    }

    public static Response createRezultatAddedResponse(Rezultat rezultat) {
        Response response = new Response();
        response.setTypeJava(ResponseType.REZULTAT_ADDED);
        response.setTypeCsharp(CharpTranslateType.toNumber(ResponseType.REZULTAT_ADDED));
        response.setRezultat(DTOutils.getDTO(rezultat));
        return response;
    }

    public static Request createLoginRequest(Arbitru arbitru) {
        Request request = new Request();
        request.setTypeJava(RequestType.LOGIN);
        request.setTypeCsharp(CharpTranslateType.toNumber(RequestType.LOGIN));
        request.setUser(DTOutils.getDTO(arbitru));
        return request;
    }

    public static Request createAddRezultatRequest(Rezultat rezultat) {
        Request request = new Request();
        request.setTypeJava(RequestType.ADD_REZULTAT);
        request.setTypeCsharp(CharpTranslateType.toNumber(RequestType.ADD_REZULTAT));
        request.setRezultat(DTOutils.getDTO(rezultat));
        return request;
    }

    public static Request createGetRezultateRequest() {
        Request request = new Request();
        request.setTypeJava(RequestType.GET_REZULTATE);
        request.setTypeCsharp(CharpTranslateType.toNumber(RequestType.GET_REZULTATE));
        return request;
    }

    public static Request createGetAllRezultateRequest() {
        Request request = new Request();
        request.setTypeJava(RequestType.GET_ALL_REZULTATE);
        request.setTypeCsharp(CharpTranslateType.toNumber(RequestType.GET_ALL_REZULTATE));
        return request;
    }

    public static Request createGetParticipantiRequest() {
        Request request = new Request();
        request.setTypeJava(RequestType.GET_PARTICIPANTI);
        request.setTypeCsharp(CharpTranslateType.toNumber(RequestType.GET_PARTICIPANTI));
        return request;
    }

    public static Request createLogoutRequest(Arbitru arbitru) {
        Request request = new Request();
        request.setUser(DTOutils.getDTO(arbitru));
        request.setTypeJava(RequestType.LOGOUT);
        request.setTypeCsharp(CharpTranslateType.toNumber(RequestType.LOGOUT));
        return request;
    }
}
