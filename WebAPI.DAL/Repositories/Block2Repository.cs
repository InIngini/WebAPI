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
    public class Block2Repository : IRepository<Block2>
    {
        private Context db;

        public Block2Repository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Block2> GetAll()
        {
            return db.Block2s;
        }

        public Block2 Get(int id)
        {
            return db.Block2s.Find(id);
        }

        public void Create(Block2 block2)
        {
            db.Block2s.Add(block2);
        }

        public void Update(Block2 block2)
        {
            db.Entry(block2).State = EntityState.Modified;
        }

        public IEnumerable<Block2> Find(Func<Block2, Boolean> predicate)
        {
            return db.Block2s.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Block2 block2 = db.Block2s.Find(id);
            if (block2 != null)
                db.Block2s.Remove(block2);
        }
    }
}
