package ro.mpp2024.repository;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Categorie;
import ro.mpp2024.model.Proba;
import ro.mpp2024.repository.interfaces.IArbitruRepository;
import ro.mpp2024.utils.Encryption;
import ro.mpp2024.utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class ArbitruRepository implements IArbitruRepository {

    private JdbcUtils dbUtils;

    private static final Logger logger= LogManager.getLogger();

    public ArbitruRepository(JdbcUtils dbUtils) {
        logger.info("Initializing IArbitruRepository with properties: {} ");
        this.dbUtils = dbUtils;
    }

    @Override
    public Arbitru createEntityFromResultSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("createEntityFromResultSet");
        Long id = resultSet.getLong("id");
        String nume = resultSet.getString("nume");
        String username = resultSet.getString("username");
        String password = resultSet.getString("parola");
        String proba_id = resultSet.getString("proba_asociata");
        Arbitru arbitru = new Arbitru(id,nume,username,password,proba_id);
        logger.traceExit(arbitru);
        return arbitru;
    }

    @Override
    public Iterable<Arbitru> findAll() {
        logger.traceEntry("findAll");
        Connection con = dbUtils.getConnection();
        List<Arbitru> arbitri = new ArrayList<>();
        try (PreparedStatement preStmt = con.prepareStatement("select * from Arbitru")) {
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {

                    Arbitru arbitru = createEntityFromResultSet(result);
                    arbitri.add(arbitru);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }

        logger.traceExit();
        return arbitri;
    }

    @Override
    public Arbitru findOne(Long id) {
        logger.traceEntry("findOne");
        Connection con = dbUtils.getConnection();
        Arbitru arbitru = null;
        try (PreparedStatement preStmt = con.prepareStatement("select * from Arbitru where id=?")) {
            preStmt.setLong(1, id);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    arbitru = createEntityFromResultSet(result);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();
        return arbitru;
    }

    @Override
    public void save(Arbitru entity) {
        logger.traceEntry("save");
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("insert into Arbitru (nume,username,parola,proba_asociata) values (?,?,?,?)")) {
            preStmt.setString(1, entity.getNume());
            preStmt.setString(2, entity.getUsername());
            preStmt.setString(3, entity.getPassword());
            preStmt.setString(4, entity.getId_proba());
            int result = preStmt.executeUpdate();
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("S-a adaugat");
    }

    @Override
    public Arbitru delete(Long aLong) {
        //TODO: implement method when neeeded
        return null;
    }

    @Override
    public Arbitru update(Long aLong, Arbitru new_entity) {
        //TODO: implement method when neeeded
        return null;
    }

    @Override
    public Arbitru findByUser(String username, String password) {
        Connection con = dbUtils.getConnection();
        Arbitru arbitru = null;
        try (PreparedStatement preStmt = con.prepareStatement("select * from Arbitru where username=? and parola=?")) {
            preStmt.setString(1, username);
            preStmt.setString(2, password);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    arbitru = createEntityFromResultSet(result);
                }
                else{
                    arbitru = new Arbitru(-1L,"","test","","");
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        return arbitru;
    }
}
