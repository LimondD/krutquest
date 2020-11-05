using System;
using System.Collections.Generic;
using System.Linq;
using KrutQuest.Service.Models;
using KrutQuest.Service.Repositories.Abstract;
using KrutQuest.Service.Data;
using Microsoft.EntityFrameworkCore;

namespace KrutQuest.Service.Repositories.Concrete
{
    /// <summary>
    /// Репозиторий (Команда)
    /// </summary>
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public TeamRepository(DataContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Получает Команду по логину и паролю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Команда</returns>
        public Team GetByLoginAndPassword(string login, string password)
        {
            return RetrieveFirstOrDefault(team =>
                string.Compare(team.Login, login, true) == 0
                && string.Compare(team.Password, password, false) == 0);
        }

        /// <summary>
        /// Получает Команду по логину и связанный с ней Квест
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Команда</returns>
        public Team GetByLoginWithQuest(string login)
        {
            return DbSet
                .AsNoTracking()
                .Where(t => t.Login == login)
                .Include(t => t.Quest)
                    .ThenInclude(q => q.FinishPicture)
                .FirstOrDefault();
        }

        /// <summary>
        /// Получает все команды
        /// </summary>
        /// <returns>Список команд</returns>
        public List<Team> GetAll()
        {
            return DbSet
                .AsNoTracking()
                .Include(t => t.Quest)
                .ToList();
        }

        /// <summary>
        /// Получает все команды
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Список команд</returns>
        public List<Team> GetAll(Func<Team, bool> predicate)
        {
            return DbSet
                .AsNoTracking()                
                .Include(t => t.Quest)
                .Where(predicate)
                .ToList();
        }
    }
}