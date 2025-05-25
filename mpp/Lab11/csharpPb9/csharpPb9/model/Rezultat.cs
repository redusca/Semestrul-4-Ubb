using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Rezultat")]
public class Rezultat : Entity<long>
{
    [Key]
    [Column("id")]
    public override long Id { get; set; }
    [ForeignKey("FK_Rezultat_0_0")]
    [Column("id_participant")]
    public Participant Participant { get; set; }
    [ForeignKey("FK_Rezultat_1_0")]
    [Column("id_proba")]
    public Proba Proba { get; set; }
    [Column("numar_puncte")]
    public long numar_puncte { get; set; }

    public Rezultat() { }

    public Rezultat(long id, Participant participant, Proba proba, long scor)
    {
        this.Id = id;
        Participant = participant;
        Proba = proba;
        numar_puncte = scor;
    }

    public Rezultat(Participant participant, Proba proba, long scor)
    {
        Participant = participant;
        Proba = proba;
        numar_puncte = scor;
    }

    public override string ToString()
    {
        return "Rezultat{" +
                "id=" + Id +
                ", Participant=" + Participant +
                ", Proba=" + Proba +
                ", Scor=" + numar_puncte +
                '}';
    }
}