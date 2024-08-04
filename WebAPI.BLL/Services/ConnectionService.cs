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
using WebAPI.DB.Guide;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления связями между персонажами.
    /// </summary>
    public class ConnectionService : IConnectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConnectionService"/>.
        /// </summary>
        /// <param name="unitOfWork">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public ConnectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новую связь между персонажами.
        /// </summary>
        /// <param name="connectionData">Данные для создания связи.</param>
        /// <returns>Созданная связь.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Connection> CreateConnection(ConnectionData connectionData)
        {
            var validationContext = new ValidationContext(connectionData);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(connectionData, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            var connection = _mapper.Map<Connection>(connectionData);
            connection.TypeConnection = _unitOfWork.TypeConnections.Find(t => t.Name == connectionData.TypeConnection).FirstOrDefault().Id;

            var scheme = _unitOfWork.Schemes
                            .Find(s => s.NameScheme == "Главная схема" && s.IdBook == connectionData.IdBook)
                            .FirstOrDefault();

            _unitOfWork.Connections.Create(connection);
            _unitOfWork.Save();

            // Добавление связи в главную схему
            var belongToScheme = new BelongToScheme()
            {
                IdScheme = scheme.IdScheme,
                IdConnection = connection.IdConnection
            };
            _unitOfWork.BelongToSchemes.Create(belongToScheme);
            _unitOfWork.Save();

            return connection;
        }

        /// <summary>
        /// Удаляет связь по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор связи.</param>
        /// <returns>Удаленная связь.</returns>
        /// <exception cref="KeyNotFoundException">Если связь не найдена.</exception>
        public async Task<Connection> DeleteConnection(int id)
        {
            var connection = _unitOfWork.Connections.Get(id);
            if (connection == null)
            {
                throw new KeyNotFoundException();
            }
            
            var schemes = _unitOfWork.BelongToSchemes.Find(b=>b.IdConnection == id).ToList();
            // Удаление IdConnection удаляемой связи из схем
            foreach (var scheme in schemes)
            {
                _unitOfWork.BelongToSchemes.Delete(scheme.IdConnection);
            }
            _unitOfWork.Save();

            // Удаление связи
            _unitOfWork.Connections.Delete(id);
            _unitOfWork.Save();

            return connection;
        }

        /// <summary>
        /// Получает связь по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор связи.</param>
        /// <returns>Данные связи.</returns>
        /// <exception cref="KeyNotFoundException">Если связь не найдена.</exception>
        public async Task<ConnectionData> GetConnection(int id)
        {
            var connection = _unitOfWork.Connections.Get(id);

            if (connection == null)
            {
                throw new KeyNotFoundException();
            }
            var connectionData = _mapper.Map<ConnectionData>(connection);
            connectionData.Name1 = _unitOfWork.Answers.Get(connection.IdCharacter1).Name;
            connectionData.Name2 = _unitOfWork.Answers.Get(connection.IdCharacter2).Name;
            connectionData.TypeConnection = _unitOfWork.TypeConnections.Get(connection.TypeConnection).Name;

            return connectionData;
        }

        /// <summary>
        /// Получает все связи для указанной схемы.
        /// </summary>
        /// <param name="idScheme">Идентификатор схемы.</param>
        /// <returns>Список всех связей.</returns>
        public async Task<IEnumerable<ConnectionAllData>> GetAllConnections(int idScheme)
        {
            var connections = _unitOfWork.Connections.GetAll(idScheme).ToList();
            var connectionsData = new List<ConnectionAllData>();

            foreach (var connection in connections)
            {

                var connectionData = _mapper.Map<ConnectionAllData>(connection);
                connectionData.TypeConnection = _unitOfWork.TypeConnections.Get(connection.TypeConnection).Name;
                connectionsData.Add(connectionData);
            }
            return connectionsData;
        }
    }

}
