using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IEventService
    {
        Task<Event> CreateEvent(Event @event);
        Task<Event> UpdateEvent(Event @event);
        Task<Event> DeleteEvent(int id);
        Task<Event> GetEvent(int id);
        Task<IEnumerable<Event>> GetAllEvents();
    }

}
