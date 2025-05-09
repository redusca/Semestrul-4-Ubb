using model;
using model.Enum;
using networking.jsonProtocol.responseAndrequest;
using networking.jsonProtocol.ServerException;
using services.Interfaces;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace networking.jsonProtocol
{
    public class ServerProxy : IService
    {
        private readonly string host;
        private readonly int port;

        private IManageObserver _client;
        private TcpClient _connection;
        private NetworkStream _stream;
        private Queue<Response> _responses;
        private volatile bool _finished;
        private EventWaitHandle _waitHandle;

        public ServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            _responses = new Queue<Response>();
        }

        private void initializeConnection()
        {
            try
            {
                _connection = new TcpClient(host, port);
                _stream = _connection.GetStream();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void closeConnection()
        {
            _finished = true;
            try
            {
                _connection.Close();
                _stream.Close();
                _waitHandle.Close();
                _connection = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void sendRequest(Request request)
        {
            try
            {
                lock (_stream)
                {
                    string requestJson = JsonSerializer.Serialize(request);
                    byte[] data = Encoding.UTF8.GetBytes(requestJson + "\n");
                    _stream.Write(data, 0, data.Length);
                    _stream.Flush();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error sending request", e);
            }
        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (_responses)
                {
                    response = _responses.Dequeue();
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        public User Login(string username, string password, IManageObserver client)
        {
            initializeConnection();

            sendRequest(RRBuilder.CreateLoginRequest(new User(username,password,UserType.admin)));
            Response response = readResponse();

            if (response.type == responseType.ERROR)
            {
                closeConnection();
                Console.WriteLine("Error: " + response.errorMessage);
                throw new Exception("Error:" + response.errorMessage);
            }

            if (response.type == responseType.LOGGED_CLIENT)
            {
                _client = client;
                Console.WriteLine("Login successful");
                return new User(response.user.id, response.user.username, response.user.password, response.user.type);
            }

            return null;
        }

        public void AddBugs(IEnumerable<Bug> bugs)
        {
            sendRequest(RRBuilder.CreateAddBugsRequest(bugs));
            Response response = readResponse();

            if (response.type == responseType.ERROR)
            {
                Console.WriteLine("Error: " + response.errorMessage);
                throw new Exception("Error: " + response.errorMessage);
            }
            if (response.type == responseType.OK)
            {
                Console.WriteLine("Bugs added successfully");
            }
        }

        public void ChangeBugStatus(Bug newBug)
        {
            sendRequest(RRBuilder.CreateStatusBugsRequest(newBug));
            Response response = readResponse();

            if (response.type == responseType.ERROR)
            {
                Console.WriteLine("Error: " + response.errorMessage);
                throw new Exception("Error: " + response.errorMessage);
            }
            if (response.type == responseType.OK)
            {
                Console.WriteLine("Bug status changed successfully");
            }
        }

        public void CreateUser(string username, string password, UserType type)
        {
            sendRequest(RRBuilder.CreateCreateUserRequest(new User(username, password, type)));
            Response response = readResponse();
            if (response.type == responseType.ERROR)
            {
                Console.WriteLine("Error: " + response.errorMessage);
                throw new Exception("Error: " + response.errorMessage);
            }
            if (response.type == responseType.OK)
            {
                Console.WriteLine("User created successfully");
            }
        }

        public IEnumerable<Bug> GetBugs()
        {
            sendRequest(RRBuilder.CreateGetBugsRequest());
            Response response = readResponse();

            if(response.type == responseType.REQUESTED_BUGS)
            {
                Console.WriteLine("Bugs received successfully");
                return response.bugDTOs.Select(b => new Bug(b.bugNo, b.description, b.status));
            }

            if (response.type == responseType.ERROR)
            {
                Console.WriteLine("Error: " + response.errorMessage);
                throw new Exception("Error: " + response.errorMessage);
            }

            return null;
        }

        public void Logout(User user)
        {
            sendRequest(RRBuilder.CreateLogoutRequest(user));
            Response response = readResponse();
            closeConnection();
            if(response.type == responseType.ERROR)
            {
                Console.WriteLine("Error: " + response.errorMessage);
                throw new Exception("Error: " + response.errorMessage);
            }
        }

        private void startReader()
        {
            Thread thread = new Thread(run);
            thread.Start();
        }

        public virtual void run()
        {
            using StreamReader reader = new StreamReader(_stream, Encoding.UTF8);
            while (!_finished)
            {
                try
                {
                    string responseJson = reader.ReadLine();
                    if (string.IsNullOrEmpty(responseJson)) continue;

                    Console.WriteLine("Response Recieved: " + responseJson);
                    Response response = JsonSerializer.Deserialize<Response>(responseJson);
                    Console.WriteLine("Response Deserialize " + response);

                    if (isUpdate(response))
                    {
                        handleUpdate(response);
                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue(response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                   Console.WriteLine("Error: " + e.Message);
                   //throw new ServerStopException(e.Message);
                }
            }
        }

        private void handleUpdate(Response response)
        {
            if (response == null)
                return;

            Console.WriteLine("Handling update: " + response.type);
            switch (response.type)
            {
                case responseType.NEW_BUGS:
                    try
                    {
                        Console.WriteLine("New bugs received");
                        if (response.bugDTOs != null)
                        {
                            IEnumerable<Bug> bugs = response.bugDTOs.Select(b => new Bug(b.bugNo, b.description, b.status));
                            _client.BugsAdded(bugs);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error handling new bugs: " + e.Message);
                    }
                    return;
                case responseType.STATUS_BUG_CHANGED:
                    Console.WriteLine("Bug status changed");
                    try
                    {
                        if (response.bugDTOs != null && response.bugDTOs.Length > 0)
                        {
                            Bug bug = new(response.bugDTOs[0].bugNo, response.bugDTOs[0].description, response.bugDTOs[0].status);
                            _client.BugStatusChanged(bug);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error handling bug status change: " + e.Message);
                    }
                    return;
                default:
                    return;
            }
        }

        private bool isUpdate(Response? response)
        {
            if(response == null)
            {
                return false;
            }

            return response.type == responseType.NEW_BUGS ||
                   response.type == responseType.STATUS_BUG_CHANGED;
        }
    }
}
