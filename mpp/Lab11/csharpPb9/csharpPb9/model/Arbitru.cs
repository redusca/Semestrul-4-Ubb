using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Arbitru")]
public class Arbitru : Entity<long>
{
    [Key]
    [Column("Id")]
    public override long Id { get; set; }
    [Column("Nume")]
    public string Nume { get; set; }
    [Column("Username")]
    public string Username { get; set; }
    [Column("Parola")]
    public string Parola { get; set; }
    [Column("Proba_asociata")]
    public string Id_proba { get; set; }

    public Arbitru() { }

    public Arbitru(long id, string nume, string username, string parola, string id_proba)
    {
        Id = id;
        Nume = nume;
        Username = username;
        Parola = parola;
        Id_proba = id_proba;
    }

    public Arbitru(string nume, string username, string parola, string id_proba)
    {
        Nume = nume;
        Username = username;
        Parola = parola;
        Id_proba = id_proba;
    }

    public override string ToString()
    {
        return "Arbitru{" +
                "id=" + Id +
                ", nume='" + Nume + '\'' +
                ", username='" + Username + '\'' +
                ", password='" + Parola + '\'' +
                ", id_proba='" + Id_proba + '\'' +
                '}';
    }
}
