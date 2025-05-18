using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services.services
{
    public interface IService
    {
        Arbitru login(string username, string password, IManageObserver client);

        void addRezultat(long id, string nume, string prenume, string idProba, long punctaj);

        Dictionary<Participant, long> getParticipantiAlfabtic();
        Dictionary<Participant, long> getParticipantiPuncteDesc(string idProba);

        IEnumerable<Participant> GetParticipants();

        void logutOut(Arbitru arbitru, IManageObserver client);
    }
}