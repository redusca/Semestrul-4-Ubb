package ro.mpp2024.repository;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.model.Categorie;
import ro.mpp2024.model.Proba;
import ro.mpp2024.repository.interfaces.IProbaRepository;
import ro.mpp2024.utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class ProbaRepository implements IProbaRepository {

    private JdbcUtils dbUtils;

    private static final Logger logger= LogManager.getLogger();

    public ProbaRepository(JdbcUtils dbUtils) {
        logger.info("Initializing ProbaRepository with properties: {} ");
        this.dbUtils = dbUtils;
    }

    @Override
    public Proba createEntityFromResultSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("createEntityFromResultSet");
        String id = resultSet.getString("id");
        String nume = resultSet.getString("nume");
        Categorie categorie = Categorie.valueOf(resultSet.getString("categorie"));
        Proba proba = new Proba(id,nume,categorie);
        proba.setArbitru(resultSet.getLong("arbitru_probei"));
        logger.traceExit(proba);
        return proba;
    }

    @Override
    public Iterable<Proba> findAll() {
        logger.traceEntry("findAll");
        Connection con = dbUtils.getConnection();
        List<Proba> probe = new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Proba")) {
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    Proba proba = createEntityFromResultSet(result);
                    probe.add(proba);
                }
            }
        }catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(probe);
        return probe;
    }

    @Override
    public Proba findOne(String s) {
        logger.traceEntry("findOne");
        Connection con = dbUtils.getConnection();
        Proba proba = null;
        try(PreparedStatement preStmt=con.prepareStatement("select * from Proba where id=?")){
            preStmt.setString(1,s);
            try(ResultSet result=preStmt.executeQuery()){
                if(result.next()){
                    proba = createEntityFromResultSet(result);
                }
            }
        }catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(proba);
        return proba;
    }

    @Override
    public void save(Proba entity) {
        logger.traceEntry("save");
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("insert into Proba (id,nume,arbitru_probei,categorie) values (?,?,?,?)")){
            Arbitru arbitru= getArbitru(entity.getId());
            preStmt.setString(1,entity.getId());
            preStmt.setString(2,entity.getNume());
            preStmt.setLong( 3, arbitru.getId());
            preStmt.setString(4,entity.getCategorie().toString());
            int result = preStmt.executeUpdate();
        }catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB " + e);
        }
    }

    @Override
    public Proba delete(String s) {
        //  TODO: implement method when needed
        return null;
    }

    @Override
    public Proba update(String s, Proba new_entity) {
        //TODO: implement method when neeeded
        return null;
    }

    @Override
    public Arbitru getArbitru(String id) {
        logger.traceEntry("getArbitru");
        Arbitru arbitru = null;
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select * from Arbitru where proba_asociata=?")) {
            preStmt.setString(1, id);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    arbitru = new Arbitru(result.getLong("id"), result.getString("nume"),
                            result.getString("username"), result.getString("parola")
                            , id);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(arbitru);
        return arbitru;
    }
}

