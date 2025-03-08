public class Rezultat : Entity<string>
{
    public long Id_participant { get; set; }
    public string Id_proba { get; set; }
    public long Scor { get; set; }
    public Rezultat(string id, long id_participant, string id_proba, long scor)
    {
        Id = id;
        Id_participant = id_participant;
        Id_proba = id_proba;
        Scor = scor;
    }
    public override string ToString()
    {
        return "Rezultat{" +
                "id=" + Id +
                ", id_participant='" + Id_participant + '\'' +
                ", id_proba='" + Id_proba + '\'' +
                ", scor='" + Scor + '\'' +
                '}';
    }
}