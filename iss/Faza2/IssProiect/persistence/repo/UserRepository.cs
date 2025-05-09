using model;
using repository;
using System.Data;
using model.Enum;
using persistence.utils;

namespace persistence.repo
{
    public class UserRepository : IUserRepository
    {
        private readonly IDictionary<string, string> props;

        public UserRepository(IDictionary<string, string> props)
        {
            this.props = props;

            Console.WriteLine(FindById(1));
        }

        public void Add(User user)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "INSERT INTO Users(username, password, type) VALUES(@username, @password, @tip)";

                        IDbDataParameter paramUsername = comm.CreateParameter();
                        paramUsername.ParameterName = "@username";
                        paramUsername.Value = user.Username;
                        comm.Parameters.Add(paramUsername);

                        IDbDataParameter paramPassword = comm.CreateParameter();
                        paramPassword.ParameterName = "@password";
                        paramPassword.Value = user.Password;
                        comm.Parameters.Add(paramPassword);

                        IDbDataParameter paramTip = comm.CreateParameter();
                        paramTip.ParameterName = "@tip";
                        paramTip.Value = user.Type.ToString();
                        comm.Parameters.Add(paramTip);

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException("No user added!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error adding user: {ex.Message}", ex);
            }
        }

        public void Update(User user)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "UPDATE Users SET username=@username, password=@password, type=@tip WHERE id=@id";

                        IDbDataParameter paramId = comm.CreateParameter();
                        paramId.ParameterName = "@id";
                        paramId.Value = user.Id;
                        comm.Parameters.Add(paramId);

                        IDbDataParameter paramUsername = comm.CreateParameter();
                        paramUsername.ParameterName = "@username";
                        paramUsername.Value = user.Username;
                        comm.Parameters.Add(paramUsername);

                        IDbDataParameter paramPassword = comm.CreateParameter();
                        paramPassword.ParameterName = "@password";
                        paramPassword.Value = user.Password;
                        comm.Parameters.Add(paramPassword);

                        IDbDataParameter paramTip = comm.CreateParameter();
                        paramTip.ParameterName = "@tip";
                        paramTip.Value = user.Type.ToString();
                        comm.Parameters.Add(paramTip);

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException("No user updated!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error updating user: {ex.Message}", ex);
            }
        }

        public void Delete(long id)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "DELETE FROM Users WHERE id=@id";

                        IDbDataParameter paramId = comm.CreateParameter();
                        paramId.ParameterName = "@id";
                        paramId.Value = id;
                        comm.Parameters.Add(paramId);

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException("No user deleted!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error deleting user: {ex.Message}", ex);
            }
        }

        public User FindById(long id)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "SELECT id, username, password, type FROM Users WHERE id=@id";

                        IDbDataParameter paramId = comm.CreateParameter();
                        paramId.ParameterName = "@id";
                        paramId.Value = id;
                        comm.Parameters.Add(paramId);

                        using (var dataR = comm.ExecuteReader())
                        {
                            if (dataR.Read())
                            {
                                int userId = dataR.GetInt32(0);
                                string username = dataR.GetString(1);
                                string password = dataR.GetString(2);
                                UserType tip = Enum.Parse<UserType>(dataR.GetString(3));
                                return new User(userId, username, password, tip);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error finding user by ID: {ex.Message}", ex);
            }
        }

        public User FindByUsername(string username)
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "SELECT id, username, password, type FROM Users WHERE username=@username";

                        IDbDataParameter paramUsername = comm.CreateParameter();
                        paramUsername.ParameterName = "@username";
                        paramUsername.Value = username;
                        comm.Parameters.Add(paramUsername);

                        using (var dataR = comm.ExecuteReader())
                        {
                            if (dataR.Read())
                            {
                                int userId = dataR.GetInt32(0);
                                string foundUsername = dataR.GetString(1);
                                string password = dataR.GetString(2);
                                UserType tip = Enum.Parse<UserType>(dataR.GetString(3));
                                return new User(userId, foundUsername, password, tip);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error finding user by username: {ex.Message}", ex);
            }
        }

        public IEnumerable<User> FindAll()
        {
            try
            {
                using (IDbConnection con = DBUtils.getConnection(props))
                {
                    IList<User> users = new List<User>();
                    using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = "SELECT id, username, password, type FROM Users";

                        using (var dataR = comm.ExecuteReader())
                        {
                            while (dataR.Read())
                            {
                                int id = dataR.GetInt32(0);
                                string username = dataR.GetString(1);
                                string password = dataR.GetString(2);
                                UserType tip = Enum.Parse<UserType>(dataR.GetString(3));
                                users.Add(new User(id, username, password, tip));
                            }
                        }
                    }
                    return users;
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error finding all users: {ex.Message}", ex);
            }
        }
    }
}
