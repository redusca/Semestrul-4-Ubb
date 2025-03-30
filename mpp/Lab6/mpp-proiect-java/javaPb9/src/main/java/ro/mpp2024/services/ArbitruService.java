package ro.mpp2024.services;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.mpp2024.model.Arbitru;
import ro.mpp2024.repository.interfaces.IArbitruRepository;
import ro.mpp2024.utils.Encryption;

public class ArbitruService {
    IArbitruRepository arbitruRepository;

    private static final Logger logger= LogManager.getLogger();

    public ArbitruService(IArbitruRepository arbitruRepository) {
        this.arbitruRepository = arbitruRepository;
        logger.info("Initializing ArbitruService");

        arbitruRepository.findAll().forEach(arbitru -> logger.info(Encryption.code(arbitru.getPassword())));
    }

    public Arbitru login(String username, String password) {
        return arbitruRepository.findByUser(username, password);
    }
}
