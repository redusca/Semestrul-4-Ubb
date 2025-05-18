using log4net;
using services.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class ServiceServer : IService
    {
        private IArbitruRepository arbitruRepository;
        private IProbaRepository probaRepository;
        private IParticipantRepository participantRepository;
        private IRezultatRepository rezultatRepository;

        private readonly IDictionary<string, IManageObserver> loggedClients;

        private static readonly ILog log = LogManager.GetLogger(typeof(ServiceServer));

        public ServiceServer(IArbitruRepository arbitruRepository, IProbaRepository probaRepository, 
            IParticipantRepository participantRepository, IRezultatRepository rezultatRepository)
        {
            log.Info("Creating Service");
            this.arbitruRepository = arbitruRepository;
            this.probaRepository = probaRepository;
            this.participantRepository = participantRepository;
            this.rezultatRepository = rezultatRepository;
            loggedClients = new Dictionary<string, IManageObserver>();
        }
        public void addRezultat(long id, string nume, string prenume, string idProba, long punctaj)
        {
            log.Info("Adding rezultat");
            Participant participant = participantRepository.FindOne(id);
            Proba proba = probaRepository.FindOne(idProba);
            rezultatRepository.Save(new Rezultat(participant, proba, punctaj));

           // notifyUsers(id, nume, prenume, idProba, punctaj);
        }

        private void notifyUsers(long id, string nume, string prenume, string idProba, long punctaj)
        {
            log.Info("Notify users");
           
            foreach (var client in loggedClients)
            {
                try
                {
                    IManageObserver observer = client.Value;
                    observer.RezultatAdded(id, nume, prenume, idProba, punctaj);
                    return;
                }
                catch (Exception e)
                {
                    log.Error("Error notifying user", e);
                }
            } 
        }

        public Dictionary<Participant, long> getParticipantiAlfabtic()
        {
            return rezultatRepository.ParticipantiAlfabetic();
        }

        public Dictionary<Participant, long> getParticipantiPuncteDesc(string idProba)
        {
            return rezultatRepository.ParticipantScorDescrescator(idProba);
        }

        public IEnumerable<Participant> GetParticipants()
        {
            return participantRepository.FindAll();
        }

        public Arbitru login(string username, string password, IManageObserver client)
        {
            if (loggedClients.ContainsKey(username))
                throw new Exception("User already logged in!");
            
            Arbitru arbitru = arbitruRepository.FindByUser(username);
            if (arbitru == null)
                throw new Exception("Username doesn't exist!");
            if (password == arbitru.Parola)
                {
                    loggedClients.Add(username, client);
                    log.Info("Login user " + arbitru);
                    return arbitru;
                }
            else
            {
                throw new Exception("Wrong Password!");
            }
       
        }

        public void logutOut(Arbitru arbitru, IManageObserver client)
        {
            log.Info("Logout user " + arbitru);
            var arb = arbitruRepository.FindOne(arbitru.Id);

            loggedClients.Remove(arb.Username);
        }
    }
}
