using model;
using model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace networking.DTO
{
    [Serializable]
    public class UserDTO{

        [JsonPropertyName("id")]
        public long id { get; set; }
        [JsonPropertyName("username")] 
        public string username { get; set; }
        [JsonPropertyName("password")]
        public string password { get; set; }
        [JsonPropertyName("type")]
        public UserType type { get; set; }

        public UserDTO() { }
        public UserDTO(long id, string username, string password, UserType type)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.type = type;
        }

        public UserDTO(User user)
        {
            this.id = user.Id;
            this.username = user.Username;
            this.password = user.Password;
            this.type = user.Type;
        }

        public override string ToString()
        {
            return $"UserDTO{{id={id}, username={username}, password={password}, type={type}}}";
        }
    }
}
