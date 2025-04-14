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
    public class QuestionData
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

    }
}
