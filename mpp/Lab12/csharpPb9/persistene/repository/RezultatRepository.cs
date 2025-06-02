using csharpPb9.utils;
using log4net;
using System.Data;

public class RezultatRepository : IRezultatRepository
{
    private static readonly ILog log = LogManager.GetLogger("");

    IDictionary<string, string> props;

    public RezultatRepository(IDictionary<string, string> props)
    {
        log.Info("Creating ProbaRepository with properties: " + props);
        this.props = props;
    }

    public Rezultat fromSettoEntity(IDataReader read)
    {
        long id = read.GetInt64(0);
        long id_participant = read.GetInt64(2);
        string id_proba = read.GetString(1);
        int scor = read.GetInt32(3);

        #region Find participant and proba
        var con = DBUtils.getConnection(props);
        Participant p = null;
        try
        {
            log.Info("Finding participant");
;
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Participant WHERE id = @id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id_participant;
                comm.Parameters.Add(paramId);
                using (IDataReader dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        p = new Participant(dataR.GetInt64(0), dataR.GetString(1), dataR.GetString(2), dataR.GetInt32(3));
                    }
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.Message);
        }

        con = DBUtils.getConnection(props);
        Proba proba = null;
        try
        {
            log.Info("Finding proba");
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Proba WHERE id = @id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id_proba;
                comm.Parameters.Add(paramId);
                using (IDataReader dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        proba = new Proba(dataR.GetString(0), dataR.GetString(1), (Categorie)Enum.Parse(typeof(Categorie), dataR.GetString(3)));
                        proba.Id_arbitru = dataR.GetInt64(2);
                    }
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.Message);
        }
        finally
        {
            log.Info("Closing connection");
        }
        #endregion

        return new Rezultat(id, p, proba, scor);
    }

    public IEnumerable<Rezultat> FindAll()
    {
        log.Info("Finding all rezultate");
        var con = DBUtils.getConnection(props);
        IList<Rezultat> rezultate = new List<Rezultat>();
        try
        {
            con.Open();
            using (IDbCommand conn = con.CreateCommand())
            {
                conn.CommandText = "SELECT * FROM Rezultat";
                using (IDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rezultat rezultat = fromSettoEntity(reader);
                        rezultate.Add(rezultat);
                    }
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.Message);
        }
        finally
        {
            log.Info("Closing connection");
            con.Close();
        }
        log.InfoFormat("Exiting FindAll with value {0}", rezultate);
        return rezultate;
    }

    public Rezultat FindOne(long id)
    {
       log.InfoFormat("Finding rezultat with id {0}", id);
        var con = DBUtils.getConnection(props);
        Rezultat rezultat = null;
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Rezultat WHERE id = @id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                using (IDataReader dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                         rezultat = fromSettoEntity(dataR);
                    }
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.Message);
        }
        finally
        {
            log.Info("Closing connection");
            con.Close();
        }
        log.InfoFormat("Exiting findOne with value {0}", rezultat);
        return rezultat;
    }

    public Dictionary<Participant, long> ParticipantiAlfabetic()
    {
        log.Info("Participanti alfabetic");
        Dictionary<Participant, long> dict = new Dictionary<Participant, long>();
        var con = DBUtils.getConnection(props);
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT sum(r.numar_puncte) as total_punct, p.id, p.nume,p.prenume, p.varsta " +
                    "FROM Rezultat as r Inner join Participant as p " +
                    "On r.id_participant = p.id group by r.id_participant " +
                    "order by lower(p.nume),lower(p.prenume)";
                using (IDataReader dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        log.Info("Reading data");
                        var puncte = dataR.GetInt32(0);
                        Participant par = new Participant(dataR.GetInt64(1), dataR.GetString(2), dataR.GetString(3), dataR.GetInt32(4));
                        dict.Add(par, puncte);
                    }
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.Message);
        }
        finally
        {
            log.Info("Closing connection");
            con.Close();
        }

        log.Info("Exiting ParticipantScorDescrescator");
        return dict;
    }

    public Dictionary<Participant, long> ParticipantScorDescrescator(string id)
    {
        log.Info($"Proba {id} scor descrescator");
        Dictionary<Participant, long> dict = new Dictionary<Participant, long>();
        var con = DBUtils.getConnection(props);
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT SUM(r.numar_puncte) AS total_puncte, p.id, p.nume, p.prenume, p.varsta FROM Rezultat AS r INNER JOIN Participant AS p ON r.id_participant = p.id WHERE r.id_proba = @id GROUP BY r.id_participant ORDER BY total_puncte DESC;";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                using (IDataReader dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        log.Info("Reading data");
                        var puncte = dataR.GetInt32(0);
                        Participant par = new Participant(dataR.GetInt64(1), dataR.GetString(2), dataR.GetString(3), dataR.GetInt32(4));
                        dict.Add(par, puncte);
                    }
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.Message);
        }
        finally
        {
            log.Info("Closing connection");
            con.Close();
        }

        dict = dict.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);

        log.Info("Exiting ParticipantScorDescrescator");
        return dict;
    }

    public void Save(Rezultat entity)
    {
        log.Info("Saving rezultat");
        var con = DBUtils.getConnection(props);
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO Rezultat(id_proba,id_participant,numar_puncte) VALUES (@id_proba,@id_participant, @scor)";
                #region vars
                IDbDataParameter paramIdParticipant = comm.CreateParameter();
                paramIdParticipant.ParameterName = "@id_participant";
                paramIdParticipant.Value = entity.Participant.Id;
                comm.Parameters.Add(paramIdParticipant);

                IDbDataParameter paramIdProba = comm.CreateParameter();
                paramIdProba.ParameterName = "@id_proba";
                paramIdProba.Value = entity.Proba.Id;
                comm.Parameters.Add(paramIdProba);

                IDbDataParameter paramScor = comm.CreateParameter();
                paramScor.ParameterName = "@scor";
                paramScor.Value = entity.numar_puncte;
                comm.Parameters.Add(paramScor);
                #endregion

                var resultat = comm.ExecuteNonQuery();
                if(resultat == 0)
                {
                    log.Info("No rezultat added");
                }
                else
                {
                    log.InfoFormat("Added rezultat with value {0}", entity);
                }
            }
        }
        catch (Exception e)
        {
            log.Error(e);
            Console.WriteLine(e.Message);
        }
        finally
        {
            log.Info("Closing connection");
            con.Close();
        }
        log.Info("Exiting Save");
    }
    #region To implement
    public Rezultat Delete(long id)
    {
        throw new NotImplementedException();
    }


    public Rezultat Update(long id, Rezultat new_entity)
    {
        throw new NotImplementedException();
    }
    #endregion
}