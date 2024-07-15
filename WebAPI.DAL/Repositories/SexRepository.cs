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
    public class SexRepository : IRepository<Sex>
    {
        private Context db;

        public SexRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Sex> GetAll(int id)
        {
            return db.Sex;
        }

        public Sex Get(int id)
        {
            return db.Sex.Find(id);
        }

        public void Create(Sex sex)
        {
            db.Sex.Add(sex);
        }

        public void Update(Sex sex)
        {
            db.Entry(sex).State = EntityState.Modified;
        }

        public IEnumerable<Sex> Find(Func<Sex, Boolean> predicate)
        {
            return db.Sex.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Sex sex = db.Sex.Find(id);
            if (sex != null)
                db.Sex.Remove(sex);
        }
    }

}
