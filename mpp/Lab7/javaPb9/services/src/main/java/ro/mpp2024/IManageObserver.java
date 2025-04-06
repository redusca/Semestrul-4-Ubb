package ro.mpp2024;

public interface IManageObserver {
    void RezultatAdded(Long idParticipant, String numeParticipant, String prenumeParticipant, String idProba, long puncte) throws ManageException;
}
