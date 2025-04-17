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
    /// Сервис для управления изображениями.
    /// </summary>
    public class PictureService : IPictureService
    {
        private readonly IContext Context;
        private readonly IMapper Mapper;
        private DeletionRepository DeletionRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PictureService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public PictureService(IContext context, IMapper mapper, DeletionRepository deletionRepository)
        {
            Context = context;
            Mapper = mapper;
            DeletionRepository = deletionRepository;
        }

        /// <summary>
        /// Создает новое изображение.
        /// </summary>
        /// <param name="picture">Информация о изображении для создания.</param>
        /// <returns>Созданное изображение.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Picture> CreatePicture(Picture picture)
        {
            var validationContext = new ValidationContext(picture);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(picture, validationContext, validationResults, true))
            {
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            Context.Pictures.Add(picture);
            await Context.SaveChangesAsync();

            return picture;
        }

        /// <summary>
        /// Удаляет изображение по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <returns>Удаленное изображение.</returns>
        /// <exception cref="KeyNotFoundException">Если изображение не найдено.</exception>
        public async Task<Picture> DeletePicture(int id)
        {
            var picture = await Context.Pictures.FirstOrDefaultAsync(x => x.Id == id);

            if (picture == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Изображение", 2));
            }

            await DeletionRepository.DeletePicture(id, Context);

            return picture;
        }

        /// <summary>
        /// Получает изображение по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Запрашиваемое изображение.</returns>
        /// <exception cref="KeyNotFoundException">Если изображение не найдено.</exception>
        public async Task<Picture> GetPicture(int id, CancellationToken cancellationToken)
        {
            var picture = await Context.Pictures.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (picture == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Изображение", 2));
            }

            return picture;
        }
    }

}
