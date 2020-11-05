using System;
using System.Collections.Generic;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Repositories.Abstract
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, IDbObject
    {
        /// <summary>
        /// Создает запись в таблице
        /// </summary>
        /// <param name="entity">Запись</param>
        /// <returns>ID записи</returns>
        Guid Create(TEntity entity);

        /// <summary>
        /// Удаляет запись в таблице по ID
        /// </summary>
        /// <param name="id">ID записи</param>
        void Delete(Guid id);

        /// <summary>
        /// Получает запись из таблицы по ID
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Запись</returns>
        TEntity Retrieve(Guid id);

        /// <summary>
        /// Получает все записи из таблицы
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Список записей</returns>
        IList<TEntity> RetrieveAll();

        /// <summary>
        /// Получает первую запись из таблицы по предикату, если такой нет возвращает null
        /// </summary>
        /// <param name="predicate">Предикат</param>
        /// <returns>Запись или null</returns>
        TEntity RetrieveFirstOrDefault(Func<TEntity, bool> predicate);

        /// <summary>
        /// Получает все записи из таблицы по предикату
        /// </summary>
        /// <param name="predicate">Предикат</param>
        /// <returns>Список записей</returns>
        IList<TEntity> RetrieveMultiple(Func<TEntity, bool> predicate);

        /// <summary>
        /// Обновляет запись в таблице
        /// </summary>
        /// <param name="entity">Запись</param>
        void Update(TEntity entity);
    }
}