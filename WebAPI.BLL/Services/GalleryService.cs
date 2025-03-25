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
using WebAPI.Errors;
using WebAPI.BLL.Additional;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления галереями.
    /// </summary>
    public class GalleryService : IGalleryService
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;
        private DeletionRepository DeletionRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GalleryService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public GalleryService(IContext context, IMapper mapper, DeletionRepository deletionRepository)
        {
            _context = context;
            _mapper = mapper;
            DeletionRepository = deletionRepository;
        }

        /// <summary>
        /// Создает новую галерею.
        /// </summary>
        /// <param name="galleryData">Данные для создания галереи.</param>
        /// <returns>Созданная галерея.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<BelongToGallery> CreateGallery(GalleryData galleryData)
        {
            var gallery = _mapper.Map<BelongToGallery>(galleryData);
            var validationContext = new ValidationContext(gallery);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(gallery, validationContext, validationResults, true))
            {
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            _context.BelongToGalleries.Add(gallery);
            await _context.SaveChangesAsync();

            return gallery;
        }

        /// <summary>
        /// Удаляет изображение из галереи по идентификатору изображения.
        /// </summary>
        /// <param name="PictureId">Идентификатор изображения.</param>
        /// <returns>Галерея, из которой было удалено изображение.</returns>
        /// <exception cref="KeyNotFoundException">Если галерея или изображение не найдены.</exception>
        public async Task<BelongToGallery> DeletePictureFromGallery(int PictureId)
        {
            var gallery = await _context.BelongToGalleries.FindAsync(PictureId);
            if (gallery == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Изображение", 2));
            }

            var picture = await _context.Pictures.FindAsync(PictureId);
            if (picture == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Изображение", 2));
            }

            await DeletionRepository.DeletePicture(PictureId, _context);

            return gallery;
        }

        /// <summary>
        /// Получает галерею по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор галереи.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Запрашиваемая галерея.</returns>
        /// <exception cref="KeyNotFoundException">Если галерея не найдена.</exception>
        public async Task<BelongToGallery> GetGallery(int id, CancellationToken cancellationToken)
        {
            var gallery = await _context.BelongToGalleries.FindAsync(id, cancellationToken);

            if (gallery == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Изображение", 2));
            }

            return gallery;
        }

        /// <summary>
        /// Получает все галереи для указанного персонажа.
        /// </summary>
        /// <param name="idCharacter">Идентификатор персонажа.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список галерей персонажа.</returns>
        public async Task<IEnumerable<BelongToGallery>> GetAllGalleries(int idCharacter, CancellationToken cancellationToken)
        {
            var galleries = await _context.BelongToGalleries.Where(g => g.CharacterId == idCharacter).ToListAsync(cancellationToken);

            return galleries;
        }
    }

}
