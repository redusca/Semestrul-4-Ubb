using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace networking.DTO
{
    [Serializable]
    internal class RezultatDTO
    {
        [JsonPropertyName("idProba")]
        public String idProba { get; set; }
        [JsonPropertyName("idParticipant")]
        public long idParticipant { get; set; }
        [JsonPropertyName("numeParticipant")]
        public String numeParticipant { get;  set; }
        [JsonPropertyName("prenumeParticipant")]
        public String prenumeParticipant { get;  set; }
        [JsonPropertyName("puncte")]
        public long puncte { get; set; }


        public RezultatDTO() { }

        public RezultatDTO(String idProba, long idParticipant, String numeParticipant, String prenumeParticipant, long puncte)
        {
            this.idProba = idProba;
            this.idParticipant = idParticipant;
            this.numeParticipant = numeParticipant;
            this.prenumeParticipant = prenumeParticipant;
            this.puncte = puncte;
        }

        public override string ToString()
        {
            return $"RezultatDTO [idProba={idProba}, idParticipant={idParticipant}, numeParticipant={numeParticipant}" +
                $", prenumeParticipant={prenumeParticipant}, puncte={puncte}]";
        }
    }
}
