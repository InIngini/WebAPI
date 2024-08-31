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
    public class EventDataToEventProfile : Profile
    {
        public EventDataToEventProfile()
        {
            CreateMap<EventData, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Игнорируем IdEvent, если он генерируется в базе данных
        }
    }
}
