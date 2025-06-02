
using log4net;
using System.Data;
using System.Threading.Channels;
namespace csharpPb9.utils;
public class ArbitruRepository : IArbitruRepository
{

    private static readonly ILog log = LogManager.GetLogger("");

    IDictionary<string, string> props;

    public ArbitruRepository(IDictionary<string, string> props)
    {
        log.Info("Creating ArbitruRepository with properties: " + props);
        this.props = props;
    }

    public Arbitru fromSettoEntity(IDataReader read)
    {
        long id = read.GetInt64(0);
        string nume = read.GetString(1);
        string username = read.GetString(2);
        string password = read.GetString(3);
        string proba_asociata = read.GetString(4);
        return new Arbitru(id, nume, username, password, proba_asociata); ;
    }

    public IEnumerable<Arbitru> FindAll()
    {
        log.Info("Finding all arbitri");
        IDbConnection con = DBUtils.getConnection(props);
        IList<Arbitru> arbitri = new List<Arbitru>();
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Arbitru";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        Arbitru arbitru = fromSettoEntity(dataR);
                        arbitri.Add(arbitru);
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

        log.InfoFormat("Exiting FindAll with value {0}", arbitri);
        return arbitri;
    }

    public Arbitru FindOne(long id)
    {
        log.Info("Finding one arbitru");
        IDbConnection con = DBUtils.getConnection(props);
        Arbitru arbitru = null;
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Arbitru where id=@id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                        arbitru = fromSettoEntity(dataR);
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

        log.InfoFormat("Exiting FindOne with value {0}", arbitru);
        return arbitru;
    }

    public void Save(Arbitru entity)
    {
        log.Info("Saving arbitru");
        var con = DBUtils.getConnection(props);
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Arbitru(nume, username, parola, proba_asociata) values (@nume, @username, @password, @proba_asociata)";
                #region params
                var paramNume = comm.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = entity.Nume;
                comm.Parameters.Add(paramNume);

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = entity.Username;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = entity.Parola;
                comm.Parameters.Add(paramPassword);

                var paramProbaAsociata = comm.CreateParameter();
                paramProbaAsociata.ParameterName = "@proba_asociata";
                paramProbaAsociata.Value = entity.Id_proba;
                comm.Parameters.Add(paramProbaAsociata);
                #endregion
                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No arbitru saved!");
                }
                else
                {
                    log.Info("Arbitru saved!");
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


    public Arbitru FindByUser(string username)
    {
        log.Info("Finding arbitru by user");
        var con = DBUtils.getConnection(props);
        Arbitru arbitru = null;
        try
        {
            con.Open();
            using (var conn = con.CreateCommand())
            {
                conn.CommandText = "select * from Arbitru where username=@username";
                var paramUsername = conn.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                conn.Parameters.Add(paramUsername);
                using (var dataR = conn.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        arbitru = fromSettoEntity(dataR);
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

        log.InfoFormat("Exiting FindByUser with value {0}", arbitru);
        return arbitru;
    }

    #region To be implemented

    public Arbitru Delete(long id)
    {
        log.Info("Deleting arbitru");
        var con = DBUtils.getConnection(props);
        Arbitru arbitru = FindOne(id);
        try
        {
            SetProba(id);
            con.Open();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Arbitru where id=@id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No arbitru deleted!");
                }
                else
                {
                    log.Info("Arbitru deleted!");
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
        return arbitru;
    }

    private void SetProba(long id)
    {
        var con = DBUtils.getConnection(props);
        con.Open();

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "Update Proba set arbitru_probei=-1 where arbitru_probei=@id";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            var result = comm.ExecuteNonQuery();
            if (result == 0)
            {
                log.Info("No proba updated!");
            }
            else
            {
                log.Info("Proba updated!");
            }
        }
        con.Close();
    }

    public Arbitru Update(long id, Arbitru new_entity)
    {
        throw new NotImplementedException();
    }

    #endregion
}