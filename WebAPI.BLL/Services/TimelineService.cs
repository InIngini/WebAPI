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

namespace WebAPI.BLL.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimelineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Timeline> CreateTimeline(Timeline timeline)
        {
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
            timeline.IdEvents.Add(@event);
            
            _unitOfWork.Timelines.Update(timeline);
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
