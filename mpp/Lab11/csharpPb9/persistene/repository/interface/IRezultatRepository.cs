public interface IRezultatRepository : DataBaseRepository<long, Rezultat>
{
    Dictionary<Participant, long> ParticipantiAlfabetic();
    Dictionary<Participant, long> ParticipantScorDescrescator(string id);
}

