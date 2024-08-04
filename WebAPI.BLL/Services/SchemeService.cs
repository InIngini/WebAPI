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
    /// <summary>
    /// Сервис для управления схемами.
    /// </summary>
    public class SchemeService : ISchemeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SchemeService"/>.
        /// </summary>
        /// <param name="unitOfWork">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public SchemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новую схему.
        /// </summary>
        /// <param name="schemedata">Данные для создания схемы.</param>
        /// <returns>Созданная схема.</returns>
        /// <exception cref="ArgumentException">Если модели не валидны.</exception>
        public async Task<Scheme> CreateScheme(SchemeData schemedata)
        {
            var scheme = _mapper.Map<Scheme>(schemedata);
            var validationContext = new ValidationContext(scheme);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(scheme, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Schemes.Create(scheme);
            _unitOfWork.Save();

            return scheme;
        }

        /// <summary>
        /// Обновляет схему и добавляет связь.
        /// </summary>
        /// <param name="scheme">Схема, которую необходимо обновить.</param>
        /// <param name="idConnection">Идентификатор связи.</param>
        /// <returns>Обновленная схема.</returns>
        /// <exception cref="KeyNotFoundException">Если связь не найдена.</exception>
        public async Task<Scheme> UpdateScheme(Scheme scheme, int idConnection)
        {
            var connection = _unitOfWork.Connections.Get(idConnection);
            if (connection == null)
            {
                throw new KeyNotFoundException();
            }

            // Добавление соединения в схему
            var belongToScheme = new BelongToScheme()
            {
                IdConnection = idConnection,
                IdScheme = scheme.IdScheme
            };
            _unitOfWork.BelongToSchemes.Create(belongToScheme);
            _unitOfWork.Save();

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
            var scheme = _unitOfWork.Schemes.Get(id);

            if (scheme == null)
            {
                throw new KeyNotFoundException();
            }
            var belongToSchemes = _unitOfWork.BelongToSchemes.Find(b=>b.IdScheme==id).ToList();
            foreach(var belongToScheme in belongToSchemes)
            {
                _unitOfWork.BelongToSchemes.Delete(id,belongToScheme.IdConnection);
            }
            _unitOfWork.Save();
            _unitOfWork.Schemes.Delete(id);
            _unitOfWork.Save();

            return scheme;
        }

        /// <summary>
        /// Получает схему по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <returns>Запрашиваемая схема.</returns>
        /// <exception cref="KeyNotFoundException">Если схема не найдена.</exception>
        public async Task<Scheme> GetScheme(int id)
        {
            var scheme = _unitOfWork.Schemes.Get(id);

            if (scheme == null)
            {
                throw new KeyNotFoundException();
            }

            return scheme;
        }

        /// <summary>
        /// Получает все схемы для указанной книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <returns>Список всех схем книги.</returns>
        public async Task<IEnumerable<Scheme>> GetAllSchemes(int idBook)
        {
            var schemes = _unitOfWork.Schemes.Find(s => s.IdBook == idBook).ToList();

            return schemes;
        }
    }

}
