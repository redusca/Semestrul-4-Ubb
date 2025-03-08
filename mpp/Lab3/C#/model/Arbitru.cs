public class Arbitru : Entity<long>
{
    public string Nume { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string id_proba { get; set; }

    public Arbitru(long id, string nume, string username, string password, Proba pb)
    {
        Id = id;
        Nume = nume;
        Username = username;
        Password = password;
        this.id_proba = pb.Id;
        
        pb.Id_arbitru = id;
    }

    public override string ToString()
    {
        return "Arbitru{" +
                "id=" + Id +
                ", nume='" + Nume + '\'' +
                ", username='" + Username + '\'' +
                ", password='" + Password + '\'' +
                ", id_proba='" + id_proba + '\'' +
                '}';
    }
}
