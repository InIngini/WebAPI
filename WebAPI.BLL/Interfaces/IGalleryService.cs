using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IGalleryService
    {
        Task<Gallery> CreateGallery(Gallery gallery);
        Task<Gallery> DeletePictureFromGallery(int idPicture);
        Task<Gallery> GetGallery(int id);
        Task<IEnumerable<Gallery>> GetAllGalleries(int idCharacter);
    }

}
