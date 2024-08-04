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
    /// Класс, представляющий изображения в галерее персонажей.
    /// </summary>
    public class Gallery
    {
        /// <summary>
        /// Уникальный идентификатор изображения (может быть null).
        /// </summary>
        [Key]
        public int? IdPicture { get; set; }

        /// <summary>
        /// Связанное изображение.
        /// </summary>
        [ForeignKey(nameof(IdPicture))]
        public Picture? Picture { get; set; }

        /// <summary>
        /// Идентификатор персонажа, к которому относится изображение.
        /// </summary>
        public int IdCharacter { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        [ForeignKey(nameof(IdCharacter))]
        public Character Character { get; set; }
    }
}
