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
    /// <summary>
    /// Данные вопроса.
    /// </summary>
    public class QuestionData : IMapWith<Question>
    {
        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название вопроса.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Блок, к которому принадлежит вопрос (может быть null).
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Question"/> и <see cref="QuestionData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Question, QuestionData>()
                .ForMember(dest => dest.Block, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
