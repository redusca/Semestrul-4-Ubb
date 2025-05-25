using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace networking.DTO
{
    [Serializable]
    internal class PunctajParticipantDTO 
    {
        [JsonPropertyName("idParticipant")]
        public long idParticipant { get; set; }

        [JsonPropertyName("nume")]
        public string nume { get; set; }

        [JsonPropertyName("prenume")]
        public string prenume { get; set; }

        [JsonPropertyName("varsta")]
        public int varsta { get; set; }

        [JsonPropertyName("punctaj")]
        public long punctaj { get; set; }

        public PunctajParticipantDTO() { }

        public PunctajParticipantDTO(long idParticipant, String nume, String prenume, int varsta, long punctaj)
        {
            this.idParticipant = idParticipant;
            this.nume = nume;
            this.prenume = prenume;
            this.varsta = varsta;
            this.punctaj = punctaj;
        }

        public override string ToString()
        {
            return $"ParticipantDTO [idParticipant={idParticipant}, nume={nume}, prenume={prenume}, varsta={varsta}, punctaj={punctaj}]";
        }
    }
}
