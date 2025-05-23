using model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace model
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("type")]
        public UserType Type { get; set; }

        public User(long id, string username, string password, UserType type)
        {
            Id = id;
            Username = username;
            Password = password;
            Type = type;
        }

        public User(string username, string password, UserType type)
        {
            Username = username;
            Password = password;
            Type = type;
        }

        public override string ToString()
        {
            return $"User[{Id}, {Username}, {Password}, {Type}]";
        }
    }
}