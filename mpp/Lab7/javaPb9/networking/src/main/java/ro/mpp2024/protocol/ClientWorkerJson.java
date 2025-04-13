package ro.mpp2024.protocol;

import org.apache.logging.log4j.LogManager;

import org.apache.logging.log4j.Logger;
import ro.mpp2024.DTO.DTOutils;
import ro.mpp2024.DTO.UserDTO;
import ro.mpp2024.IManageObserver;
import ro.mpp2024.IService;
import ro.mpp2024.ManageException;
import ro.mpp2024.model.*;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import com.google.gson.Gson;

public class ClientWorkerJson implements Runnable, IManageObserver {
    private IService server;
    private Socket connection;

    private BufferedReader input;
    private PrintWriter output;
    private Gson gsonFormatter;
    private volatile boolean connected;

    private static Logger logger = LogManager.getLogger(ClientWorkerJson.class);

    public ClientWorkerJson(IService server, Socket connection) {
        this.server = server;
        this.connection = connection;
        gsonFormatter=new Gson();
        try{
            output=new PrintWriter(connection.getOutputStream());
            input=new BufferedReader(new InputStreamReader(connection.getInputStream()));
            connected=true;
        } catch (IOException e) {
            logger.error(e);
            logger.error(e.getStackTrace());
        }
    }

    @Override
    public void run() {
        while(connected){
            try {
                String requestLine=input.readLine();
                Request request=gsonFormatter.fromJson(requestLine, Request.class);
                Response response=handleRequest(request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException e) {
                logger.error(e);
                logger.error(e.getStackTrace());
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                logger.error(e);
                logger.error(e.getStackTrace());
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            logger.error("Error "+e);
        }
    }

    private static Response okResponse=JsonProtocolUtils.createOkResponse();

    private void sendResponse(Response response) {
        String responseLine=gsonFormatter.toJson(response);
        logger.debug("sending response "+responseLine);
        synchronized (output) {
            output.println(responseLine);
            output.flush();
        }
    }

    private Response handleRequest(Request request) {
        Response response=null;
        switch (request.getTypeJava()){
            case LOGIN:
                logger.debug("Login request "+request.getUser());
                UserDTO userDTO=request.getUser();
                Arbitru arbitru= DTOutils.getFromDTO(userDTO);
                try{
                    return JsonProtocolUtils.createLoggedResponse(
                            server.login(arbitru.getUsername(), arbitru.getPassword(),
                                    this));
                }
                catch (ManageException e){
                    logger.error(e);
                    return JsonProtocolUtils.createErrorResponse(e.getMessage());
                }
            case GET_REZULTATE:
                logger.debug("Get rezultate request for"+request.getUser());
                UserDTO userDTO3=request.getUser();
                Arbitru arbitru3= DTOutils.getFromDTO(userDTO3);
                try{
                    return JsonProtocolUtils.createRezultateResponse(
                            server.getParticipantiPuncteDesc(arbitru3.getId_proba()));
                }
                catch (ManageException e) {
                    logger.error(e);
                    return JsonProtocolUtils.createErrorResponse(e.getMessage());
                }
            case GET_ALL_REZULTATE:
                logger.debug("Get all rezultate request for"+request.getUser());
                try{
                    return JsonProtocolUtils.createAllRezultateResponse(
                            server.getParticipantiAlfabetic());
                }
                catch (ManageException e) {
                    logger.error(e);
                    return JsonProtocolUtils.createErrorResponse(e.getMessage());
                }

            case ADD_REZULTAT:
                logger.debug("Add rezultat request "+request.getRezultat());
                try{
                    server.addRezultat(request.getRezultat().getIdParticipant(),
                            request.getRezultat().getNumeParticipant(),
                            request.getRezultat().getPrenumeParticipant(),
                            request.getRezultat().getIdProba(),
                            request.getRezultat().getPuncte()
                    );
                    return okResponse;
                }
                catch (ManageException e){
                    logger.error(e);
                    return JsonProtocolUtils.createErrorResponse(e.getMessage());
                }
            case GET_PARTICIPANTI:
                logger.debug("Get participanti request for"+request.getUser());
                try{
                    return JsonProtocolUtils.createParticipantiResponse(
                            server.getParticipanti());
                }
                catch (ManageException e) {
                    logger.error(e);
                    return JsonProtocolUtils.createErrorResponse(e.getMessage());
                }
            case LOGOUT:
                logger.debug("Logout request for"+request.getUser());
                UserDTO userDTO2=request.getUser();
                Arbitru arbitru2= DTOutils.getFromDTO(userDTO2);
                try{
                    server.logout(arbitru2,this);
                    connected=false;
                    return okResponse;
                }
                catch (ManageException e) {
                    logger.error(e);
                    return JsonProtocolUtils.createErrorResponse(e.getMessage());
                }
            default:
                logger.error("Unknown request "+request);
        }
        return response;
    }


    @Override
    public void RezultatAdded(Long id,String nume,String prenume, String proba, long puncte) throws ManageException {
        Response response=JsonProtocolUtils.createRezultatAddedResponse(
                new Rezultat(
                        -1L,
                        new Participant(id, nume, prenume, 0),
                        new Proba(proba,"", Categorie.ciclism),
                        puncte
                ));
        logger.debug("Sending rezultat added response "+response);
        try{
            sendResponse(response);
        }
        catch (Exception e){
            logger.error("Error sending response "+e);
            logger.error(e.getStackTrace());
        }
    }
}
