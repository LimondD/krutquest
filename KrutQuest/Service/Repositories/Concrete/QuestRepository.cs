using System;
using System.Collections.Generic;
using System.Linq;
using KrutQuest.Service.Data;
using KrutQuest.Service.Models;
using KrutQuest.Service.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KrutQuest.Service.Repositories.Concrete
{
    /// <summary>
    /// Репозиторий (Квест)
    /// </summary>
    public class QuestRepository : BaseRepository<Quest>, IQuestRepository
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public QuestRepository(DataContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Получает все квесты
        /// </summary>
        /// <returns>Список квестов</returns>
        public List<Quest> GetAll()
        {
            return DbSet
                .AsNoTracking()
                .Include(q => q.FinishPicture)
                .ToList();
        }
    }
}