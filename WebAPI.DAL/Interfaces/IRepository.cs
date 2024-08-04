using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DAL.Interfaces
{
    /// <summary>
    /// Общий интерфейс репозитория для работы с сущностями типа T.
    /// </summary>
    /// <typeparam name="T">Тип сущности, должен быть классом.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получить все сущности для заданного идентификатора.
        /// </summary>
        /// <param name="id">Идентификатор для выборки.</param>
        /// <returns>Коллекция всех сущностей типа T.</returns>
        IEnumerable<T> GetAll(int id);

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность типа T.</returns>
        T Get(int id);

        /// <summary>
        /// Найти сущности по условию.
        /// </summary>
        /// <param name="predicate">Условие для фильтрации.</param>
        /// <returns>Коллекция найденных сущностей типа T.</returns>
        IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// Создать новую сущность.
        /// </summary>
        /// <param name="item">Новая сущность типа T для создания.</param>
        void Create(T item);

        /// <summary>
        /// Обновить существующую сущность.
        /// </summary>
        /// <param name="item">Обновленная сущность типа T.</param>
        void Update(T item);

        /// <summary>
        /// Удалить сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности.</param>
        void Delete(int id);

        /// <summary>
        /// Удалить связанную сущность по двум идентификаторам в белонгах.
        /// </summary>
        /// <param name="id">Первый идентификатор для удаления.</param>
        /// <param name="id2">Второй идентификатор для удаления.</param>
        void Delete(int id, int id2)
        { 
            //тут определено, чтоб в других, не белонгах, не определять
        }
    }
}
