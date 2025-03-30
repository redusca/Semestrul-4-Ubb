package ro.mpp2024.controllers;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import ro.mpp2024.MainApp;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.services.ArbitruService;
import ro.mpp2024.utils.Encryption;

import java.io.IOException;

public class LoginController {

    @FXML
    private TextField usernameField;
    @FXML
    private PasswordField passwordField;

    private ArbitruService service;

    public void setService(ArbitruService arbitruService) {
        service = arbitruService;
    }

    public void login(ActionEvent actionEvent) {
        try{
            System.out.println(usernameField.getText());
            Arbitru arbitru = service.login(usernameField.getText(), Encryption.code(passwordField.getText()));
            if(arbitru.getId() != -1) {
                System.out.println("Login successful");
                changeToMainWindow(arbitru);
            }
            else{
                throw new Exception("Username or password Wrong");
            }
        }
        catch (Exception e){
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error");
            alert.setHeaderText("Error");
            alert.setContentText(e.toString());
            System.out.println((e.toString()));
            alert.showAndWait();
        }
    }

    private void changeToMainWindow(Arbitru arbitru) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(MainApp.class.getResource("/fxml/main.fxml"));
        Stage stage = new Stage();
        AnchorPane mainLayout = fxmlLoader.load();
        stage.setScene(new Scene(mainLayout));

        MainController userController = fxmlLoader.getController();
        userController.setService(MainApp.getRezultatService(),arbitru);

        stage.setTitle(arbitru.getUsername() + " Window");
        stage.setResizable(false);
        stage.show();

        ((Stage) usernameField.getScene().getWindow()).close();
    }
}
