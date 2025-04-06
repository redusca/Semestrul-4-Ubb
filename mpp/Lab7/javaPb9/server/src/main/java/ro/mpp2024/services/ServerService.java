package ro.mpp2024.services;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.IManageObserver;
import ro.mpp2024.IService;
import ro.mpp2024.ManageException;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;
import ro.mpp2024.model.Proba;
import ro.mpp2024.model.Rezultat;
import ro.mpp2024.repository.interfaces.IArbitruRepository;
import ro.mpp2024.repository.interfaces.IParticipantRepository;
import ro.mpp2024.repository.interfaces.IProbaRepository;
import ro.mpp2024.repository.interfaces.IRezultatRepository;

import java.util.LinkedHashMap;
import java.util.Map;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class ServerService implements IService {
    IRezultatRepository rezultatRepository;
    IParticipantRepository participantRepository;
    IProbaRepository probaRepository;
    IArbitruRepository arbitruRepository;

    private Map<String, IManageObserver> loggedClients;

    private static final Logger logger= LogManager.getLogger();

    public ServerService(IArbitruRepository arb, IRezultatRepository rez, IParticipantRepository par, IProbaRepository proba) {
        this.arbitruRepository = arb;
        this.rezultatRepository = rez;
        this.participantRepository = par;
        this.probaRepository = proba;
        logger.info("Service Rezultat made");
        loggedClients = new LinkedHashMap<String, IManageObserver>();
    }

    public synchronized Arbitru login(String username, String password, IManageObserver client) throws ManageException {

        Arbitru arbitru =  arbitruRepository.findByUser(username, password);
        if(loggedClients.containsKey(username)){
            return new Arbitru(-2L,"","","","");
        }
        loggedClients.put(username, client);
        return arbitru;
    }

    public synchronized void addRezultat(Long idParticipant,String Nume,String Prenume, String idProba, long puncte) {
        logger.traceEntry();
        Participant  par = participantRepository.findOne(idParticipant);
        Proba proba = probaRepository.findOne(idProba);
        rezultatRepository.save(new Rezultat(0L,par, proba, puncte));
        logger.traceExit();
        rezultatAdded(idParticipant, Nume, Prenume, idProba, puncte);
    }

    private final int defaultThreadsNo=3;
    private void rezultatAdded(Long idParticipant, String Nume, String Prenume, String idProba, long puncte) {
        logger.debug("rezultat added");

        ExecutorService executor = Executors.newFixedThreadPool(defaultThreadsNo);
        loggedClients.forEach((username, client) -> {
            executor.execute(() -> {
                try {
                    client.RezultatAdded(idParticipant, Nume, Prenume, idProba, puncte);
                } catch (ManageException e) {
                    logger.error("Error notifying client: " + e.getMessage());
                }
            });
        });
        executor.shutdown();
    }

    public synchronized Map<Participant,Long> getParticipantiAlfabetic(){
        return rezultatRepository.listaParticipantiPuncteAlfabetic();
    }

    public synchronized Map<Participant,Long> getParticipantiPuncteDesc(String Proba){
        return rezultatRepository.listaParticiapntPuncteProbaDescrescator(Proba);
    }

    public synchronized Iterable<Participant> getParticipanti(){
        return participantRepository.findAll();
    }

    public synchronized void logout(Arbitru arbitru,IManageObserver client) throws ManageException {
        logger.info("Logout user "+arbitru.getUsername());

        loggedClients.remove(arbitru.getUsername());
    }

}
