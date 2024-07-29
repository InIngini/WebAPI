using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;
using WebAPI.DB.Guide;

namespace WebAPI.BLL.DTO
{
    public class QuestionData : IMapWith<QuestionData>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Block { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Question, QuestionData>()
                .ForMember(dest => dest.Block, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
