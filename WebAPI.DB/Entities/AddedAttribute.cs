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
    /// Класс, представляющий добавленный атрибут.
    /// </summary>
    public class AddedAttribute
    {
        /// <summary>
        /// Уникальный идентификатор атрибута.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер ответа, связанный с атрибутом.
        /// </summary>
        public int NumberAnswer { get; set; }

        /// <summary>
        /// Название атрибута.
        /// </summary>
        public string NameAttribute { get; set; }

        /// <summary>
        /// Содержимое атрибута.
        /// </summary>
        public string ContentAttribute { get; set; }

        /// <summary>
        /// Идентификатор персонажа, к которому принадлежит атрибут.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        [ForeignKey(nameof(CharacterId))]
        public Character Character { get; set; }
    }

}
