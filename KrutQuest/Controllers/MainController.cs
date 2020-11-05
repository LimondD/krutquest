using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KrutQuest.Service.Transport;
using KrutQuest.Service.Models;
using KrutQuest.Service.Models.View;
using KrutQuest.Service.Repositories.Abstract;

namespace KrutQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/Main")]
    public class MainController : ControllerBase
    {
        /// <summary>
        /// Репозиторий (Команда)
        /// </summary>
        private readonly ITeamRepository _TeamRepository;

        /// <summary>
        /// Репозиторий (Группа вопросов)
        /// </summary>
        private readonly IQuestionGroupRepository _QuestionGroupRepository;

        /// <summary>
        /// Репозиторий (Вопрос)
        /// </summary>
        private readonly IQuestionRepository _QuestionRepository;

        /// <summary>
        /// Репозиторий (Ответ команды)
        /// </summary>
        private readonly ITeamAnswerRepository _TeamAnswerRepository;

        /// <summary>
        /// Разбирает строку с порядком групп вопросов на последовательность индексов групп
        /// </summary>
        /// <param name="questionGroupsOrderStr">Строка с порядком групп вопросов</param>
        /// <returns>Последовательность индексов групп</returns>
        private IEnumerable<int> ParseQuestionGroupsOrder(string questionGroupsOrderStr)
        {
            return questionGroupsOrderStr.Split(';')
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .Select(item =>
                {
                    var raw = item.Trim();
                    if (int.TryParse(raw, out int index))
                    {
                        return index;
                    }
                    else
                    {
                        return -1;
                    }
                });
        }        

        public MainController(ITeamRepository teamRepository, IQuestionGroupRepository questionGroupRepository, IQuestionRepository questionRepository, ITeamAnswerRepository teamAnswerRepository)
        {
            _TeamRepository = teamRepository;
            _QuestionGroupRepository = questionGroupRepository;
            _QuestionRepository = questionRepository;
            _TeamAnswerRepository = teamAnswerRepository;
        }
            
        [HttpGet("getViewModel")]
        public ActionResult GetViewModel()
        {
            var viewModel = new MainViewModel
            {
                StatusCode = TeamStatus.Initial
            };

            // текущий блок вопросов для команды
            MainViewQuestionGroup currentGroup = null;

            // тянем команду
            var team = _TeamRepository.GetByLoginWithQuest(User.Identity.Name);
            if (team != null && team.QuestId.HasValue)
            {
                viewModel.Team = new MainViewTeam
                {
                    Name = team.Name,
                    BeginDate = team.BeginDate,
                    HasBegun = team.HasBegun                    
                };

                if (team.HasBegun && team.BeginDate != null)
                {
                    viewModel.StatusCode = TeamStatus.Started;
                }

                viewModel.Quest = new MainViewQuest
                {
                    Name = team.Quest.Name,
                    FinishPicture = team.Quest.FinishPicture,
                    FinishText = team.Quest.FinishText,
                    RulesText = team.Quest.RulesText
                };

                // тянем ответы команды
                var teamAnswers = _TeamAnswerRepository.RetrieveMultiple(ta => ta.TeamId == team.Id);

                // тянем группы и вопросы
                var questionGroups = _QuestionGroupRepository
                    .GetByQuestIdWithQuestions(team.QuestId.Value)
                    .Select(g =>
                    {
                        var questions = g.Questions.ConvertAll(q => 
                        {
                            var answer = teamAnswers.FirstOrDefault(ta => ta.QuestionId == q.Id);
                            return new MainViewQuestion
                            {
                                Id = q.Id,
                                Score = answer != null ? answer.Score : q.Score,
                                Text = q.Text,
                                IsDone = answer != null ? answer.IsDone : false,
                                IsHintUsed = answer != null ? answer.IsHintUsed : false,
                                Hint = answer != null && answer.IsHintUsed ? q.Hint : string.Empty,
                                Picture = q.Picture,
                                Answer = answer != null && answer.IsDone ? answer.Answer : string.Empty
                            };
                        });

                        return new MainViewQuestionGroup
                        {
                            Id = g.Id,
                            Name = g.Name,
                            Index = g.Index,
                            Questions = questions,
                            IsDone = questions.All(q => q.IsDone),
                            MapPicture = g.MapPicture
                        };
                    })
                    .OrderBy(g => g.Index);

                viewModel.Team.TotalScore = questionGroups.Sum(g =>
                {
                    return g.Questions
                        .Where(q => q.IsDone)
                        .Sum(q => q.Score);
                });

                viewModel.Team.IsQuestDone = questionGroups.All(g => g.IsDone);

                if (!string.IsNullOrWhiteSpace(team.QuestionGroupsOrder))
                {
                    // пробуем взять группу из порядка заданного команде
                    var questionGroupsOrder = ParseQuestionGroupsOrder(team.QuestionGroupsOrder);
                    foreach (var index in questionGroupsOrder)
                    {
                        currentGroup = questionGroups.FirstOrDefault(g => g.Index == index && !g.IsDone);
                        if (currentGroup != null)
                        {
                            break;
                        }
                    }
                }

                // если порядок не задан, берем по индексу
                if (currentGroup == null)
                {                    
                    currentGroup = questionGroups.FirstOrDefault(g => !g.IsDone);
                }

                viewModel.CurrentGroup = currentGroup;

                // если блоки закончились, завершаем
                if (currentGroup == null)
                {
                    FinishQuest(team.Id);
                }

                if (team.HasFinished && team.FinishDate != null)
                {
                    viewModel.StatusCode = TeamStatus.Finished;
                }
            }

            return Ok(ServiceResponse.Ok(viewModel));
        }

        [HttpPost("sendAnswer")]
        public ActionResult SendAnswer(Guid questionId, string answer)
        {
            var team = _TeamRepository.RetrieveFirstOrDefault(t => t.Login == User.Identity.Name);
            var question = _QuestionRepository.RetrieveFirstOrDefault(q => q.Id == questionId);

            if (team != null && question != null)
            {
                var teamAnswer = _TeamAnswerRepository.RetrieveFirstOrDefault(ta => ta.QuestionId == questionId && ta.TeamId == team.Id);

                if (teamAnswer == null)
                {
                    teamAnswer = new TeamAnswer
                    {
                        QuestionId = questionId,
                        Answer = answer,
                        TeamId = team.Id,
                        IsDone = false,
                        Score = question.Score,
                        AttemptNumber = 1
                    };

                    _TeamAnswerRepository.Create(teamAnswer);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(teamAnswer.Answer))
                    {
                        teamAnswer.Answer = answer;
                    }
                    else
                    {
                        teamAnswer.Answer = string.Join(';', teamAnswer.Answer, answer);
                    }
                }

                // не обновляем уже отвеченный вопрос
                if (teamAnswer.IsDone)
                {
                    return Ok(ServiceResponse.Ok());
                }

                var answers = question.Answers.Split(';');
                foreach (var ans in answers)
                {
                    if (string.Compare(ans?.Trim(), answer?.Trim(), true) == 0)
                    {
                        teamAnswer.IsDone = true;
                        _TeamAnswerRepository.Update(teamAnswer);
                        return Ok(ServiceResponse.Ok());
                    }
                }

                // отнимаем балл за ошибки
                teamAnswer.Score -=
                    teamAnswer.AttemptNumber == 1 
                    ? 3 
                    : 2;

                teamAnswer.AttemptNumber++;

                if (teamAnswer.Score <= 0)
                {
                    teamAnswer.Score = 0;
                    teamAnswer.IsDone = true;
                }

                _TeamAnswerRepository.Update(teamAnswer);
            }

            return Ok(ServiceResponse.Ok());
        }

        [HttpPost("getQuestionHint")]
        public ActionResult GetQuestionHint(Guid questionId)
        {
            var team = _TeamRepository.RetrieveFirstOrDefault(t => t.Login == User.Identity.Name);
            var question = _QuestionRepository.RetrieveFirstOrDefault(q => q.Id == questionId);

            if (team != null && question != null)
            {
                var teamAnswer = _TeamAnswerRepository.RetrieveFirstOrDefault(ta => ta.QuestionId == questionId && ta.TeamId == team.Id);

                if (teamAnswer == null)
                {
                    teamAnswer = new TeamAnswer
                    {
                        QuestionId = questionId,
                        Answer = "",
                        TeamId = team.Id,
                        IsDone = false,
                        AttemptNumber = 2,
                        IsHintUsed = true,
                        Score = question.Score - 3
                    };

                    _TeamAnswerRepository.Create(teamAnswer);
                }
                else
                {
                    if (teamAnswer.IsDone || teamAnswer.IsHintUsed || teamAnswer.Score == 1)
                    {
                        return Ok(ServiceResponse.Ok());
                    }
                    
                    teamAnswer.IsHintUsed = true;                    
                    teamAnswer.Score = teamAnswer.Score -= teamAnswer.AttemptNumber <= 1
                        ? 3
                        : 2;

                    teamAnswer.AttemptNumber++;

                    _TeamAnswerRepository.Update(teamAnswer);
                }
            }

            return Ok(ServiceResponse.Ok());
        }

        private void FinishGroup(Team team, Guid groupId)
        {
            var questions = _QuestionRepository.RetrieveMultiple(q => q.GroupId == groupId);
            var teamAnswers = _TeamAnswerRepository.RetrieveMultiple(ta => ta.TeamId == team.Id);

            foreach (var question in questions)
            {
                var teamAnswer = teamAnswers.FirstOrDefault(ta => ta.QuestionId == question.Id);
                if (teamAnswer != null && !teamAnswer.IsDone)
                {
                    teamAnswer.IsDone = true;
                    teamAnswer.Score = 0;

                    _TeamAnswerRepository.Update(teamAnswer);
                }
                else if (teamAnswer == null)
                {
                    teamAnswer = new TeamAnswer
                    {
                        QuestionId = question.Id,
                        AttemptNumber = 1,
                        Score = 0,
                        Answer = string.Empty,
                        TeamId = team.Id,
                        IsDone = true
                    };

                    _TeamAnswerRepository.Create(teamAnswer);
                }
            }
        }

        [HttpPost("finishGroup")]
        public ActionResult FinishGroup([FromQuery]Guid groupId)
        {
            var team = _TeamRepository.RetrieveFirstOrDefault(t => t.Login == User.Identity.Name);

            if (team != null)
            {
                FinishGroup(team, groupId);
            }

            return Ok(ServiceResponse.Ok());
        }

        [HttpPost("startQuest")]
        public ActionResult StartQuest()
        {
            var team = _TeamRepository.RetrieveFirstOrDefault(t => t.Login == User.Identity.Name);
            if (team != null)
            {
                team.HasBegun = true;
                team.BeginDate = DateTime.UtcNow;

                _TeamRepository.Update(team);
            }

            return Ok(ServiceResponse.Ok());
        }

        private void FinishQuest(Guid teamId)
        {
            var team = _TeamRepository.Retrieve(teamId);

            if (!team.HasFinished || !team.FinishDate.HasValue)
            {
                var groups = _QuestionGroupRepository.RetrieveMultiple(gr => gr.QuestId == team.QuestId);
                foreach (var group in groups)
                {
                    FinishGroup(group.Id);
                }

                team.HasFinished = true;
                team.FinishDate = DateTime.UtcNow;
                _TeamRepository.Update(team);
            }
        }

        [HttpGet("getTeamStatus")]
        public ActionResult GetTeamStatus()
        {
            var status = new TeamStatus
            {
                Score = 0,
                Timer = 0,
                Code = TeamStatus.Initial
            };

            var team = _TeamRepository.GetByLoginWithQuest(User.Identity.Name);
            if (team != null && team.Quest != null)
            {
                if (team.HasFinished && team.FinishDate != null)
                {
                    status.Code = TeamStatus.Finished;
                }
                else if (team.HasBegun && team.BeginDate != null)
                {
                    var score = _TeamAnswerRepository.GetTeamScore(team.Id);

                    var diff = DateTime.UtcNow - team.BeginDate.Value;
                    var left = team.Quest.Duration - (int)Math.Floor(diff.TotalMinutes);
                    if (left <= 0)
                    {
                        left = 0;
                        FinishQuest(team.Id);
                    }

                    status.Code = left > 0 ? TeamStatus.Started : TeamStatus.Finished;
                    status.Score = score;
                    status.Timer = left;
                }
            }

            return Ok(ServiceResponse.Ok(status));
        }

        [HttpGet("getQuest")]
        public ActionResult GetQuest()
        {
            var team = _TeamRepository.GetByLoginWithQuest(User.Identity.Name);
            return Ok(ServiceResponse.Ok(team));
        }
    }
}