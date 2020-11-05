using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KrutQuest.Service.Models;
using KrutQuest.Service.Transport;
using KrutQuest.Service.Repositories.Abstract;
using KrutQuest.Service.Models.View;

namespace KrutQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/TeamAnswer")]
    public class TeamAnswerController : ControllerBase
    {
        /// <summary>
        /// Репозиторий (Ответ команды)
        /// </summary>
        private readonly ITeamAnswerRepository _TeamAnswerRepository;

        /// <summary>
        /// Репозиторий (Команда)
        /// </summary>
        private readonly ITeamRepository _TeamRepository;

        public TeamAnswerController(ITeamAnswerRepository teamAnswerRepository, ITeamRepository teamRepository)
        {
            _TeamAnswerRepository = teamAnswerRepository;
            _TeamRepository = teamRepository;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var model = new List<TeamAnswerViewModel>();

            var teams = _TeamRepository.GetAll();
            foreach (var team in teams)
            {
                var teamModel = new TeamAnswerViewModel
                {
                    Id = team.Id,
                    Name = team.Name,
                    QuestName = team.Quest?.Name,
                    Score = _TeamAnswerRepository.GetTeamScore(team.Id),
                    Time = 0
                };

                if (team.BeginDate.HasValue)
                {
                    if (team.FinishDate.HasValue)
                    {
                        teamModel.Time = (int)(team.FinishDate.Value - team.BeginDate.Value).TotalMinutes;
                    }
                    else
                    {
                        teamModel.Time = (int)(DateTime.UtcNow - team.BeginDate.Value).TotalMinutes;
                    }
                }

                if (team.Quest != null && teamModel.Time > team.Quest.Duration)
                {
                    teamModel.Time = team.Quest.Duration;
                }

                model.Add(teamModel);
            }            

            return Ok(ServiceResponse.Ok(model));
        }

        [HttpGet("getById")]
        public IActionResult GetById(Guid teamId)
        {
            TeamAnswerViewModel model = null;

            var team = _TeamRepository.GetAll(t => t.Id == teamId).FirstOrDefault();
            if (team != null)
            {
                var teamAnswers = _TeamAnswerRepository.GetByTeamIdWithQuestion(teamId);

                model = new TeamAnswerViewModel
                {
                    Id = team.Id,
                    Name = team.Name,
                    QuestName = team.Quest?.Name,
                    Score = teamAnswers.Where(ta => ta.IsDone).Sum(ta => ta.Score),
                    Time = team.Quest != null ? team.Quest.Duration : 0
                };

                if (team.BeginDate.HasValue && team.FinishDate.HasValue)
                {
                    model.Time = (int)(team.FinishDate.Value - team.BeginDate.Value).TotalMinutes;
                }
                
                model.Details = new List<TeamAnswerViewDetails>();
                foreach (var answer in teamAnswers)
                {
                    var detailsModel = new TeamAnswerViewDetails
                    {
                        GroupName = answer.Question?.Group?.Name,
                        QuestionId = answer.QuestionId ?? Guid.Empty,
                        QuestionText = answer.Question?.Text,
                        Score = answer.IsDone ? answer.Score : 0,
                        Id = answer.Id,
                        Answer = answer.Answer,
                        IsHintUsed = answer.IsHintUsed
                    };
                    model.Details.Add(detailsModel);
                }
            }

            return Ok(ServiceResponse.Ok(model));
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody]TeamAnswerViewModel viewModel)
        {
            if (viewModel != null && viewModel.Details != null)
            {
                foreach (var detail in viewModel.Details)
                {
                    var teamAnswer = _TeamAnswerRepository.RetrieveFirstOrDefault(ta => ta.Id == detail.Id);
                    teamAnswer.Score = detail.Score;
                    teamAnswer.IsDone = true;
                    _TeamAnswerRepository.Update(teamAnswer);
                }
            }

            return Ok(ServiceResponse.Ok());
        }
    }
}