using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using log4net;
using services.services;

namespace networking.grpc
{
    public class TriatlonServiceImpl : TriatlonService.TriatlonServiceBase, IManageObserver
    {
        private readonly IService serviceImplementation;
        private readonly GRPCServer grpcServer;
        private static readonly ILog log = LogManager.GetLogger(typeof(TriatlonServiceImpl));

        public TriatlonServiceImpl(IService serviceImplementation, GRPCServer grpcServer)
        {
            this.serviceImplementation = serviceImplementation;
            this.grpcServer = grpcServer;
            log.Debug("Creating ConcursServiceImpl...");
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            log.Info($"Login request received for username: {request.Username}");

            try
            {
                var arbitru = serviceImplementation.login(request.Username, request.Password, this);

                var response = new LoginResponse
                {
                    Success = true,
                    Arbitru = new ArbitruDTO
                    {
                        Id = arbitru.Id,
                        Nume = arbitru.Nume,
                        Username = arbitru.Username,
                        Password = arbitru.Parola,
                        IdProba = arbitru.Id_proba
                    }
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                log.Error($"Error during login: {ex.Message}", ex);
                return Task.FromResult(new LoginResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        public override Task<StatusResponse> Logout(LogoutRequest request, ServerCallContext context)
        {
            log.Info($"Logout request received for arbitruId: {request.ArbitruId}");

            try
            {
                // Creating a dummy Arbitru with just the ID - the service layer should handle this appropriately
                var arbitru = new Arbitru(request.ArbitruId, "", "", "", "");
                serviceImplementation.logutOut(arbitru, this);

                return Task.FromResult(new StatusResponse
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {
                log.Error($"Error during logout: {ex.Message}", ex);
                return Task.FromResult(new StatusResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        public override Task<ParticipantsResponse> GetParticipants(EmptyRequest request, ServerCallContext context)
        {
            log.Info("GetParticipants request received");

            try
            {
                var participants = serviceImplementation.GetParticipants();
                var response = new ParticipantsResponse { Success = true };

                foreach (var participant in participants)
                {
                    response.Participanti.Add(new PunctajParticipantDTO
                    {
                        IdParticipant = participant.Id,
                        Nume = participant.Nume,
                        Prenume = participant.Prenume,
                        Varsta = participant.Varsta,
                        Punctaj = 0 // Default value as we're just listing participants
                    });
                }

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                log.Error($"Error getting participants: {ex.Message}", ex);
                return Task.FromResult(new ParticipantsResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        public override Task<PunctajeResponse> GetRezultateByProba(ProbaRequest request, ServerCallContext context)
        {
            log.Info($"GetRezultateByProba request received for probaId: {request.ProbaId}");

            try
            {
                var punctajDict = serviceImplementation.getParticipantiPuncteDesc(request.ProbaId);
                var response = new PunctajeResponse { Success = true };

                foreach (var entry in punctajDict)
                {
                    response.Punctaje.Add(new PunctajParticipantDTO
                    {
                        IdParticipant = entry.Key.Id,
                        Nume = entry.Key.Nume,
                        Prenume = entry.Key.Prenume,
                        Varsta = entry.Key.Varsta,
                        Punctaj = entry.Value
                    });
                }

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                log.Error($"Error getting rezultate by proba: {ex.Message}", ex);
                return Task.FromResult(new PunctajeResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        public override Task<PunctajeResponse> GetAllRezultate(EmptyRequest request, ServerCallContext context)
        {
            log.Info("GetAllRezultate request received");

            try
            {
                var punctajDict = serviceImplementation.getParticipantiAlfabtic();
                var response = new PunctajeResponse { Success = true };

                foreach (var entry in punctajDict)
                {
                    response.Punctaje.Add(new PunctajParticipantDTO
                    {
                        IdParticipant = entry.Key.Id,
                        Nume = entry.Key.Nume,
                        Prenume = entry.Key.Prenume,
                        Varsta = entry.Key.Varsta,
                        Punctaj = entry.Value
                    });
                }

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                log.Error($"Error getting all rezultate: {ex.Message}", ex);
                return Task.FromResult(new PunctajeResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        public override async Task<StatusResponse> AddRezultat(RezultatRequest request, ServerCallContext context)
        {
            log.Info($"AddRezultat request received for participant: {request.Nume} {request.Prenume}, points: {request.Puncte}");

            try
            {
                serviceImplementation.addRezultat(request.ParticipantId, request.Nume, request.Prenume, request.ProbaId, request.Puncte);

                RezultatAdded(request.ParticipantId, request.Nume, request.Prenume, request.ProbaId, request.Puncte);

                return new StatusResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                log.Error($"Error adding rezultat: {ex.Message}", ex);
                return new StatusResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public override async Task SubscribeToNotifications(
            SubscribeRequest request,
            IServerStreamWriter<NotificationResponse> responseStream,
            ServerCallContext context)
        {
            log.Info($"SubscribeToNotifications request received for arbitruId: {request.ArbitruId}");
            try
            {
                // Register the observer with the provided responseStream
                grpcServer.RegisterObserver(request.ArbitruId, responseStream);

                try
                {
                    // Keep the stream open until client disconnects or server stops
                    await Task.Delay(Timeout.Infinite, context.CancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // Expected when the client disconnects or server shuts down
                    log.Info($"Notification stream closed for arbitruId: {request.ArbitruId}");
                }
                finally
                {
                    // Make sure to unregister the observer
                    grpcServer.RemoveObserver(request.ArbitruId, responseStream);
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error during notification subscription: {ex.Message}", ex);
                // Don't rethrow - we want to end the stream gracefully
            }
        }


        // Implementation of IManageObserver interface
        public void RezultatAdded(long idParticipant, string numeParticipant, string prenumeParticipant, string idproba, long punctaj)
        {
            log.Info($"RezultatAdded event: {numeParticipant} {prenumeParticipant}, points: {punctaj}");

            // Notify all observers through the gRPC server
            _ = grpcServer.NotifyRezultatAdded(idParticipant, numeParticipant, prenumeParticipant, idproba, punctaj);
        }
    }
}