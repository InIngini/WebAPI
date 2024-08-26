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
    [PrimaryKey(nameof(TimelineId), nameof(EventId))]
    public class BelongToTimeline
    {
        /// <summary>
        /// Уникальный идентификатор временной шкалы.
        /// </summary>
        public int TimelineId { get; set; }

        /// <summary>
        /// Связанная временная шкала.
        /// </summary>
        [ForeignKey(nameof(TimelineId))]
        public Timeline Timeline { get; set; }

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
