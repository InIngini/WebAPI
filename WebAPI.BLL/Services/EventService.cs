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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAPI.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Event> CreateEvent(EventData eventData)
        {
            var validationContext = new ValidationContext(eventData);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(eventData, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            Event @event = new Event()
            {
                Name = eventData.Name,
                Content = eventData.Content,
                Time = eventData.Time,

            };
            if (eventData.IdCharacter != null)
            {
                foreach (var idCharacter in eventData.IdCharacter)
                {
                    var character = _unitOfWork.Characters.Get(idCharacter);
                    if (character != null)
                    {
                        @event.IdCharacters.Add(character);
                    }
                }
            }
            _unitOfWork.Events.Create(@event);
            _unitOfWork.Save();

            var timeline = _unitOfWork.Timelines
                            .Find(s => s.NameTimeline == "Главный таймлайн" && s.IdBook == eventData.IdBook)
                            .SingleOrDefault();
            // Добавление связи в главную схему
            timeline.IdEvents.Add(@event);
            _unitOfWork.Save();

            return @event;
        }

        //public async Task<Event> UpdateEvent(EventData eventData, int id)
        //{
        //    Event @event = _unitOfWork.Events.Get(id);
        //    // Обновление события
        //    @event.Name = eventData.Name;
        //    @event.Content = eventData.Content;
        //    @event.Time = eventData.Time;

        //    if (eventData.IdCharacter != null)
        //    {
        //        var characters = @event.IdCharacters.ToList();
        //        // Удаление существующих связей персонажей с событием
        //        foreach (var character in characters)
        //        {
        //            @event.IdCharacters.Remove(character);
        //        }
        //        // Добавление новых
        //        foreach (var idCharacter in eventData.IdCharacter)
        //        {
        //            var character = _unitOfWork.Characters.Get(idCharacter);
        //            if (character != null)
        //            {
        //                @event.IdCharacters.Add(character);
        //            }
        //        }
        //    }

        //    _unitOfWork.Events.Update(@event);
        //    _unitOfWork.Save();

        //    return @event;
        //}

        public async Task<Event> DeleteEvent(int id)
        {
            var @event = _unitOfWork.Events.Get(id);

            if (@event == null)
            {
                throw new KeyNotFoundException();
            }

            // Получение всех таймлайнов, связанных с событием
            var timelines = _unitOfWork.Timelines.Find(t => t.IdEvents.Any(e => e.IdEvent == id)).ToList();

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

        public async Task<IEnumerable<EventAllData>> GetAllEvents(int id)
        {
            
            var events = _unitOfWork.Events.GetAll(id).ToList();
            var eventsData = new List<EventAllData>();
            foreach (var connection in events)
            {
                var eventData = new EventAllData()
                {
                    IdEvent = connection.IdEvent,
                    Name = connection.Name,
                    Time = connection.Time,
                };
                eventsData.Add(eventData);
            }
            return eventsData;
        }
    }

}
