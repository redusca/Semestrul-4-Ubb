using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Participant")]
public class Participant : Entity<long>
{
    [Key]
    [Column("id")]
    public override long Id { get; set; }
    [Column("nume")]
    public string Nume { get; set; }
    [Column("prenume")]
    public string Prenume { get; set; }
    [Column("varsta")]
    public int Varsta { get; set; }

    public Participant(long id, string nume, string prenume, int varsta)
    {
        Id = id;
        Nume = nume;
        Prenume = prenume;
        Varsta = varsta;
    }

    public override string ToString()
    {
        return Nume + " " + Prenume;
    }
}
