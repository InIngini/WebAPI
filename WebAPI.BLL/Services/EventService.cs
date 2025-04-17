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
        private readonly IContext Context;
        private readonly IMapper Mapper;
        private DeletionRepository DeletionRepository;
        private CreationRepository CreationRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public EventService(IContext context, IMapper mapper, CreationRepository creationRepository, DeletionRepository deletionRepository)
        {
            Context = context;
            Mapper = mapper;
            DeletionRepository = deletionRepository;
            CreationRepository = creationRepository;
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
            Event @event = Mapper.Map<Event>(eventData);
            await CreationRepository.CreateEvent(@event, (int)eventData.BookId, Context);

            if (eventData.CharactersId != null)
            {
                foreach (var CharacterId in eventData.CharactersId)
                {
                    await CreationRepository.CreateBelongToEvent(@event.Id, CharacterId, Context);
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
            Event @event = await Context.Events.FirstOrDefaultAsync(x => x.Id == id);
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
                var oldInvolvedCharactersId = Context.BelongToEvents.Where(b => b.EventId == id).Select(a=>a.CharacterId).ToList();
                foreach (var CharacterId in eventData.CharactersId)
                {
                    if(!oldInvolvedCharactersId.Contains(CharacterId))//если его там не было, добавляем
                        await CreationRepository.CreateBelongToEvent(id, CharacterId, Context);
                    else //если он там был, удаляем из списка старых, потому что он уже новый 
                        oldInvolvedCharactersId.Remove(CharacterId);             
                }
                //перебираем, что осталось и удаляем, так как должен остаться только список на удаление
                foreach (var characterId in oldInvolvedCharactersId)
                {
                    await DeletionRepository.DeleteBelongToEvent(id, characterId, Context);
                }
            }

            Context.Events.Update(@event);
            await Context.SaveChangesAsync();

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
            var @event = await Context.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 0));
            }

            await DeletionRepository.DeleteEvent(id, Context);

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
            var @event = await Context.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 0));
            }
            var eventdata = Mapper.Map<EventData>(@event);

            var characters = await Context.BelongToEvents.Where(b => b.EventId == id).ToListAsync(cancellationToken);
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
            var belongToTimelines = await Context.BelongToTimelines.Where(b => b.TimelineId == idTimeline).ToListAsync(cancellationToken);

            var events = new List<Event>();
            foreach (var belongToTimeline in belongToTimelines)
            {
                var @event = await Context.Events.FirstOrDefaultAsync(x => x.Id == belongToTimeline.EventId, cancellationToken);
                if (@event == null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 0));
                }
                events.Add(@event);
            }
            
            var eventsData = new List<EventAllData>();
            foreach (var @event in events)
            {
                var eventData = Mapper.Map<EventAllData>(@event);
                eventsData.Add(eventData);
            }
            return eventsData;
        }
    }

}
