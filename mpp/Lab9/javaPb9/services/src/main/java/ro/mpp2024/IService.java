package ro.mpp2024;

import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;

import java.util.Map;

public interface IService {
    Arbitru login(String username, String password, IManageObserver client) throws ManageException;

    void addRezultat(Long idParticipant,String nume,String prenume, String idProba, long puncte) ;

    Map<Participant,Long> getParticipantiAlfabetic();

    Map<Participant,Long> getParticipantiPuncteDesc(String Proba);

    Iterable<Participant> getParticipanti();

    void logout(Arbitru user,IManageObserver client) throws ManageException;
}
