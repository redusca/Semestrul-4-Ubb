package ro.mpp2024.controllers;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import javafx.util.Pair;
import ro.mpp2024.MainApp;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Participant;
import ro.mpp2024.services.RezultatService;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Map;
import java.util.stream.StreamSupport;

public class MainController {

    @FXML
    private Label usernameLabel;
    @FXML
    private TextField pointsTextField;
    @FXML
    private Button logoutButton;
    @FXML
    private TableView<Pair<Participant,Long>> tableView1;
    @FXML
    private TableColumn<Participant, String> participantColumn1;
    @FXML
    private TableColumn<Participant, Long> pointsColumn1;
    @FXML
    private TableView<Pair<Participant,Long>> tableView2;
    @FXML
    private TableColumn<Participant, String> participantColumn2;
    @FXML
    private TableColumn<Participant, Long> pointsColumn2;
    @FXML
    private ComboBox<Participant> participantComboBox;


    private Arbitru arbitru;
    private RezultatService rezultatService;

    public void setService(RezultatService service , Arbitru arbitru) {
        rezultatService = service;
        this.arbitru = arbitru;

        initView();
    }

    private void initView() {
        participantColumn1.setSortable(false);
        pointsColumn1.setSortable(false);
        participantColumn2.setSortable(false);
        pointsColumn2.setSortable(false);

        usernameLabel.setText(arbitru.getUsername() + " " + arbitru.getId());

        populate();
    }

    private void populate(){
        participantComboBox.setItems(FXCollections.observableArrayList());
        tableView1.setItems(FXCollections.observableArrayList());
        tableView2.setItems(FXCollections.observableArrayList());

        Map<Participant, Long> participantsAlfabetic = rezultatService.getParticipantiAlfabetic();
        Map<Participant, Long> participantsPuncteDesc = rezultatService.getParticipantiPuncteDesc(arbitru.getId_proba());

        ObservableList<Pair<Participant,Long>> data1 = FXCollections.observableArrayList(
                participantsAlfabetic.entrySet().stream().map(e -> new Pair<>(e.getKey(),e.getValue())).toList());
        ObservableList<Pair<Participant,Long>> data2 = FXCollections.observableArrayList(
                participantsPuncteDesc.entrySet().stream().map(e -> new Pair<>(e.getKey(),e.getValue())).toList());

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
    }

    public void addRezultat(ActionEvent actionEvent) {
        Participant participant = participantComboBox.getValue();
        if(participant == null){
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error");
            alert.setHeaderText("Error");
            alert.setContentText("Select a participant");
            alert.showAndWait();
            return;
        }

        if(pointsTextField.getText().isEmpty() || !pointsTextField.getText().matches("^[1-9][0-9]+")){
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error");
            alert.setHeaderText("Error");
            alert.setContentText("Points not valid");
            alert.showAndWait();
            return;
        }

        rezultatService.addRezultat(participant.getId(),arbitru.getId_proba(),Long.parseLong(pointsTextField.getText()));
        populate();
    }

    public void logout(ActionEvent actionEvent) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(MainApp.class.getResource("/fxml/login.fxml"));
        Stage stage = new Stage();
        AnchorPane mainLayout = fxmlLoader.load();
        stage.setScene(new Scene(mainLayout));

        LoginController login = fxmlLoader.getController();
        login.setService(MainApp.getArbitruService());

        stage.setTitle("Login");
        stage.setResizable(false);
        stage.show();

        ((Stage) logoutButton.getScene().getWindow()).close();
    }
}
