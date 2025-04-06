package ro.mpp2024.networkUtils;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.IService;
import ro.mpp2024.protocol.ClientWorkerJson;

import java.net.Socket;

public class JsonConcurrentServer extends AbsConcurrentServer{
    private IService Server;
    private static Logger logger = LogManager.getLogger(JsonConcurrentServer.class);

    public JsonConcurrentServer(int port, IService Server) {
        super(port);
        this.Server = Server;
        logger.info("JsonConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        ClientWorkerJson worker=new ClientWorkerJson(Server, client);

        Thread tw=new Thread(worker);
        return tw;
    }
}