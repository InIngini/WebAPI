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
    [PrimaryKey(nameof(CharacterId), nameof(EventId))]
    public class BelongToEvent
    {
        /// <summary>
        /// Уникальный идентификатор персонажа.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        [ForeignKey(nameof(CharacterId))]
        public Character Character { get; set; }

        /// <summary>
        /// Уникальный идентификатор события.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Связанное событие.
        /// </summary>
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
    }

}
