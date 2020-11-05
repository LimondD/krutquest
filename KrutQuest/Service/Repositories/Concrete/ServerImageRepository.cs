using System;
using System.Collections.Generic;
using System.Linq;
using KrutQuest.Service.Data;
using KrutQuest.Service.Models;
using KrutQuest.Service.Repositories.Abstract;

namespace KrutQuest.Service.Repositories.Concrete
{
    /// <summary>
    /// Репозиторий (Изображение)
    /// </summary>
    public class ServerImageRepository : BaseRepository<ServerImage>, IServerImageRepository
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public ServerImageRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
