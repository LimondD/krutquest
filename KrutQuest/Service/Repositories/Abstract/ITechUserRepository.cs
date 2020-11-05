using System;
using System.Collections.Generic;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Repositories.Abstract
{
    /// <summary>
    /// Репозиторий (Технический пользователь)
    /// </summary>
    public interface ITechUserRepository : IBaseRepository<TechUser>
    {
        /// <summary>
        /// Получает Технического пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Технический пользователь</returns>
        TechUser GetByLoginAndPassword(string login, string password);
    }
}
