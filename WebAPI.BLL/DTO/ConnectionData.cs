using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class ConnectionData : IMapWith<Connection>
    {
        public int? IdConnection { get; set; }
        public int? IdBook { get; set; }
        public int IdCharacter1 { get; set; }
        public string? Name1 { get; set; }
        public int IdCharacter2 { get; set; }
        public string? Name2 { get; set; }
        public string TypeConnection { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Connection, ConnectionData>()
                .ForMember(dest => dest.IdBook, opt => opt.Ignore())
                .ForMember(dest => dest.Name1, opt => opt.Ignore())
                .ForMember(dest => dest.Name2, opt => opt.Ignore())
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
            profile.CreateMap<ConnectionData, Connection>()
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore());
        }
    }
}
