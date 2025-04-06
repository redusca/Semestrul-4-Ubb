package ro.mpp2024.protocol;

import ro.mpp2024.DTO.DTOutils;
import ro.mpp2024.IManageObserver;
import ro.mpp2024.IService;
import ro.mpp2024.ManageException;
import ro.mpp2024.model.*;

import com.google.gson.Gson;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

import java.util.Map;

public class ServicesJsonProxy implements IService {

    private String host;
    private int port;

    private IManageObserver client;

    private BufferedReader input;
    private PrintWriter output;
    private Gson gsonFormatter;
    private Socket connection;

    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;

    private static Logger logger = LogManager.getLogger(ServicesJsonProxy.class);

    public ServicesJsonProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses=new LinkedBlockingQueue<Response>();
    }


    public Arbitru login(String username, String password, IManageObserver client) throws ManageException {
        initializeConnection();
        Request request=JsonProtocolUtils.createLoginRequest(new Arbitru("",username,password,""));
        sendRequest(request);
         Response response=readResponse();

        if (response.getType()== ResponseType.ERROR) {
            closeConnection();
            throw new ManageException(response.getErrormessage());
        }

        this.client = client;

        return DTOutils.getFromDTO(response.getUser());
    }


    public void addRezultat(Long idParticipant,String Nume,String Prenume, String idProba, long puncte) {
        Proba proba=new Proba(idProba,"", Categorie.ciclism);
        Participant participant=new Participant(idParticipant,Nume,Prenume,0);
        Request request=JsonProtocolUtils.createAddRezultatRequest(new Rezultat(-1L,participant,proba,puncte));
        sendRequest(request);
         Response response=readResponse();
        if (response.getType()== ResponseType.ERROR) {
            throw new ManageException(response.getErrormessage());
        }

    }


    public Map<Participant, Long> getParticipantiAlfabetic() {
        Request request= JsonProtocolUtils.createGetAllRezultateRequest();
        sendRequest(request);
         Response response=readResponse();
        if (response.getType()== ResponseType.ERROR) {
            throw new ManageException(response.getErrormessage());
        }

        return DTOutils.getParticiapntiFromDTO(response.getPunctaje());
    }


    public Map<Participant, Long> getParticipantiPuncteDesc(String Proba) {
        Request request= JsonProtocolUtils.createGetRezultateRequest();
        request.setUser(DTOutils.getDTO(new Arbitru("","","",Proba)));
        sendRequest(request);
         Response response=readResponse();
        if (response.getType()== ResponseType.ERROR) {
            throw new ManageException(response.getErrormessage());
        }

        return DTOutils.getParticiapntiFromDTO(response.getPunctaje());
    }


    public Iterable<Participant> getParticipanti() {
        Request request= JsonProtocolUtils.createGetParticipantiRequest();
        sendRequest(request);
         Response response=readResponse();
        if (response.getType()== ResponseType.ERROR) {
            throw new ManageException(response.getErrormessage());
        }

        return DTOutils
                .getParticiapntiFromDTO(response.getParticipanti()).keySet()
                 .stream()
                 .toList();
    }

    @Override
    public void logout(Arbitru arbitru,IManageObserver client) throws ManageException {
        Request request= JsonProtocolUtils.createLogoutRequest(arbitru);
        sendRequest(request);
         Response response=readResponse();
        if (response.getType()== ResponseType.ERROR) {
            throw new ManageException(response.getErrormessage());
        }
        closeConnection();

    }

    private void closeConnection() {
        finished=true;
        try {
            input.close();
            output.close();
            connection.close();
            logger.debug("Connection closed with {}" ,client);
            client=null;
        } catch (IOException e) {
            logger.error(e);
            logger.error(e.getStackTrace());
        }

    }

    private void sendRequest(Request request)throws ManageException {
        String reqLine=gsonFormatter.toJson(request);
        try {
            output.println(reqLine);
            output.flush();
        } catch (Exception e) {
            throw new ManageException("Error sending object "+e);
        }

    }

    private Response readResponse() throws ManageException {
        Response response=null;
        try{
            response=qresponses.take();
        } catch (InterruptedException e) {
            logger.error(e);
            logger.error(e.getStackTrace());
        }
        return response;
    }
    private void initializeConnection() throws ManageException {
        try {
            gsonFormatter=new Gson();
            connection=new Socket(host,port);
            output=new PrintWriter(connection.getOutputStream());
            output.flush();
            input=new BufferedReader(new InputStreamReader(connection.getInputStream()));
            finished=false;
            startReader();
        } catch (IOException e) {
            logger.error(e);
            logger.error(e.getStackTrace());
        }
    }
    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }

    private void handleUpdate(Response response){
        if (response.getType()==ResponseType.REZULTAT_ADDED){
            try {
                client.RezultatAdded(response.getRezultat().getIdParticipant(),
                        response.getRezultat().getNumeParticipant(),
                        response.getRezultat().getPrenumeParticipant(),
                        response.getRezultat().getIdProba(),
                        response.getRezultat().getPuncte());
            } catch (ManageException e) {
                logger.error(e);
                logger.error(e.getStackTrace());
            }
        }
    }

    private boolean isUpdate(Response response){
        if(response==null)
            return false;
        return response.getType()==ResponseType.REZULTAT_ADDED;
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    String responseLine=input.readLine();
                    logger.debug("response received {}",responseLine);
                    Response response=gsonFormatter.fromJson(responseLine, Response.class);
                    if (isUpdate(response)){
                        handleUpdate(response);
                    }else{
                        try {
                            if(response != null)
                                qresponses.put(response);
                        } catch (InterruptedException e) {
                            logger.error(e);
                            logger.error(e.getStackTrace());
                        }
                    }
                } catch (IOException e) {
                    logger.error("Reading error "+e);
                }
            }
        }
    }
}
