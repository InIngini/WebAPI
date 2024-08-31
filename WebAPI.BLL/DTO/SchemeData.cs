using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные схемы.
    /// </summary>
    public class SchemeData
    {
        /// <summary>
        /// Идентификатор книги, к которой относится схема.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Название схемы.
        /// </summary>
        public string NameScheme { get; set; }
    }
}
