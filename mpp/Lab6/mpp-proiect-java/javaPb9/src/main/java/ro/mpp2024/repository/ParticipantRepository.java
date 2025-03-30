package ro.mpp2024.repository;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.model.Participant;
import ro.mpp2024.repository.interfaces.IParticipantRepository;
import ro.mpp2024.utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class ParticipantRepository implements IParticipantRepository {

    private JdbcUtils dbUtils;

    private static final Logger logger= LogManager.getLogger();


    public ParticipantRepository(JdbcUtils dbUtils) {
        logger.info("Initializing ParticipantRepository with properties: {} ");
        this.dbUtils = dbUtils;
    }

    @Override
    public Participant createEntityFromResultSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("createEntityFromResultSet");
        Long id = resultSet.getLong("id");
        String nume = resultSet.getString("nume");
        String prenume = resultSet.getString("prenume");
        int varsta = resultSet.getInt("varsta");
        Participant participant = new Participant(id,nume,prenume,varsta);
        logger.traceExit(participant);
        return participant;
    }

    @Override
    public Iterable<Participant> findAll() {
        logger.traceEntry();
        Connection con=dbUtils.getConnection();
        List<Participant> participants = new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Participant")){
            try(ResultSet result=preStmt.executeQuery()){
                while(result.next()){
                    Participant participant = createEntityFromResultSet(result);
                    participants.add(participant);
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.out.println("Error DB "+ex);
        }
        logger.traceExit(participants);
        return participants;
    }

    @Override
    public Participant findOne(Long id) {
        logger.traceEntry();
        Connection con=dbUtils.getConnection();
        Participant participant = null;
        try(PreparedStatement preStmt=con.prepareStatement("select * from Participant where id=?")) {
            preStmt.setLong(1, id);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    participant = createEntityFromResultSet(result);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(participant);
        return participant;
    }

    @Override
    public void save(Participant entity) {
        logger.traceEntry("save");
        Connection con=dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("insert into Participant(nume,prenume,varsta) values (?,?,?)")){
            preStmt.setString(1,entity.getNume());
            preStmt.setString(2,entity.getPrenume());
            preStmt.setInt(3,entity.getVarsta());
            int result=preStmt.executeUpdate();
        }catch (SQLException ex){
            logger.error(ex);
            System.out.println("Error DB "+ex);
        }
        logger.traceExit("S-a adaugat");
    }

    @Override
    public Participant delete(Long aLong) {
        //TODO: implement method when neeeded
        return null;
    }

    @Override
    public Participant update(Long aLong, Participant new_entity) {
        //TODO: implement method when neeeded
        return null;
    }
}
