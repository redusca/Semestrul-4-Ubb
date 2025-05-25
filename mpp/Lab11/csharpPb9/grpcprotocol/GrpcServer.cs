using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Grpc.Core;
using log4net;
using services.services;
using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Formatters;
namespace networking.grpc
{
    public class GRPCServer : IHostedService
    {
        private Server server;
        private readonly string host;
        private readonly int port;
        private readonly IService serviceImplementation;
        private static readonly ILog log = LogManager.GetLogger(typeof(GRPCServer));
        private ConcurrentDictionary<long, List<IServerStreamWriter<NotificationResponse>>> connectedObservers = new ConcurrentDictionary<long, List<IServerStreamWriter<NotificationResponse>>>();
        private ConcurrentDictionary<IServerStreamWriter<NotificationResponse>, SemaphoreSlim> observerSemaphores =
    new ConcurrentDictionary<IServerStreamWriter<NotificationResponse>, SemaphoreSlim>();
        public GRPCServer(string host, int port, IService serviceImplementation)
        {
            this.host = host;
            this.port = port;
            this.serviceImplementation = serviceImplementation;
            log.Debug("Creating GRPCServer...");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            server = new Server
            {
                Services = { TriatlonService.BindService(new TriatlonServiceImpl(serviceImplementation, this)) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };

            server.Start();
            log.Info($"gRPC server started on {host}:{port}");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log.Info("Stopping gRPC server...");
            return server.ShutdownAsync();
        }

        internal void RegisterObserver(long arbitruId, IServerStreamWriter<NotificationResponse> responseStream)
        {
            log.Info($"Registering observer for arbitru ID: {arbitruId}");

            connectedObservers.AddOrUpdate(
                arbitruId,
                new List<IServerStreamWriter<NotificationResponse>> { responseStream },
                (key, existingList) =>
                {
                    existingList.Add(responseStream);
                    return existingList;
                }
            );
        }

        internal void RemoveObserver(long arbitruId, IServerStreamWriter<NotificationResponse> responseStream)
        {
            log.Info($"Removing observer for arbitru ID: {arbitruId}");

            if (connectedObservers.TryGetValue(arbitruId, out var observers))
            {
                observers.Remove(responseStream);
                if (observers.Count == 0)
                {
                    connectedObservers.TryRemove(arbitruId, out _);
                }
            }

            if (observerSemaphores.TryRemove(responseStream, out var semaphore))
            {
                semaphore.Dispose();
            }
        }

        internal async Task NotifyRezultatAdded(long idParticipant, string numeParticipant, string prenumeParticipant, string idProba, long puncte)
        {
            log.Info($"Notifying all observers about new rezultat for participant: {numeParticipant} {prenumeParticipant}");

            var notification = new NotificationResponse
            {
                Type = NotificationResponse.Types.NotificationType.RezultatAdded,
                Rezultat = new RezultatDTO
                {
                    IdProba = idProba,
                    IdParticipant = idParticipant,
                    NumeParticipant = numeParticipant,
                    PrenumeParticipant = prenumeParticipant,
                    Puncte = puncte
                }
            };

            var notificationTasks = new List<Task>();

            foreach (var observers in connectedObservers.Values)
            {
                foreach (var observer in observers)
                {
                    notificationTasks.Add(NotifyObserver(observer, notification));
                }
            }

            await Task.WhenAll(notificationTasks);
        }

        private async Task NotifyObserver(IServerStreamWriter<NotificationResponse> observer, NotificationResponse notification)
        {
            var semaphore = observerSemaphores.GetOrAdd(observer, _ => new SemaphoreSlim(1, 1));

            try
            {
                // Wait to acquire the semaphore before writing
                await semaphore.WaitAsync();

                try
                {
                    // Now we have exclusive access to write to this observer
                    await observer.WriteAsync(notification);
                }
                finally
                {
                    // Always release the semaphore
                    semaphore.Release();
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                log.Info("Observer stream was cancelled");
                // Remove the semaphore if we're done with this observer
                observerSemaphores.TryRemove(observer, out _);
            }
            catch (Exception ex)
            {
                log.Error($"Error notifying observer: {ex.Message}", ex);
                // Observer is likely disconnected, but it will be removed when the stream completes
                observerSemaphores.TryRemove(observer, out _);
            }
        }
    }
}
