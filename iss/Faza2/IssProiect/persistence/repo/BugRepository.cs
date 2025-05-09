using model;
using model.Enum;
using persistence.repo.Interface;
using persistence.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.repo
{
    public class BugRepository : IBugRepository
    {
        private readonly IDictionary<string, string> props;

        public BugRepository(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public void Add(Bug bug)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "INSERT INTO Bugs(description, status) VALUES(@description, @status)";

                        IDbDataParameter paramDescription = comm.CreateParameter();
                        paramDescription.ParameterName = "@description";
                        paramDescription.Value = bug.Description;
                        comm.Parameters.Add(paramDescription);

                        IDbDataParameter paramStatus = comm.CreateParameter();
                        paramStatus.ParameterName = "@status";
                        paramStatus.Value = bug.Status.ToString();
                        comm.Parameters.Add(paramStatus);

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException("No bug added!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error adding bug: {ex.Message}", ex);
            }
        }

        public void Update(Bug bug)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "UPDATE Bugs SET description=@description, status=@status WHERE bugNo=@bugNo";

                        IDbDataParameter paramBugNo = comm.CreateParameter();
                        paramBugNo.ParameterName = "@bugNo";
                        paramBugNo.Value = bug.BugNo;
                        comm.Parameters.Add(paramBugNo);

                        IDbDataParameter paramDescription = comm.CreateParameter();
                        paramDescription.ParameterName = "@description";
                        paramDescription.Value = bug.Description;
                        comm.Parameters.Add(paramDescription);

                        IDbDataParameter paramStatus = comm.CreateParameter();
                        paramStatus.ParameterName = "@status";
                        paramStatus.Value = bug.Status.ToString();
                        comm.Parameters.Add(paramStatus);

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException("No bug updated!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error updating bug: {ex.Message}", ex);
            }
        }

        public void Delete(long bugNo)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "DELETE FROM Bugs WHERE bugNo=@bugNo";

                        IDbDataParameter paramBugNo = comm.CreateParameter();
                        paramBugNo.ParameterName = "@bugNo";
                        paramBugNo.Value = bugNo;
                        comm.Parameters.Add(paramBugNo);

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException("No bug deleted!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error deleting bug: {ex.Message}", ex);
            }
        }

        public Bug FindByBugNo(long bugNo)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "SELECT bugNo, description, status FROM Bugs WHERE bugNo=@bugNo";

                        IDbDataParameter paramBugNo = comm.CreateParameter();
                        paramBugNo.ParameterName = "@bugNo";
                        paramBugNo.Value = bugNo;
                        comm.Parameters.Add(paramBugNo);

                        using (var dataR = comm.ExecuteReader())
                        {
                            if (dataR.Read())
                            {
                                int foundBugNo = dataR.GetInt32(0);
                                string description = dataR.GetString(1);
                                BugStatus status = Enum.Parse<BugStatus>(dataR.GetString(2));
                                return new Bug(foundBugNo, description,status);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error finding bug by BugNo: {ex.Message}", ex);
            }
        }

        public IEnumerable<Bug> FindByStatus(string status)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    IList<Bug> bugs = new List<Bug>();
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "SELECT bugNo, description, status FROM Bugs WHERE status=@status";

                        IDbDataParameter paramStatus = comm.CreateParameter();
                        paramStatus.ParameterName = "@status";
                        paramStatus.Value = status;
                        comm.Parameters.Add(paramStatus);

                        using (var dataR = comm.ExecuteReader())
                        {
                            while (dataR.Read())
                            {
                                int bugNo = dataR.GetInt32(0);
                                string description = dataR.GetString(1);
                                BugStatus foundStatus = Enum.Parse<BugStatus>(dataR.GetString(2));
                                bugs.Add(new Bug(bugNo, description, foundStatus));
                            }
                        }
                    }
                    return bugs;
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error finding bugs by status: {ex.Message}", ex);
            }
        }

        public IEnumerable<Bug> FindAll()
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    IList<Bug> bugs = new List<Bug>();
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "SELECT bugNo, description, status FROM Bugs";

                        using (var dataR = comm.ExecuteReader())
                        {
                            while (dataR.Read())
                            {
                                int bugNo = dataR.GetInt32(0);
                                string description = dataR.GetString(1);
                                BugStatus status = Enum.Parse<BugStatus>(dataR.GetString(2));
                                bugs.Add(new Bug(bugNo, description, status));
                            }
                        }
                    }
                    return bugs;
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error finding all bugs: {ex.Message}", ex);
            }
        }
    }
}
