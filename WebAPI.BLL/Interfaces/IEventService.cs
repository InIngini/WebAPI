using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.BLL.DTO;

namespace WebAPI.BLL.Interfaces
{
    public interface IEventService
    {
        Task<Event> CreateEvent(EventData eventData);
        Task<Event> UpdateEvent(EventData eventData,int id);
        Task<Event> DeleteEvent(int id);
        Task<Event> GetEvent(int id);
        Task<IEnumerable<EventAllData>> GetAllEvents(int id);
    }

}
