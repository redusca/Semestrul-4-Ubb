using System.Text;

namespace pass
{
    public class PasswordEncrypt
    {
        public static string Encrypt(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            byte[] encryptedData = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                encryptedData[i] = (byte)(data[i] ^ 0xAA);
            }

            return Convert.ToBase64String(encryptedData);
        }
    }
}
