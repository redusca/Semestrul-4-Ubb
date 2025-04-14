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
        Id_arbitru = -1;
    }

    public Proba(string id,string nume, Categorie categorie, long id_arbitru)
    {
        Id = id;
        Nume = nume;
        Categorie = categorie;
        Id_arbitru = id_arbitru;
    }

    public override string ToString()
    {
        return Id + " " + Nume;
    }

}