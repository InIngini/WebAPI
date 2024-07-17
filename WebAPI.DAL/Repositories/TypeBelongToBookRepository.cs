using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Guide;
using WebAPI.DB;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class TypeBelongToBookRepository : IRepository<TypeBelongToBook>
    {
        private Context db;

        public TypeBelongToBookRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<TypeBelongToBook> GetAll(int id)
        {
            return db.TypeBelongToBooks;
        }

        public TypeBelongToBook Get(int id)
        {
            return db.TypeBelongToBooks.Find(id);
        }

        public void Create(TypeBelongToBook typeBelongToBook)
        {
            db.TypeBelongToBooks.Add(typeBelongToBook);
        }

        public void Update(TypeBelongToBook typeBelongToBook)
        {
            db.Entry(typeBelongToBook).State = EntityState.Modified;
        }

        public IEnumerable<TypeBelongToBook> Find(Func<TypeBelongToBook, Boolean> predicate)
        {
            return db.TypeBelongToBooks.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            TypeBelongToBook typeBelongToBook = db.TypeBelongToBooks.Find(id);
            if (typeBelongToBook != null)
                db.TypeBelongToBooks.Remove(typeBelongToBook);
        }
    }

}
