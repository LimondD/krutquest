using System;
using System.Collections.Generic;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Repositories.Abstract
{
    /// <summary>
    /// Репозиторий (Группа вопросов)
    /// </summary>
    public interface IQuestionGroupRepository : IBaseRepository<QuestionGroup>
    {
        /// <summary>
        /// Получает следующий индекс для группы вопросов в рамках квеста
        /// </summary>
        /// <param name="questId">ID Квеста</param>
        /// <returns>Индекс</returns>
        int GetNextIndex(Guid questId);

        /// <summary>
        /// Получает группы вопросов и связанные вопросы по квесту
        /// </summary>
        /// <param name="questId">ID Квеста</param>
        /// <returns>Список групп вопросов</returns>
        List<QuestionGroup> GetByQuestIdWithQuestions(Guid questId);

        /// <summary>
        /// Получает все группы вопросов
        /// </summary>
        /// <returns>Список групп вопросов</returns>
        List<QuestionGroup> GetAll();
    }
}
