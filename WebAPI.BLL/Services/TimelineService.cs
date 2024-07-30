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
using AutoMapper;

namespace WebAPI.BLL.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TimelineService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Timeline> CreateTimeline(TimelineData timelinedata)
        {
            //Timeline timeline = new Timeline()
            //{
            //    IdBook = timelinedata.IdBook,
            //    NameTimeline = timelinedata.NameTimeline,
            //};
            var timeline = _mapper.Map<Timeline>(timelinedata);

            var validationContext = new ValidationContext(timeline);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(timeline, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Timelines.Create(timeline);
            _unitOfWork.Save();

            return timeline;
        }

        public async Task<Timeline> UpdateTimeline(Timeline timeline, int idEvent)
        {
            // Поиск события по указанному идентификатору
            var @event = _unitOfWork.Events.Get(idEvent);

            // Если событие не найдено, вернуть ошибку
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
            _unitOfWork.BelongToTimelines.Create(belongToTimeline);
            _unitOfWork.Save();

            return timeline;
        }

        public async Task<Timeline> DeleteTimeline(int id)
        {
            var timeline = _unitOfWork.Timelines.Get(id);

            if (timeline == null)
            {
                throw new KeyNotFoundException();
            }
            var belongToTimelines = _unitOfWork.BelongToTimelines.Find(b => b.IdTimeline == id).ToList();
            foreach (var belongToTimeline in belongToTimelines)
            {
                _unitOfWork.BelongToTimelines.Delete(id, belongToTimeline.IdEvent);
            }
            _unitOfWork.Save();

            _unitOfWork.Timelines.Delete(id);
            _unitOfWork.Save();

            return timeline;
        }

        public async Task<Timeline> GetTimeline(int id)
        {
            var timeline = _unitOfWork.Timelines.Get(id);

            if (timeline == null)
            {
                throw new KeyNotFoundException();
            }

            return timeline;
        }

        public async Task<IEnumerable<Timeline>> GetAllTimelines(int idBook)
        {
            var timelines = _unitOfWork.Timelines.Find(t => t.IdBook == idBook)
                                                 .ToList();

            return timelines;
        }
    }

}
