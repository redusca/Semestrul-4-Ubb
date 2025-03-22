public class Rezultat : Entity<long>
{
    public long id { get; set; }
    public Participant Participant { get; set; }
    public Proba Proba { get; set; }
    public long Scor { get; set; }

    public Rezultat(long id, Participant participant, Proba proba, long scor)
    {
        this.id = id;
        Participant = participant;
        Proba = proba;
        Scor = scor;
    }

    public override string ToString()
    {
        return "Rezultat{" +
                "id=" + id +
                ", Participant=" + Participant +
                ", Proba=" + Proba +
                ", Scor=" + Scor +
                '}';
    }
}