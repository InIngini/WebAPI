using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные таймлайна.
    /// </summary>
    public class TimelineData
    {
        /// <summary>
        /// Идентификатор книги, к которой относится таймлайн.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Название таймлайна.
        /// </summary>
        public string NameTimeline { get; set; }

    }
}
