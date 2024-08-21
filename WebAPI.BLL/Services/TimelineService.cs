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
using AutoMapper;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления таймлайнами.
    /// </summary>
    public class TimelineService : ITimelineService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TimelineService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public TimelineService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новый таймлайн.
        /// </summary>
        /// <param name="timelinedata">Данные для создания таймлайна.</param>
        /// <returns>Созданный таймлайн.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Timeline> CreateTimeline(TimelineData timelinedata)
        {
            var timeline = _mapper.Map<Timeline>(timelinedata);

            var validationContext = new ValidationContext(timeline);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(timeline, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _context.Timelines.Add(timeline);
            _context.SaveChanges();

            return timeline;
        }

        /// <summary>
        /// Обновляет таймлайн, добавляя событие.
        /// </summary>
        /// <param name="timeline">Таймлайн, который необходимо обновить.</param>
        /// <param name="idEvent">Идентификатор события.</param>
        /// <returns>Обновленный таймлайн.</returns>
        /// <exception cref="KeyNotFoundException">Если событие не найдено.</exception>
        public async Task<Timeline> UpdateTimeline(Timeline timeline, int idEvent)
        {
            var @event = _context.Events.Find(idEvent);
            if (@event == null)
            {
                throw new KeyNotFoundException();
            }

            // Добавление события в таймлайн
            var belongToTimeline = new BelongToTimeline()
            {
                IdEvent = idEvent,
                IdTimeline = timeline.IdTimeline
            };
            _context.BelongToTimelines.Add(belongToTimeline);
            _context.SaveChanges();

            return timeline;
        }

        /// <summary>
        /// Удаляет таймлайн по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <returns>Удаленный таймлайн.</returns>
        /// <exception cref="KeyNotFoundException">Если таймлайн не найден.</exception>
        public async Task<Timeline> DeleteTimeline(int id)
        {
            var timeline = _context.Timelines.Find(id);
            if (timeline == null)
            {
                throw new KeyNotFoundException();
            }

            var belongToTimelines = _context.BelongToTimelines.Where(b => b.IdTimeline == id).ToList();
            foreach (var belongToTimeline in belongToTimelines)
            {
                _context.BelongToTimelines.Remove(belongToTimeline);
            }
            _context.SaveChanges();

            _context.Timelines.Remove(timeline);
            _context.SaveChanges();

            return timeline;
        }

        /// <summary>
        /// Получает таймлайн по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <returns>Запрашиваемый таймлайн.</returns>
        /// <exception cref="KeyNotFoundException">Если таймлайн не найден.</exception>
        public async Task<Timeline> GetTimeline(int id)
        {
            var timeline = _context.Timelines.Find(id);

            if (timeline == null)
            {
                throw new KeyNotFoundException();
            }

            return timeline;
        }

        /// <summary>
        /// Получает все таймлайны для указанной книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <returns>Список всех таймлайнов книги.</returns>
        public async Task<IEnumerable<Timeline>> GetAllTimelines(int idBook)
        {
            var timelines = _context.Timelines.Where(t => t.IdBook == idBook)
                                                 .ToList();

            return timelines;
        }
    }

}
