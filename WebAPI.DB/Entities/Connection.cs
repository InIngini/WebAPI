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
        public int IdConnection { get; set; }

        /// <summary>
        /// Тип связи между персонажами.
        /// </summary>
        public int TypeConnection { get; set; }

        /// <summary>
        /// Идентификатор первого персонажа в связи.
        /// </summary>
        public int IdCharacter1 { get; set; }

        /// <summary>
        /// Связанный первый персонаж.
        /// </summary>
        [ForeignKey(nameof(IdCharacter1))]
        public Character Character1 { get; set; }

        /// <summary>
        /// Идентификатор второго персонажа в связи.
        /// </summary>
        public int IdCharacter2 { get; set; }

        /// <summary>
        /// Связанный второй персонаж.
        /// </summary>
        [ForeignKey(nameof(IdCharacter2))]
        public Character Character2 { get; set; }
    }
}
