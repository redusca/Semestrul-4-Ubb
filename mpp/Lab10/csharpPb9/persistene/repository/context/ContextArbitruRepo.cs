using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistene.repository.context
{
    public class ContextArbitruRepo : IArbitruRepository
    {
        private readonly Context context;
        public ContextArbitruRepo(Context context)
        {
            this.context = context;
        }

        public Arbitru Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Arbitru> FindAll()
        {
           return [.. context.Arbitru];
        }

        public Arbitru FindByUser(string username)
        {
            return context.Arbitru
                .Where(a => a.Username == username)
                .FirstOrDefault();
        }

        public Arbitru FindOne(long id)
        {
            return context.Arbitru.Find(id);
        }

        public Arbitru fromSettoEntity(IDataReader read)
        {
            throw new NotImplementedException();
        }

        public void Save(Arbitru entity)
        {
            context.Arbitru.Add(entity);
        }

        public Arbitru Update(long id, Arbitru new_entity)
        {
            throw new NotImplementedException();
        }
    }
}
