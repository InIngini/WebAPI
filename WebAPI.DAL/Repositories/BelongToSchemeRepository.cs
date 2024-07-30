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
    public class BelongToSchemeRepository : IRepository<BelongToScheme>
    {
        private Context db;

        public BelongToSchemeRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<BelongToScheme> GetAll(int id)
        {
            return db.BelongToSchemes.Where(a => a.IdScheme == id);
        }

        public BelongToScheme Get(int id)
        {
            return db.BelongToSchemes.Find(id);
        }

        public void Create(BelongToScheme belongToScheme)
        {
            db.BelongToSchemes.Add(belongToScheme);
        }

        public void Update(BelongToScheme belongToScheme)
        {
            db.Entry(belongToScheme).State = EntityState.Modified;
        }

        public IEnumerable<BelongToScheme> Find(Func<BelongToScheme, Boolean> predicate)
        {
            return db.BelongToSchemes.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            BelongToScheme belongToScheme = db.BelongToSchemes.SingleOrDefault(b => b.IdConnection == id);
            if (belongToScheme != null)
                db.BelongToSchemes.Remove(belongToScheme);
        }
        public void Delete(int idScheme, int idConnection)
        {
            BelongToScheme belongToScheme = db.BelongToSchemes.SingleOrDefault(b => b.IdScheme == idScheme && b.IdConnection == idConnection);
            if (belongToScheme != null)
                db.BelongToSchemes.Remove(belongToScheme);
        }
    }
}
