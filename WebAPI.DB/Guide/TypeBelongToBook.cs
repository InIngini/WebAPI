using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Guide
{
    /// <summary>
    /// Класс, представляющий тип принадлежности к книги.
    /// </summary>
    public class TypeBelongToBook
    {
        /// <summary>
        /// Уникальный идентификатор типа принадлежности.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название типа принадлежности (например: автор, соавтор).
        /// </summary>
        public string Name { get; set; }
    }
}
