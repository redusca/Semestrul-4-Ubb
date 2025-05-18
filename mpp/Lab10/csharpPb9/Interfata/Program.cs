using client;
using csharpPb9.utils;
using Interfata.controllers;
using log4net;
using log4net.Config;
using networking.grpc;
using networking.protocol;
using services;
using services.services;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;

namespace Interfata
{
    public class Client
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Client));

        private static int DEFAULT_PORT = 55555;
        private static string DEFAULT_IP = "127.0.0.1";


        public static void Main(string[] args)
        {

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));

            log.Info("Start Client");

            int port = DEFAULT_PORT;
            String ip = DEFAULT_IP;
            String portS = ConfigurationManager.AppSettings["port"];
            if (portS == null)
            {
                log.DebugFormat("Port property not set. Using default value {0}", DEFAULT_PORT);
            }
            else
            {
                bool result = Int32.TryParse(portS, out port);
                if (!result)
                {
                    log.DebugFormat("Port property not a number. Using default value {0}", DEFAULT_PORT);
                    port = DEFAULT_PORT;
                    log.DebugFormat("Portul {0}", port);
                }
            }
            String ipS = ConfigurationManager.AppSettings["ip"];

            if (ipS == null)
            {
                log.DebugFormat("Port property not set. Using default value {0}", DEFAULT_IP);
            }

            log.InfoFormat("Using  server on IP {0} and port {1}", ip, port);
            // IService server = new ServerProxy(ip, port);
            IService server = new GRPCServerProxy(ip, port);

            ApplicationConfiguration.Initialize();
            Form1 form1 = new Form1(server);

            AppController userControl = new AppController(server, form1);

            form1.setAppController(userControl);

            LoginController loginController = new LoginController(form1);
            form1.setLoginController(loginController);

            Application.Run(form1);
        }
    }
}