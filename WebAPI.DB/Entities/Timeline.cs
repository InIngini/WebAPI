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
    /// Класс, представляющий таймлайн.
    /// </summary>
    public class Timeline
    {
        /// <summary>
        /// Уникальный идентификатор временной шкалы.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название временной шкалы.
        /// </summary>
        public string NameTimeline { get; set; }

        /// <summary>
        /// Идентификатор книги, к которой относится временная шкала.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Связанная книга.
        /// </summary>
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
    }
}
