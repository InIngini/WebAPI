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
    public class SchemeDataToSchemeProfile : Profile
    {
        public SchemeDataToSchemeProfile()
        {
            CreateMap<SchemeData, Scheme>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
