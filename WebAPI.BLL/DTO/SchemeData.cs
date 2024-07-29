using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class SchemeData : IMapWith<Scheme>
    {
        public int IdBook { get; set; }
        public string NameScheme { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SchemeData, Scheme>()
                .ForMember(dest => dest.IdScheme, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
