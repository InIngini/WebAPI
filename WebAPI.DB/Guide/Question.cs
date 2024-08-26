using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Guide
{
    /// <summary>
    /// Класс, представляющий вопрос.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Уникальный идентификатор вопроса.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название или текст вопроса.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Идентификатор блока, к которому принадлежит вопрос.
        /// </summary>
        public int Block { get; set; }

        /// <summary>
        /// Связанный блок номеров.
        /// </summary>
        [ForeignKey(nameof(Block))]
        public NumberBlock NumberBlock { get; set; }
    }
}
