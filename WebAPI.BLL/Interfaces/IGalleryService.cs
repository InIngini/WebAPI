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
        Task<BelongToGallery> CreateGallery(GalleryData galleryData);
        Task<BelongToGallery> DeletePictureFromGallery(int idPicture);
        Task<BelongToGallery> GetGallery(int id, CancellationToken cancellationToken);
        Task<IEnumerable<BelongToGallery>> GetAllGalleries(int idCharacter, CancellationToken cancellationToken);
    }

}
