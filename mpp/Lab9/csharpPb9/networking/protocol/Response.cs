using networking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace networking.protocol
{
    [Serializable]
    internal class Response
    {
        [JsonPropertyName("typeCsharp")]
        public ResponseType typeCsharp { get; set; }

        [JsonPropertyName("typeJava")]
        public string typeJava
        {
            get { return typeCsharp.ToString(); }
            set { typeCsharp = (ResponseType)Enum.Parse(typeof(ResponseType), value); }
        }

        [JsonPropertyName("errormessage")]
        public string errormessage { get; set; }
        [JsonPropertyName("user")]
        public UserDTO user { get; set; }
        [JsonPropertyName("rezultat")]
        public RezultatDTO rezultat { get; set; }
        [JsonPropertyName("punctaje")]
        public PunctajParticipantDTO[] punctaje { get; set; }
        [JsonPropertyName("participanti")]
        public PunctajParticipantDTO[] participanti { get; set; }
        public Response() { }
        public override string ToString()
        {
            var punctajeStr = punctaje != null ? string.Join(", ", punctaje.Select(p => p.ToString())) : "null";
            var participantiStr = participanti != null ? string.Join(", ", participanti.Select(p => p.ToString())) : "null";

            return $"Response [type={typeCsharp},typeJava={typeJava}, errormessage={errormessage}, user={user}, rezultat={rezultat}, punctaje=[{punctajeStr}], participanti=[{participantiStr}]]";
        }
    }
}
