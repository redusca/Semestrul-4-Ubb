using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using log4net;
using services.services;

namespace networking.grpc
{
    public class GRPCServerProxy : IService
    {
        private readonly string host;
        private readonly int port;
        private readonly TriatlonService.TriatlonServiceClient client;
        private IManageObserver observer;
        private CancellationTokenSource notificationCancellation;
        private GrpcChannel channel;
        private static readonly ILog log = LogManager.GetLogger(typeof(GRPCServerProxy));

        public GRPCServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;

            // Create a channel and client
            channel = GrpcChannel.ForAddress($"http://{host}:{port}");
            client = new TriatlonService.TriatlonServiceClient(channel);

            log.Debug($"GRPCServerProxy initialized for {host}:{port}");
        }

        public Arbitru login(string username, string password, IManageObserver client)
        {
            log.Info($"Login attempt for username: {username}");
            this.observer = client;

            try
            {
                var request = new LoginRequest
                {
                    Username = username,
                    Password = password
                };

                var response = this.client.Login(request);

                if (!response.Success)
                {
                    log.Error($"Login failed: {response.ErrorMessage}");
                    throw new Exception(response.ErrorMessage);
                }

                var arbitru = new Arbitru(
                    response.Arbitru.Id,
                    response.Arbitru.Nume,
                    response.Arbitru.Username,
                    response.Arbitru.Password,
                    response.Arbitru.IdProba
                );

                // Start notifications subscription after successful login
                StartNotificationSubscription(arbitru.Id);

                return arbitru;
            }
            catch (RpcException ex)
            {
                log.Error($"RPC error during login: {ex.Message}", ex);
                throw new Exception($"Communication error: {ex.Message}", ex);
            }
        }

