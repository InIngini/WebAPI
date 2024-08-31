using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Mappings.ProfileForMapping
{
    public class ConnectionToConnectionAllDataProfile : Profile
    {
        public ConnectionToConnectionAllDataProfile()
        {
            CreateMap<Connection, ConnectionAllData>()
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore()); // Игнорируем TypeConnection, если значения генерируются
        }
    }
}
