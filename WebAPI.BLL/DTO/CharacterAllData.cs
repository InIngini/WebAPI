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
    public class CharacterAllData : IMapWith<Character>
    {
        public int IdCharacter { get; set; }

        public string Name { get; set; }

        public byte[]? Picture1 { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Character, CharacterAllData>()
                .ForMember(dest => dest.IdCharacter, opt => opt.MapFrom(src => src.IdCharacter))
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Picture1, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
