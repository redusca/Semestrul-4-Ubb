
namespace restApi
{
    public class ProbaService : IProbaService
    {
        private IProbaRepository probaRepository;

        public ProbaService(IProbaRepository probaRepository)
        {
            this.probaRepository = probaRepository;
        }

        public string Add(string nume, Categorie categorie)
        {
            var id = getNextId(categorie);
            probaRepository.Save(new Proba(id,nume,categorie));
            return id;
        }

        public Proba Delete(string id)
        {
            if(id[0] != 'i' && id[0] != 'c' && id[0] != 's')
            {
                throw new Exception("Id invalid");
            }

            return probaRepository.Delete(id);
        }

        public IEnumerable<Proba> GetAll()
        {
            return probaRepository.FindAll();
        }

        public Proba GetById(string id)
        {
            if (id[0] != 'i' && id[0] != 'c' && id[0] != 's')
            {
                throw new Exception("Id invalid");
            }

            return probaRepository.FindOne(id);
        }

        public string getNextId(Categorie categorie)
        {
            string id = "";
            if(categorie == Categorie.inot)
            {
                id = "i";
            }
            else if (categorie == Categorie.ciclism)
            {
                id = "c";
            }
            else if (categorie == Categorie.alergat)
            {
                id = "s";
            }
            else
            {
                throw new Exception("Categorie invalida");
            }

            var list = probaRepository.FindAll().Where(p => p.Categorie == categorie).ToList();
            if (list.Count == 0)
            {
                return id + "0";
            }
            else
            {
                var numbers = list.Select(p => int.Parse(p.Id.Substring(1))).ToList();
                var max = numbers.Max();
                return id + (max + 1);
            }

        }

        public Proba Update(string id, string nume, Categorie categorie)
        {
            if(categorie != Categorie.inot && categorie != Categorie.ciclism && categorie != Categorie.alergat)
            {
                throw new Exception("Categorie invalida");
            }

            if (id[0] != 'i' && id[0] != 'c' && id[0] != 's')
            {
                throw new Exception("Id invalid");
            }

            return probaRepository.Update(id, new Proba(id,nume,categorie));
        }
    }
}
