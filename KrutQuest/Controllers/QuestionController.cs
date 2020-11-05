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
    [Route("api/Question")]
    public class QuestionController : ControllerBase
    {
        /// <summary>
        /// Репозиторий (Вопрос)
        /// </summary>
        private readonly IQuestionRepository _QuestionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _QuestionRepository = questionRepository;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _QuestionRepository.GetAll();
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpGet("getById")]
        public IActionResult GetById(Guid questionId)
        {
            var result = _QuestionRepository.Retrieve(questionId);
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]Question question)
        {
            _QuestionRepository.Create(question);
            return Ok(ServiceResponse.Ok());
        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]Question question)
        {
            _QuestionRepository.Update(question);
            return Ok(ServiceResponse.Ok());
        }

        [HttpGet("delete")]
        public IActionResult Delete(Guid questionId)
        {
            _QuestionRepository.Delete(questionId);
            return Ok(ServiceResponse.Ok());
        }
    }
}