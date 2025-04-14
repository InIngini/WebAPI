using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные для добавленного атрибута.
    /// </summary>
    public class AddedAttributeData
    {
        /// <summary>
        /// Номер ответа атрибута.
        /// </summary>
        public int NumberAnswer { get; set; }

        /// <summary>
        /// Имя атрибута.
        /// </summary>
        public string NameAttribute { get; set; }

    }
}
