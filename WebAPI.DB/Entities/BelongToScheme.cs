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
    [PrimaryKey(nameof(IdScheme), nameof(IdConnection))]
    public class BelongToScheme
    {
        /// <summary>
        /// Уникальный идентификатор схемы.
        /// </summary>
        public int IdScheme { get; set; }

        /// <summary>
        /// Связанная схема.
        /// </summary>
        [ForeignKey(nameof(IdScheme))]
        public Scheme Scheme { get; set; }

        /// <summary>
        /// Уникальный идентификатор связи.
        /// </summary>
        public int IdConnection { get; set; }

        /// <summary>
        /// Связанная связь.
        /// </summary>
        [ForeignKey(nameof(IdConnection))]
        public Connection Connection { get; set; }
    }

}
