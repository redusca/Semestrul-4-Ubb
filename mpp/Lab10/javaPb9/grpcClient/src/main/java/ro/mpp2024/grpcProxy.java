package ro.mpp2024;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import networking.grpc.TriatlonServiceGrpc;
import networking.grpc.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.apache.logging.log4j.Logger;
import org.apache.logging.log4j.LogManager;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;

import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;

public class grpcProxy implements IService, AutoCloseable {
    private final String host;
    private final int port;
    private final TriatlonServiceGrpc.TriatlonServiceBlockingStub blockingStub;
    private final TriatlonServiceGrpc.TriatlonServiceStub asyncStub;
    private IManageObserver observer;
    private ManagedChannel channel;
    private final AtomicBoolean isSubscribed = new AtomicBoolean(false);
    private static final Logger log = LogManager.getLogger(grpcProxy.class);

    public grpcProxy(String host, int port) {
        this.host = host;
        this.port = port;

        // Create a channel and client
        channel = ManagedChannelBuilder.forAddress(host, port)
                .usePlaintext() // For development only, use TLS in production
                .build();
        blockingStub = TriatlonServiceGrpc.newBlockingStub(channel);
        asyncStub = TriatlonServiceGrpc.newStub(channel);

        log.debug(String.format("GRPCServerProxy initialized for %s:%d", host, port));
    }

    @Override
    public Arbitru login(String username, String password, IManageObserver client) throws ManageException {
        log.info("Login attempt for username: " + username);
        this.observer = client;

        try {
            LoginRequest request = LoginRequest.newBuilder()
                    .setUsername(username)
                    .setPassword(password)
                    .build();

            LoginResponse response = blockingStub.login(request);

            if (!response.getSuccess()) {
                log.error("Login failed: " + response.getErrorMessage());
                throw new ManageException(response.getErrorMessage());
            }

            ArbitruDTO arbitruDTO = response.getArbitru();
            Arbitru arbitru = new Arbitru(
                    arbitruDTO.getId(),
                    arbitruDTO.getNume(),
                    arbitruDTO.getUsername(),
                    arbitruDTO.getPassword(),
                    arbitruDTO.getIdProba()
            );

            // Start notifications subscription after successful login
            startNotificationSubscription(arbitru.getId());

            return arbitru;
        } catch (StatusRuntimeException ex) {
            log.error("RPC error during login: " + ex.getMessage(), ex);
            throw new ManageException("Communication error: " + ex.getMessage(), ex);
        }
    }

    @Override
    public void logout(Arbitru arbitru, IManageObserver client) throws ManageException {
        log.info("Logout for arbitru: " + arbitru.getId());

        try {
            // Cancel notification subscription
            stopNotificationSubscription();

            LogoutRequest request = LogoutRequest.newBuilder()
                    .setArbitruId(arbitru.getId())
                    .build();

            StatusResponse response = blockingStub.logout(request);

            if (!response.getSuccess()) {
                log.error("Logout failed: " + response.getErrorMessage());
                throw new ManageException(response.getErrorMessage());
            }
        } catch (StatusRuntimeException ex) {
            log.error("RPC error during logout: " + ex.getMessage(), ex);
            throw new ManageException("Communication error: " + ex.getMessage(), ex);
        } finally {
            this.observer = null;
        }
    }

    @Override
    public Iterable<Participant> getParticipanti() {
        log.info("Getting participants");

        try {
            EmptyRequest request = EmptyRequest.newBuilder().build();
            ParticipantsResponse response = blockingStub.getParticipants(request);

            if (!response.getSuccess()) {
                log.error("GetParticipants failed: " + response.getErrorMessage());
                throw new ManageException(response.getErrorMessage());
            }

            List<Participant> participants = new ArrayList<>();
            for (PunctajParticipantDTO p : response.getParticipantiList()) {
                participants.add(new Participant(
                        p.getIdParticipant(),
                        p.getNume(),
                        p.getPrenume(),
                        p.getVarsta()
                ));
            }

            return participants;
        } catch (StatusRuntimeException ex) {
            log.error("RPC error getting participants: " + ex.getMessage(), ex);
            throw new ManageException("Communication error: " + ex.getMessage(), ex);
        }
    }

    @Override
    public Map<Participant, Long> getParticipantiPuncteDesc(String idProba) {
        log.info("Getting rezultate for proba: " + idProba);

        try {
            ProbaRequest request = ProbaRequest.newBuilder()
                    .setProbaId(idProba)
                    .build();

            PunctajeResponse response = blockingStub.getRezultateByProba(request);

            if (!response.getSuccess()) {
                log.error("GetRezultateByProba failed: " + response.getErrorMessage());
                throw new ManageException(response.getErrorMessage());
            }

            Map<Participant, Long> result = new HashMap<>();
            for (PunctajParticipantDTO p : response.getPunctajeList()) {
                result.put(
                        new Participant(p.getIdParticipant(), p.getNume(), p.getPrenume(), p.getVarsta()),
                        p.getPunctaj()
                );
            }

            return result;
        } catch (StatusRuntimeException ex) {
            log.error("RPC error getting rezultate by proba: " + ex.getMessage(), ex);
            throw new ManageException("Communication error: " + ex.getMessage(), ex);
        }
    }

