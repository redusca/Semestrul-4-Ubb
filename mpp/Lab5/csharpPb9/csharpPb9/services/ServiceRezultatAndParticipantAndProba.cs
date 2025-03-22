using log4net;

public class ServiceRezultatAndParticipantAndProba
{
    IProbaRepository probaRepository;
    IParticipantRepository participantRepository;
    IRezultatRepository rezultatRepository;

    private static readonly ILog log = LogManager.GetLogger("");

    public ServiceRezultatAndParticipantAndProba(IProbaRepository probaRepository, IParticipantRepository participantRepository, IRezultatRepository rezultatRepository)
    {
        log.Info("Creating Service");
        this.probaRepository = probaRepository;
        this.participantRepository = participantRepository;
        this.rezultatRepository = rezultatRepository;
    }

    public IEnumerable<Participant> GetParticipantiFaraScor(string id)
    {
        log.Info("Get Participanti Fara Scor");
        var arb = participantRepository.FindAll();
        var rez = rezultatRepository.FindAll().Where(x => x.Proba.Id == id).Select(x => x.Participant.Id);
        return arb.Where( par => !rez.Contains(par.Id));
    }

    public IDictionary<Participant,long> GetRezultate()
    {
        log.Info("Get Rezultate");
        return rezultatRepository.ParticipantiAlfabetic();
    }
    public IDictionary<Participant, long> GetRezultateScorDescrescator(string id)
    {
        log.Info("Get Rezultate Scor Descrescator");
        return rezultatRepository.ParticipantScorDescrescator(id);
    }

    public void SaveRezultat(DataSetRezultat dataRezultat)
    {
        log.Info("Saving Rezultat");
        var Proba = probaRepository.FindOne(dataRezultat.id_proba);
        var Participant = participantRepository.FindOne(dataRezultat.id_participant);

        rezultatRepository.Save(new Rezultat(0, Participant, Proba, dataRezultat.punctaj));
    }
}
