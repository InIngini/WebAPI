using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий ответ.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Уникальный идентификатор персонажа, к которому относится ответ.
        /// </summary>
        [Key]
        public int IdCharacter { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        [ForeignKey(nameof(IdCharacter))]
        public Character Character { get; set; }

        /// <summary>
        /// Имя ответа.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ответ 1 по личным характеристикам.
        /// </summary>
        public string Answer1Personality { get; set; }

        /// <summary>
        /// Ответ 2 по личным характеристикам.
        /// </summary>
        public string Answer2Personality { get; set; }

        /// <summary>
        /// Ответ 3 по личным характеристикам.
        /// </summary>
        public string Answer3Personality { get; set; }

        /// <summary>
        /// Ответ 4 по личным характеристикам.
        /// </summary>
        public string Answer4Personality { get; set; }

        /// <summary>
        /// Ответ 5 по личным характеристикам.
        /// </summary>
        public string Answer5Personality { get; set; }

        /// <summary>
        /// Ответ 6 по личным характеристикам.
        /// </summary>
        public string Answer6Personality { get; set; }

        /// <summary>
        /// Ответ 1 по внешности.
        /// </summary>
        public string Answer1Appearance { get; set; }

        /// <summary>
        /// Ответ 2 по внешности.
        /// </summary>
        public string Answer2Appearance { get; set; }

        /// <summary>
        /// Ответ 3 по внешности.
        /// </summary>
        public string Answer3Appearance { get; set; }

        /// <summary>
        /// Ответ 4 по внешности.
        /// </summary>
        public string Answer4Appearance { get; set; }

        /// <summary>
        /// Ответ 5 по внешности.
        /// </summary>
        public string Answer5Appearance { get; set; }

        /// <summary>
        /// Ответ 6 по внешности.
        /// </summary>
        public string Answer6Appearance { get; set; }

        /// <summary>
        /// Ответ 7 по внешности.
        /// </summary>
        public string Answer7Appearance { get; set; }

        /// <summary>
        /// Ответ 8 по внешности.
        /// </summary>
        public string Answer8Appearance { get; set; }

        /// <summary>
        /// Ответ 9 по внешности.
        /// </summary>
        public string Answer9Appearance { get; set; }

        /// <summary>
        /// Ответ 1 по темпераменту.
        /// </summary>
        public string Answer1Temperament { get; set; }

        /// <summary>
        /// Ответ 2 по темпераменту.
        /// </summary>
        public string Answer2Temperament { get; set; }

        /// <summary>
        /// Ответ 3 по темпераменту.
        /// </summary>
        public string Answer3Temperament { get; set; }

        /// <summary>
        /// Ответ 4 по темпераменту.
        /// </summary>
        public string Answer4Temperament { get; set; }

        /// <summary>
        /// Ответ 5 по темпераменту.
        /// </summary>
        public string Answer5Temperament { get; set; }

        /// <summary>
        /// Ответ 6 по темпераменту.
        /// </summary>
        public string Answer6Temperament { get; set; }

        /// <summary>
        /// Ответ 7 по темпераменту.
        /// </summary>
        public string Answer7Temperament { get; set; }

        /// <summary>
        /// Ответ 8 по темпераменту.
        /// </summary>
        public string Answer8Temperament { get; set; }

        /// <summary>
        /// Ответ 9 по темпераменту.
        /// </summary>
        public string Answer9Temperament { get; set; }

        /// <summary>
        /// Ответ 10 по темпераменту.
        /// </summary>
        public string Answer10Temperament { get; set; }

        /// <summary>
        /// Ответ 1 по истории.

        /// </summary>
        public string Answer1ByHistory { get; set; }

        /// <summary>
        /// Ответ 2 по истории.
        /// </summary>
        public string Answer2ByHistory { get; set; }

        /// <summary>
        /// Ответ 3 по истории.
        /// </summary>
        public string Answer3ByHistory { get; set; }

        /// <summary>
        /// Ответ 4 по истории.
        /// </summary>
        public string Answer4ByHistory { get; set; }

        /// <summary>
        /// Ответ 5 по истории.
        /// </summary>
        public string Answer5ByHistory { get; set; }
    }
}
