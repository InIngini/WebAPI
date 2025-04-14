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
    /// Класс, представляющий связь между двумя персонажами.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Уникальный идентификатор связи.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Тип связи между персонажами.
        /// </summary>
        public int TypeConnection { get; set; }

        /// <summary>
        /// Идентификатор первого персонажа в связи.
        /// </summary>
        public int Character1Id { get; set; }

        /// <summary>
        /// Связанный первый персонаж.
        /// </summary>
        [ForeignKey(nameof(Character1Id))]
        public Character Character1 { get; set; }

        /// <summary>
        /// Идентификатор второго персонажа в связи.
        /// </summary>
        public int Character2Id { get; set; }

        /// <summary>
        /// Связанный второй персонаж.
        /// </summary>
        [ForeignKey(nameof(Character2Id))]
        public Character Character2 { get; set; }
    }
}
