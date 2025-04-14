using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Guide
{
    /// <summary>
    /// Класс, представляющий пол.
    /// </summary>
    public class Sex
    {
        /// <summary>
        /// Уникальный идентификатор пола.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название пола (например, "М", "Ж", "Не указан").
        /// </summary>
        public string Name { get; set; }
    }
}
