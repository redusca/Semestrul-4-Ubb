using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistene.repository.context
{
    public class ContextProbaRepo : IProbaRepository
    {
        private readonly Context context;
        public ContextProbaRepo(Context context)
        {
            this.context = context;
        }

        public Proba Delete(string id)
        {
            var ob = context.Proba.Remove(FindOne(id));
            context.SaveChanges();
            return ob.Entity;
        }

        public IEnumerable<Proba> FindAll()
        {
            return context.Proba.ToList();
        }

        public Arbitru FindArbitru(string id)
        {
            return context.Arbitru.FirstOrDefault(x => x.Id_proba == id);
        }

        public Proba FindOne(string id)
        {
            return context.Proba.Find(id);
        }

        public Proba fromSettoEntity(IDataReader read)
        {
            throw new NotImplementedException();
        }

        public void Save(Proba entity)
        {
            entity.Id_arbitru = -1;
            context.Proba.Add(entity);
            context.SaveChanges();
        }

        public void SetArbitruForProba(string id, long arbitru)
        {
            Proba pr = context.Proba.Find(id);
            if (pr != null)
            {
                pr.Id_arbitru = arbitru;
                context.Proba.Update(pr);
                context.SaveChanges();
            }
        }

        public Proba Update(string id, Proba new_entity)
        {
            var old_entity = context.Proba.Find(id);
            if (old_entity == null)
            {
                return null;
            }
            old_entity.Nume = new_entity.Nume;
            old_entity.Categorie = new_entity.Categorie;

            context.SaveChanges();
            return old_entity;
        }
    }
}
