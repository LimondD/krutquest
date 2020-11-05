using System;
using System.Collections.Generic;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Repositories.Abstract
{
    /// <summary>
    /// Репозиторий (Ответ команды)
    /// </summary>
    public interface ITeamAnswerRepository : IBaseRepository<TeamAnswer>
    {
        /// <summary>
        /// Получает сумму баллов по всем ответам команды
        /// </summary>
        /// <param name="teamId">ID Команды</param>
        /// <returns>Сумма баллов</returns>
        int GetTeamScore(Guid teamId);

        /// <summary>
        /// Получает запись ответа по вопросу для данной команды
        /// </summary>
        /// <param name="teamId">ID Команды</param>
        /// <param name="questionId">ID Вопроса</param>
        /// <returns>Ответ команды</returns>
        TeamAnswer GetQuestionAnswer(Guid teamId, Guid questionId);

        /// <summary>
        /// Получает ответы команды и связанные вопросы
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>Список ответов</returns>
        List<TeamAnswer> GetByTeamIdWithQuestion(Guid teamId);
    }
}
