using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий связь между персонажем и событием.
    /// </summary>
    [PrimaryKey(nameof(IdCharacter), nameof(IdEvent))]
    public class BelongToEvent
    {
        /// <summary>
        /// Уникальный идентификатор персонажа.
        /// </summary>
        public int IdCharacter { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        [ForeignKey(nameof(IdCharacter))]
        public Character Character { get; set; }

        /// <summary>
        /// Уникальный идентификатор события.
        /// </summary>
        public int IdEvent { get; set; }

        /// <summary>
        /// Связанное событие.
        /// </summary>
        [ForeignKey(nameof(IdEvent))]
        public Event Event { get; set; }
    }

}
