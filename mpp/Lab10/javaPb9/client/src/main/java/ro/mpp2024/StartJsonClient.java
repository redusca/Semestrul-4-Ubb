package ro.mpp2024;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.controllers.LoginController;
import ro.mpp2024.controllers.MainController;
import ro.mpp2024.protocol.ServicesJsonProxy;

import java.io.File;
import java.io.IOException;
import java.util.Properties;

public class StartJsonClient extends Application {

    private static int defaultChatPort = 55555;
    private static String defaultServer = "localhost";

    private static Logger logger = LogManager.getLogger(StartJsonClient.class);

    @Override
    public void start(Stage stage) throws Exception {

        logger.debug("Starting client application");
        Properties clientProps = new Properties();
        try {
            clientProps.load(StartJsonClient.class.getResourceAsStream("/client.properties"));
            logger.info("Client properties set {}" ,clientProps);
            System.out.println(clientProps);
        } catch (IOException e) {
            logger.error("Cannot find client.properties " + e);
            logger.debug("Looking for chatclient.properties in folder {}",(new File(".")).getAbsolutePath());
            return;
        }
        String serverIP = clientProps.getProperty("server.host", defaultServer);

        int chatServerPort = defaultChatPort;

        try{
            chatServerPort = Integer.parseInt(clientProps.getProperty("server.port"));
        } catch (NumberFormatException e) {
            logger.error("Wrong port number " + e.getMessage());
            logger.debug("Using default port " + defaultChatPort);
        }
        logger.info("Connecting to server {} on port {}", serverIP, chatServerPort);

       //IService server = new ServicesJsonProxy(serverIP, chatServerPort);
        IService server = new grpcProxy(serverIP, chatServerPort);

        initView(stage,server);

        stage.setTitle("Login");
        stage.setResizable(false);
        stage.show();
    }

    private void initView(Stage stage,IService server) throws IOException {
        FXMLLoader fxmlLoaderLogin = new FXMLLoader(StartJsonClient.class.getResource("/fxml/login.fxml"));
        Parent root = fxmlLoaderLogin.load();

        LoginController logincontroller = fxmlLoaderLogin.getController();
        logincontroller.setService(server);

        FXMLLoader fxmlLoaderMain = new FXMLLoader(StartJsonClient.class.getResource("/fxml/main.fxml"));
        Parent mroot = fxmlLoaderMain.load();

        MainController mainController = fxmlLoaderMain.getController();
        mainController.setService(server);

        Scene loginScene = new Scene(root);
        Scene mainScene = new Scene(mroot);

        logincontroller.setMainCtr(mainController);
        logincontroller.setScene(mainScene);

        stage.setScene(loginScene);
    }

}
