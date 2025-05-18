package ro.mpp2024.controllers;

import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import javafx.util.Pair;
import ro.mpp2024.IManageObserver;
import ro.mpp2024.IService;
import ro.mpp2024.ManageException;
import ro.mpp2024.StartJsonClient;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Map;
import java.util.stream.StreamSupport;

public class MainController implements IManageObserver {

    @FXML
    private Label usernameLabel;
    @FXML
    private TextField pointsTextField;
    @FXML
    private Button logoutButton;
    @FXML
    private TableView<Pair<Participant, Long>> tableView1;
    @FXML
    private TableColumn<Participant, String> participantColumn1;
    @FXML
    private TableColumn<Participant, Long> pointsColumn1;
    @FXML
    private TableView<Pair<Participant, Long>> tableView2;
    @FXML
    private TableColumn<Participant, String> participantColumn2;
    @FXML
    private TableColumn<Participant, Long> pointsColumn2;
    @FXML
    private ComboBox<Participant> participantComboBox;


    private Arbitru arbitru;
    private IService rezultatService;
    private Scene LoginScene;

    public void setService(IService service) {
        rezultatService = service;
    }

    public void setArbitru(Arbitru arbitru) {
        this.arbitru = arbitru;
    }

    public void initView() {
        participantColumn1.setSortable(false);
        pointsColumn1.setSortable(false);
        participantColumn2.setSortable(false);
        pointsColumn2.setSortable(false);

        usernameLabel.setText(arbitru.getNume() + " " + arbitru.getUsername() + " " + arbitru.getId_proba());

        populate();
    }

    private void populate() {
        participantComboBox.setItems(FXCollections.observableArrayList());
        tableView1.setItems(FXCollections.observableArrayList());
        tableView2.setItems(FXCollections.observableArrayList());

        Map<Participant, Long> participantsAlfabetic = rezultatService.getParticipantiAlfabetic();
        Map<Participant, Long> participantsPuncteDesc = rezultatService.getParticipantiPuncteDesc(arbitru.getId_proba());

        ObservableList<Pair<Participant, Long>> data1 = FXCollections.observableArrayList(
                participantsAlfabetic.entrySet().stream().map(e -> new Pair<>(e.getKey(), e.getValue())).toList());
        ObservableList<Pair<Participant, Long>> data2 = FXCollections.observableArrayList(
                participantsPuncteDesc.entrySet().stream().map(e -> new Pair<>(e.getKey(), e.getValue())).toList());

        participantColumn1.setCellValueFactory(new PropertyValueFactory<>("key"));
        pointsColumn1.setCellValueFactory(new PropertyValueFactory<>("value"));
        tableView1.setItems(data1);

        participantColumn2.setCellValueFactory(new PropertyValueFactory<>("key"));
        pointsColumn2.setCellValueFactory(new PropertyValueFactory<>("value"));
        tableView2.setItems(data2);

        Iterable<Participant> participants = rezultatService.getParticipanti();

        ArrayList<Participant> participantsList = new ArrayList<>(StreamSupport.stream(participants.spliterator(), false)
                .sorted((p1, p2) -> (p1.getNume() + p1.getPrenume()).compareToIgnoreCase(p2.getNume() + p2.getPrenume()))
                .filter(p -> !participantsPuncteDesc.containsKey(p))
                .toList());

        participantComboBox.setItems(FXCollections.observableArrayList(participantsList));
        tableView1.getItems().sort((p1, p2) -> p1.getKey().getNume().compareToIgnoreCase(p2.getKey().getNume()));
        tableView2.getItems().sort((p1, p2) -> p1.getKey().getNume().compareToIgnoreCase(p2.getKey().getNume()));
    }

    public void addRezultat(ActionEvent actionEvent) {
        Participant participant = participantComboBox.getValue();
        if (participant == null) {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error");
            alert.setHeaderText("Error");
            alert.setContentText("Select a participant");
            alert.showAndWait();
            return;
        }
        tableView1.sort();
        if (pointsTextField.getText().isEmpty() || !pointsTextField.getText().matches("^[1-9][0-9]+")) {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error");
            alert.setHeaderText("Error");
            alert.setContentText("Points not valid");
            alert.showAndWait();
            return;
        }

        tableView2.getItems().add(new Pair<>(participant, Long.parseLong(pointsTextField.getText())));
        tableView2.getItems().sort((p1, p2) -> p1.getKey().getNume().compareToIgnoreCase(p2.getKey().getNume()));

        rezultatService.addRezultat(participant.getId(),participant.getNume(),participant.getPrenume(),
                arbitru.getId_proba(), Long.parseLong(pointsTextField.getText()));

        participantComboBox.getSelectionModel().clearSelection();
        participantComboBox.setValue(null);
        participantComboBox.getItems().remove(participant);

        pointsTextField.clear();

    }

    public void logout(ActionEvent actionEvent) throws IOException {

        rezultatService.logout(arbitru,this);
        arbitru = null;

        Stage stage = ((Stage) logoutButton.getScene().getWindow());
        stage.close();

        FXMLLoader fxmlLoaderLogin = new FXMLLoader(StartJsonClient.class.getResource("/fxml/login.fxml"));
        AnchorPane root = fxmlLoaderLogin.load();
        LoginController loginController = fxmlLoaderLogin.getController();

        loginController.setService(rezultatService);
        loginController.setMainCtr(this);
        loginController.setScene(logoutButton.getScene());

        stage.setScene(LoginScene);
        stage.setTitle("Login");
        stage.setResizable(false);
        stage.show();
    }

    private void updateTable(Long id, String nume, String prenume, String proba, long puncte) {
        boolean found = tableView1.getItems().stream().anyMatch(pair -> pair.getKey().getId().equals(id));

        if (found) {
            Pair<Participant,Long> P = tableView1.getItems().stream()
                    .filter(pair -> pair.getKey().getId().equals(id)).toList().get(0);
            tableView1.getItems().remove(P);
            tableView1.getItems().add(new Pair<>(P.getKey(), puncte + P.getValue()));

        }
        else  {
            tableView1.getItems().add(new Pair<>(new Participant(id, nume, prenume, 0), puncte));
        }

        tableView1.getItems().sort((p1, p2) -> p1.getKey().getNume().compareToIgnoreCase(p2.getKey().getNume()));
    }

    public void RezultatAdded(Long id,String nume,String prenume, String proba, long puncte) throws ManageException {
       Platform.runLater(() -> {
           updateTable(id, nume, prenume, proba, puncte);
       });
    }

    public void setScene(Scene scene) {
        this.LoginScene = scene;
    }


}
