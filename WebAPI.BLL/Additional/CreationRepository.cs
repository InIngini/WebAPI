﻿using Microsoft.EntityFrameworkCore;
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
        public void CreateBelongToBook(int UserId,int BookId,string TypeBelongName,IContext context)
        {
            var TypeBelong = context.TypeBelongToBooks.Where(t => t.Name == TypeBelongName).FirstOrDefault();
            if(TypeBelong == null)
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
            context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает новую схему для заданной книги с указанным именем.
        /// </summary>
        /// <param name="Name">Имя схемы, которую необходимо создать.</param>
        /// <param name="BookId">Идентификатор книги, к которой относится схема.</param>
        /// <param name="context">Контекст базы данных.</param>
        public void CreateScheme(Scheme scheme,IContext context)
        {
            context.Schemes.Add(scheme);
            context.SaveChangesAsync();
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
            var connection = await context.Connections.FindAsync(ConnectionId); // Используем асинхронный поиск
            if (connection == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Связь", 0));
            }

            var scheme = await context.Schemes.FindAsync(SchemeId); // Используем асинхронный поиск
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
        public void CreateTimeline(Timeline timeline, IContext context)
        {
            context.Timelines.Add(timeline);
            context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает новое событие и автоматически связывает его с "Главным таймлайном".
        /// </summary>
        /// <param name="@event">Объект события, который нужно создать.</param>
        /// <param name="BookId">Идентификатор книги, к которой может относиться событие.</param>
        /// <param name="context">Контекст базы данных.</param>
        public void CreateEvent(Event @event, int BookId, IContext context)
        {
            context.Events.Add(@event);
            context.SaveChangesAsync();

            var timeline = context.Timelines
                             .Where(s => s.NameTimeline == "Главный таймлайн" && s.BookId == BookId)
                             .FirstOrDefault();
            if (timeline == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Главный таймлайн", 1));
            }
            CreateBelongToTimeline(@event.Id, timeline.Id, context);
        }
        /// <summary>
        /// Создает связь между событием и таймлайном.
        /// </summary>
        /// <param name="EventId">Идентификатор события, которое нужно связать с таймлайном.</param>
        /// <param name="TimelineId">Идентификатор таймлайна, с которым связывается событие.</param>
        /// <param name="context">Контекст базы данных.</param>
        public void CreateBelongToTimeline(int EventId, int TimelineId, IContext context)
        {
            var @event = context.Events.Find(EventId);
            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 2));
            }

            var timeline = context.Timelines.Find(TimelineId);
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
            context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает связь между событием и персонажем.
        /// </summary>
        /// <param name="EventId">Идентификатор события, с которым связывается персонаж.</param>
        /// <param name="CharacterId">Идентификатор персонажа, который связывается с событием.</param>
        /// <param name="context">Контекст базы данных.</param>
        public void CreateBelongToEvent(int EventId, int CharacterId, IContext context)
        {
            var character = context.Characters.Find(CharacterId);
            if (character == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Персонаж", 1));
            }

            var @event = context.Events.Find(EventId);
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
            context.SaveChangesAsync();
        }
        /// <summary>
        /// Создает ответы для всех заданных вопросов, связывая их с указанным персонажем.
        /// </summary>
        /// <param name="CharacterId">Идентификатор персонажа, для которого создаются ответы.</param>
        /// <param name="context">Контекст базы данных.</param>
        public void CreateAllAnswerByCharacter(int CharacterId, IContext context)
        {
            foreach (var question in context.Questions.ToList())
            {
                Answer answer = new Answer()
                {
                    CharacterId = CharacterId,
                    QuestionId = question.Id,
                    AnswerText = ""
                };
                context.Answers.Add(answer);
                context.SaveChangesAsync();
            }
        }
    }
}
