package ro.mpp2024.controllers;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import ro.mpp2024.IService;
import ro.mpp2024.ManageException;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.utils.Encryption;

import java.io.IOException;

public class LoginController {

    @FXML
    private TextField usernameField;
    @FXML
    private PasswordField passwordField;

    private IService service;

    private MainController mainCtr;
    private Scene mainScene;

    public void setService(IService service) {
        this.service = service;
    }

    public void login(ActionEvent actionEvent) {
        try{
            System.out.println(usernameField.getText());
            Arbitru arbitru = service.login(usernameField.getText(), Encryption.code(passwordField.getText()),mainCtr);
            changeToMainWindow(arbitru);
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
        Stage stage = ((Stage) usernameField.getScene().getWindow());
        stage.close();

        stage.setTitle(arbitru.getUsername() + " Window");
        stage.setResizable(false);
        stage.setScene(mainScene);

        mainCtr.setScene(usernameField.getScene());
        mainCtr.setArbitru(arbitru);
        mainCtr.initView();

        stage.show();
    }

    public void setMainCtr(MainController mainCtr) {
        this.mainCtr = mainCtr;
    }


    public void setScene(Scene mainScene) {
        this.mainScene = mainScene;
    }
}
