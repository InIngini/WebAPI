using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о связи между персонажами и, возможно, их отношение к книге.
    /// </summary>
    public class ConnectionData
    {
        /// <summary>
        /// Идентификатор связи (может быть null).
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Идентификатор книги (может быть null).
        /// </summary>
        public int? BookId { get; set; }

        /// <summary>
        /// Идентификатор первого персонажа.
        /// </summary>
        public int Character1Id { get; set; }

        /// <summary>
        /// Имя первого персонажа (может быть null).
        /// </summary>
        public string? Name1 { get; set; }

        /// <summary>
        /// Идентификатор второго персонажа.
        /// </summary>
        public int Character2Id { get; set; }

        /// <summary>
        /// Имя второго персонажа (может быть null).
        /// </summary>
        public string? Name2 { get; set; }

        /// <summary>
        /// Тип связи (например: партнер, ребенок-родитель, сиблинг).
        /// </summary>
        public string TypeConnection { get; set; }

    }
}
