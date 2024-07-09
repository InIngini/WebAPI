﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface ITimelineService
    {
        Task<Timeline> CreateTimeline(Timeline timeline);
        Task<Timeline> UpdateTimeline(Timeline timeline);
        Task<Timeline> DeleteTimeline(int id);
        Task<Timeline> GetTimeline(int id);
        Task<IEnumerable<Timeline>> GetAllTimelines(int idBook);
    }

}
