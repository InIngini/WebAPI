using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.BLL.Mappings.ProfileForMapping
{
    public class UserBookDataToBookProfile : Profile
    {
        public UserBookDataToBookProfile()
        {
            CreateMap<UserBookData, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Игнорируем, если это не нужно в маппинге
        }
    }
}
