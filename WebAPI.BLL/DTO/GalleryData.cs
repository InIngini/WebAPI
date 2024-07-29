using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class GalleryData : IMapWith<Gallery>
    {
        public int IdCharacter { get; set; }
        public int IdPicture { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GalleryData, Gallery>(); // Если IdUser генерируется в базе данных, то можно игнорировать
        }

    }
}
