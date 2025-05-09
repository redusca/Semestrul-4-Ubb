using networking.DTO;
using System.Text.Json.Serialization;


namespace networking.jsonProtocol.responseAndrequest
{
    public class Response
    {
        [JsonPropertyName("type")]
        public responseType type { get; set; }
        [JsonPropertyName("errorMessage")]
        public string errorMessage { get; set; }
        [JsonPropertyName("user")]
        public UserDTO user { get; set; }
        [JsonPropertyName("bugDTOs")]
        public BugDTO[] bugDTOs { get; set; }

        public Response() { }

        public override string ToString()
        {
            //var bugsStr = string.Join(", ", bugDTOs.Select(b => b.ToString()));
            return $"Response [type = {type}, errormMssage = {errorMessage}, User = {user}, bugDTOs = test]";
        }
    }
}
