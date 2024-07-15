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
    public class TimelineRepository : IRepository<Timeline>
    {
        private Context db;

        public TimelineRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Timeline> GetAll(int id)
        {
            return db.Timelines;
        }

        public Timeline Get(int id)
        {
            return db.Timelines.Find(id);
        }

        public void Create(Timeline timeline)
        {
            db.Timelines.Add(timeline);
        }

        public void Update(Timeline timeline)
        {
            db.Entry(timeline).State = EntityState.Modified;
        }

        public IEnumerable<Timeline> Find(Func<Timeline, Boolean> predicate)
        {
            return db.Timelines.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Timeline timeline = db.Timelines.Find(id);
            if (timeline != null)
                db.Timelines.Remove(timeline);
        }
    }
}
