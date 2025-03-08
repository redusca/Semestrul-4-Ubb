public class Proba : Entity<string>
{
    public string Nume { get; set; }
    public long Id_arbitru { get; set; }
    public Categorie Categorie { get; set; }

    public Proba(string id, string nume, Categorie categorie)
    {
        Id = id;
        Nume = nume;
        this.Categorie = categorie;
    }

    public override string ToString()
    {
        return "Proba{" +
                "id=" + Id +
                ", nume='" + Nume + '\'' +
                ", id_arbitru='" + Id_arbitru + '\'' +
                ", categorie='" + Categorie + '\'' +
                '}';
    }

}