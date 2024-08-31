using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using WebAPI.DB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebAPI.Errors;
using System.Threading;


namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления добавленными атрибутами.
    /// </summary>
    public class AddedAttributeService : IAddedAttributeService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddedAttributeService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public AddedAttributeService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новый добавленный атрибут.
        /// </summary>
        /// <param name="id">Идентификатор персонажа.</param>
        /// <param name="aa">Данные для добавленного атрибута.</param>
        /// <returns>Созданный добавленный атрибут.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<AddedAttribute> CreateAddedAttribute(int id,AddedAttributeData aa)
        {
            var addedAttribute = _mapper.Map<AddedAttribute>(aa);
            addedAttribute.ContentAttribute = String.Empty;
            addedAttribute.CharacterId = id;

            var validationContext = new ValidationContext(addedAttribute);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(addedAttribute, validationContext, validationResults, true))
            {
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            _context.AddedAttributes.Add(addedAttribute);
            await _context.SaveChangesAsync();

            return addedAttribute;
        }

        /// <summary>
        /// Обновляет существующий добавленный атрибут.
        /// </summary>
        /// <param name="addedAttribute">Добавленный атрибут для обновления.</param>
        /// <returns>Обновленный добавленный атрибут.</returns>
        public async Task<AddedAttribute> UpdateAddedAttribute(int AttributeId, string content)
        {
            var addedAttribute = await _context.AddedAttributes.FindAsync(AttributeId);
            if (addedAttribute == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Атрибут", 1));
            }

            addedAttribute.ContentAttribute = content;

            _context.AddedAttributes.Update(addedAttribute);
            await _context.SaveChangesAsync();

            return addedAttribute;
        }

        /// <summary>
        /// Удаляет добавленный атрибут по идентификатору атрибута и персонажа.
        /// </summary>
        /// <param name="idc">Идентификатор персонажа.</param>
        /// <param name="ida">Идентификатор атрибута.</param>
        /// <returns>Удалённый добавленный атрибут.</returns>
        /// <exception cref="KeyNotFoundException">Если атрибут не найден.</exception>
        public async Task<AddedAttribute> DeleteAddedAttribute(int idc, int ida)
        {
            var addedAttribute = await _context.AddedAttributes.Where(a=> a.Id==ida && a.CharacterId==idc).FirstOrDefaultAsync();

            if (addedAttribute == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Атрибут", 1));
            }

            _context.AddedAttributes.Remove(addedAttribute);
            await _context.SaveChangesAsync();

            return addedAttribute;

        }
        /// <summary>
        /// Получает добавленный атрибут по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор добавленного атрибута.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Добавленный атрибут с указанным идентификатором.</returns>
        /// <exception cref="KeyNotFoundException">Если атрибут не найден.</exception>
        public async Task<AddedAttribute> GetAddedAttribute(int id, CancellationToken cancellationToken)
        {
            var addedAttribute = await _context.AddedAttributes.FindAsync(id, cancellationToken);

            if (addedAttribute == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Атрибут", 1));
            }
            
            return addedAttribute;
        }

        /// <summary>
        /// Получает все добавленные атрибуты для указанного персонажа.
        /// </summary>
        /// <param name="idCharacter">Идентификатор персонажа.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список добавленных атрибутов для указанного персонажа.</returns>
        public async Task<IEnumerable<AddedAttribute>> GetAllAddedAttributes(int idCharacter, CancellationToken cancellationToken)
        {
            var addedAttributes = await _context.AddedAttributes.Where(aa => aa.CharacterId == idCharacter).ToListAsync(cancellationToken);

            return addedAttributes;
        }
    }

}
