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
using WebAPI.Errors;
using WebAPI.BLL.Additional;

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
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }
            Event @event = _mapper.Map<Event>(eventData);
            Creation.CreateEvent(@event, (int)eventData.BookId, _context);

            if (eventData.CharactersId != null)
            {
                foreach (var CharacterId in eventData.CharactersId)
                {
                    Creation.CreateBelongToEvent(@event.Id, CharacterId, _context);
                }
            }

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
            Event @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 2));
            }
            // Обновление события
            @event.Name = eventData.Name;
            @event.Content = eventData.Content;
            @event.Time = eventData.Time;

            if (eventData.CharactersId != null)
            {
                var characters = await _context.BelongToEvents.Where(b=>b.EventId==id).ToListAsync();
                // Удаление ненужных связей персонажей с событием. Допустим было [1,2], мы передали [2,3], значит будет [2]
                foreach (var character in characters)
                {
                    if(!eventData.CharactersId.Contains(character.CharacterId))
                    {
                        Deletion.DeleteBelongToEvent(id, character.CharacterId,_context);
                    }    
                }
                // Добавление новых. То есть после этого уже будет [2,3]
                foreach (var CharacterId in eventData.CharactersId)
                {
                    Creation.CreateBelongToEvent(id, CharacterId, _context);
                }
            }

            _context.Events.Update(@event);
            await _context.SaveChangesAsync();

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
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 0));
            }

            Deletion.DeleteEvent(id, _context);

            return @event;
        }

        /// <summary>
        /// Получает событие по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор события.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Данные события.</returns>
        /// <exception cref="KeyNotFoundException">Если событие не найдена.</exception>
        public async Task<EventData> GetEvent(int id, CancellationToken cancellationToken)
        {
            var @event = await _context.Events.FindAsync(id, cancellationToken);

            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 0));
            }
            var eventdata = _mapper.Map<EventData>(@event);

            var characters = await _context.BelongToEvents.Where(b => b.EventId == id).ToListAsync(cancellationToken);
            int[] ints = new int[characters.Count];
            for(int i = 0;i<ints.Length;i++)
                ints[i] = characters[i].CharacterId;
            eventdata.CharactersId = ints;
            return @eventdata;
        }

        /// <summary>
        /// Получает все события для указанного таймлайна.
        /// </summary>
        /// <param name="idTimeline">Идентификатор таймлайна.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех событий таймлайна.</returns>
        public async Task<IEnumerable<EventAllData>> GetAllEvents(int idTimeline, CancellationToken cancellationToken)
        {
            var belongToTimelines = await _context.BelongToTimelines.Where(b => b.TimelineId == idTimeline).ToListAsync(cancellationToken);

            var events = new List<Event>();
            foreach (var belongToTimeline in belongToTimelines)
            {
                var @event = await _context.Events.FindAsync(belongToTimeline.EventId, cancellationToken);
                if (@event == null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 0));
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
