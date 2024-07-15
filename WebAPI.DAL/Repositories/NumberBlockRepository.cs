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
    public class NumberBlockRepository : IRepository<NumberBlock>
    {
        private Context db;

        public NumberBlockRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<NumberBlock> GetAll(int id)
        {
            return db.NumberBlocks;
        }

        public NumberBlock Get(int id)
        {
            return db.NumberBlocks.Find(id);
        }

        public void Create(NumberBlock numberBlock)
        {
            db.NumberBlocks.Add(numberBlock);
        }

        public void Update(NumberBlock numberBlock)
        {
            db.Entry(numberBlock).State = EntityState.Modified;
        }

        public IEnumerable<NumberBlock> Find(Func<NumberBlock, Boolean> predicate)
        {
            return db.NumberBlocks.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            NumberBlock numberBlock = db.NumberBlocks.Find(id);
            if (numberBlock != null)
                db.NumberBlocks.Remove(numberBlock);
        }
    }

}
