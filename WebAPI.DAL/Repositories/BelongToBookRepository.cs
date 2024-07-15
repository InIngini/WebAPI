using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.EF;
using WebAPI.DAL.Entities;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class BelongToBookRepository : IRepository<BelongToBook>
    {
        private Context db;

        public BelongToBookRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<BelongToBook> GetAll(int id)
        {
            return db.BelongToBooks.Where(a => a.IdUser == id);
        }

        public BelongToBook Get(int id)
        {
            return db.BelongToBooks.Find(id);
        }

        public void Create(BelongToBook belongToBook)
        {
            db.BelongToBooks.Add(belongToBook);
        }

        public void Update(BelongToBook belongToBook)
        {
            db.Entry(belongToBook).State = EntityState.Modified;
        }

        public IEnumerable<BelongToBook> Find(Func<BelongToBook, Boolean> predicate)
        {
            return db.BelongToBooks.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            BelongToBook belongToBook = db.BelongToBooks.SingleOrDefault(b => b.IdBook == id);
            if (belongToBook != null)
                db.BelongToBooks.Remove(belongToBook);
        }
    }
}
