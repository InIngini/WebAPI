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
    public class EventRepository : IRepository<Event>
    {
        private Context db;

        public EventRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Event> GetAll(int id)
        {
            var timelines = db.BelongToTimelines.Where(s => s.IdTimeline == id).ToList();
            var events = new List<Event>();
            foreach (var timeline in timelines)
            {
                var @event = db.Events.Find(timeline.IdEvent);
                events.Add(@event);
            }
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
