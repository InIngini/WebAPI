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
    public class UserTokenData : IMapWith<User>
    {
        public int IdUser { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Token { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserTokenData>()
             .ForMember(dest => dest.Token, opt => opt.Ignore()); // Игнорируем, если это не нужно в маппинге
        }
    }
}
