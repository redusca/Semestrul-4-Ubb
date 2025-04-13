using System.Text;

namespace pass
{
    public class PasswordEncrypt
    {
        public static string Encrypt(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static void Main()
        {
            Console.WriteLine("andrei03: " + Encrypt("12345678Wa"));
            Console.WriteLine("bucatarul: " + Encrypt("123456789Ww"));
            Console.WriteLine("coco123: " + Encrypt("abcdefgW123"));
            Console.WriteLine("supermario: " + Encrypt("12341234Ab"));
            Console.WriteLine("\"jojoBiz \": " + Encrypt("part2bestPart"));
            Console.WriteLine("ARBITRU: " + Encrypt("a123123123A"));
        }
    }
}
