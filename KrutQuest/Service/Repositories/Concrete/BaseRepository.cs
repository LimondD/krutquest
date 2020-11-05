using System;
using System.Collections.Generic;
using System.Linq;
using KrutQuest.Service.Models;
using Microsoft.EntityFrameworkCore;
using KrutQuest.Service.Repositories.Abstract;
using KrutQuest.Service.Data;

namespace KrutQuest.Service.Repositories.Concrete
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IDbObject
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        public readonly DataContext DbContext;

        /// <summary>
        /// Набор данных
        /// </summary>
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public BaseRepository(DataContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        /// <summary>
        /// Создает запись в таблице
        /// </summary>
        /// <param name="entity">Запись</param>
        /// <returns>ID записи</returns>
        public Guid Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = DbSet.Add(entity);
            DbContext.SaveChanges();

            return entry.Entity.Id;
        }

        /// <summary>
        /// Удаляет запись в таблице по ID
        /// </summary>
        /// <param name="id">ID записи</param>
        public void Delete(Guid id)
        {
            var entity = Retrieve(id);

            if (entity == null)
            {
                return;
            }

            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Получает запись из таблицы по ID
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Запись</returns>
        public TEntity Retrieve(Guid id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Получает все записи из таблицы по предикату
        /// </summary>
        /// <param name="predicate">Предикат</param>
        /// <returns>Список записей</returns>
        public IList<TEntity> RetrieveMultiple(Func<TEntity, bool> predicate)
        {
            return DbSet
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }

        /// <summary>
        /// Получает первую запись из таблицы по предикату, если такой нет возвращает null
        /// </summary>
        /// <param name="predicate">Предикат</param>
        /// <returns>Запись или null</returns>
        public TEntity RetrieveFirstOrDefault(Func<TEntity, bool> predicate)
        {
            return DbSet
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefault();
        }

        /// <summary>
        /// Обновляет запись в таблице
        /// </summary>
        /// <param name="entity">Запись</param>
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var tracks = DbContext.ChangeTracker.Entries<TEntity>()
                .Where(e => e.Entity.Id == entity.Id)
                .ToList();

            for (var i = 0; i < tracks.Count; i++)
            {
                tracks[i].State = EntityState.Detached;
            }

            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Получает все записи из таблицы
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Список записей</returns>
        public IList<TEntity> RetrieveAll()
        {
            return DbSet.AsNoTracking().ToList(); 
        }
    }
}