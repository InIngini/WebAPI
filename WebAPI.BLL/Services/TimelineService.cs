﻿using System;
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
using WebAPI.Errors;
using WebAPI.BLL.Additional;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления таймлайнами.
    /// </summary>
    public class TimelineService : ITimelineService
    {
        private readonly IContext Context;
        private readonly IMapper Mapper;
        private DeletionRepository DeletionRepository;
        private CreationRepository CreationRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TimelineService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public TimelineService(IContext context, IMapper mapper, CreationRepository creationRepository, DeletionRepository deletionRepository)
        {
            Context = context;
            Mapper = mapper;
            DeletionRepository = deletionRepository;
            CreationRepository = creationRepository;
        }

        /// <summary>
        /// Создает новый таймлайн.
        /// </summary>
        /// <param name="timelinedata">Данные для создания таймлайна.</param>
        /// <returns>Созданный таймлайн.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Timeline> CreateTimeline(TimelineData timelinedata)
        {
            var timeline = Mapper.Map<Timeline>(timelinedata);

            var validationContext = new ValidationContext(timeline);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(timeline, validationContext, validationResults, true))
            {
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            await CreationRepository.CreateTimeline(timeline, Context);

            return timeline;
        }

        /// <summary>
        /// Обновляет таймлайн, добавляя событие.
        /// </summary>
        /// <param name="TimelineId">Идентификатор таймлайна.</param>
        /// <param name="EventId">Идентификатор события.</param>
        /// <returns>Обновленный таймлайн.</returns>
        /// <exception cref="KeyNotFoundException">Если событие не найдено.</exception>
        public async Task<Timeline> UpdateTimeline(int TimelineId, int EventId)
        {
            var timeline = await Context.Timelines.FirstOrDefaultAsync(x => x.Id == TimelineId);
            if (timeline == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Таймлайн", 1));
            }

            await CreationRepository.CreateBelongToTimeline(EventId, TimelineId,Context);

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
            var timeline = await Context.Timelines.FirstOrDefaultAsync(x => x.Id == id);
            if (timeline == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Таймлайн", 0));
            }

            await DeletionRepository.DeleteTimeline(id, Context);

            return timeline;
        }

        /// <summary>
        /// Получает таймлайн по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Запрашиваемый таймлайн.</returns>
        /// <exception cref="KeyNotFoundException">Если таймлайн не найден.</exception>
        public async Task<Timeline> GetTimeline(int id, CancellationToken cancellationToken)
        {
            var timeline = await Context.Timelines.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (timeline == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Таймлайн", 0));
            }

            return timeline;
        }

        /// <summary>
        /// Получает все таймлайны для указанной книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех таймлайнов книги.</returns>
        public async Task<IEnumerable<Timeline>> GetAllTimelines(int idBook, CancellationToken cancellationToken)
        {
            var timelines = await Context.Timelines.Where(t => t.BookId == idBook)
                                                 .ToListAsync(cancellationToken);

            return timelines;
        }
    }

}
