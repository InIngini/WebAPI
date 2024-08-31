using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Errors;
using WebAPI.DB.Entities;
using WebAPI.DB;
using WebAPI.Errors;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.BLL.Additional
{
    /// <summary>
    /// Класс для удаления каких-то записей из таблиц.
    /// </summary>
    public static class Deletion
    {
        /// <summary>
        /// Удаляет связь между книгой и пользователем.
        /// </summary>
        /// <param name="BookId">Идентификатор книги, связи которой необходимо удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteBelongToBook(int BookId, Context context)
        {
            var belongToBook =  context.BelongToBooks.Where(b => b.BookId == BookId).FirstOrDefault();
            if (belongToBook == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Книга", 0));
            }
            context.BelongToBooks.Remove(belongToBook);
            context.SaveChanges();
        }
        /// <summary>
        /// Удаляет схему по заданному идентификатору и все связи, связанные с этой схемой.
        /// Если схема является "Главной схемой", то связи удаляются полностью.
        /// </summary>
        /// <param name="SchemeId">Идентификатор схемы, которую необходимо удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteScheme(int SchemeId, Context context)
        {
            var scheme =  context.Schemes.Find(SchemeId);
            if (scheme == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Схема", 0));
            }
            // Удаление всех связей схемы
            var belongToSchemes =  context.BelongToSchemes.Where(b => b.SchemeId == scheme.Id).ToList();
            foreach (var belongToScheme in belongToSchemes)
            {
                DeleteBelongToScheme(belongToScheme, context);
                if (scheme.NameScheme == "Главная схема")
                {
                    DeleteConnection(belongToScheme.ConnectionId, context);
                }
            }
            context.Schemes.Remove(scheme);
            context.SaveChanges();
        }
        /// <summary>
        /// Удаляет связь между персонажами.
        /// </summary>
        /// <param name="belongToScheme">Объект, представляющий связь, которую нужно удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteBelongToScheme(BelongToScheme belongToScheme, Context context)
        {
            context.BelongToSchemes.Remove(belongToScheme);
            context.SaveChanges();
        }
        /// <summary>
        /// Удаляет связь по заданному идентификатору связи и все связанные с ней связи.
        /// </summary>
        /// <param name="ConnectionId">Идентификатор связи, которую необходимо удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteConnection(int ConnectionId,Context context)
        {
            var connection =  context.Connections.Find(ConnectionId);
            if (connection == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Связь", 0));
            }
            //если мы удаляем связь, то надо и все белонги удалить
            var belongToSchemes =  context.BelongToSchemes.Where(b => b.ConnectionId == connection.Id).ToList();
            foreach (var belongToScheme in belongToSchemes)
            {
                DeleteBelongToScheme(belongToScheme,context);
            }
            context.Connections.Remove(connection);
            context.SaveChanges();
        }
        /// <summary>
        /// Удаляет таймлайн по заданному идентификатору и все связи, связанные с этим таймлайном.
        /// Если таймлайн является "Главным таймлайном", то удаляются все события.
        /// </summary>
        /// <param name="TimelineId">Идентификатор таймлайна, который необходимо удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteTimeline(int TimelineId, Context context)
        {
            var timeline =  context.Timelines.Find(TimelineId);
            if (timeline == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Таймлайн", 1));
            }
            // Удаление всех связей таймлайна
            var belongToTimelines =  context.BelongToTimelines.Where(b => b.TimelineId == timeline.Id).ToList();
            foreach (var belongToTimeline in belongToTimelines)
            {
                DeleteBelongToTimeline(belongToTimeline, context);
                if (timeline.NameTimeline == "Главный таймлайн")
                {
                    DeleteEvent(belongToTimeline.EventId, context);
                }
            }
            context.Timelines.Remove(timeline);
             context.SaveChanges();
        }
        /// <summary>
        /// Удаляет связь между таймлайном и событием.
        /// </summary>
        /// <param name="belongToTimeline">Объект, представляющий связь, которую нужно удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteBelongToTimeline(BelongToTimeline belongToTimeline, Context context)
        {
            context.BelongToTimelines.Remove(belongToTimeline);
             context.SaveChanges();
        }
        /// <summary>
        /// Удаляет событие по заданному идентификатору и все связи, связанные с этим событием.
        /// </summary>
        /// <param name="EventId">Идентификатор события, которое необходимо удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteEvent(int EventId, Context context)
        {
            var @event =  context.Events.Find(EventId);
            if (@event == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 2));
            }
            //если мы удаляем событие, то надо и все белонги удалить
            var belongToTimelines =  context.BelongToTimelines.Where(b => b.EventId == @event.Id).ToList();
            foreach (var belongToTimeline in belongToTimelines)
            {
                DeleteBelongToTimeline(belongToTimeline, context);
            }
            context.Events.Remove(@event);
             context.SaveChanges();
        }
        /// <summary>
        /// Удаляет связь между событием и персонажем по идентификаторам события и персонажа.
        /// </summary>
        /// <param name="EventId">Идентификатор события, связи которого нужно удалить.</param>
        /// <param name="CharacterId">Идентификатор персонажа, связи которого нужно удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteBelongToEvent(int EventId, int CharacterId, Context context)
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

            var belongToEvent = context.BelongToEvents.Where(b=>b.EventId == @event.Id && b.CharacterId==CharacterId).FirstOrDefault();
            if (belongToEvent == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Событие", 2));
            }
            context.BelongToEvents.Remove(belongToEvent);
             context.SaveChanges();
        }
        /// <summary>
        /// Удаляет изображение по заданному идентификатору, а также устраняет все связи этого изображения
        /// с галереей, персонажами и книгами в базе данных.
        /// </summary>
        /// <param name="PictureId">Идентификатор изображения, которое необходимо удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeletePicture(int PictureId, Context context)
        {
            var picture =  context.Pictures.Find(PictureId);
            if (picture == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Изображение", 2));
            }
            var belongToGallery = context.BelongToGalleries.Where(b => b.PictureId == PictureId).FirstOrDefault();
            if(belongToGallery!=null)
            {
                context.BelongToGalleries.Remove(belongToGallery);
            }
            var character = context.Characters.Where(b=>b.PictureId == PictureId).FirstOrDefault();
            if (character != null)
            {
                character.PictureId = null;
                context.Characters.Update(character);
            }
            var book = context.Books.Where(b => b.PictureId == PictureId).FirstOrDefault();
            if (book != null)
            {
                book.PictureId = null;
                context.Books.Update(book);
            }
            context.Pictures.Remove(picture);
             context.SaveChanges();
        }
        /// <summary>
        /// Удаляет все ответы, связанные с заданным идентификатором персонажа,
        /// по каждому вопросу в базе данных.
        /// </summary>
        /// <param name="CharacterId">Идентификатор персонажа, ответы которого нужно удалить.</param>
        /// <param name="context">Контекст базы данных.</param>
        public static void DeleteAllAnswerByCharacter(int CharacterId, Context context)
        {
            foreach (var question in  context.Questions.ToList())
            {
                var answer =  context.Answers.Where(a => a.CharacterId == CharacterId && a.QuestionId == question.Id).FirstOrDefault();
                if (answer == null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Ответ", 1));
                }
                // Удаление ответов
                context.Answers.Remove(answer);
                context.SaveChanges();
            }
        }
    }
}
