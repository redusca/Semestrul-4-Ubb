using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking.DTO
{
    internal class DTOutils
    {
        public static Arbitru getFromDTO(UserDTO arbitruDTO)
        {
            return new Arbitru(arbitruDTO.id, arbitruDTO.nume, arbitruDTO.username, arbitruDTO.password, arbitruDTO.id_proba);
        }

        public static UserDTO getDTO(Arbitru arbitru)
        {
            return new UserDTO(arbitru.Id, arbitru.Nume, arbitru.Username, arbitru.Password, arbitru.Id_proba);
        }

        public static RezultatDTO getDTO(Rezultat rezultat)
        {
            return new RezultatDTO(rezultat.Proba.Id, rezultat.Participant.Id, rezultat.Participant.Nume, rezultat.Participant.Prenume, rezultat.Scor);
        }

        public static PunctajParticipantDTO[] getDTO(Dictionary<Participant,long> punctajParticipant)
        {
            PunctajParticipantDTO[] rez = new PunctajParticipantDTO[punctajParticipant.Count];
            int i = 0;
            foreach (var entry in punctajParticipant)
            {
                rez[i++] = new PunctajParticipantDTO(entry.Key.Id, entry.Key.Nume, entry.Key.Prenume, entry.Key.Varsta, entry.Value);
            }
            return rez;
        }

        public static PunctajParticipantDTO[] getDTO(IEnumerable<Participant> participanti)
        {
            PunctajParticipantDTO[] rez = new PunctajParticipantDTO[participanti.Count()];
            int i = 0;
            foreach (var entry in participanti)
            {
                rez[i++] = new PunctajParticipantDTO(entry.Id, entry.Nume, entry.Prenume, entry.Varsta, 0);
            }
            return rez;
        }

        public static Dictionary<Participant, long> getFromDTO(PunctajParticipantDTO[] participanti)
        {
            Dictionary<Participant, long> rez = new Dictionary<Participant, long>();
            foreach (var entry in participanti)
            {
                rez.Add(new Participant(entry.idParticipant, entry.nume, entry.prenume, entry.varsta), entry.punctaj);
            }
            return rez;
        }
    }
}
