using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о записи в галерее.
    /// </summary>
    public class GalleryData : IMapWith<Gallery>
    {
        /// <summary>
        /// Идентификатор персонажа, к которому относится галерея.
        /// </summary>
        public int IdCharacter { get; set; }

        /// <summary>
        /// Идентификатор изображения в галерее.
        /// </summary>
        public int IdPicture { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Gallery"/> и <see cref="GalleryData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GalleryData, Gallery>(); // Если IdUser генерируется в базе данных, то можно игнорировать
        }

    }
}
