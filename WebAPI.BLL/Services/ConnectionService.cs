using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.BLL.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConnectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Connection> CreateConnection(ConnectionData connectionData)
        {
            var validationContext = new ValidationContext(connectionData);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(connectionData, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            Connection connection = new Connection()
            {
                TypeConnection = connectionData.TypeConnection,
                IdCharacter1 = connectionData.IdCharacter1,
                IdCharacter2 = connectionData.IdCharacter2,
            };

            var scheme = _unitOfWork.Schemes
                            .Find(s => s.NameScheme == "Главная схема" && s.IdBook == connectionData.IdBook)
                            .FirstOrDefault();

            _unitOfWork.Connections.Create(connection);
            _unitOfWork.Save();

            // Добавление связи в главную схему
            scheme.IdConnections.Add(connection);
            _unitOfWork.Save();

            return connection;
        }

        public async Task<Connection> DeleteConnection(int id)
        {
            // Получение связи из базы данных
            var connection = _unitOfWork.Connections.Get(id);

            // Если связь не найдена, вернуть ошибку
            if (connection == null)
            {
                throw new KeyNotFoundException();
            }

            // Получение схем, связанных со связью, и удаление их оттуда
            var schemes = _unitOfWork.Schemes.Find(c => c.IdConnections.Any(s => s.IdConnection == id)).ToList();

            // Удаление IdConnection удаляемой связи из схем
            foreach (var scheme in schemes)
            {
                var connection1 = scheme.IdConnections.FirstOrDefault(c => c.IdConnection == id);
                if (connection1 != null)
                {
                    scheme.IdConnections.Remove(connection1);
                }
            }
            // Сохранение изменений в базе данных
            _unitOfWork.Save();

            // Удаление связи
            _unitOfWork.Connections.Delete(id);
            _unitOfWork.Save();

            return connection;
        }

        public async Task<Connection> GetConnection(int id)
        {
            var connection = _unitOfWork.Connections.Get(id);

            if (connection == null)
            {
                throw new KeyNotFoundException();
            }

            return connection;
        }

        public async Task<IEnumerable<ConnectionAllData>> GetAllConnections(int idScheme)
        {
            var connections = _unitOfWork.Connections.GetAll(idScheme).ToList();
            var connectionsData = new List<ConnectionAllData>();
            foreach (var connection in connections)
            {
                var connectionData = new ConnectionAllData()
                {
                    IdConnection = connection.IdConnection,
                    IdCharacter1 = connection.IdCharacter1,
                    IdCharacter2 = connection.IdCharacter2,
                    TypeConnection = connection.TypeConnection
                };
                connectionsData.Add(connectionData);
            }
            return connectionsData;
        }
    }

}
