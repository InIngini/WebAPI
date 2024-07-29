using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class EventAllData : IMapWith<Event>
    {
        public int IdEvent { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventAllData>()
                ; // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
