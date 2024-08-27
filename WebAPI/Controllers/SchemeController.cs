﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Errors;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления схемами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class SchemeController : ControllerBase
    {
        private readonly ISchemeService _schemeService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SchemeController"/>.
        /// </summary>
        /// <param name="schemeService">Сервис для работы с схемами.</param>
        public SchemeController(ISchemeService schemeService)
        {
            _schemeService = schemeService;
        }

        /// <summary>
        /// Создает новую схему.
        /// </summary>
        /// <param name="schemedata">Данные о схеме, которую необходимо создать.</param>
        /// <returns>Результат создания схемы.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateScheme([FromBody] SchemeData schemedata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NoValidModel(ModelState));
            }
            
            var createdScheme = await _schemeService.CreateScheme(schemedata);

            return CreatedAtAction(nameof(GetScheme), new { id = createdScheme.Id }, createdScheme);

        }

        /// <summary>
        /// Обновляет схему, добавляя связи.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <param name="idConnection">Идентификатор связи для добавления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Обновлённая схема.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScheme(int id, [FromBody] int idConnection,CancellationToken cancellationToken)
        {
            var scheme = await _schemeService.GetScheme(id, cancellationToken);

            if (scheme == null)
            {
                return NotFound(TypesOfErrors.NoFoundById("Схема", 0));
            }

            var updatedScheme = await _schemeService.UpdateScheme(scheme,idConnection);

            return Ok(updatedScheme);
        }

        /// <summary>
        /// Удаляет схему по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы для удаления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Результат удаления схемы.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheme(int id,CancellationToken cancellationToken)
        {
            var scheme = await _schemeService.GetScheme(id, cancellationToken);

            if (scheme == null)
            {
                return NotFound(TypesOfErrors.NoFoundById("Схема", 0));
            }

            await _schemeService.DeleteScheme(id);

            return NoContent();
        }

        /// <summary>
        /// Получает схему по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Схема с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheme(int id, CancellationToken cancellationToken)
        {
            var scheme = await _schemeService.GetScheme(id,cancellationToken);

            if (scheme == null)
            {
                return NotFound(TypesOfErrors.NoFoundById("Схема", 0));
            }

            return Ok(scheme);
        }

        /// <summary>
        /// Получает все схемы по заданному идентификатору книги.
        /// </summary>
        /// <param name="id">Идентификатор для получения всех схем книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех схем для указанного идентификатора книги.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllScheme([FromBody] int id, CancellationToken cancellationToken)
        {
            var schemes = await _schemeService.GetAllSchemes(id,cancellationToken);

            if (schemes == null)
            {
                return NotFound(TypesOfErrors.NoFoundById("Схемы", 3));
            }

            return Ok(schemes);
        }
    }
}

