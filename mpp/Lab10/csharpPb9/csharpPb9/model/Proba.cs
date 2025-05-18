using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Proba")]
public class Proba : Entity<string>
{
    [Key]
    [Column("id")]
    public override string Id { get; set; }
    [Column("Nume")]
    public string Nume { get; set; }
    [ForeignKey("FK_Proba_0_0")]
    [Column("arbitru_probei")]
    public long Id_arbitru { get; set; }
    [Column("Categorie")]
    public Categorie Categorie { get; set; }

    public Proba() { }

    public Proba(string id, string nume, Categorie categorie)
    {
        Id = id;
        Nume = nume;
        this.Categorie = categorie;
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