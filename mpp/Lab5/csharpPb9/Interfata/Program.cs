using csharpPb9.utils;
using log4net;
using log4net.Config;
using System.Reflection;

namespace Interfata
{
    internal static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main()
        {
            #region logger + props
            log.Info("Start Application");
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));

            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", ConnectionString.GetConnectionStringByName("triatlon.db"));

            //test(props);
            #endregion

            #region appstart
            ArbitruRepository arbitruRepository = new ArbitruRepository(props);
            ProbaRepository probaRepository = new ProbaRepository(props);
            ParticipantRepository participantRepository = new ParticipantRepository(props);
            RezultatRepository rezultatRepository = new RezultatRepository(props);

            ServiceArbitru serviceArbitru = new ServiceArbitru(arbitruRepository,probaRepository);
            ServiceRezultatAndParticipantAndProba serviceRPP = new ServiceRezultatAndParticipantAndProba(
                probaRepository, participantRepository, rezultatRepository);

            ApplicationConfiguration.Initialize();
            Form1 form1 = new Form1(serviceArbitru,serviceRPP);
            
            Application.Run(form1);
            #endregion
        }

        private static void test(IDictionary<string, string> props)
        {
            log.Info("Test");
            ArbitruRepository arbitruRepository = new ArbitruRepository(props);
            foreach (var arbitru in arbitruRepository.FindAll())
            {
                Console.WriteLine(arbitru);
            }
            Console.WriteLine(arbitruRepository.FindOne(2L));

            log.Info("Next text");

            // arbitruRepository.Save(new Arbitru(10,"Andrei","Andrei04","2004","c10"));

            ParticipantRepository participant = new ParticipantRepository(props);
            foreach (var part in participant.FindAll())
            {
                Console.WriteLine(part);
            }
            Console.WriteLine(participant.FindOne(2L));

            //participant.Save(new Participant(10, "Andrei", "Vasile", 2004));

            log.Info("Next text");

            ProbaRepository proba = new ProbaRepository(props);
            foreach (var p in proba.FindAll())
            {
                Console.WriteLine(p);
            }
            Console.WriteLine(proba.FindOne("c2"));
            Console.WriteLine(proba.FindArbitru("c2"));

            // Proba pr = new Proba("c10", "probatest", Categorie.ciclism);
            // pr.Id_arbitru = 20;
            // proba.Save(pr);


            log.Info("Next text");
            RezultatRepository rezultat = new RezultatRepository(props);
            foreach (var r in rezultat.FindAll())
            {
                Console.WriteLine(r);
            }
            Console.WriteLine(rezultat.FindOne(2L));
            //rezultat.Save(new Rezultat(0, new Participant(10, "Andrei", "Vasile", 2004), new Proba("c10", "probatest", Categorie.ciclism) , 100));
            foreach (var r in rezultat.ParticipantiAlfabetic())
            {
                Console.WriteLine(r.Key + " " + r.Value);
            }
            foreach (var r in rezultat.ParticipantScorDescrescator("c2"))
            {
                Console.WriteLine(r.Key + " " + r.Value);
            }

            log.Info("End test");
        }
    }
}