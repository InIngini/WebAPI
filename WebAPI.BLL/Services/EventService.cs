using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventService"/>.
        /// </summary>
        /// <param name="unitOfWork">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
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

            _unitOfWork.Events.Create(@event);
            _unitOfWork.Save();
            if (eventData.IdCharacters != null)
            {
                foreach (var idCharacter in eventData.IdCharacters)
                {
                    var character = _unitOfWork.Characters.Get(idCharacter);
                    if (character != null)
                    {
                        var belongToEvent=new BelongToEvent()
                        { 
                            IdEvent=@event.IdEvent,
                            IdCharacter=character.IdCharacter
                        };
                        _unitOfWork.BelongToEvents.Create(belongToEvent);
                    }
                }
            }
            _unitOfWork.Save();

            var timeline = _unitOfWork.Timelines
                            .Find(s => s.NameTimeline == "Главный таймлайн" && s.IdBook == eventData.IdBook)
                            .SingleOrDefault();
            // Добавление связи в главную схему
            var belongToTimeline = new BelongToTimeline()
            { 
                IdEvent=@event.IdEvent, 
                IdTimeline=timeline.IdTimeline
            };
            _unitOfWork.BelongToTimelines.Create(belongToTimeline);
            _unitOfWork.Save();

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
            Event @event = _unitOfWork.Events.Get(id);
            // Обновление события
            @event.Name = eventData.Name;
            @event.Content = eventData.Content;
            @event.Time = eventData.Time;

            if (eventData.IdCharacters != null)
            {
                var characters = _unitOfWork.BelongToEvents.Find(b=>b.IdEvent==id).ToList();
                // Удаление ненужных связей персонажей с событием. Допустим было [1,2], мы передали [2,3], значит будет [2]
                foreach (var character in characters)
                {
                    if(!eventData.IdCharacters.Contains(character.IdCharacter))
                    {
                        _unitOfWork.BelongToEvents.Delete(character.IdCharacter);
                    }    
                    
                }
                // Добавление новых. То есть после этого уже будет [2,3]
                foreach (var idCharacter in eventData.IdCharacters)
                {
                    var character = _unitOfWork.Characters.Get(idCharacter);
                    var belongToEvents = _unitOfWork.BelongToEvents.Find(b => b.IdCharacter == character.IdCharacter && b.IdEvent == id);
                    //если такой персонаж существует и в белонгтуивант нет записи с этим персонажем и ивентом
                    if (character != null && belongToEvents.Count()==0)
                    {
                        var belong = new BelongToEvent()
                        {
                            IdEvent = id,
                            IdCharacter=character.IdCharacter,
                        };
                        _unitOfWork.BelongToEvents.Create(belong);
                    }
                }
            }

            _unitOfWork.Events.Update(@event);
            _unitOfWork.Save();

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
            var @event = _unitOfWork.Events.Get(id);

            if (@event == null)
            {
                throw new KeyNotFoundException();
            }

            // Получение всех таймлайнов, связанных с событием
            var belongToTimeline = _unitOfWork.BelongToTimelines.Find(t=>t.IdEvent == id).ToList();

            // Удаление IdConnection удаляемой связи из схем
            foreach (var timeline in belongToTimeline)
            {
                _unitOfWork.BelongToTimelines.Delete(timeline.IdEvent);
            }

            _unitOfWork.Events.Delete(id);
            _unitOfWork.Save();

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
            var @event = _unitOfWork.Events.Get(id);

            if (@event == null)
            {
                throw new KeyNotFoundException();
            }
            var eventdata = _mapper.Map<EventData>(@event);

            var characters = _unitOfWork.BelongToEvents.Find(b => b.IdEvent == id).ToList();
            int[] ints = new int[characters.Count];
            for(int i = 0;i<ints.Length;i++)
                ints[i] = characters[i].IdCharacter;
            eventdata.IdCharacters = ints;
            return @eventdata;
        }

        /// <summary>
        /// Получает все события для указанного таймлайна.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <returns>Список всех событий таймлайна.</returns>
        public async Task<IEnumerable<EventAllData>> GetAllEvents(int id)
        {
            
            var events = _unitOfWork.Events.GetAll(id).ToList();
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
