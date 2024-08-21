using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DB;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления галереями.
    /// </summary>
    public class GalleryService : IGalleryService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GalleryService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public GalleryService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новую галерею.
        /// </summary>
        /// <param name="galleryData">Данные для создания галереи.</param>
        /// <returns>Созданная галерея.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Gallery> CreateGallery(GalleryData galleryData)
        {
            var gallery = _mapper.Map<Gallery>(galleryData);
            var validationContext = new ValidationContext(gallery);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(gallery, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _context.Galleries.Add(gallery);
            _context.SaveChanges();

            return gallery;
        }

        /// <summary>
        /// Удаляет изображение из галереи по идентификатору изображения.
        /// </summary>
        /// <param name="idPicture">Идентификатор изображения.</param>
        /// <returns>Галерея, из которой было удалено изображение.</returns>
        /// <exception cref="KeyNotFoundException">Если галерея или изображение не найдены.</exception>
        public async Task<Gallery> DeletePictureFromGallery(int idPicture)
        {
            var gallery = _context.Galleries.Find(idPicture);
            if (gallery == null)
            {
                throw new KeyNotFoundException();
            }

            var picture = _context.Pictures.Find(idPicture);
            if (picture == null)
            {
                throw new KeyNotFoundException();
            }

            // Удаление записи из галереи
            _context.Galleries.Remove(gallery);
            _context.SaveChanges();

            // Удаление картинки из галереи
            _context.Pictures.Remove(picture);
            _context.SaveChanges();

            return gallery;
        }

        /// <summary>
        /// Получает галерею по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор галереи.</param>
        /// <returns>Запрашиваемая галерея.</returns>
        /// <exception cref="KeyNotFoundException">Если галерея не найдена.</exception>
        public async Task<Gallery> GetGallery(int id)
        {
            var gallery = _context.Galleries.Find(id);

            if (gallery == null)
            {
                throw new KeyNotFoundException();
            }

            return gallery;
        }

        /// <summary>
        /// Получает все галереи для указанного персонажа.
        /// </summary>
        /// <param name="idCharacter">Идентификатор персонажа.</param>
        /// <returns>Список галерей персонажа.</returns>
        public async Task<IEnumerable<Gallery>> GetAllGalleries(int idCharacter)
        {
            var galleries = _context.Galleries.Where(g => g.IdCharacter == idCharacter).ToList();

            return galleries;
        }
    }

}
