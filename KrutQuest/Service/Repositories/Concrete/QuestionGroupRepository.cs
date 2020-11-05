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
    /// Репозиторий (Группа вопросов)
    /// </summary>
    public class QuestionGroupRepository : BaseRepository<QuestionGroup>, IQuestionGroupRepository
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public QuestionGroupRepository(DataContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Получает следующий индекс для группы вопросов в рамках квеста
        /// </summary>
        /// <param name="questId">ID Квеста</param>
        /// <returns>Индекс</returns>
        public int GetNextIndex(Guid questId)
        {
            return RetrieveMultiple(g => g.QuestId == questId)
                    .Select(g => g.Index)
                    .OrderByDescending(i => i)
                    .FirstOrDefault() + 1;
        }

        /// <summary>
        /// Получает группы вопросов и связанные вопросы по квесту
        /// </summary>
        /// <param name="questId">ID Квеста</param>
        /// <returns>Список групп вопросов</returns>
        public List<QuestionGroup> GetByQuestIdWithQuestions(Guid questId)
        {
            return DbSet
                .AsNoTracking()
                .Where(g => g.QuestId == questId)
                .Include(g => g.MapPicture)
                .Include(g => g.Questions) 
                    .ThenInclude(q => q.Picture)
                .ToList();
        }

        /// <summary>
        /// Получает все группы вопросов
        /// </summary>
        /// <returns>Список групп вопросов</returns>
        public List<QuestionGroup> GetAll()
        {
            return DbSet
                .AsNoTracking()
                .Include(g => g.MapPicture)
                .Include(g => g.Quest)
                .ToList();
        }
    }
}