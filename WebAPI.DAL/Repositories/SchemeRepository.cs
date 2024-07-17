using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB;
using WebAPI.DB.Entities;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class SchemeRepository : IRepository<Scheme>
    {
        private Context db;

        public SchemeRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Scheme> GetAll(int id)
        {
            return db.Schemes;
        }

        public Scheme Get(int id)
        {
            return db.Schemes.Find(id);
        }

        public void Create(Scheme scheme)
        {
            db.Schemes.Add(scheme);
        }

        public void Update(Scheme scheme)
        {
            db.Entry(scheme).State = EntityState.Modified;
        }

        public IEnumerable<Scheme> Find(Func<Scheme, Boolean> predicate)
        {
            return db.Schemes.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Scheme scheme = db.Schemes.Find(id);
            if (scheme != null)
                db.Schemes.Remove(scheme);
        }
    }
}
