using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о событии, включая информацию о связанных книгах и персонажах.
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// Идентификатор книги (может быть null).
        /// </summary>
        public int? BookId { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержимое события.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Время события.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Массив идентификаторов персонажей, участвующих в событии (может быть null).
        /// </summary>
        public int[]? CharactersId { get; set; }

    }
}
