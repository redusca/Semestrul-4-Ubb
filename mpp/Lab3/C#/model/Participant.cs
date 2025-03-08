public class Participant : Entity<long>
{
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public long Varsta { get; set; }

    public Participant(long id, string nume, string prenume, long varsta)
    {
        Id = id;
        Nume = nume;
        Prenume = prenume;
        Varsta = varsta;
    }

    public override string ToString()
    {
        return "Participant{" +
                "id=" + Id +
                ", nume='" + Nume + '\'' +
                ", prenume='" + Prenume + '\'' +
                ", varsta='" + Varsta + '\'' +
                '}';
    }
}
