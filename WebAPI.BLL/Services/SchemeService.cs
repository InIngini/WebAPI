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
    /// Сервис для управления схемами.
    /// </summary>
    public class SchemeService : ISchemeService
    {
        private readonly IContext Context;
        private readonly IMapper Mapper;
        private DeletionRepository DeletionRepository;
        private CreationRepository CreationRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SchemeService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public SchemeService(IContext context, IMapper mapper, CreationRepository creationRepository, DeletionRepository deletionRepository)
        {
            Context = context;
            Mapper = mapper;
            DeletionRepository = deletionRepository;
            CreationRepository = creationRepository;
        }

        /// <summary>
        /// Создает новую схему.
        /// </summary>
        /// <param name="schemedata">Данные для создания схемы.</param>
        /// <returns>Созданная схема.</returns>
        /// <exception cref="ArgumentException">Если модели не валидны.</exception>
        public async Task<Scheme> CreateScheme(SchemeData schemedata)
        {
            var scheme = Mapper.Map<Scheme>(schemedata);
            var validationContext = new ValidationContext(scheme);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(scheme, validationContext, validationResults, true))
            {
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            await CreationRepository.CreateScheme(scheme, Context);

            return scheme;
        }

        /// <summary>
        /// Обновляет схему и добавляет связь.
        /// </summary>
        /// <param name="scheme">Схема, которую необходимо обновить.</param>
        /// <param name="ConnectionId">Идентификатор связи.</param>
        /// <returns>Обновленная схема.</returns>
        /// <exception cref="KeyNotFoundException">Если связь не найдена.</exception>
        public async Task<Scheme> UpdateScheme(int SchemeId, int ConnectionId)
        {
            var scheme = await Context.Schemes.FirstOrDefaultAsync(x => x.Id == SchemeId);
            if (scheme == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Схема", 0));
            }
            await CreationRepository.CreateBelongToScheme(ConnectionId, SchemeId,Context);

            return scheme;
        }

        /// <summary>
        /// Удаляет схему по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <returns>Удаленная схема.</returns>
        /// <exception cref="KeyNotFoundException">Если схема не найдена.</exception>
        public async Task<Scheme> DeleteScheme(int id)
        {
            var scheme = await Context.Schemes.FirstOrDefaultAsync(x => x.Id == id);

            if (scheme == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Схема", 0));
            }

            await DeletionRepository.DeleteScheme(id, Context);

            return scheme;
        }

        /// <summary>
        /// Получает схему по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Запрашиваемая схема.</returns>
        /// <exception cref="KeyNotFoundException">Если схема не найдена.</exception>
        public async Task<Scheme> GetScheme(int id, CancellationToken cancellationToken)
        {
            var scheme = await Context.Schemes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (scheme == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Схема", 0));
            }

            return scheme;
        }

        /// <summary>
        /// Получает все схемы для указанной книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех схем книги.</returns>
        public async Task<IEnumerable<Scheme>> GetAllSchemes(int idBook, CancellationToken cancellationToken)
        {
            var schemes = await Context.Schemes.Where(s => s.BookId == idBook).ToListAsync(cancellationToken);

            return schemes;
        }
    }

}
