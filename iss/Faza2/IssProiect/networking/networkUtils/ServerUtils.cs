using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using services.Interfaces;
using networking.jsonProtocol;

namespace networking.Utils;

public abstract class AbstractServer
{
    private TcpListener server;
    private String host;
    private int port;
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
            Console.WriteLine("Waiting for clients...");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client accepted!");
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
    private IService service;
    private ClientWorker worker;
    public JsonServer(string host, int port, IService service) : base(host, port) { this.service = service; }

    protected override Thread createWorker(TcpClient client)
    {
        worker = new ClientWorker(service, client);
        return new Thread(worker.run);
    }
}

