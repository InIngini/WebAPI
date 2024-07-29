using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class LoginData : IMapWith<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        // Реализация метода Mapping
        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginData, User>()
                .ForMember(dest => dest.IdUser, opt => opt.Ignore())
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password)); // Если IdUser генерируется в базе данных, то можно игнорировать
        }
    }
}
