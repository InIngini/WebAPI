using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class BookCharacterData : IMapWith<Character>
    {
        public int IdBook { get; set; }
        public int? IdPicture { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookCharacterData, Character>()
                .ForMember(dest => dest.IdBook, opt => opt.MapFrom(src => src.IdBook))
                .ForMember(dest => dest.IdPicture, opt => opt.MapFrom(src => src.IdPicture))
                .ForMember(dest => dest.IdCharacter, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
