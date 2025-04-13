using networking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking.protocol
{
    internal class Request
    {
        public RequestType typeCsharp { get; set; }

        public string typeJava
        {
            get { return typeCsharp.ToString(); }
            set { typeCsharp = (RequestType)Enum.Parse(typeof(RequestType), value); }
        }
        public UserDTO user { get; set; }
        public RezultatDTO rezultat { get; set; }

        public Request() { }

        public override string ToString()
        {
            return $"Request [type={typeCsharp}, user={user}, rezultat={rezultat}]";
        }
    }
}
