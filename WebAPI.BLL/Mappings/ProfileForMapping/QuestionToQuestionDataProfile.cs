using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Guide;

namespace WebAPI.BLL.Mappings.ProfileForMapping
{
    public class QuestionToQuestionDataProfile : Profile
    {
        public QuestionToQuestionDataProfile()
        {
            CreateMap<Question, QuestionData>()
                .ForMember(dest => dest.Block, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