        public void logutOut(Arbitru arbitru, IManageObserver client)
        {
            log.Info($"Logout for arbitru: {arbitru.Id}");

            try
            {
                // Cancel notification subscription
                StopNotificationSubscription();

                var request = new LogoutRequest
                {
                    ArbitruId = arbitru.Id
                };

                var response = this.client.Logout(request);

                if (!response.Success)
                {
                    log.Error($"Logout failed: {response.ErrorMessage}");
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (RpcException ex)
            {
                log.Error($"RPC error during logout: {ex.Message}", ex);
                throw new Exception($"Communication error: {ex.Message}", ex);
            }
            finally
            {
                this.observer = null;
            }
        }

        public IEnumerable<Participant> GetParticipants()
        {
            log.Info("Getting participants");

            try
            {
                var request = new EmptyRequest();
                var response = client.GetParticipants(request);

                if (!response.Success)
                {
                    log.Error($"GetParticipants failed: {response.ErrorMessage}");
                    throw new Exception(response.ErrorMessage);
                }

                return response.Participanti.Select(p => new Participant(
                    p.IdParticipant,
                    p.Nume,
                    p.Prenume,
                    p.Varsta
                )).ToList();
            }
            catch (RpcException ex)
            {
                log.Error($"RPC error getting participants: {ex.Message}", ex);
                throw new Exception($"Communication error: {ex.Message}", ex);
            }
        }

        public Dictionary<Participant, long> getParticipantiPuncteDesc(string idProba)
        {
            log.Info($"Getting rezultate for proba: {idProba}");

            try
            {
                var request = new ProbaRequest
                {
                    ProbaId = idProba
                };

                var response = client.GetRezultateByProba(request);

                if (!response.Success)
                {
                    log.Error($"GetRezultateByProba failed: {response.ErrorMessage}");
                    throw new Exception(response.ErrorMessage);
                }

                var result = new Dictionary<Participant, long>();
                foreach (var p in response.Punctaje)
                {
                    result.Add(
                        new Participant(p.IdParticipant, p.Nume, p.Prenume, p.Varsta),
                        p.Punctaj
                    );
                }

                return result;
            }
            catch (RpcException ex)
            {
                log.Error($"RPC error getting rezultate by proba: {ex.Message}", ex);
                throw new Exception($"Communication error: {ex.Message}", ex);
            }
        }

        public Dictionary<Participant, long> getParticipantiAlfabtic()
        {
            log.Info("Getting all rezultate");

            try
            {
                var request = new EmptyRequest();
                var response = client.GetAllRezultate(request);

                if (!response.Success)
                {
                    log.Error($"GetAllRezultate failed: {response.ErrorMessage}");
                    throw new Exception(response.ErrorMessage);
                }

                var result = new Dictionary<Participant, long>();
                foreach (var p in response.Punctaje)
                {
                    result.Add(
                        new Participant(p.IdParticipant, p.Nume, p.Prenume, p.Varsta),
                        p.Punctaj
                    );
                }

                return result;
            }
            catch (RpcException ex)
            {
                log.Error($"RPC error getting all rezultate: {ex.Message}", ex);
                throw new Exception($"Communication error: {ex.Message}", ex);
            }
        }

        public void addRezultat(long id, string nume, string prenume, string idProba, long punctaj)
        {
            log.Info($"Adding rezultat for participant: {nume} {prenume}, points: {punctaj}");

            try
            {
                var request = new RezultatRequest
                {
                    ParticipantId = id,
                    Nume = nume,
                    Prenume = prenume,
                    ProbaId = idProba,
                    Puncte = punctaj
                };

                var response = client.AddRezultat(request);

                if (!response.Success)
                {
                    log.Error($"AddRezultat failed: {response.ErrorMessage}");
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (RpcException ex)
            {
                log.Error($"RPC error adding rezultat: {ex.Message}", ex);
                throw new Exception($"Communication error: {ex.Message}", ex);
            }
        }

        private void StartNotificationSubscription(long arbitruId)
        {
            log.Info($"Starting notification subscription for arbitruId: {arbitruId}");

            // Cancel any existing subscription
            StopNotificationSubscription();

            // Create a new cancellation token source
            notificationCancellation = new CancellationTokenSource();

            // Start the subscription task
            Task.Run(async () =>
            {
                try
                {
                    var request = new SubscribeRequest
                    {
                        ArbitruId = arbitruId
                    };

                    using var call = client.SubscribeToNotifications(request);

                    while (await call.ResponseStream.MoveNext(notificationCancellation.Token))
                    {
                        var notification = call.ResponseStream.Current;
                        ProcessNotification(notification);
                    }
                }
                catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
                {
                    log.Info("Notification subscription was cancelled");
                }
                catch (OperationCanceledException)
                {
                    log.Info("Notification subscription was cancelled");
                }
                catch (Exception ex)
                {
                    log.Error($"Error in notification subscription: {ex.Message}", ex);
                }
            });
        }

        private void StopNotificationSubscription()
        {
            if (notificationCancellation != null)
            {
                log.Info("Stopping notification subscription");
                notificationCancellation.Cancel();
                notificationCancellation.Dispose();
                notificationCancellation = null;
            }
        }

        private void ProcessNotification(NotificationResponse notification)
        {
            if (observer == null) return;

            switch (notification.Type)
            {
                case NotificationResponse.Types.NotificationType.RezultatAdded:
                    log.Info($"Received rezultat added notification for participant: {notification.Rezultat.NumeParticipant} {notification.Rezultat.PrenumeParticipant}");

                    observer.RezultatAdded(
                        notification.Rezultat.IdParticipant,
                        notification.Rezultat.NumeParticipant,
                        notification.Rezultat.PrenumeParticipant,
                        notification.Rezultat.IdProba,
                        notification.Rezultat.Puncte
                    );
                    break;
                default:
                    log.Warn($"Unknown notification type: {notification.Type}");
                    break;
            }
        }

        // Cleanup method to properly dispose of resources
        public void Dispose()
        {
            StopNotificationSubscription();
            channel?.Dispose();
        }
    }
}