using client.Controller;
using Microsoft.VisualBasic.Logging;
using networking.jsonProtocol;
using services.Interfaces;
using System.Configuration;

namespace client
{
    public static class StartClient
    {
        private static int DEFAULT_PORT = 55555;
        private static string DEFAULT_IP = "127.0.0.1";

        public static void Main(string[] args)
        {

            int port = DEFAULT_PORT;
            String ip = DEFAULT_IP;
            String portS = ConfigurationManager.AppSettings["port"];

            if (portS == null)
            {
                Console.WriteLine("Port property not set. Using default value {0}", DEFAULT_PORT);
            }
            else
            {
                bool result = Int32.TryParse(portS, out port);
                if (!result)
                {
                    Console.WriteLine("Port property not set. Using default value {0}", DEFAULT_PORT);
                    port = DEFAULT_PORT;
                    Console.WriteLine("Portul {0}", port);
                }
            }
            String ipS = ConfigurationManager.AppSettings["ip"];

            if (ipS == null)
            {
                Console.WriteLine("IP property not set. Using default value {0}", DEFAULT_IP);
            }

            Console.WriteLine("Using server on IP {0} and port {1}", ip, port);

            IService server = new ServerProxy(ip, port);

            ApplicationConfiguration.Initialize();
            Form1 form1 = new(server);

            Login login = new (server, form1);
            AdminWindow aw = new (server, form1);
            ProgrammerWindow pw = new (server, form1);
            TesterWindow tw = new (server, form1);

            form1.setWindows(login, aw, pw, tw);

            Application.Run(form1);
        }
    }
}