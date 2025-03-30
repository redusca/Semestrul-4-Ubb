package ro.mpp2024;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import ro.mpp2024.controllers.LoginController;
import ro.mpp2024.repository.ArbitruRepository;
import ro.mpp2024.repository.ParticipantRepository;
import ro.mpp2024.repository.ProbaRepository;
import ro.mpp2024.repository.RezultatRepository;
import ro.mpp2024.services.ArbitruService;
import ro.mpp2024.services.RezultatService;
import ro.mpp2024.utils.JdbcUtils;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class MainApp extends Application {
    private static ArbitruService arbitruService;
    private static RezultatService rezultatService;

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage stage) throws Exception {
        Properties props = new Properties();
        try {
            props.load(new FileReader("./bd.config"));
            System.out.println(props);
        } catch (IOException e) {
            System.out.println("Cannot find bd.config " + e);
        }

        JdbcUtils dbUtils = new JdbcUtils(props);

        ArbitruRepository arbitruRepository = new ArbitruRepository(dbUtils);

        ProbaRepository probaRepository = new ProbaRepository(dbUtils);

        ParticipantRepository participantRepository = new ParticipantRepository(dbUtils);

        RezultatRepository rezultatRepository = new RezultatRepository(dbUtils);

        arbitruService = new ArbitruService(arbitruRepository);
        rezultatService = new RezultatService(rezultatRepository,participantRepository,probaRepository);

        rezultatRepository.listaParticiapntPuncteProbaDescrescator("c2").forEach((k,v)-> System.out.println(k+" "+v));

        initView(stage);
        stage.setTitle("Login");
        stage.setResizable(false);
        stage.show();
    }

    private void initView(Stage stage) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(MainApp.class.getResource("/fxml/login.fxml"));

        AnchorPane loginLayout = fxmlLoader.load();
        stage.setScene(new Scene(loginLayout));
        stage.setResizable(false);

        LoginController controller = fxmlLoader.getController();
        controller.setService(arbitruService);
    }

    public static ArbitruService getArbitruService(){
        return arbitruService;
    }

    public static RezultatService getRezultatService(){
        return rezultatService;
    }
}
