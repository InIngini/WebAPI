using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;

namespace WebAPI.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Event> CreateEvent(Event @event)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Events.Create(@event);
            _unitOfWork.Save();

            return @event;
        }

        public async Task<Event> UpdateEvent(Event @event)
        {
            _unitOfWork.Events.Update(@event);
            _unitOfWork.Save();

            return @event;
        }

        public async Task<Event> DeleteEvent(int id)
        {
            var @event = _unitOfWork.Events.Get(id);

            if (@event == null)
            {
                throw new KeyNotFoundException();
            }

            // Получение всех схем
            var timelines = await _unitOfWork.Timelines.ToListAsync();

            // Удаление IdConnection удаляемой связи из схем
            foreach (var timeline in timelines)
            {
                var @event1 = timeline.IdEvents.FirstOrDefault(c => c.IdEvent == id);
                if (@event1 != null)
                {
                    timeline.IdEvents.Remove(@event1);
                }
            }

            _unitOfWork.Events.Delete(id);
            _unitOfWork.Save();

            return @event;
        }

        public async Task<Event> GetEvent(int id)
        {
            var @event = _unitOfWork.Events.Get(id);

            if (@event == null)
            {
                throw new KeyNotFoundException();
            }

            return @event;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var events = await _unitOfWork.Events.GetAll().ToListAsync();

            return events;
        }
    }

}
