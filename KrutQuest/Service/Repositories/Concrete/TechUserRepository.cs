using System;
using System.Collections.Generic;
using System.Linq;
using KrutQuest.Service.Data;
using KrutQuest.Service.Models;
using KrutQuest.Service.Repositories.Abstract;

namespace KrutQuest.Service.Repositories.Concrete
{
    /// <summary>
    /// Репозиторий (Технический пользователь)
    /// </summary>
    public class TechUserRepository : BaseRepository<TechUser>, ITechUserRepository
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public TechUserRepository(DataContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Получает Технического пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Технический пользователь</returns>
        public TechUser GetByLoginAndPassword(string login, string password)
        {
            return RetrieveFirstOrDefault(techUser =>
                string.Compare(techUser.Login, login, true) == 0
                && string.Compare(techUser.Password, password, false) == 0);
        }
    }
}
