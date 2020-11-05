using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using KrutQuest.Service.Models;
using KrutQuest.Service.Transport;
using KrutQuest.Service.Repositories.Abstract;

namespace KrutQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/Quest")]
    public class QuestController : ControllerBase
    {
        private readonly IHostingEnvironment _HostingEnvironment;

        /// <summary>
        /// Репозиторий (Квест)
        /// </summary>
        private readonly IQuestRepository _QuestRepository;
        
        public QuestController(IHostingEnvironment hostingEnvironment, IQuestRepository questRepository)
        {
            _HostingEnvironment = hostingEnvironment;
            _QuestRepository = questRepository;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _QuestRepository.GetAll();
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpGet("getById")]
        public IActionResult GetById(Guid questId)
        {            
            var result = _QuestRepository.Retrieve(questId);
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]Quest quest)
        {
            _QuestRepository.Create(quest);
            return Ok(ServiceResponse.Ok());
        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]Quest quest)
        {            
            _QuestRepository.Update(quest);
            return Ok(ServiceResponse.Ok());
        }

        [HttpGet("delete")]
        public IActionResult Delete(Guid questId)
        {
            _QuestRepository.Delete(questId);
            return Ok(ServiceResponse.Ok());
        }
    }
}