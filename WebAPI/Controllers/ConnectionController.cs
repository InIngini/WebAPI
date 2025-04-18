﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Errors;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления связями между персонажами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/Scheme/[controller]")]
    public class ConnectionController : Controller
    {
        private readonly IConnectionService ConnectionService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConnectionController"/>.
        /// </summary>
        /// <param name="connectionService">Сервис для работы со связями.</param>
        public ConnectionController(IConnectionService connectionService)
        {
            ConnectionService = connectionService;
        }

        /// <summary>
        /// Создает новую связь.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///         "BookId": "1",
        ///         "Character1Id": "1",
        ///         "Character2Id": "2",
        ///         "TypeConnection": "Сиблинг"
        ///     }
        ///
        /// </remarks>
        /// <param name="connectionData">Данные о связи, которую необходимо создать.</param>
        /// <returns>Результат создания связи.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Connection), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateConnection([FromBody] ConnectionData connectionData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }
            var createdConnection = await ConnectionService.CreateConnection(connectionData);
            
            return CreatedAtAction(nameof(GetConnection), new { id = createdConnection.Id }, createdConnection);
        }

        /// <summary>
        /// Удаляет связь по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор связь для удаления.</param>
        /// <returns>Результат удаления связи.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnection(int id)
        {
            var connection = await ConnectionService.DeleteConnection(id);

            if (connection == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Связь", 0));
            }

            return Ok();
        }

        /// <summary>
        /// Получает связь по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор связи.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Связь с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConnectionData), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConnection(int id, CancellationToken cancellationToken)
        {
            var connection = await ConnectionService.GetConnection(id, cancellationToken);

            return Ok(connection);
        }

        /// <summary>
        /// Получает все связи схемы по идентификатору схемы.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех связей для указанной схемы.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ConnectionAllData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllConnection([FromBody] int id, CancellationToken cancellationToken)
        {
            var connections = await ConnectionService.GetAllConnections(id, cancellationToken);

            if (connections == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Связи", 3));
            }
            
            return Ok(connections);
        }
    }

}
