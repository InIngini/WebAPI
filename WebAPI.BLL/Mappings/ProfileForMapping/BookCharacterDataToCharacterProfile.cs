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
    public class BookCharacterDataToCharacterProfile : Profile
    {
        public BookCharacterDataToCharacterProfile()
        {
            CreateMap<BookCharacterData, Character>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.PictureId, opt => opt.MapFrom(src => src.PictureId))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
