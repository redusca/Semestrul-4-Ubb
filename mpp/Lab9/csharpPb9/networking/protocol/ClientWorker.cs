using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using networking.DTO;
using services.services;

namespace networking.protocol
{
    public class ClientWorker : IManageObserver
    {
        private IService server;
        private TcpClient connection;

        private NetworkStream stream;
        private volatile bool connected;
        private static readonly ILog log = LogManager.GetLogger(typeof(ClientWorker));

        public ClientWorker(IService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                connected = true;
            }
            catch (Exception e)
            {
                log.Error("Error getting stream", e);
            }
        }

        public virtual void run()
        {
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            while (connected)
            {
                try
                {
                    string requestJson = reader.ReadLine();
                    if (string.IsNullOrEmpty(requestJson)) continue;
                    log.DebugFormat("Recieved Request: {0}", requestJson);
                    Request request = JsonSerializer.Deserialize<Request>(requestJson);
                    log.DebugFormat("Deserialized Request: {0}", request);
                    Response response = handleRequest(request);
                    if (response != null)
                    {
                        sendResponse(response);
                    }
                }
                catch (IOException e)
                {
                    log.ErrorFormat("run eroare: {0}", e.Message);
                    if(e.InnerException != null)
                        log.ErrorFormat("run innner eroare: {0}", e.InnerException.Message);
                    log.Error(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e) 
                {
                    log.Error(e.StackTrace);
                }
            }
        }

   
        private static Response okResponse = JsonProtocolUtils.createOkResponse();

        private Response handleRequest(Request request)
        {
            Response response = null;
            switch (request.typeCsharp)
            {
                case RequestType.LOGIN:
                    log.Info("Login request" + request.user);
                    Arbitru user = DTOutils.getFromDTO(request.user);
                    try
                    {
                        return JsonProtocolUtils.createLoggedReposponse(
                            server.login(user.Username, user.Password,this)
                            );
                    }
                    catch (Exception e)
                    {
                        log.Error("Error login", e);
                        return JsonProtocolUtils.createErrorResponse(e.Message);
                    }
                case RequestType.GET_PARTICIPANTI:
                    log.Info("Get participanti request");
                    try
                    {
                        return JsonProtocolUtils.createParticipantiResponse(server.GetParticipants());
                    }
                    catch (Exception e)
                    {
                        log.Error("Error get participanti", e);
                        return JsonProtocolUtils.createErrorResponse(e.Message);
                    }
                case RequestType.GET_REZULTATE:
                    log.Info("Get rezultate request");
                    try
                    {
                        return JsonProtocolUtils.createRezultateResponse(
                            server.getParticipantiPuncteDesc(DTOutils.getFromDTO(request.user).Id_proba));
                    }
                    catch (Exception e)
                    {
                        log.Error("Error get rezultate", e);
                        return JsonProtocolUtils.createErrorResponse(e.Message);
                    }
                case RequestType.ADD_REZULTAT:
                    log.Info("Add rezultat request");
                    try
                    {
                        server.addRezultat(
                            request.rezultat.idParticipant,
                            request.rezultat.numeParticipant,
                            request.rezultat.prenumeParticipant,
                            request.rezultat.idProba,
                            request.rezultat.puncte);

                        return okResponse;
                    }
                    catch (Exception e)
                    {
                        log.Error("Error add rezultat", e);
                        return JsonProtocolUtils.createErrorResponse(e.Message);
                    }
                case RequestType.GET_ALL_REZULTATE:
                    log.Info("Get all rezultate request");
                    try
                    {
                        return JsonProtocolUtils.createAllRezultateResponse(server.getParticipantiAlfabtic());
                    }
                    catch (Exception e)
                    {
                        log.Error("Error get all rezultate", e);
                        return JsonProtocolUtils.createErrorResponse(e.Message);
                    }
                case RequestType.LOGOUT:
                    log.Info("Logout request");
                    Arbitru arbitru = DTOutils.getFromDTO(request.user);
                    try
                    {
                        server.logutOut(arbitru, this);
                        connected = false;
                        return okResponse;
                    }
                    catch (Exception e)
                    {
                        log.Error("Error logout", e);
                        return JsonProtocolUtils.createErrorResponse(e.Message);
                        log.Error("Unknown request type");
                    }
                default:
                    log.Error("Unknown request type");
                    break;

            }
            return response;
        }

        private void sendResponse(Response response)
        {
            String jsonResponse = JsonSerializer.Serialize(response);
            log.DebugFormat("Sending Response: {0}", jsonResponse);
            lock(stream)
            {
                byte[] data = Encoding.UTF8.GetBytes(jsonResponse + "\n");
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }

        public virtual void RezultatAdded(long idParticipant, string numeParticipant, string prenumeParticipant, string idproba, long punctaj)
        {
            Response response = JsonProtocolUtils.createRezultatAddedResponse(
                new Rezultat(
                    -1L,
                    new Participant(idParticipant, numeParticipant, prenumeParticipant, 0),
                    new Proba(idproba,"",Categorie.inot),
                    punctaj
                )
            );
            log.DebugFormat("Sending Rezultat Added: {0}", response);
            try
            {
                sendResponse(response);
            }
            catch (Exception e)
            {
                log.Error("Error sending rezultat added", e);
                log.Error(e.StackTrace);
            }
        }
    }
}
