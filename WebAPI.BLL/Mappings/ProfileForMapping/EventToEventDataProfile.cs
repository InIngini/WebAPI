using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.BLL.DTO;

namespace WebAPI.BLL.Mappings.ProfileForMapping
{
    public class EventToEventDataProfile : Profile
    {
        public EventToEventDataProfile()
        {
            CreateMap<Event, EventData>();
        }
    }
}