    @Override
    public Map<Participant, Long> getParticipantiAlfabetic() {
        log.info("Getting all rezultate");

        try {
            EmptyRequest request = EmptyRequest.newBuilder().build();
            PunctajeResponse response = blockingStub.getAllRezultate(request);

            if (!response.getSuccess()) {
                log.error("GetAllRezultate failed: " + response.getErrorMessage());
                throw new ManageException(response.getErrorMessage());
            }

            Map<Participant, Long> result = new HashMap<>();
            for (PunctajParticipantDTO p : response.getPunctajeList()) {
                result.put(
                        new Participant(p.getIdParticipant(), p.getNume(), p.getPrenume(), p.getVarsta()),
                        p.getPunctaj()
                );
            }

            return result;
        } catch (StatusRuntimeException ex) {
            log.error("RPC error getting all rezultate: " + ex.getMessage(), ex);
            throw new ManageException("Communication error: " + ex.getMessage(), ex);
        }
    }

    @Override
    public void addRezultat(Long idParticipant, String nume, String prenume, String idProba, long puncte) {
        log.info(String.format("Adding rezultat for participant: %s %s, points: %d", nume, prenume, puncte));

        try {
            RezultatRequest request = RezultatRequest.newBuilder()
                    .setParticipantId(idParticipant)
                    .setNume(nume)
                    .setPrenume(prenume)
                    .setProbaId(idProba)
                    .setPuncte(puncte)
                    .build();

            StatusResponse response = blockingStub.addRezultat(request);

            if (!response.getSuccess()) {
                log.error("AddRezultat failed: " + response.getErrorMessage());
                throw new ManageException(response.getErrorMessage());
            }
        } catch (StatusRuntimeException ex) {
            log.error("RPC error adding rezultat: " + ex.getMessage(), ex);
            throw new ManageException("Communication error: " + ex.getMessage(), ex);
        }
    }

    private void startNotificationSubscription(long arbitruId) {
        log.info("Starting notification subscription for arbitruId: " + arbitruId);

        // Cancel any existing subscription
        stopNotificationSubscription();

        // Set the subscribed flag to true
        isSubscribed.set(true);

        // Create subscription request
        SubscribeRequest request =  SubscribeRequest.newBuilder()
                .setArbitruId(arbitruId)
                .build();

        // Start the notification stream
        asyncStub.subscribeToNotifications(request, new StreamObserver<NotificationResponse>() {
            @Override
            public void onNext(NotificationResponse notification) {
                processNotification(notification);
            }

            @Override
            public void onError(Throwable throwable) {
                if (!isSubscribed.get()) {
                    log.info("Notification subscription was cancelled");
                } else {
                    log.error("Error in notification subscription: " + throwable.getMessage(), throwable);
                }
            }

            @Override
            public void onCompleted() {
                log.info("Notification subscription completed");
            }
        });
    }

    private void stopNotificationSubscription() {
        if (isSubscribed.getAndSet(false)) {
            log.info("Stopping notification subscription");
            // The flag will cause the error handler to ignore errors
        }
    }

    private void processNotification(NotificationResponse notification) {
        if (observer == null) return;

        switch (notification.getType()) {
            case REZULTAT_ADDED:
                RezultatDTO rezultat = notification.getRezultat();
                log.info(String.format("Received rezultat added notification for participant: %s %s",
                        rezultat.getNumeParticipant(), rezultat.getPrenumeParticipant()));

                try {
                    observer.RezultatAdded(
                            rezultat.getIdParticipant(),
                            rezultat.getNumeParticipant(),
                            rezultat.getPrenumeParticipant(),
                            rezultat.getIdProba(),
                            rezultat.getPuncte()
                    );
                } catch (ManageException e) {
                    log.error("Error notifying observer: " + e.getMessage(), e);
                }
                break;
            default:
                log.warn("Unknown notification type: " + notification.getType());
                break;
        }
    }

    @Override
    public void close() {
        stopNotificationSubscription();
        if (channel != null) {
            try {
                channel.shutdown().awaitTermination(5, TimeUnit.SECONDS);
            } catch (InterruptedException e) {
                log.error("Error shutting down channel: " + e.getMessage(), e);
                Thread.currentThread().interrupt();
            }
        }
    }
}