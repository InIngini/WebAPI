using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IPictureService
    {
        Task<Picture> CreatePicture(Picture picture);
        Task<Picture> DeletePicture(int id);
        Task<Picture> GetPicture(int id);
    }

}
