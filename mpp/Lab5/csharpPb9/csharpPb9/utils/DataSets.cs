using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DataSets
{
    public string Nume { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Id_proba { get; set; }
}

public class DataSetProba
{
    public string id { get; set; }
    public string Nume { get; set; }
    public Categorie Categorie { get; set; }
    public long Id_arbitru { get; set; }

}

public class DataSetRezultat
{
    public long id_participant { get; set; }
    public string id_proba { get; set; }
    public long punctaj { get; set; }
}
