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
    public class Block1Repository : IRepository<Block1>
    {
        private Context db;

        public Block1Repository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Block1> GetAll()
        {
            return db.Block1s;
        }

        public Block1 Get(int id)
        {
            return db.Block1s.Find(id);
        }

        public void Create(Block1 block1)
        {
            db.Block1s.Add(block1);
        }

        public void Update(Block1 block1)
        {
            db.Entry(block1).State = EntityState.Modified;
        }

        public IEnumerable<Block1> Find(Func<Block1, Boolean> predicate)
        {
            return db.Block1s.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Block1 block1 = db.Block1s.Find(id);
            if (block1 != null)
                db.Block1s.Remove(block1);
        }
    }
}
