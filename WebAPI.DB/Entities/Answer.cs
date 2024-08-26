using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Guide;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий ответ.
    /// </summary>
    [PrimaryKey(nameof(CharacterId), nameof(QuestionId))]
    public class Answer
    {
        /// <summary>
        /// Уникальный идентификатор персонажа, к которому относится ответ.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        [ForeignKey(nameof(CharacterId))]
        public Character Character { get; set; }

        /// <summary>
        /// Уникальный идентификатор вопроса, к которому относится ответ.
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Связанный вопрос.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        /// <summary>
        /// Ответ к вопросу.
        /// </summary>
        public string AnswerText { get; set; }
        
    }
}
