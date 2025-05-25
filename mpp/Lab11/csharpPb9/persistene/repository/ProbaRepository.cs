using csharpPb9.utils;
using log4net;
using System.Data;

public class ProbaRepository : IProbaRepository
{
    private static readonly ILog log = LogManager.GetLogger("");

    IDictionary<string, string> props;


    public ProbaRepository(IDictionary<string, string> props)
    {
        log.Info("Creating ProbaRepository with properties: " + props);
        this.props = props;
    }
    public Proba fromSettoEntity(IDataReader read)
    {
        string id = read.GetString(0);
        string nume = read.GetString(1);
        long id_arbitru = read.GetInt64(2);
        Categorie categorie = (Categorie)Enum.Parse(typeof(Categorie), read.GetString(3));
        Proba proba = new Proba(id, nume, categorie);
        proba.Id_arbitru = id_arbitru;
        return proba;
    }
    public IEnumerable<Proba> FindAll()
    {
        log.Info("Finding all probe");
        var con = DBUtils.getConnection(props);
        IList<Proba> probe = new List<Proba>();
        try
        {
            con.Open();
            using (IDbCommand conn = con.CreateCommand())
            {
                conn.CommandText = "SELECT * FROM Proba";
                using (IDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Proba proba = fromSettoEntity(reader);
                        probe.Add(proba);
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
        log.InfoFormat("Exiting FindAll with value {0}", probe);
        return probe;
    }

    public Arbitru FindArbitru(string id)
    {
        log.Info($"Find arbitru");
        var con = DBUtils.getConnection(props);
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Arbitru WHERE proba_asociata=@id";
                var param = comm.CreateParameter();
                param.ParameterName = "@id";
                param.Value = id;
                comm.Parameters.Add(param);
                using (IDataReader reader = comm.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        log.Info("Found Arbitru");
                        return new Arbitru(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
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

        log.Info("No Arbitru found");
        return null;
    }

    public Proba FindOne(string id)
    {
       log.Info($"Finding proba with id {id}");
        var con = DBUtils.getConnection(props);
        Proba proba = null;
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Proba WHERE id=@id";
                var param = comm.CreateParameter();
                param.ParameterName = "@id";
                param.Value = id;
                comm.Parameters.Add(param);
                using (IDataReader reader = comm.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        proba = fromSettoEntity(reader);
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

        log.InfoFormat("Exiting FindOne with value {0}", proba);
        return proba;
    }

    public void Save(Proba entity)
    {
        log.Info("Saving proba");
        var con = DBUtils.getConnection(props);
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO Proba(id, nume, arbitru_probei, categorie) VALUES (@id, @nume, @id_arbitru, @categorie)";
                #region vars
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramNume = comm.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = entity.Nume;
                comm.Parameters.Add(paramNume);

                var paramIdArbitru = comm.CreateParameter();
                paramIdArbitru.ParameterName = "@id_arbitru";
                paramIdArbitru.Value = entity.Id_arbitru;
                comm.Parameters.Add(paramIdArbitru);

                var paramCategorie = comm.CreateParameter();
                paramCategorie.ParameterName = "@categorie";
                paramCategorie.Value = entity.Categorie.ToString();
                comm.Parameters.Add(paramCategorie);

                #endregion
                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No proba saved!");
                }
                else
                {
                    log.Info("Proba saved");
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

    public void SetArbitruForProba(string id, long arbitru)
    {
        log.Info("Setting arbitru for proba");
        var con = DBUtils.getConnection(props);
        try
        {
            con.Open();
            using (IDbCommand comm = con.CreateCommand())
            {
                comm.CommandText = "UPDATE Proba SET arbitru_probei=@arbitru WHERE id=@id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var paramArbitru = comm.CreateParameter();
                paramArbitru.ParameterName = "@arbitru";
                paramArbitru.Value = arbitru;
                comm.Parameters.Add(paramArbitru);
                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No proba updated!");
                }
                else
                {
                    log.Info("Proba updated");
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
    }

    #region To implement
    public Proba Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Proba Update(string id, Proba new_entity)
    {
        throw new NotImplementedException();
    }

    #endregion
}