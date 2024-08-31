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
    public class CharacterToCharacterWithAnswersProfile : Profile
    {
        public CharacterToCharacterWithAnswersProfile()
        {
            CreateMap<Character, CharacterWithAnswers>()
                .ForMember(dest => dest.Answers, opt => opt.Ignore()); // Игнорировать, если Id генерируется в базе данных
        }
    }
}
