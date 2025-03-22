public class Arbitru : Entity<long>
{
    public string Nume { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Id_proba { get; set; }

    public Arbitru(long id, string nume, string username, string password, string id_proba)
    {
        Id = id;
        Nume = nume;
        Username = username;
        Password = password;
        Id_proba = id_proba;
    }

    public Arbitru(string nume, string username, string password, string id_proba)
    {
        Id = -1;
        Nume = nume;
        Username = username;
        Password = password;
        Id_proba = id_proba;
    }

    public override string ToString()
    {
        return "Arbitru{" +
                "id=" + Id +
                ", nume='" + Nume + '\'' +
                ", username='" + Username + '\'' +
                ", password='" + Password + '\'' +
                ", id_proba='" + Id_proba + '\'' +
                '}';
    }
}
