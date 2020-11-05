using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KrutQuest.Service.Models;
using KrutQuest.Service.Transport;
using KrutQuest.Service.Repositories.Abstract;

namespace KrutQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/Team")]
    public class TeamController : ControllerBase
    {
        /// <summary>
        /// Репозиторий (Команда)
        /// </summary>
        private readonly ITeamRepository _TeamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _TeamRepository = teamRepository;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _TeamRepository.GetAll();
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpGet("getById")]
        public IActionResult GetById(Guid teamId)
        {
            var result = _TeamRepository.Retrieve(teamId);
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]Team team)
        {
            _TeamRepository.Create(team);
            return Ok(ServiceResponse.Ok());
        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]Team team)
        {
            _TeamRepository.Update(team);
            return Ok(ServiceResponse.Ok());
        }

        [HttpGet("delete")]
        public IActionResult Delete(Guid teamId)
        {
            _TeamRepository.Delete(teamId);
            return Ok(ServiceResponse.Ok());
        }
    }
}