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
    public class Block3Repository : IRepository<Block3>
    {
        private Context db;

        public Block3Repository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Block3> GetAll()
        {
            return db.Block3s;
        }

        public Block3 Get(int id)
        {
            return db.Block3s.Find(id);
        }

        public void Create(Block3 block3)
        {
            db.Block3s.Add(block3);
        }

        public void Update(Block3 block3)
        {
            db.Entry(block3).State = EntityState.Modified;
        }

        public IEnumerable<Block3> Find(Func<Block3, Boolean> predicate)
        {
            return db.Block3s.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Block3 block3 = db.Block3s.Find(id);
            if (block3 != null)
                db.Block3s.Remove(block3);
        }
    }
}
