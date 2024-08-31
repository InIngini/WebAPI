using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Mappings.ProfileForMapping
{
    public class EventToEventAllDataProfile : Profile
    {
        public EventToEventAllDataProfile()
        {
            CreateMap<Event, EventAllData>(); // Игнорируем IdEvent, если он генерируется в базе данных
        }
    }
}
