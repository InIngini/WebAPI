using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Mappings.ProfileForMapping
{
    public class AddedAttributeDataToAddedAttributeProfile : Profile
    {
        public AddedAttributeDataToAddedAttributeProfile()
        {
            CreateMap<AddedAttributeData, AddedAttribute>()
                .ForMember(dest => dest.ContentAttribute, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CharacterId, opt => opt.Ignore()); // Игнорировать, если Id генерируется в базе данных
        }
    }

}
