
using log4net;
using SQLitePCL;
using System.Configuration;
using System.Text.RegularExpressions;
public class ValidatorArbitru
{
    public static void ValidateArbitru(DataSets arbitru, Proba proba_asociata, Arbitru username)
    {
        string error = "";

        if (arbitru.Nume == null || arbitru.Nume.Length == 0)
            error += "Nume is empty.\n";

        if (arbitru.Username == null || arbitru.Username.Length == 0)
            error += "Username is empty.\n";

        if (username != null)
            error += "Username already exists.\n";

        error += ValidatePassword(arbitru.Password);
        error += ValidateProbaAsociata(proba_asociata);

        if (error.Length > 0)
            throw new ArgumentException(error);
    }

    private static string ValidatePassword(string password)
    {
        string er = "";

        if (string.IsNullOrEmpty(password) || password.Length < 8)
            er += "Password is too short.\n";

        if (!Regex.IsMatch(password, @"[a-z]"))
            er += "Password must contain at least one lowercasse letter\n.";

        if (!Regex.IsMatch(password, @"[A-Z]"))
            er += "Password must contain at least one uppercase letter.\n";

        if (!Regex.IsMatch(password, @"\d"))
            er += "Password must contain at least one number.\n";

        return er;
    }

    private static string ValidateProbaAsociata(Proba proba)
    {
        string error = "";

        if (proba == null)
            return "Proba asociata is null.\n";

        if (proba.Id_arbitru != -1)
            error += "Proba asociata has already an arbitru.\n";

        return error;
    }
}

public class ValidatorProbaAsociata
{
    public static void ValidateProba(DataSetProba proba)
    {
        string error = "";
        if (proba.Nume == null || proba.Nume.Length == 0)
            error += "Nume is empty.\n";

        if (!Enum.IsDefined(typeof(Categorie), proba.Categorie))
            error += "Categorie is invalid.\n";

        if (error.Length > 0)
            throw new ArgumentException(error);
    }
}

public class ServiceArbitru
{
    private IArbitruRepository arbitruRepository;
    private IProbaRepository probaRepository;

    private static readonly ILog log = LogManager.GetLogger("");

    public ServiceArbitru(IArbitruRepository arbitruRepository, IProbaRepository probaRepository)
    {
        log.Info("Creating Service");
        this.arbitruRepository = arbitruRepository;
        this.probaRepository = probaRepository;
    }

    public Arbitru FindArbitru(string username, string parola)
    {
        log.Info("Find Arbitru - Service");
        parola = pass.PasswordEncrypt.Encrypt(parola);
        Arbitru arbitru = arbitruRepository.FindByUser(username);
        if (arbitru != null)
            if (arbitru.Password == parola)
                return arbitru;
            else
                return new Arbitru(-1, "", "", "", "");

        return null;
    }

    public void SaveArbitru(DataSets dataArbitru)
    {
        log.Info("Validate Arbitru");
        ValidatorArbitru.ValidateArbitru(dataArbitru,
            probaRepository.FindOne(dataArbitru.Id_proba)
               , arbitruRepository.FindByUser(dataArbitru.Username));

        arbitruRepository.Save(new Arbitru(dataArbitru.Nume, dataArbitru.Username,
            pass.PasswordEncrypt.Encrypt(dataArbitru.Password), dataArbitru.Id_proba));
    }

    public void SaveArbitruPlusProba(DataSets dataArbitru, DataSetProba dataProba)
    {
        log.Info("Validate Proba");
        ValidatorProbaAsociata.ValidateProba(dataProba);

        log.Info("Validate Arbitru");
        ValidatorArbitru.ValidateArbitru(dataArbitru,
            new Proba("", "", 0L), arbitruRepository.FindByUser(dataArbitru.Username));

        arbitruRepository.Save(new Arbitru(dataArbitru.Nume, 
            dataArbitru.Username,
            pass.PasswordEncrypt.Encrypt(dataArbitru.Password), 
            dataProba.id));
        probaRepository.Save(new Proba(dataProba.id,
            dataProba.Nume,
            dataProba.Categorie,
            arbitruRepository.FindByUser(dataArbitru.Username).Id));
    }

    public void setProbaArbitru(string id, long arbitru)
    {
        log.Info("Set Arbitru for Proba");
        probaRepository.SetArbitruForProba(id, arbitru);
    }

    public List<Proba> getAllProbe()
    {
        log.Info("Get All Probe");
        return probaRepository.FindAll().ToList();
    }

    public void deleteArbitru(long id)
    {
        log.Info("Delete Arbitru - Service");
        arbitruRepository.Delete(id);
    }
}
