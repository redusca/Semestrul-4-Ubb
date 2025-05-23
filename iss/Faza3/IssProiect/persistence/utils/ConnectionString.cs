using System.Configuration;
using System.Reflection;

namespace persistence.utils
{
    public class ConnectionString
    {
        public static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];


            // If found, return the connection string.
            if (settings != null)
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory;
                int binIndex = directory.IndexOf("bin");
                if (binIndex != -1)
                {
                    directory = directory.Substring(0, binIndex);
                }

                return settings.ConnectionString.Replace("{AppDir}", directory);
            }

            return returnValue;
        }
    }
}