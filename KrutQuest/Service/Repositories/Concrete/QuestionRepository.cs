using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KrutQuest.Service.Data;
using KrutQuest.Service.Models;
using KrutQuest.Service.Repositories.Abstract;

namespace KrutQuest.Service.Repositories.Concrete
{
    /// <summary>
    /// Репозиторий (Вопрос)
    /// </summary>
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public QuestionRepository(DataContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Получает все вопросы
        /// </summary>
        /// <returns>Список вопросов</returns>
        public List<Question> GetAll()
        {
            return DbSet
                .AsNoTracking()
                .Include(q => q.Picture)
                .Include(q => q.Group)
                .ToList();
        }
    }
}