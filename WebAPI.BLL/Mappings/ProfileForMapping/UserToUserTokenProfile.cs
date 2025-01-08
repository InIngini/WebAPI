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
    public class UserToUserTokenProfile : Profile
    {
        public UserToUserTokenProfile()
        {
            CreateMap<User, UserTokenData>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Token, opt => opt.Ignore()); // Игнорируем, если это не нужно в маппинге
        }
    }
}
