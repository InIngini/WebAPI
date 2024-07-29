using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class UserBookData : IMapWith<Book>
    {
        public int idUser { get; set; }
        public string NameBook { get; set; }
        public int? IdPicture { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserBookData, Book>()
                .ForMember(dest => dest.IdBook, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
