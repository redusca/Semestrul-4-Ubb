using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace networking.DTO
{
    [Serializable]
    internal class UserDTO
    {
        [JsonPropertyName("id")]
        public long id { get; set; }

        [JsonPropertyName("nume")]
        public string nume { get; set; }

        [JsonPropertyName("username")]
        public string username { get; set; }

        [JsonPropertyName("password")]
        public string password { get; set; }

        [JsonPropertyName("id_proba")]
        public string id_proba { get; set; }
        public UserDTO() { }

        public UserDTO(long id, string nume, string username, string password, string id_proba)
        {
            this.id = id;
            this.nume = nume;
            this.username = username;
            this.password = password;
            this.id_proba = id_proba;
        }

        public UserDTO(string nume, string username, string password, string id_proba)
            : this(0, nume, username, password, id_proba)
        {}

        public override string ToString()
        {
            return $"UserDTO [id={id}, nume={nume}, username={username}, password={password}, id_proba={id_proba}]";
        }
    }
}
