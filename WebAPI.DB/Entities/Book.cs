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
    /// Класс, представляющий книгу.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Уникальный идентификатор книги.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название книги.
        /// </summary>
        public string NameBook { get; set; }

        /// <summary>
        /// Уникальный идентификатор изображения, связанного с книгой (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Связанное изображение книги.
        /// </summary>
        [ForeignKey(nameof(PictureId))]
        public Picture? Picture { get; set; }
    }
}
