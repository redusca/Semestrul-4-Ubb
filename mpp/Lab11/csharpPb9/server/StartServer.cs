using csharpPb9.utils;
using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using networking.grpc;
using networking.Utils;
using persistene.repository.context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class StartServer
    {
        private static int DEFAULT_PORT = 55555;
        private static string DEFAULT_IP = "127.0.0.1";
        private static readonly ILog log = LogManager.GetLogger(typeof(StartServer));

        static async Task Main(string[] args)
        {

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));

            log.Info("Start Sever");

            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", ConnectionString.GetConnectionStringByName("triatlon.db"));

            int port = DEFAULT_PORT;
            string host = DEFAULT_IP;
            string portS = ConfigurationManager.AppSettings["port"];
            if (portS == null)
                log.Debug("Port not set in app.config, using default port " + DEFAULT_PORT);

            else
            {
                bool result = Int32.TryParse(portS, out port);
                if (!result)
                {
                    log.Debug("Port not set in app.config, using default port " + DEFAULT_PORT);
                    port = DEFAULT_PORT;
                    log.Debug("Portul " + port);
                }
            }
            string ipS = ConfigurationManager.AppSettings["ip"];

            if (ipS == null)
                log.Debug("IP not set in app.config, using default IP " + DEFAULT_IP);

            log.InfoFormat("Configuration Settings for database: {0}", props["ConnectionString"]);

            Context context = new Context(props["ConnectionString"]);

            IArbitruRepository arbitruRepository = new ContextArbitruRepo(context);
            IProbaRepository probaRepository = new ContextProbaRepo(context);
            IParticipantRepository participantRepository = new ParticipantRepository(props);
            IRezultatRepository rezultatRepository = new RezultatRepository(props);

            ServiceServer serviceServer = new ServiceServer(arbitruRepository, probaRepository, participantRepository, rezultatRepository);

            log.DebugFormat("Starting server on IP {0} and port {1}", host, port);
            //JsonServer server = new JsonServer(host, port, serviceServer);

            var server = new GRPCServer(host, port, serviceServer);

            try
            {
                await server.StartAsync(new CancellationToken());
                log.Info($"Server started on {host}:{port}");
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                log.Error("Error starting server", e);
            }
        }
    }              
}