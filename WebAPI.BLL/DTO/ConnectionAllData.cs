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
    public class ConnectionAllData : IMapWith<Connection>
    {
        public int IdConnection { get; set; }
        public int IdCharacter1 { get; set; }
        public int IdCharacter2 { get; set; }
        public string TypeConnection { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Connection, ConnectionAllData>()
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
