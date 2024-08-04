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
    /// Класс, представляющий персонажа книги.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Уникальный идентификатор персонажа.
        /// </summary>
        [Key]
        public int IdCharacter { get; set; }

        /// <summary>
        /// Идентификатор книги, к которой принадлежит персонаж.
        /// </summary>
        public int IdBook { get; set; }

        /// <summary>
        /// Связанная книга, к которой принадлежит персонаж.
        /// </summary>
        [ForeignKey(nameof(IdBook))]
        public Book Book { get; set; }

        /// <summary>
        /// Уникальный идентификатор изображения, связанного с персонажем (может быть null).
        /// </summary>
        public int? IdPicture { get; set; }

        /// <summary>
        /// Связанное изображение персонажа.
        /// </summary>
        [ForeignKey(nameof(IdPicture))]
        public Picture? Picture { get; set; }
    }
}
