using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Guide
{
    /// <summary>
    /// Класс, представляющий блок для вопросов и ответов.
    /// </summary>
    public class NumberBlock
    {
        /// <summary>
        /// Уникальный идентификатор блока.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название блока.
        /// </summary>
        public string Name { get; set; }
    }
}
