using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.BLL.DTO;

namespace WebAPI.BLL.Interfaces
{
    public interface IGalleryService
    {
        Task<Gallery> CreateGallery(GalleryData galleryData);
        Task<Gallery> DeletePictureFromGallery(int idPicture);
        Task<Gallery> GetGallery(int id);
        Task<IEnumerable<Gallery>> GetAllGalleries(int idCharacter);
    }

}
