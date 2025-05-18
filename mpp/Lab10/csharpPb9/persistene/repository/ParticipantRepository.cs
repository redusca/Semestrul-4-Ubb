using csharpPb9.utils;
using log4net;
using System.Data;

public class ParticipantRepository : IParticipantRepository
{
    private static readonly ILog log = LogManager.GetLogger("");

    IDictionary<string, string> props;

    public ParticipantRepository(IDictionary<string, string> props)
    {
        log.Info("Creating ArbitruRepository with properties: " + props);
        this.props = props;
    }

    public Participant fromSettoEntity(IDataReader read)
    {
        long id = read.GetInt64(0);
        string nume = read.GetString(1);
        string prenume = read.GetString(2);
        int varsta = read.GetInt32(3);

        return new Participant(id, nume, prenume, varsta);
    }

    public IEnumerable<Participant> FindAll()
    {
        log.Info("Finding all participanti");
        IDbConnection con = DBUtils.getConnection(props);
        IList<Participant> participanti = new List<Participant>();
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Participant";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        Participant participant = fromSettoEntity(dataR);
                        participanti.Add(participant);
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

        log.InfoFormat("Exiting FindAll with value {0}", participanti);
        return participanti;
    }

    public Participant FindOne(long id)
    {
        log.Info("Finding One participant");
        IDbConnection con = DBUtils.getConnection(props);
        Participant participant = null;
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "Select * from Participant where id=@id";
                var param = comm.CreateParameter();
                param.ParameterName = "@id";
                param.Value = id;
                comm.Parameters.Add(param);
                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        participant = fromSettoEntity(dataR);
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

        log.InfoFormat("Exiting FindOne with value {0}", participant);
        return participant;
    }


    public void Save(Participant entity)
    {
        log.Info("Save Participant");
        IDbConnection con = DBUtils.getConnection(props);

        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Participant(nume,prenume,varsta) values (@nume, @prenume, @varsta)";
                #region vars
                var numeParam = comm.CreateParameter();
                numeParam.ParameterName = "@nume";
                numeParam.Value = entity.Nume;
                comm.Parameters.Add(numeParam);

                var prenumeParam = comm.CreateParameter();
                prenumeParam.ParameterName = "@prenume";
                prenumeParam.Value = entity.Prenume;
                comm.Parameters.Add(prenumeParam);

                var varstaParam = comm.CreateParameter();
                varstaParam.ParameterName = "@varsta";
                varstaParam.Value = entity.Varsta;
                comm.Parameters.Add(varstaParam);
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
    #region TO implement
    public Participant Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Participant Update(long id, Participant new_entity)
    {
        throw new NotImplementedException();
    }
    #endregion 
}
