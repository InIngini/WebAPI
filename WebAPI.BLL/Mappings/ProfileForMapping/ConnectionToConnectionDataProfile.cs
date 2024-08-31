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
    public class ConnectionToConnectionDataProfile : Profile
    {
        public ConnectionToConnectionDataProfile()
        {
            CreateMap<Connection, ConnectionData>()
                .ForMember(dest => dest.BookId, opt => opt.Ignore()) // Игнорируем IdBook
                .ForMember(dest => dest.Name1, opt => opt.Ignore())  // Игнорируем имя первого персонажа
                .ForMember(dest => dest.Name2, opt => opt.Ignore())  // Игнорируем имя второго персонажа
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore()); // Игнорируем TypeConnection
        }
    }
}
