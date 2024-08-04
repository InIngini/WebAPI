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
    /// Класс, представляющий связь между временной шкалой и событием.
    /// </summary>
    [PrimaryKey(nameof(IdTimeline), nameof(IdEvent))]
    public class BelongToTimeline
    {
        /// <summary>
        /// Уникальный идентификатор временной шкалы.
        /// </summary>
        public int IdTimeline { get; set; }

        /// <summary>
        /// Связанная временная шкала.
        /// </summary>
        [ForeignKey(nameof(IdTimeline))]
        public Timeline Timeline { get; set; }

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
