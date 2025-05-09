using model;
using model.Enum;
using networking.jsonProtocol.responseAndrequest;
using services.Interfaces;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace networking.jsonProtocol
{
    public class ClientWorker : IManageObserver
    {
        private IService _server;
        private TcpClient _connection;

        private NetworkStream _stream;
        private volatile bool _connected;
        public ClientWorker(IService server, TcpClient connection)
        {
            this._server = server;
            this._connection = connection;
            try
            {
                _stream = connection.GetStream();
                _connected = true;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void run()
        {
            using StreamReader reader = new StreamReader(_stream, Encoding.UTF8);
            while (_connected)
            {
                try {
                    string requestJson = reader.ReadLine();
                    if (string.IsNullOrEmpty(requestJson)) continue;
                    Console.WriteLine("Request Recieved: " + requestJson);
                    Request request = JsonSerializer.Deserialize<Request>(requestJson);
                    Console.WriteLine("Request Deserialized: " + request);
                    Response response = HandleRequest(request);
                    if (response != null) { sendResponse(response); }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Connection error: " + e.Message);
                    _connected = false;
                    _server.Logout(_crashUser);
                }
                catch (JsonException e)
                {
                    Console.WriteLine("JSON error: " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected error: " + e.Message);
                    _server.Logout(_crashUser);
                    _connected = false;
                }

                try
                {
                    Thread.Sleep(100); // Sleep for a short duration to avoid busy wait
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine("Thread interrupted: " + e.Message);
                }
            }
        }

        private static Response okResponse = RRBuilder.CreateOkResponse();
        private User _crashUser;

        private void sendResponse(Response response)
        {
            String jsonResponse = JsonSerializer.Serialize(response);
            Console.WriteLine("Sending response: " + jsonResponse);
            lock (_stream)
            {
                byte[] data = Encoding.UTF8.GetBytes(jsonResponse + "\n");
                _stream.Write(data, 0, data.Length);
                _stream.Flush();
            }
        }

        private Response HandleRequest(Request request)
        {
            switch (request.type)
            {
                case requestType.LOGIN:
                    Console.WriteLine("Login request" + request.User);
                    try
                    {
                        _crashUser = new User(request.User.username, request.User.password, UserType.admin);
                        return RRBuilder.CreateLoginResponse(_server.Login(
                            request.User.username,
                            request.User.password,
                            this
                        ));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Login error: " + e.Message);
                        return RRBuilder.CreateErrorResponse(e.Message);
                    }
                case requestType.LOGOUT:
                    Console.WriteLine("Logout request" + request.User);
                    try
                    {
                        _server.Logout(new User(request.User.id, request.User.username, request.User.password, request.User.type));
                        return okResponse;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Logout error: " + e.Message);
                        return RRBuilder.CreateErrorResponse(e.Message);
                    }
                case requestType.GET_BUGS:
                    Console.WriteLine("Get bugs request");
                    try
                    {
                        return RRBuilder.CreateGetBugsResponse(_server.GetBugs());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Get bugs error: " + e.Message);
                        return RRBuilder.CreateErrorResponse(e.Message);
                    }
                case requestType.ADD_BUGS:
                    Console.WriteLine("Add bugs request");
                    try
                    {
                        IEnumerable<Bug> bugs = request.bugDTOs.Select(b => new Bug(b.bugNo, b.description, b.status));
                        _server.AddBugs(bugs);
                        return okResponse;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Add bugs error: " + e.Message);
                        return RRBuilder.CreateErrorResponse(e.Message);
                    }
                case requestType.STATUS_BUG:
                    Console.WriteLine("Status bugs request");
                    try
                    {
                        Bug bug = new Bug(request.bugDTOs[0].bugNo, request.bugDTOs[0].description, request.bugDTOs[0].status);
                        _server.ChangeBugStatus(bug);
                        return okResponse;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Change bug status error: " + e.Message);
                        return RRBuilder.CreateErrorResponse(e.Message);
                    }
                case requestType.CREATE_USER:
                    Console.WriteLine("Create user request" + request.User);
                    try
                    {
                        _server.CreateUser(request.User.username, request.User.password, request.User.type);
                        return okResponse;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Create user error: " + e.Message);
                        return RRBuilder.CreateErrorResponse(e.Message);
                    }
                default:
                    Console.WriteLine("No request type found");
                    return RRBuilder.CreateErrorResponse("Type request didn't exist!");
            }
        }

        public void BugsAdded(IEnumerable<Bug> bugs)
        {
            Response response = RRBuilder.CreateNewBugsResponse(bugs);
            Console.WriteLine("Sending new bugs response: " + response);
            try
            {
                sendResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error: " + e.Message);
            }
        }

        public void BugStatusChanged(Bug newBug)
        {
            Response response = RRBuilder.CreateStatusBugChangedResponse(newBug);
            try
            {
                sendResponse(response);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unexpected error: " + e.Message);
            }
        }
    }
}
