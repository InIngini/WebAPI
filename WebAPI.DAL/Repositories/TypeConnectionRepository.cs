using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Guide;
using WebAPI.DAL.EF;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class TypeConnectionRepository : IRepository<TypeConnection>
    {
        private Context db;

        public TypeConnectionRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<TypeConnection> GetAll(int id)
        {
            return db.TypeConnections;
        }

        public TypeConnection Get(int id)
        {
            return db.TypeConnections.Find(id);
        }

        public void Create(TypeConnection typeConnection)
        {
            db.TypeConnections.Add(typeConnection);
        }

        public void Update(TypeConnection typeConnection)
        {
            db.Entry(typeConnection).State = EntityState.Modified;
        }

        public IEnumerable<TypeConnection> Find(Func<TypeConnection, Boolean> predicate)
        {
            return db.TypeConnections.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            TypeConnection typeConnection = db.TypeConnections.Find(id);
            if (typeConnection != null)
                db.TypeConnections.Remove(typeConnection);
        }
    }

}
