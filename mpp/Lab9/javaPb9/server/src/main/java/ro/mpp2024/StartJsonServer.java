package ro.mpp2024;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.networkUtils.JsonConcurrentServer;
import ro.mpp2024.repository.ArbitruRepository;
import ro.mpp2024.repository.ParticipantRepository;
import ro.mpp2024.repository.ProbaRepository;
import ro.mpp2024.repository.RezultatRepository;
import ro.mpp2024.services.ServerService;
import ro.mpp2024.utils.JdbcUtils;

import java.io.File;
import java.util.Properties;

public class StartJsonServer {
    private static int defaultPort=55555;
    private static Logger logger = LogManager.getLogger(StartJsonServer.class);

    public static void main(String[] args) {
        Properties serverProps = new Properties();
        try{
            serverProps.load(StartJsonServer.class.getResourceAsStream("/server.properties"));
            logger.info("Server properties: {}", serverProps);
        } catch (Exception e) {
            logger.error("Cannot find server.properties {}", e);
            logger.debug("Looking for file in "+(new File(".")).getAbsolutePath());
            return;
        }
       
        JdbcUtils dbUtils = new JdbcUtils(serverProps);

        ArbitruRepository arbitruRepository = new ArbitruRepository(dbUtils);

        ProbaRepository probaRepository = new ProbaRepository(dbUtils);

        ParticipantRepository participantRepository = new ParticipantRepository(dbUtils);

        RezultatRepository rezultatRepository = new RezultatRepository(dbUtils);

        IService service = new ServerService(arbitruRepository,rezultatRepository,participantRepository,probaRepository);

        int port = defaultPort;
        try {
            port = Integer.parseInt(serverProps.getProperty("server.port"));
        } catch (NumberFormatException e) {
            logger.error("Wrong  Port Number"+e.getMessage());
            logger.debug("Using default port "+defaultPort);
        }
        logger.debug("Starting server on port: "+ port);
        chat.network.utils.AbstractServer server = new JsonConcurrentServer(port, service);

        try {
            server.start();
        } catch (Exception e) {
            logger.error("Error starting server: "+e.getMessage());
        }
    }
}
