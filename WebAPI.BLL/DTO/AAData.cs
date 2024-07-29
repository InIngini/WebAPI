using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class AAData : IMapWith<AddedAttribute>
    {
        public int NumberAnswer { get; set; }
        public string NameAttribute { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AAData, AddedAttribute>()
                .ForMember(dest => dest.ContentAttribute, opt => opt.Ignore())
                .ForMember(dest => dest.IdAttribute, opt => opt.Ignore())
                .ForMember(dest => dest.IdCharacter, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
