using log4net;
using log4net.Repository.Hierarchy;
using networking.DTO;
using services.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace networking.protocol
{
    public class ServerProxy : IService
    {
        private string host;
        private int port;

        private IManageObserver client;
        private TcpClient connection;
        private NetworkStream stream;
        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        private static readonly ILog log = LogManager.GetLogger(typeof(ServerProxy));

        public ServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        public void addRezultat(long id, string nume, string prenume, string idProba, long punctaj)
        {
            Proba proba = new Proba(idProba, "", 0);
            Participant participant = new Participant(id, nume, prenume, 0);
            Rezultat rezultat = new Rezultat(-1L,participant,proba, punctaj);
            sendRequest(JsonProtocolUtils.createAddRezultatRequest(rezultat));
            Response response = readResponse();
            if (response.typeCsharp == ResponseType.ERROR)
            {
                throw new Exception(response.errormessage);
            }
        }

        public Dictionary<Participant, long> getParticipantiAlfabtic()
        {
            sendRequest(JsonProtocolUtils.createGetAllRezultateRequest());
            Response response = readResponse();
            if (response.typeCsharp == ResponseType.ERROR)
            {
                throw new Exception(response.errormessage);
            }
            return DTOutils.getFromDTO(response.punctaje);  
        }

        public Dictionary<Participant, long> getParticipantiPuncteDesc(string idProba)
        {
            sendRequest(JsonProtocolUtils.createGetRezultateRequest(new Arbitru("","","",idProba)));
            Response response = readResponse();
            if (response.typeCsharp == ResponseType.ERROR)
            {
                throw new Exception(response.errormessage);
            }
            return DTOutils.getFromDTO(response.punctaje);
        }

        public IEnumerable<Participant> GetParticipants()
        {
            sendRequest(JsonProtocolUtils.createGetParticipantiRequest());
            Response response = readResponse();
            if (response.typeCsharp == ResponseType.ERROR)
            {
                throw new Exception(response.errormessage);
            }
            return DTOutils.getFromDTO(response.participanti).Keys.ToList();
        }

        public Arbitru login(string username, string password, IManageObserver client)
        {
            initializeConnection();
            sendRequest(JsonProtocolUtils.createLoginRequest(new Arbitru("",username, password,"c0")));
            Response response = readResponse();
            if (response.typeCsharp == ResponseType.LOGGED_ACC)
            {
                this.client = client;
                Arbitru arbitru = DTOutils.getFromDTO(response.user);
                return arbitru;
            }
            
            if(response.typeCsharp == ResponseType.ERROR)
            {
                closeConnection();
                log.ErrorFormat("Login error {0}", response.errormessage);
                throw new Exception(response.errormessage);
            }

            return null;
        }

        public void logutOut(Arbitru arbitru, IManageObserver client)
        {
            sendRequest(JsonProtocolUtils.createLogoutRequest(arbitru));
            Response response = readResponse();
            closeConnection();
            if (response.typeCsharp == ResponseType.ERROR)
            {
                throw new Exception(response.errormessage);
            }

        }

        private void sendRequest(Request request)
        {
            try
            {
                lock (stream)
                {
                    string requestJson = JsonSerializer.Serialize(request);
                    log.DebugFormat("Sending Request: {0}", requestJson);
                    byte[] data = Encoding.UTF8.GetBytes(requestJson + "\n");
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
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
                lock (responses)
                {
                    response = responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                log.Error("Error reading response", e);
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                connection.Close();
                stream.Close();
                _waitHandle.Close();
                connection = null;
            }
            catch (Exception e)
            {
                log.Error("Error closing connection", e);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                log.Error("Error initializing connection", e);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void startReader()
        {
            Thread thread = new Thread(run);
            thread.Start();
        }

        public virtual void run()
        {
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            while (!finished)
            {
                try
                {
                    string responseJson = reader.ReadLine();
                    if (string.IsNullOrEmpty(responseJson)) continue;

                    log.DebugFormat("Received Response: {0}", responseJson);
                    Response response = JsonSerializer.Deserialize<Response>(responseJson);
                    log.DebugFormat("Deserialized Response: {0}", response);


                    if (isUpdate(response))
                    {
                        handleUpdate(response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue(response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    log.Error("reading eror");
                }
            }
        }

        private void handleUpdate(Response? response)
        {
            if (response.typeCsharp == ResponseType.REZULTAT_ADDED)
            {
                try
                {
                    client.RezultatAdded(
                        response.rezultat.idParticipant,
                        response.rezultat.numeParticipant,
                        response.rezultat.prenumeParticipant,
                        response.rezultat.idProba,
                        response.rezultat.puncte);
                }
                catch (Exception e)
                {
                    log.Error(e.StackTrace);
                }
            }
        }

        private bool isUpdate(Response response)
        {
            if (response == null)
                return false;
            
            return response.typeCsharp == ResponseType.REZULTAT_ADDED;
        }
    }
}
