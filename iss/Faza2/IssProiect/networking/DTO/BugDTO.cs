using model;
using model.Enum;
using System.Text.Json.Serialization;

namespace networking.DTO
{
    public class BugDTO
    {
        [JsonPropertyName("bugNo")]
        public long bugNo { get; set; }
        [JsonPropertyName("description")]
        public string description { get; set; }
        [JsonPropertyName("status")]
        public BugStatus status { get; set; }

        public BugDTO() { }

        public BugDTO(long bugNo, string description, BugStatus status)
        {
            this.bugNo = bugNo;
            this.description = description;
            this.status = status;
        }

        public BugDTO(Bug bug)
        {
            this.bugNo = bug.BugNo;
            this.description = bug.Description;
            this.status = bug.Status;
        }

        public override string ToString()
        {
            return $"BugDTO[bugNo={bugNo}, description={description}, status={status}]";
        }
    }
}
