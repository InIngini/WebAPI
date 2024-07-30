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
    public class BelongToTimelineRepository : IRepository<BelongToTimeline>
    {
        private Context db;

        public BelongToTimelineRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<BelongToTimeline> GetAll(int id)
        {
            return db.BelongToTimelines.Where(a => a.IdTimeline == id);
        }

        public BelongToTimeline Get(int id)
        {
            return db.BelongToTimelines.Find(id);
        }

        public void Create(BelongToTimeline belongToTimeline)
        {
            db.BelongToTimelines.Add(belongToTimeline);
        }

        public void Update(BelongToTimeline belongToTimeline)
        {
            db.Entry(belongToTimeline).State = EntityState.Modified;
        }

        public IEnumerable<BelongToTimeline> Find(Func<BelongToTimeline, Boolean> predicate)
        {
            return db.BelongToTimelines.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            BelongToTimeline belongToTimeline = db.BelongToTimelines.SingleOrDefault(b => b.IdEvent == id);
            if (belongToTimeline != null)
                db.BelongToTimelines.Remove(belongToTimeline);
        }
        public void Delete(int idTimeline, int idEvent)
        {
            BelongToTimeline belongToTimeline = db.BelongToTimelines.SingleOrDefault(b => b.IdTimeline == idTimeline && b.IdEvent == idEvent);
            if (belongToTimeline != null)
                db.BelongToTimelines.Remove(belongToTimeline);
        }
    }
}
