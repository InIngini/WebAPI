using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class EventData : IMapWith<Event>
    {
        public int? IdBook { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public int[]? IdCharacters { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventData>(); 
            profile.CreateMap<EventData, Event>()
                .ForMember(dest => dest.IdEvent, opt => opt.Ignore());// Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
