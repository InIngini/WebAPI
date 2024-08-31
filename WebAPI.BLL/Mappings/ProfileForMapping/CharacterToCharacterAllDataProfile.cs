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
    public class CharacterToCharacterAllDataProfile : Profile
    {
        public CharacterToCharacterAllDataProfile()
        {
            CreateMap<Character, CharacterAllData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.Ignore()) // Игнорирует имя
                .ForMember(dest => dest.PictureContent, opt => opt.Ignore()); // Игнорирует изображение
        }
    }
}
