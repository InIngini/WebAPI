using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class TimelineData : IMapWith<Timeline>
    {
        public int IdBook { get; set; }
        public string NameTimeline { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TimelineData, Timeline>()
                .ForMember(dest => dest.IdTimeline, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
