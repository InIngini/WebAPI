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
    public class BelongToEventRepository : IRepository<BelongToEvent>
    {
        private Context db;

        public BelongToEventRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<BelongToEvent> GetAll(int id)
        {
            return db.BelongToEvents.Where(a => a.IdCharacter == id);
        }

        public BelongToEvent Get(int id)
        {
            return db.BelongToEvents.Find(id);
        }

        public void Create(BelongToEvent belongToEvent)
        {
            db.BelongToEvents.Add(belongToEvent);
        }

        public void Update(BelongToEvent belongToEvent)
        {
            db.Entry(belongToEvent).State = EntityState.Modified;
        }

        public IEnumerable<BelongToEvent> Find(Func<BelongToEvent, Boolean> predicate)
        {
            return db.BelongToEvents.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            BelongToEvent belongToEvent = db.BelongToEvents.SingleOrDefault(b => b.IdEvent == id);
            if (belongToEvent != null)
                db.BelongToEvents.Remove(belongToEvent);
        }
    }
    
}
