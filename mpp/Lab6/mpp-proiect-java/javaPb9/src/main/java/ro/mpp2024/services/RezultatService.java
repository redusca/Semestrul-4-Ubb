package ro.mpp2024.services;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.model.Participant;
import ro.mpp2024.model.Proba;
import ro.mpp2024.model.Rezultat;
import ro.mpp2024.repository.RezultatRepository;
import ro.mpp2024.repository.interfaces.IParticipantRepository;
import ro.mpp2024.repository.interfaces.IProbaRepository;
import ro.mpp2024.repository.interfaces.IRezultatRepository;

import java.util.Map;

public class RezultatService {
    IRezultatRepository rezultatRepository;
    IParticipantRepository participantRepository;
    IProbaRepository probaRepository;

    private static final Logger logger= LogManager.getLogger();

    public RezultatService(IRezultatRepository rez, IParticipantRepository par, IProbaRepository proba) {
        this.rezultatRepository = rez;
        this.participantRepository = par;
        this.probaRepository = proba;
        logger.info("Service Rezultat made");
    }

    public void addRezultat(Long idParticipant, String idProba, long puncte) {
        logger.traceEntry();
        Participant  par = participantRepository.findOne(idParticipant);
        Proba proba = probaRepository.findOne(idProba);
        rezultatRepository.save(new Rezultat(0L,par, proba, puncte));
        logger.traceExit();
    }

    public Map<Participant,Long> getParticipantiAlfabetic(){
        return rezultatRepository.listaParticipantiPuncteAlfabetic();
    }

    public Map<Participant,Long> getParticipantiPuncteDesc(String Proba){
        return rezultatRepository.listaParticiapntPuncteProbaDescrescator(Proba);
    }

    public Iterable<Participant> getParticipanti(){
        return participantRepository.findAll();
    }

}
