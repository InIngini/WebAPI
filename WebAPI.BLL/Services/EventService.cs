using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AutoMapper;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления событиями.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public EventService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новое событие.
        /// </summary>
        /// <param name="eventData">Данные о событии для создания.</param>
        /// <returns>Созданное событие.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Event> CreateEvent(EventData eventData)
        {
            var validationContext = new ValidationContext(eventData);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(eventData, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }
            Event @event = _mapper.Map<Event>(eventData);

            _context.Events.Add(@event);
            _context.SaveChanges();
            if (eventData.IdCharacters != null)
            {
                foreach (var idCharacter in eventData.IdCharacters)
                {
                    var character = _context.Characters.Find(idCharacter);
                    if (character != null)
                    {
                        var belongToEvent=new BelongToEvent()
                        { 
                            IdEvent=@event.IdEvent,
                            IdCharacter=character.IdCharacter
                        };
                        _context.BelongToEvents.Add(belongToEvent);
                    }
                }
            }
            _context.SaveChanges();

            var timeline = _context.Timelines
                            .Where(s => s.NameTimeline == "Главный таймлайн" && s.IdBook == eventData.IdBook)
                            .SingleOrDefault();
            // Добавление связи в главную схему
            var belongToTimeline = new BelongToTimeline()
            { 
                IdEvent=@event.IdEvent, 
                IdTimeline=timeline.IdTimeline
            };
            _context.BelongToTimelines.Add(belongToTimeline);
            _context.SaveChanges();

            return @event;
        }

        /// <summary>
        /// Обновляет существующее событие.
        /// </summary>
        /// <param name="eventData">Данные о событии для обновления.</param>
        /// <param name="id">Идентификатор события.</param>
        /// <returns>Обновленное событие.</returns>
        public async Task<Event> UpdateEvent(EventData eventData, int id)
        {
            Event @event = _context.Events.Find(id);
            // Обновление события
            @event.Name = eventData.Name;
            @event.Content = eventData.Content;
            @event.Time = eventData.Time;

            if (eventData.IdCharacters != null)
            {
                var characters = _context.BelongToEvents.Where(b=>b.IdEvent==id).ToList();
                // Удаление ненужных связей персонажей с событием. Допустим было [1,2], мы передали [2,3], значит будет [2]
                foreach (var character in characters)
                {
                    if(!eventData.IdCharacters.Contains(character.IdCharacter))
                    {
                        var belongToEvent = _context.BelongToEvents.Where(b=>b.IdCharacter==character.IdCharacter&&b.IdEvent==id).FirstOrDefault();
                        if (belongToEvent == null)
                        {
                            throw new KeyNotFoundException();
                        }
                        _context.BelongToEvents.Remove(belongToEvent);
                    }    
                    
                }
                // Добавление новых. То есть после этого уже будет [2,3]
                foreach (var idCharacter in eventData.IdCharacters)
                {
                    var character = _context.Characters.Find(idCharacter);
                    var belongToEvents = _context.BelongToEvents.Where(b => b.IdCharacter == character.IdCharacter && b.IdEvent == id).ToList();
                    //если такой персонаж существует и в белонгтуивант нет записи с этим персонажем и ивентом
                    if (character != null && belongToEvents.Count()==0)
                    {
                        var belong = new BelongToEvent()
                        {
                            IdEvent = id,
                            IdCharacter=character.IdCharacter,
                        };
                        _context.BelongToEvents.Add(belong);
                    }
                }
            }

            _context.Events.Update(@event);
            _context.SaveChanges();

            return @event;
        }

        /// <summary>
        /// Удаляет событие по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор события.</param>
        /// <returns>Удаленное событие.</returns>
        /// <exception cref="KeyNotFoundException">Если событие не найдено.</exception>
        public async Task<Event> DeleteEvent(int id)
        {
            var @event = _context.Events.Find(id);

            if (@event == null)
            {
                throw new KeyNotFoundException();
            }

            // Получение всех таймлайнов, связанных с событием
            var belongToTimelines = _context.BelongToTimelines.Where(t=>t.IdEvent == id).ToList();

            // Удаление IdConnection удаляемой связи из схем
            foreach (var belongToTimeline in belongToTimelines)
            {
                _context.BelongToTimelines.Remove(belongToTimeline);
            }

            _context.Events.Remove(@event);
            _context.SaveChanges();

            return @event;
        }

        /// <summary>
        /// Получает событие по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор события.</param>
        /// <returns>Данные события.</returns>
        /// <exception cref="KeyNotFoundException">Если событие не найдена.</exception>
        public async Task<EventData> GetEvent(int id)
        {
            var @event = _context.Events.Find(id);

            if (@event == null)
            {
                throw new KeyNotFoundException();
            }
            var eventdata = _mapper.Map<EventData>(@event);

            var characters = _context.BelongToEvents.Where(b => b.IdEvent == id).ToList();
            int[] ints = new int[characters.Count];
            for(int i = 0;i<ints.Length;i++)
                ints[i] = characters[i].IdCharacter;
            eventdata.IdCharacters = ints;
            return @eventdata;
        }

        /// <summary>
        /// Получает все события для указанного таймлайна.
        /// </summary>
        /// <param name="idTimeline">Идентификатор таймлайна.</param>
        /// <returns>Список всех событий таймлайна.</returns>
        public async Task<IEnumerable<EventAllData>> GetAllEvents(int idTimeline)
        {
            var belongToTimelines = _context.BelongToTimelines.Where(b => b.IdTimeline == idTimeline).ToList();

            var events = new List<Event>();
            foreach (var belongToTimeline in belongToTimelines)
            {
                var @event = _context.Events.Find(belongToTimeline.IdEvent);
                if (@event == null)
                {
                    throw new KeyNotFoundException();
                }
                events.Add(@event);
            }
            
            var eventsData = new List<EventAllData>();
            foreach (var @event in events)
            {
                
                var eventData = _mapper.Map<EventAllData>(@event);
                eventsData.Add(eventData);
            }
            return eventsData;
        }
    }

}
