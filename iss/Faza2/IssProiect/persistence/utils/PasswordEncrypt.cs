using System.Text;

namespace persistence.utils
{
    public class PasswordEncrypt
    {
        public static string Encrypt(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        public static void Main()
        {
            Console.WriteLine(Encrypt("admin"));
            Console.WriteLine(Encrypt("123456789Ww"));
            Console.WriteLine(Encrypt("abcdefgW123"));
            Console.WriteLine(Encrypt("12341234Ab"));
            Console.WriteLine(Encrypt("part2bestPart"));
            Console.WriteLine(Encrypt("a123123123A"));
        }
    }
}
