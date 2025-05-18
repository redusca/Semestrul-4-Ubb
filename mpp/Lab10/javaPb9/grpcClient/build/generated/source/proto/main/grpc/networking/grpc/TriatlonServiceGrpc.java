package networking.grpc;

import static io.grpc.MethodDescriptor.generateFullMethodName;

/**
 * <pre>
 * Service definition
 * </pre>
 */
@javax.annotation.Generated(
    value = "by gRPC proto compiler (version 1.58.0)",
    comments = "Source: grpc.proto")
@io.grpc.stub.annotations.GrpcGenerated
public final class TriatlonServiceGrpc {

  private TriatlonServiceGrpc() {}

  public static final java.lang.String SERVICE_NAME = "triatlon.TriatlonService";

  // Static method descriptors that strictly reflect the proto.
  private static volatile io.grpc.MethodDescriptor<networking.grpc.LoginRequest,
      networking.grpc.LoginResponse> getLoginMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "Login",
      requestType = networking.grpc.LoginRequest.class,
      responseType = networking.grpc.LoginResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<networking.grpc.LoginRequest,
      networking.grpc.LoginResponse> getLoginMethod() {
    io.grpc.MethodDescriptor<networking.grpc.LoginRequest, networking.grpc.LoginResponse> getLoginMethod;
    if ((getLoginMethod = TriatlonServiceGrpc.getLoginMethod) == null) {
      synchronized (TriatlonServiceGrpc.class) {
        if ((getLoginMethod = TriatlonServiceGrpc.getLoginMethod) == null) {
          TriatlonServiceGrpc.getLoginMethod = getLoginMethod =
              io.grpc.MethodDescriptor.<networking.grpc.LoginRequest, networking.grpc.LoginResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "Login"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.LoginRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.LoginResponse.getDefaultInstance()))
              .setSchemaDescriptor(new TriatlonServiceMethodDescriptorSupplier("Login"))
              .build();
        }
      }
    }
    return getLoginMethod;
  }

  private static volatile io.grpc.MethodDescriptor<networking.grpc.LogoutRequest,
      networking.grpc.StatusResponse> getLogoutMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "Logout",
      requestType = networking.grpc.LogoutRequest.class,
      responseType = networking.grpc.StatusResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<networking.grpc.LogoutRequest,
      networking.grpc.StatusResponse> getLogoutMethod() {
    io.grpc.MethodDescriptor<networking.grpc.LogoutRequest, networking.grpc.StatusResponse> getLogoutMethod;
    if ((getLogoutMethod = TriatlonServiceGrpc.getLogoutMethod) == null) {
      synchronized (TriatlonServiceGrpc.class) {
        if ((getLogoutMethod = TriatlonServiceGrpc.getLogoutMethod) == null) {
          TriatlonServiceGrpc.getLogoutMethod = getLogoutMethod =
              io.grpc.MethodDescriptor.<networking.grpc.LogoutRequest, networking.grpc.StatusResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "Logout"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.LogoutRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.StatusResponse.getDefaultInstance()))
              .setSchemaDescriptor(new TriatlonServiceMethodDescriptorSupplier("Logout"))
              .build();
        }
      }
    }
    return getLogoutMethod;
  }

  private static volatile io.grpc.MethodDescriptor<networking.grpc.EmptyRequest,
      networking.grpc.ParticipantsResponse> getGetParticipantsMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "GetParticipants",
      requestType = networking.grpc.EmptyRequest.class,
      responseType = networking.grpc.ParticipantsResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<networking.grpc.EmptyRequest,
      networking.grpc.ParticipantsResponse> getGetParticipantsMethod() {
    io.grpc.MethodDescriptor<networking.grpc.EmptyRequest, networking.grpc.ParticipantsResponse> getGetParticipantsMethod;
    if ((getGetParticipantsMethod = TriatlonServiceGrpc.getGetParticipantsMethod) == null) {
      synchronized (TriatlonServiceGrpc.class) {
        if ((getGetParticipantsMethod = TriatlonServiceGrpc.getGetParticipantsMethod) == null) {
          TriatlonServiceGrpc.getGetParticipantsMethod = getGetParticipantsMethod =
              io.grpc.MethodDescriptor.<networking.grpc.EmptyRequest, networking.grpc.ParticipantsResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "GetParticipants"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.EmptyRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.ParticipantsResponse.getDefaultInstance()))
              .setSchemaDescriptor(new TriatlonServiceMethodDescriptorSupplier("GetParticipants"))
              .build();
        }
      }
    }
    return getGetParticipantsMethod;
  }

  private static volatile io.grpc.MethodDescriptor<networking.grpc.ProbaRequest,
      networking.grpc.PunctajeResponse> getGetRezultateByProbaMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "GetRezultateByProba",
      requestType = networking.grpc.ProbaRequest.class,
      responseType = networking.grpc.PunctajeResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<networking.grpc.ProbaRequest,
      networking.grpc.PunctajeResponse> getGetRezultateByProbaMethod() {
    io.grpc.MethodDescriptor<networking.grpc.ProbaRequest, networking.grpc.PunctajeResponse> getGetRezultateByProbaMethod;
    if ((getGetRezultateByProbaMethod = TriatlonServiceGrpc.getGetRezultateByProbaMethod) == null) {
      synchronized (TriatlonServiceGrpc.class) {
        if ((getGetRezultateByProbaMethod = TriatlonServiceGrpc.getGetRezultateByProbaMethod) == null) {
          TriatlonServiceGrpc.getGetRezultateByProbaMethod = getGetRezultateByProbaMethod =
              io.grpc.MethodDescriptor.<networking.grpc.ProbaRequest, networking.grpc.PunctajeResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "GetRezultateByProba"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.ProbaRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.PunctajeResponse.getDefaultInstance()))
              .setSchemaDescriptor(new TriatlonServiceMethodDescriptorSupplier("GetRezultateByProba"))
              .build();
        }
      }
    }
    return getGetRezultateByProbaMethod;
  }

  private static volatile io.grpc.MethodDescriptor<networking.grpc.EmptyRequest,
      networking.grpc.PunctajeResponse> getGetAllRezultateMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "GetAllRezultate",
      requestType = networking.grpc.EmptyRequest.class,
      responseType = networking.grpc.PunctajeResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<networking.grpc.EmptyRequest,
      networking.grpc.PunctajeResponse> getGetAllRezultateMethod() {
    io.grpc.MethodDescriptor<networking.grpc.EmptyRequest, networking.grpc.PunctajeResponse> getGetAllRezultateMethod;
    if ((getGetAllRezultateMethod = TriatlonServiceGrpc.getGetAllRezultateMethod) == null) {
      synchronized (TriatlonServiceGrpc.class) {
        if ((getGetAllRezultateMethod = TriatlonServiceGrpc.getGetAllRezultateMethod) == null) {
          TriatlonServiceGrpc.getGetAllRezultateMethod = getGetAllRezultateMethod =
              io.grpc.MethodDescriptor.<networking.grpc.EmptyRequest, networking.grpc.PunctajeResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "GetAllRezultate"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.EmptyRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.PunctajeResponse.getDefaultInstance()))
              .setSchemaDescriptor(new TriatlonServiceMethodDescriptorSupplier("GetAllRezultate"))
              .build();
        }
      }
    }
    return getGetAllRezultateMethod;
  }

  private static volatile io.grpc.MethodDescriptor<networking.grpc.RezultatRequest,
      networking.grpc.StatusResponse> getAddRezultatMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "AddRezultat",
      requestType = networking.grpc.RezultatRequest.class,
      responseType = networking.grpc.StatusResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<networking.grpc.RezultatRequest,
      networking.grpc.StatusResponse> getAddRezultatMethod() {
    io.grpc.MethodDescriptor<networking.grpc.RezultatRequest, networking.grpc.StatusResponse> getAddRezultatMethod;
    if ((getAddRezultatMethod = TriatlonServiceGrpc.getAddRezultatMethod) == null) {
      synchronized (TriatlonServiceGrpc.class) {
        if ((getAddRezultatMethod = TriatlonServiceGrpc.getAddRezultatMethod) == null) {
          TriatlonServiceGrpc.getAddRezultatMethod = getAddRezultatMethod =
              io.grpc.MethodDescriptor.<networking.grpc.RezultatRequest, networking.grpc.StatusResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "AddRezultat"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.RezultatRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.StatusResponse.getDefaultInstance()))
              .setSchemaDescriptor(new TriatlonServiceMethodDescriptorSupplier("AddRezultat"))
              .build();
        }
      }
    }
    return getAddRezultatMethod;
  }

  private static volatile io.grpc.MethodDescriptor<networking.grpc.SubscribeRequest,
      networking.grpc.NotificationResponse> getSubscribeToNotificationsMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "SubscribeToNotifications",
      requestType = networking.grpc.SubscribeRequest.class,
      responseType = networking.grpc.NotificationResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.SERVER_STREAMING)
  public static io.grpc.MethodDescriptor<networking.grpc.SubscribeRequest,
      networking.grpc.NotificationResponse> getSubscribeToNotificationsMethod() {
    io.grpc.MethodDescriptor<networking.grpc.SubscribeRequest, networking.grpc.NotificationResponse> getSubscribeToNotificationsMethod;
    if ((getSubscribeToNotificationsMethod = TriatlonServiceGrpc.getSubscribeToNotificationsMethod) == null) {
      synchronized (TriatlonServiceGrpc.class) {
        if ((getSubscribeToNotificationsMethod = TriatlonServiceGrpc.getSubscribeToNotificationsMethod) == null) {
          TriatlonServiceGrpc.getSubscribeToNotificationsMethod = getSubscribeToNotificationsMethod =
              io.grpc.MethodDescriptor.<networking.grpc.SubscribeRequest, networking.grpc.NotificationResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.SERVER_STREAMING)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "SubscribeToNotifications"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.SubscribeRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  networking.grpc.NotificationResponse.getDefaultInstance()))
              .setSchemaDescriptor(new TriatlonServiceMethodDescriptorSupplier("SubscribeToNotifications"))
              .build();
        }
      }
    }
    return getSubscribeToNotificationsMethod;
  }

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static TriatlonServiceStub newStub(io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<TriatlonServiceStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<TriatlonServiceStub>() {
        @java.lang.Override
        public TriatlonServiceStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new TriatlonServiceStub(channel, callOptions);
        }
      };
    return TriatlonServiceStub.newStub(factory, channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static TriatlonServiceBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<TriatlonServiceBlockingStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<TriatlonServiceBlockingStub>() {
        @java.lang.Override
        public TriatlonServiceBlockingStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new TriatlonServiceBlockingStub(channel, callOptions);
        }
      };
    return TriatlonServiceBlockingStub.newStub(factory, channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary calls on the service
   */
  public static TriatlonServiceFutureStub newFutureStub(
      io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<TriatlonServiceFutureStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<TriatlonServiceFutureStub>() {
        @java.lang.Override
        public TriatlonServiceFutureStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new TriatlonServiceFutureStub(channel, callOptions);
        }
      };
    return TriatlonServiceFutureStub.newStub(factory, channel);
  }

  /**
   * <pre>
   * Service definition
   * </pre>
   */
  public interface AsyncService {

    /**
     * <pre>
     * Authentication
     * </pre>
     */
    default void login(networking.grpc.LoginRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.LoginResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getLoginMethod(), responseObserver);
    }

    /**
     */
    default void logout(networking.grpc.LogoutRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.StatusResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getLogoutMethod(), responseObserver);
    }

    /**
     * <pre>
     * Participants
     * </pre>
     */
    default void getParticipants(networking.grpc.EmptyRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.ParticipantsResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getGetParticipantsMethod(), responseObserver);
    }

    /**
     * <pre>
     * Results
     * </pre>
     */
    default void getRezultateByProba(networking.grpc.ProbaRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.PunctajeResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getGetRezultateByProbaMethod(), responseObserver);
    }

    /**
     */
    default void getAllRezultate(networking.grpc.EmptyRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.PunctajeResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getGetAllRezultateMethod(), responseObserver);
    }

    /**
     */
    default void addRezultat(networking.grpc.RezultatRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.StatusResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getAddRezultatMethod(), responseObserver);
    }

    /**
     * <pre>
     * Notifications (server streaming)
     * </pre>
     */
    default void subscribeToNotifications(networking.grpc.SubscribeRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.NotificationResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getSubscribeToNotificationsMethod(), responseObserver);
    }
  }

  /**
   * Base class for the server implementation of the service TriatlonService.
   * <pre>
   * Service definition
   * </pre>
   */
  public static abstract class TriatlonServiceImplBase
      implements io.grpc.BindableService, AsyncService {

    @java.lang.Override public final io.grpc.ServerServiceDefinition bindService() {
      return TriatlonServiceGrpc.bindService(this);
    }
  }

  /**
   * A stub to allow clients to do asynchronous rpc calls to service TriatlonService.
   * <pre>
   * Service definition
   * </pre>
   */
  public static final class TriatlonServiceStub
      extends io.grpc.stub.AbstractAsyncStub<TriatlonServiceStub> {
    private TriatlonServiceStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected TriatlonServiceStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new TriatlonServiceStub(channel, callOptions);
    }

    /**
     * <pre>
     * Authentication
     * </pre>
     */
    public void login(networking.grpc.LoginRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.LoginResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getLoginMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void logout(networking.grpc.LogoutRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.StatusResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getLogoutMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     * <pre>
     * Participants
     * </pre>
     */
    public void getParticipants(networking.grpc.EmptyRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.ParticipantsResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getGetParticipantsMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     * <pre>
     * Results
     * </pre>
     */
    public void getRezultateByProba(networking.grpc.ProbaRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.PunctajeResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getGetRezultateByProbaMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void getAllRezultate(networking.grpc.EmptyRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.PunctajeResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getGetAllRezultateMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void addRezultat(networking.grpc.RezultatRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.StatusResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getAddRezultatMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     * <pre>
     * Notifications (server streaming)
     * </pre>
     */
    public void subscribeToNotifications(networking.grpc.SubscribeRequest request,
        io.grpc.stub.StreamObserver<networking.grpc.NotificationResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncServerStreamingCall(
          getChannel().newCall(getSubscribeToNotificationsMethod(), getCallOptions()), request, responseObserver);
    }
  }

  /**
   * A stub to allow clients to do synchronous rpc calls to service TriatlonService.
   * <pre>
   * Service definition
   * </pre>
   */
  public static final class TriatlonServiceBlockingStub
      extends io.grpc.stub.AbstractBlockingStub<TriatlonServiceBlockingStub> {
    private TriatlonServiceBlockingStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected TriatlonServiceBlockingStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new TriatlonServiceBlockingStub(channel, callOptions);
    }

    /**
     * <pre>
     * Authentication
     * </pre>
     */
    public networking.grpc.LoginResponse login(networking.grpc.LoginRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getLoginMethod(), getCallOptions(), request);
    }

    /**
     */
    public networking.grpc.StatusResponse logout(networking.grpc.LogoutRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getLogoutMethod(), getCallOptions(), request);
    }

    /**
     * <pre>
     * Participants
     * </pre>
     */
    public networking.grpc.ParticipantsResponse getParticipants(networking.grpc.EmptyRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getGetParticipantsMethod(), getCallOptions(), request);
    }

    /**
     * <pre>
     * Results
     * </pre>
     */
    public networking.grpc.PunctajeResponse getRezultateByProba(networking.grpc.ProbaRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getGetRezultateByProbaMethod(), getCallOptions(), request);
    }

    /**
     */
    public networking.grpc.PunctajeResponse getAllRezultate(networking.grpc.EmptyRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getGetAllRezultateMethod(), getCallOptions(), request);
    }

    /**
     */
    public networking.grpc.StatusResponse addRezultat(networking.grpc.RezultatRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getAddRezultatMethod(), getCallOptions(), request);
    }

    /**
     * <pre>
     * Notifications (server streaming)
     * </pre>
     */
    public java.util.Iterator<networking.grpc.NotificationResponse> subscribeToNotifications(
        networking.grpc.SubscribeRequest request) {
      return io.grpc.stub.ClientCalls.blockingServerStreamingCall(
          getChannel(), getSubscribeToNotificationsMethod(), getCallOptions(), request);
    }
  }

  /**
   * A stub to allow clients to do ListenableFuture-style rpc calls to service TriatlonService.
   * <pre>
   * Service definition
   * </pre>
   */
  public static final class TriatlonServiceFutureStub
      extends io.grpc.stub.AbstractFutureStub<TriatlonServiceFutureStub> {
    private TriatlonServiceFutureStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected TriatlonServiceFutureStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new TriatlonServiceFutureStub(channel, callOptions);
    }

    /**
     * <pre>
     * Authentication
     * </pre>
     */
    public com.google.common.util.concurrent.ListenableFuture<networking.grpc.LoginResponse> login(
        networking.grpc.LoginRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getLoginMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<networking.grpc.StatusResponse> logout(
        networking.grpc.LogoutRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getLogoutMethod(), getCallOptions()), request);
    }

    /**
     * <pre>
     * Participants
     * </pre>
     */
    public com.google.common.util.concurrent.ListenableFuture<networking.grpc.ParticipantsResponse> getParticipants(
        networking.grpc.EmptyRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getGetParticipantsMethod(), getCallOptions()), request);
    }

    /**
     * <pre>
     * Results
     * </pre>
     */
    public com.google.common.util.concurrent.ListenableFuture<networking.grpc.PunctajeResponse> getRezultateByProba(
        networking.grpc.ProbaRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getGetRezultateByProbaMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<networking.grpc.PunctajeResponse> getAllRezultate(
        networking.grpc.EmptyRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getGetAllRezultateMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<networking.grpc.StatusResponse> addRezultat(
        networking.grpc.RezultatRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getAddRezultatMethod(), getCallOptions()), request);
    }
  }

  private static final int METHODID_LOGIN = 0;
  private static final int METHODID_LOGOUT = 1;
  private static final int METHODID_GET_PARTICIPANTS = 2;
  private static final int METHODID_GET_REZULTATE_BY_PROBA = 3;
  private static final int METHODID_GET_ALL_REZULTATE = 4;
  private static final int METHODID_ADD_REZULTAT = 5;
  private static final int METHODID_SUBSCRIBE_TO_NOTIFICATIONS = 6;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final AsyncService serviceImpl;
    private final int methodId;

    MethodHandlers(AsyncService serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_LOGIN:
          serviceImpl.login((networking.grpc.LoginRequest) request,
              (io.grpc.stub.StreamObserver<networking.grpc.LoginResponse>) responseObserver);
          break;
        case METHODID_LOGOUT:
          serviceImpl.logout((networking.grpc.LogoutRequest) request,
              (io.grpc.stub.StreamObserver<networking.grpc.StatusResponse>) responseObserver);
          break;
        case METHODID_GET_PARTICIPANTS:
          serviceImpl.getParticipants((networking.grpc.EmptyRequest) request,
              (io.grpc.stub.StreamObserver<networking.grpc.ParticipantsResponse>) responseObserver);
          break;
        case METHODID_GET_REZULTATE_BY_PROBA:
          serviceImpl.getRezultateByProba((networking.grpc.ProbaRequest) request,
              (io.grpc.stub.StreamObserver<networking.grpc.PunctajeResponse>) responseObserver);
          break;
        case METHODID_GET_ALL_REZULTATE:
          serviceImpl.getAllRezultate((networking.grpc.EmptyRequest) request,
              (io.grpc.stub.StreamObserver<networking.grpc.PunctajeResponse>) responseObserver);
          break;
        case METHODID_ADD_REZULTAT:
          serviceImpl.addRezultat((networking.grpc.RezultatRequest) request,
              (io.grpc.stub.StreamObserver<networking.grpc.StatusResponse>) responseObserver);
          break;
        case METHODID_SUBSCRIBE_TO_NOTIFICATIONS:
          serviceImpl.subscribeToNotifications((networking.grpc.SubscribeRequest) request,
              (io.grpc.stub.StreamObserver<networking.grpc.NotificationResponse>) responseObserver);
          break;
        default:
          throw new AssertionError();
      }
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public io.grpc.stub.StreamObserver<Req> invoke(
        io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        default:
          throw new AssertionError();
      }
    }
  }

  public static final io.grpc.ServerServiceDefinition bindService(AsyncService service) {
    return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
        .addMethod(
          getLoginMethod(),
          io.grpc.stub.ServerCalls.asyncUnaryCall(
            new MethodHandlers<
              networking.grpc.LoginRequest,
              networking.grpc.LoginResponse>(
                service, METHODID_LOGIN)))
        .addMethod(
          getLogoutMethod(),
          io.grpc.stub.ServerCalls.asyncUnaryCall(
            new MethodHandlers<
              networking.grpc.LogoutRequest,
              networking.grpc.StatusResponse>(
                service, METHODID_LOGOUT)))
        .addMethod(
          getGetParticipantsMethod(),
          io.grpc.stub.ServerCalls.asyncUnaryCall(
            new MethodHandlers<
              networking.grpc.EmptyRequest,
              networking.grpc.ParticipantsResponse>(
                service, METHODID_GET_PARTICIPANTS)))
        .addMethod(
          getGetRezultateByProbaMethod(),
          io.grpc.stub.ServerCalls.asyncUnaryCall(
            new MethodHandlers<
              networking.grpc.ProbaRequest,
              networking.grpc.PunctajeResponse>(
                service, METHODID_GET_REZULTATE_BY_PROBA)))
        .addMethod(
          getGetAllRezultateMethod(),
          io.grpc.stub.ServerCalls.asyncUnaryCall(
            new MethodHandlers<
              networking.grpc.EmptyRequest,
              networking.grpc.PunctajeResponse>(
                service, METHODID_GET_ALL_REZULTATE)))
        .addMethod(
          getAddRezultatMethod(),
          io.grpc.stub.ServerCalls.asyncUnaryCall(
            new MethodHandlers<
              networking.grpc.RezultatRequest,
              networking.grpc.StatusResponse>(
                service, METHODID_ADD_REZULTAT)))
        .addMethod(
          getSubscribeToNotificationsMethod(),
          io.grpc.stub.ServerCalls.asyncServerStreamingCall(
            new MethodHandlers<
              networking.grpc.SubscribeRequest,
              networking.grpc.NotificationResponse>(
                service, METHODID_SUBSCRIBE_TO_NOTIFICATIONS)))
        .build();
  }

  private static abstract class TriatlonServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoFileDescriptorSupplier, io.grpc.protobuf.ProtoServiceDescriptorSupplier {
    TriatlonServiceBaseDescriptorSupplier() {}

    @java.lang.Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return networking.grpc.Grpc.getDescriptor();
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.ServiceDescriptor getServiceDescriptor() {
      return getFileDescriptor().findServiceByName("TriatlonService");
    }
  }

  private static final class TriatlonServiceFileDescriptorSupplier
      extends TriatlonServiceBaseDescriptorSupplier {
    TriatlonServiceFileDescriptorSupplier() {}
  }

  private static final class TriatlonServiceMethodDescriptorSupplier
      extends TriatlonServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoMethodDescriptorSupplier {
    private final java.lang.String methodName;

    TriatlonServiceMethodDescriptorSupplier(java.lang.String methodName) {
      this.methodName = methodName;
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.MethodDescriptor getMethodDescriptor() {
      return getServiceDescriptor().findMethodByName(methodName);
    }
  }

  private static volatile io.grpc.ServiceDescriptor serviceDescriptor;

  public static io.grpc.ServiceDescriptor getServiceDescriptor() {
    io.grpc.ServiceDescriptor result = serviceDescriptor;
    if (result == null) {
      synchronized (TriatlonServiceGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new TriatlonServiceFileDescriptorSupplier())
              .addMethod(getLoginMethod())
              .addMethod(getLogoutMethod())
              .addMethod(getGetParticipantsMethod())
              .addMethod(getGetRezultateByProbaMethod())
              .addMethod(getGetAllRezultateMethod())
              .addMethod(getAddRezultatMethod())
              .addMethod(getSubscribeToNotificationsMethod())
              .build();
        }
      }
    }
    return result;
  }
}
