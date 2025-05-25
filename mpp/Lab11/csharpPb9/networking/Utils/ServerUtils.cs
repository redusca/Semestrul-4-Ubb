using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using services.services;
using networking.protocol;
using networking.DTO;
using System.Text.Json;

namespace networking.Utils;

public abstract class AbstractServer
{
    private TcpListener server;
    private String host;
    private int port;

    private static readonly ILog log = LogManager.GetLogger(typeof(AbstractServer));
    public AbstractServer(String host, int port)
    {
        this.host = host;
        this.port = port;
    }
    public void Start()
    {
        IPAddress adr = IPAddress.Parse(host);
        IPEndPoint ep = new IPEndPoint(adr, port);
        server = new TcpListener(ep);
        server.Start();
        while (true)
        {
            log.Debug("Waiting for clients ...");
            TcpClient client = server.AcceptTcpClient();
            log.Debug("Client connected ...");
            processRequest(client);
        }
    }

    public abstract void processRequest(TcpClient client);

}


public abstract class ConcurrentServer : AbstractServer
{

    public ConcurrentServer(string host, int port) : base(host, port)
    { }

    public override void processRequest(TcpClient client)
    {

        Thread t = createWorker(client);
        t.Start();

    }

    protected abstract Thread createWorker(TcpClient client);

}

public class JsonServer : ConcurrentServer
{
    private IService server;
    private ClientWorker worker;
    private static readonly ILog log = LogManager.GetLogger(typeof(JsonServer));
    public JsonServer(string host, int port, IService server) : base(host, port)
    {
        this.server = server;
        log.Debug("Creating JsonChatServer...");

        // Create a list of PunctajParticipantDTO
        var punctaje = new PunctajParticipantDTO[]
        {
            new PunctajParticipantDTO(2, "Ionescu", "Maria", 30, 350),
            new PunctajParticipantDTO(28, "Lupu", "George", 33, 340)
        };

        // Create a Response object
        var response = new Response
        {
            typeCsharp = ResponseType.REZULTATE,
            errormessage = null,
            user = null,
            rezultat = null,
            punctaje = punctaje,
            participanti = null
        };

        // Serialize the Response object to JSON
        string jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine("Serialized Response:");
        Console.WriteLine(jsonResponse);

        // Deserialize the JSON back to a Response object
        var deserializedResponse = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine("\nDeserialized Response:");
        Console.WriteLine(deserializedResponse);

        // Check the contents of the deserializedResponse
        if (deserializedResponse.punctaje != null)
        {
            foreach (var p in deserializedResponse.punctaje)
            {
                Console.WriteLine(p);
            }
        }
        else
        {
            Console.WriteLine("Punctaje is null or empty.");
        }
    }
    protected override Thread createWorker(TcpClient client)
    {
        worker = new ClientWorker(server, client);
        return new Thread(worker.run);
    }
}

