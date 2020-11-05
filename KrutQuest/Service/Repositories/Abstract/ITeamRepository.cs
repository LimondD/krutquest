using System;
using System.Collections.Generic;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Repositories.Abstract
{
    /// <summary>
    /// Репозиторий (Команда)
    /// </summary>
    public interface ITeamRepository : IBaseRepository<Team>
    {
        /// <summary>
        /// Получает Команду по логину и паролю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Команда</returns>
        Team GetByLoginAndPassword(string login, string password);

        /// <summary>
        /// Получает Команду по логину и связанный с ней Квест
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Команда</returns>
        Team GetByLoginWithQuest(string login);

        /// <summary>
        /// Получает все команды
        /// </summary>
        /// <returns>Список команд</returns>
        List<Team> GetAll();

        /// <summary>
        /// Получает все команды
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Список команд</returns>
        List<Team> GetAll(Func<Team, bool> predicate);
    }
}
