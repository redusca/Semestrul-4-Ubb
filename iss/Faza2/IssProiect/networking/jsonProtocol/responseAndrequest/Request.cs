using model;
using networking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking.jsonProtocol.responseAndrequest
{
    public class Request
    {
        public requestType type { get; set; }
        public UserDTO User { get; set; }
        public BugDTO[] bugDTOs { get; set; }

        public Request() { }

        public override string ToString()
        {
            //var bugsStr = string.Join(", ", bugDTOs.Select(b => b.ToString()));
            var bugsStr = "test";

            return $"Request [type = {type}, User = {User}, bugDTOs = {bugsStr}]";
        }
    }
}
