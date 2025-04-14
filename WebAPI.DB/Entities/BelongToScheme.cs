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
    /// Класс, представляющий связь между схемой и связью.
    /// </summary>
    [PrimaryKey(nameof(SchemeId), nameof(ConnectionId))]
    public class BelongToScheme
    {
        /// <summary>
        /// Уникальный идентификатор схемы.
        /// </summary>
        public int SchemeId { get; set; }

        /// <summary>
        /// Связанная схема.
        /// </summary>
        [ForeignKey(nameof(SchemeId))]
        public Scheme Scheme { get; set; }

        /// <summary>
        /// Уникальный идентификатор связи.
        /// </summary>
        public int ConnectionId { get; set; }

        /// <summary>
        /// Связанная связь.
        /// </summary>
        [ForeignKey(nameof(ConnectionId))]
        public Connection Connection { get; set; }
    }

}
