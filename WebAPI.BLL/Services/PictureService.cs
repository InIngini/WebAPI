using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.BLL.Services
{
    public class PictureService : IPictureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PictureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Picture> CreatePicture(Picture picture)
        {
            var validationContext = new ValidationContext(picture);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(picture, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Pictures.Create(picture);
            _unitOfWork.Save();

            return picture;
        }

        public async Task<Picture> DeletePicture(int id)
        {
            var picture = _unitOfWork.Pictures.Get(id);

            if (picture == null)
            {
                throw new KeyNotFoundException();
            }

            _unitOfWork.Pictures.Delete(id);
            _unitOfWork.Save();

            return picture;
        }

        public async Task<Picture> GetPicture(int id)
        {
            var picture = _unitOfWork.Pictures.Get(id);

            if (picture == null)
            {
                throw new KeyNotFoundException();
            }

            return picture;
        }
    }

}
