using System;
using System.Collections.Generic;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Repositories.Abstract
{
    /// <summary>
    /// Репозиторий (Вопрос)
    /// </summary>
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        /// <summary>
        /// Получает все вопросы
        /// </summary>
        /// <returns>Список вопросов</returns>
        List<Question> GetAll();
    }
}
