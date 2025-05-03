package ro.mpp2024.repository;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.model.Categorie;
import ro.mpp2024.model.Participant;
import ro.mpp2024.model.Proba;
import ro.mpp2024.model.Rezultat;
import ro.mpp2024.repository.interfaces.IRezultatRepository;
import ro.mpp2024.utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class RezultatRepository implements IRezultatRepository {

    private JdbcUtils dbUtils;

    private static final Logger logger= LogManager.getLogger();

    public RezultatRepository(JdbcUtils dbUtils) {
        logger.info("Initializing RezultatRepository with properties: {} ");
        this.dbUtils = dbUtils;
    }



    @Override
    public Rezultat createEntityFromResultSet(ResultSet resultSet) throws SQLException {
        logger.traceEntry("createEntityFromResultSet");
        Long id = resultSet.getLong("id");
        Long id_participant = resultSet.getLong("id_participant");
        String id_proba = resultSet.getString("id_proba");
        Long numarPuncte = resultSet.getLong("numar_puncte");

        Proba proba = null;
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Proba where id=?")){
            preStmt.setString(1,id_proba);
            try(ResultSet result=preStmt.executeQuery()){
                System.out.println(result);
                if(result.next()){
                    proba = new Proba(result.getString("id"),result.getString("nume")
                            , Categorie.valueOf(result.getString("categorie")));
                }
            }
        }catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB "+e);
        }
        Participant participant = null;
        try(PreparedStatement preStmt=con.prepareStatement("select * from Participant where id=?")){
            preStmt.setLong(1,id_participant);
            try(ResultSet result=preStmt.executeQuery()) {
                if (result.next()) {
                    participant = new Participant(result.getLong("id"), result.getString("nume")
                            , result.getString("prenume"), result.getInt("varsta"));
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }

        Rezultat rezultat = new Rezultat(id,participant,proba, numarPuncte);
        logger.traceExit(rezultat);
        return rezultat;
    }


    @Override
    public Iterable<Rezultat> findAll() {
        logger.traceEntry();
        Connection con=dbUtils.getConnection();
        List<Rezultat> rezultate = new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Rezultat")){
            try(ResultSet result=preStmt.executeQuery()){
                while(result.next()){
                    Rezultat rezultat = createEntityFromResultSet(result);
                    rezultate.add(rezultat);
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.out.println("Error DB "+ex);
        }
        logger.traceExit(rezultate);
        return rezultate;
    }

    @Override
    public Rezultat findOne(Long aLong) {
        logger.traceEntry("findOne");
        Connection con = dbUtils.getConnection();
        Rezultat rezultat = null;
        try(PreparedStatement preStmt=con.prepareStatement("select * from Rezultat where id=?")){
            preStmt.setLong(1,aLong);
            try(ResultSet result=preStmt.executeQuery()){
                if(result.next()){
                    rezultat = createEntityFromResultSet(result);
                }
            }
        }catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(rezultat);
        return rezultat;
    }

    @Override
    public void save(Rezultat entity) {
        logger.traceEntry("save");
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("insert into Rezultat(id_proba,id_participant,numar_puncte) values (?,?,?)")){
            preStmt.setLong(2,entity.getParticipant().getId());
            preStmt.setString(1,entity.getProba().getId());
            preStmt.setLong(3,entity.getNumar_puncte());
            int result = preStmt.executeUpdate();
        }catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();
    }

    @Override
    public Rezultat delete(Long aLong) {
        // TODO: implement method when neeeded
        return null;
    }

    @Override
    public Rezultat update(Long aLong, Rezultat new_entity) {
        // TODO: implement method when neeeded
        return null;
    }

    @Override
    public Map<Participant, Long> listaParticipantiPuncteAlfabetic() {
        logger.traceEntry("listaParticipantiPuncteAlfabetic");
        Map<Participant, Long> map = new LinkedHashMap<>();
        Connection con = dbUtils.getConnection();
        try (PreparedStatement presmt = con.prepareStatement("SELECT sum(r.numar_puncte) as total_punct, p.id, p.nume,p.prenume, p.varsta " +
                "FROM Rezultat as r Inner join Participant as p " +
                "On r.id_participant = p.id group by r.id_participant " +
                "order by lower(p.nume),lower(p.prenume)")){
            try(ResultSet result = presmt.executeQuery()){
                while(result.next()){
                    Participant participant = new Participant(result.getLong("id"),result.getString("nume"),result.getString("prenume"),result.getInt("varsta"));
                    map.put(participant,result.getLong("total_punct"));
                }
            }
        }
        catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceEntry("Terminat dictionar");
        return map;
    }

    @Override
    public Map<Participant, Long> listaParticiapntPuncteProbaDescrescator(String id_proba) {
        logger.traceEntry("listaParticiapntPuncteProbaDescrescator");
        Map<Participant, Long> map = new LinkedHashMap<>();
        Connection con = dbUtils.getConnection();
        try (PreparedStatement presmt = con.prepareStatement("SELECT SUM(r.numar_puncte) AS total_puncte, p.id, p.nume, p.prenume, p.varsta " +
                "FROM Rezultat AS r INNER JOIN Participant AS p ON r.id_participant = p.id " +
                "WHERE r.id_proba = ? GROUP BY r.id_participant ORDER BY total_puncte DESC;")){
            presmt.setString(1,id_proba);
            try(ResultSet result = presmt.executeQuery()){
                while(result.next()){
                    Participant participant = new Participant(result.getLong("id"),result.getString("nume"),result.getString("prenume"),result.getInt("varsta"));
                    map.put(participant,result.getLong("total_puncte"));
                }
            }
        }
        catch (SQLException e){
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceEntry("Terminat dictionar");
        return map;
    }

}
