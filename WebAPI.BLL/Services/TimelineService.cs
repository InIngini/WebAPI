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
    public class TimelineService : ITimelineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimelineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Timeline> CreateTimeline(Timeline timeline)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Timelines.Create(timeline);
            _unitOfWork.Save();

            return timeline;
        }

        public async Task<Timeline> UpdateTimeline(Timeline timeline)
        {
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
            var timelines = await _unitOfWork.Timelines.Find(t => t.IdBook == idBook).ToListAsync();

            return timelines;
        }
    }

}
