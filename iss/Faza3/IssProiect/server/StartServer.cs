using networking.Utils;
using persistence.repo;
using persistence.repo.ContextRepository;
using persistence.repo.Interface;
using persistence.utils;
using repository;
using services;
using services.Interfaces;
using System.Configuration;
using System.Drawing;

namespace server
{
    internal class StartServer
    {
        private static int DEFAULT_PORT = 55555;
        private static string DEFAULT_IP = "127.0.0.1";

        static async Task Main(string[] args)
        {
            IDictionary<string, string> _props = new SortedList<string, string>();
            _props.Add("ConnectionString", ConnectionString.GetConnectionStringByName("bugtracer.db"));

            int port = DEFAULT_PORT;
            string host = DEFAULT_IP;
            string portS = ConfigurationManager.AppSettings["port"];
            if (portS == null)
                Console.WriteLine("Port not set in app.config, using default port " + DEFAULT_PORT);
            else
            {
                bool result = Int32.TryParse(portS, out port);
                if (!result)
                {
                    Console.WriteLine("Port not set in app.config, using default port " + DEFAULT_PORT);
                    port = DEFAULT_PORT;
                    Console.WriteLine("Portul " + port);
                }
            }
            string ipS = ConfigurationManager.AppSettings["ip"];

            if (ipS == null)
                Console.WriteLine("IP not set in app.config, using default IP " + DEFAULT_IP);

            Console.WriteLine("Configuration Settings for database: " + _props["ConnectionString"]);

            Context context = new(_props["ConnectionString"]);

            IUserRepository _userRepo = new UserContextRepository(context);
            IBugRepository _bugRepository = new BugContextRepository(context);

            IService _service = new ServiceImpl(_bugRepository, _userRepo);

            Console.WriteLine("Starting server with port " + port + " and ip " + host);

            var server = new JsonServer(host, port, _service);

            try
            {
               

                Program.Main(args);
                Console.WriteLine("==================== Server started. =====================");
                server.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error starting server: " + e.Message);
            }

        }
    }
}
