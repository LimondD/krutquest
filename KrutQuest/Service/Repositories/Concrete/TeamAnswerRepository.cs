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
    /// Репозиторий (Ответ команды)
    /// </summary>
    public class TeamAnswerRepository : BaseRepository<TeamAnswer>, ITeamAnswerRepository
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public TeamAnswerRepository(DataContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Получает сумму баллов по всем ответам команды
        /// </summary>
        /// <param name="teamId">ID Команды</param>
        /// <returns>Сумма баллов</returns>
        public int GetTeamScore(Guid teamId)
        {
            return RetrieveMultiple(ta => ta.TeamId == teamId && ta.IsDone).Sum(ta => ta.Score);
        }

        /// <summary>
        /// Получает запись ответа по вопросу для данной команды
        /// </summary>
        /// <param name="teamId">ID Команды</param>
        /// <param name="questionId">ID Вопроса</param>
        /// <returns>Ответ команды</returns>
        public TeamAnswer GetQuestionAnswer(Guid teamId, Guid questionId)
        {
            return RetrieveFirstOrDefault(ta => ta.TeamId == teamId && ta.QuestionId == questionId);
        }

        /// <summary>
        /// Получает ответы команды и связанные вопросы
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>Список ответов</returns>
        public List<TeamAnswer> GetByTeamIdWithQuestion(Guid teamId)
        {
            return DbSet
                .AsNoTracking()
                .Include(ta => ta.Question)
                    .ThenInclude(q => q.Group)
                .Where(ta => ta.TeamId == teamId)
                .ToList();
        }
    }
}