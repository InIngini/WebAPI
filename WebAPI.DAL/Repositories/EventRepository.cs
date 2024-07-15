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
    public class EventRepository : IRepository<Event>
    {
        private Context db;

        public EventRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Event> GetAll(int id)
        {
            var timelline = db.Timelines.Include(s => s.IdEvents).FirstOrDefault(s => s.IdTimeline == id);
            var events = timelline.IdEvents;

            return events;
        }

        public Event Get(int id)
        {
            return db.Events.Find(id);
        }

        public void Create(Event @event)
        {
            db.Events.Add(@event);
        }

        public void Update(Event @event)
        {
            db.Entry(@event).State = EntityState.Modified;
        }

        public IEnumerable<Event> Find(Func<Event, Boolean> predicate)
        {
            return db.Events.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event != null)
                db.Events.Remove(@event);
        }
    }
}
