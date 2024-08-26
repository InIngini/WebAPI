using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о записи в галерее.
    /// </summary>
    public class GalleryData : IMapWith<BelongToGallery>
    {
        /// <summary>
        /// Идентификатор персонажа, к которому относится галерея.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Идентификатор изображения в галерее.
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="BelongToGallery"/> и <see cref="GalleryData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GalleryData, BelongToGallery>(); // Если IdUser генерируется в базе данных, то можно игнорировать
        }

    }
}
