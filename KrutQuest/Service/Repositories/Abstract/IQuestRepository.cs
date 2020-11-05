using System;
using System.Collections.Generic;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Repositories.Abstract
{
    /// <summary>
    /// Репозиторий (Квест)
    /// </summary>
    public interface IQuestRepository : IBaseRepository<Quest>
    {
        /// <summary>
        /// Получает все квесты
        /// </summary>
        /// <returns>Список квестов</returns>
        List<Quest> GetAll();
    }
}
