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
    /// Контроллер для управления таймлайнами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineService TimelineService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TimelineController"/>.
        /// </summary>
        /// <param name="timelineService">Сервис для работы с таймлайнами.</param>
        public TimelineController(ITimelineService timelineService)
        {
            TimelineService = timelineService;
        }

        /// <summary>
        /// Создает новый таймлайн.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///         "BookId": "1",
        ///         "NameTimeline": "Новое имя"
        ///     }
        ///
        /// </remarks>
        /// <param name="timelinedata">Данные о таймлайне, который необходимо создать.</param>
        /// <returns>Результат создания таймлайна.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Timeline), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTimeline([FromBody] TimelineData timelinedata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }

            var createdTimeline = await TimelineService.CreateTimeline(timelinedata);
            return CreatedAtAction(nameof(GetTimeline), new { id = createdTimeline.Id }, createdTimeline);
        }

        /// <summary>
        /// Обновляет таймлайн, добавляя событие.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <param name="idEvent">Идентификатор события для добавления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Обновлённый таймлайн.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Timeline), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTimeline(int id, [FromBody] int idEvent,CancellationToken cancellationToken)
        {
            var timeline = await TimelineService.UpdateTimeline(id, idEvent);
            
            if (timeline == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Таймлайн", 1));
            }

            return Ok(timeline);
        }

        /// <summary>
        /// Удаляет таймлайн по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна для удаления.</param>
        /// <returns>Результат удаления таймлайна.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeline(int id)
        {
            await TimelineService.DeleteTimeline(id);

            return Ok();
        }
        /// <summary>
        /// Получает таймлайн по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Таймлайн с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Timeline), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTimeline(int id, CancellationToken cancellationToken)
        {
            var timeline = await TimelineService.GetTimeline(id,cancellationToken);
            if (timeline == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Таймлайн", 1));
            }

            return Ok(timeline);
        }

        /// <summary>
        /// Получает все таймлайны по заданному идентификатору книги.
        /// </summary>
        /// <param name="id">Идентификатор книги для получения всех таймлайнов.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех таймлайнов для указанного идентификатора книги.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Timeline>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTimeline([FromBody] int id,CancellationToken cancellationToken)
        {
            var timelines = await TimelineService.GetAllTimelines(id,cancellationToken);

            if (timelines == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Таймлайны", 3));
            }

            return Ok(timelines);
        }
    }
}

