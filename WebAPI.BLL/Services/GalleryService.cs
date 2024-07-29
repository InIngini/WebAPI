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
using AutoMapper;

namespace WebAPI.BLL.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GalleryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Gallery> CreateGallery(GalleryData galleryData)
        {
            //Gallery gallery = new Gallery()
            //{
            //    IdCharacter = galleryData.IdCharacter,
            //    IdPicture = galleryData.IdPicture,
            //};
            var gallery = _mapper.Map<Gallery>(galleryData);
            var validationContext = new ValidationContext(gallery);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(gallery, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Galleries.Create(gallery);
            _unitOfWork.Save();

            return gallery;
        }

        public async Task<Gallery> DeletePictureFromGallery(int idPicture)
        {
            // Получение галереи из базы данных
            var gallery = _unitOfWork.Galleries.Get(idPicture);

            // Если галерея не найдена, вернуть ошибку
            if (gallery == null)
            {
                throw new KeyNotFoundException();
            }

            //Получение картинки из галереи
            var picture = _unitOfWork.Pictures.Get(idPicture);

            // Если картинка не найдена, вернуть ошибку
            if (picture == null)
            {
                throw new KeyNotFoundException();
            }

            // Удаление записи из галереи
            _unitOfWork.Galleries.Delete(idPicture);
            _unitOfWork.Save();

            // Удаление картинки из галереи
            _unitOfWork.Pictures.Delete(idPicture);
            _unitOfWork.Save();

            return gallery;
        }

        public async Task<Gallery> GetGallery(int id)
        {
            var gallery = _unitOfWork.Galleries.Get(id);

            if (gallery == null)
            {
                throw new KeyNotFoundException();
            }

            return gallery;
        }

        public async Task<IEnumerable<Gallery>> GetAllGalleries(int idCharacter)
        {
            var galleries = _unitOfWork.Galleries.Find(g => g.IdCharacter == idCharacter).ToList();

            return galleries;
        }
    }

}
