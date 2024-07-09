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
    public class Block4Repository : IRepository<Block4>
    {
        private Context db;

        public Block4Repository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Block4> GetAll()
        {
            return db.Block4s;
        }

        public Block4 Get(int id)
        {
            return db.Block4s.Find(id);
        }

        public void Create(Block4 block4)
        {
            db.Block4s.Add(block4);
        }

        public void Update(Block4 block4)
        {
            db.Entry(block4).State = EntityState.Modified;
        }

        public IEnumerable<Block4> Find(Func<Block4, Boolean> predicate)
        {
            return db.Block4s.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Block4 block4 = db.Block4s.Find(id);
            if (block4 != null)
                db.Block4s.Remove(block4);
        }
    }
}
