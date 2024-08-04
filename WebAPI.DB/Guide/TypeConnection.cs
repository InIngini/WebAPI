using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Guide
{
    /// <summary>
    /// Класс, представляющий тип связи.
    /// </summary>
    public class TypeConnection
    {
        /// <summary>
        /// Уникальный идентификатор типа связи.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название типа связи (например: партнер, ребенок-родитель, сиблинг).
        /// </summary>
        public string Name { get; set; }
    }
}
