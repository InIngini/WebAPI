using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Errors;
using WebAPI.DB;
using WebAPI.DB.Entities;
using WebAPI.Errors;

namespace WebAPI.BLL.Additional
{
    /// <summary>
    /// Класс для создания каких-то записей в таблицах.
    /// </summary>
    public class CreationRepository
    {
        public CreationRepository() { }

        /// <summary>
        /// Создает связь между пользователем и книгой с указанным типом доступа.
        /// </summary>
        /// <param name="UserId">Идентификатор пользователя, которому предоставляется доступ к книге.</param>
        /// <param name="BookId">Идентификатор книги, к которой предоставляется доступ.</param>
        /// <param name="TypeBelong">Тип доступа к книге (например, автор, читатель и т.д.).</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateBelongToBook(int UserId, int BookId, string TypeBelongName, IContext context)
        {
            var TypeBelong = await context.TypeBelongToBooks.Where(t => t.Name == TypeBelongName).FirstOrDefaultAsync();
            if (TypeBelong == null)
            {
                throw new ApiException(TypesOfErrors.SomethingWentWrong("Неправильный тип доступа к книге"));
            }
            // Создание сущности BelongTo
            BelongToBook belongToBook = new BelongToBook
            {
                UserId = UserId,
                BookId = BookId,
                TypeBelong = TypeBelong.Id // автор
            };
            context.BelongToBooks.Add(belongToBook);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает новую схему для заданной книги с указанным именем.
        /// </summary>
        /// <param name="Name">Имя схемы, которую необходимо создать.</param>
        /// <param name="BookId">Идентификатор книги, к которой относится схема.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateScheme(Scheme scheme, IContext context)
        {
            context.Schemes.Add(scheme);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает новую связь для заданной книги и автоматом связывает ее с "Главной схемой".
        /// </summary>
        /// <param name="connection">Объект связи, который нужно создать.</param>
        /// <param name="BookId">Идентификатор книги, с которой связывается связь.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateConnection(Connection connection, int BookId, IContext context)
        {
            context.Connections.Add(connection);
            await context.SaveChangesAsync();

            var scheme = await context.Schemes
                         .Where(s => s.NameScheme == "Главная схема" && s.BookId == BookId)
                         .FirstOrDefaultAsync();

            if (scheme == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Главная схема", 0));
            }

            await CreateBelongToScheme(connection.Id, scheme.Id, context);
        }
        /// <summary>
        /// Создает связь между схемой и существующей связью.
        /// </summary>
        /// <param name="ConnectionId">Идентификатор существующей связи.</param>
        /// <param name="SchemeId">Идентификатор схемы, с которой связывается связь.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateBelongToScheme(int ConnectionId, int SchemeId, IContext context)
        {
            var connection = await context.Connections.FirstOrDefaultAsync(x => x.Id == ConnectionId); // Используем асинхронный поиск
            if (connection == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Связь", 0));
            }

            var scheme = await context.Schemes.FirstOrDefaultAsync(x => x.Id == SchemeId); // Используем асинхронный поиск
            if (scheme == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Схема", 0));
            }

            var belongToScheme = new BelongToScheme()
            {
                SchemeId = SchemeId,
                ConnectionId = ConnectionId
            };
            context.BelongToSchemes.Add(belongToScheme);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает новый таймлайн для заданной книги с указанным именем.
        /// </summary>
        /// <param name="Name">Имя таймлайна, который нужно создать.</param>
        /// <param name="BookId">Идентификатор книги, к которой относится таймлайн.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateTimeline(Timeline timeline, IContext context)
        {
            context.Timelines.Add(timeline);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает новое событие и автоматически связывает его с "Главным таймлайном".
        /// </summary>
        /// <param name="@event">Объект события, который нужно создать.</param>
        /// <param name="BookId">Идентификатор книги, к которой может относиться событие.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateEvent(Event @event, int BookId, IContext context)
        {
            context.Events.Add(@event);
            await context.SaveChangesAsync();

            var timeline = await context.Timelines
                .Where(s => s.NameTimeline == "Главный таймлайн" && s.BookId == BookId)
                .FirstOrDefaultAsync();

            if (timeline == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Главный таймлайн", 1));
            }

            await CreateBelongToTimeline(@event.Id, timeline.Id, context);
        }
        /// <summary>
        /// Создает связь между событием и таймлайном.
        /// </summary>
        /// <param name="EventId">Идентификатор события, которое нужно связать с таймлайном.</param>
        /// <param name="TimelineId">Идентификатор таймлайна, с которым связывается событие.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateBelongToTimeline(int EventId, int TimelineId, IContext context)
        {
            var @event = await context.Events.FirstOrDefaultAsync(x => x.Id == EventId);
            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 2));
            }

            var timeline = await context.Timelines.FirstOrDefaultAsync(x => x.Id == TimelineId);
            if (timeline == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Таймлайн", 1));
            }

            var belongToTimeline = new BelongToTimeline()
            {
                TimelineId = TimelineId,
                EventId = EventId
            };
            context.BelongToTimelines.Add(belongToTimeline);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает связь между событием и персонажем.
        /// </summary>
        /// <param name="EventId">Идентификатор события, с которым связывается персонаж.</param>
        /// <param name="CharacterId">Идентификатор персонажа, который связывается с событием.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateBelongToEvent(int EventId, int CharacterId, IContext context)
        {
            var character = await context.Characters.FirstOrDefaultAsync(x => x.Id == CharacterId);
            if (character == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Персонаж", 1));
            }

            var @event = await context.Events.FirstOrDefaultAsync(x => x.Id == EventId);
            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 2));
            }

            var belongToEvent = new BelongToEvent()
            {
                CharacterId = CharacterId,
                EventId = EventId
            };
            context.BelongToEvents.Add(belongToEvent);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает ответы для всех заданных вопросов, связывая их с указанным персонажем.
        /// </summary>
        /// <param name="CharacterId">Идентификатор персонажа, для которого создаются ответы.</param>
        /// <param name="context">Контекст базы данных.</param>
        public async Task CreateAllAnswerByCharacter(int CharacterId, IContext context)
        {
            var questions = await context.Questions.ToListAsync();
            foreach (var question in questions)
            {
                Answer answer = new Answer()
                {
                    CharacterId = CharacterId,
                    QuestionId = question.Id,
                    AnswerText = ""
                };
                context.Answers.Add(answer);
            }
            await context.SaveChangesAsync();
        }
    }
}
