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
    [Route("api/QuestionGroup")]
    public class QuestionGroupController : ControllerBase
    {
        /// <summary>
        /// Репозиторий (Группа вопросов)
        /// </summary>
        private readonly IQuestionGroupRepository _QuestionGroupRepository;

        public QuestionGroupController(IQuestionGroupRepository questionGroupRepository)
        {
            _QuestionGroupRepository = questionGroupRepository;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _QuestionGroupRepository.GetAll();
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpGet("getById")]
        public IActionResult GetById(Guid questionGroupId)
        {
            var result = _QuestionGroupRepository.Retrieve(questionGroupId);
            return Ok(ServiceResponse.Ok(result));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]QuestionGroup questionGroup)
        {
            // проставляем индекс для группы
            if (questionGroup.QuestId.HasValue && questionGroup.QuestId.Value != Guid.Empty)
            {
                questionGroup.Index = _QuestionGroupRepository.GetNextIndex(questionGroup.QuestId.Value);                
            }

            _QuestionGroupRepository.Create(questionGroup);
            return Ok(ServiceResponse.Ok());
        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]QuestionGroup questionGroup)
        {            
            // запоминаем состояние до обновления
            var preImage = _QuestionGroupRepository.Retrieve(questionGroup.Id);

            questionGroup.Index = preImage.Index;
            _QuestionGroupRepository.Update(questionGroup);

            // если изменился связанный квест, пересчитаем все индексы            
            if (questionGroup.QuestId != preImage.QuestId)
            {
                if (preImage.QuestId.HasValue && preImage.QuestId.Value != Guid.Empty)
                {
                    UpdateIndexes(preImage.QuestId.Value);
                }

                if (questionGroup.QuestId.HasValue && questionGroup.QuestId.Value != Guid.Empty)
                {
                    UpdateIndexes(questionGroup.QuestId.Value);
                }
            }            
            return Ok(ServiceResponse.Ok());
        }

        [HttpGet("delete")]
        public IActionResult Delete(Guid questionGroupId)
        {
            // запоминаем состояние до обновления
            var preImage = _QuestionGroupRepository.Retrieve(questionGroupId);

            _QuestionGroupRepository.Delete(questionGroupId);

            if (preImage.QuestId.HasValue && preImage.QuestId.Value != Guid.Empty)
            {
                UpdateIndexes(preImage.QuestId.Value);
            }

            return Ok(ServiceResponse.Ok());
        }

        /// <summary>
        /// Обновляет индексы на группах данного квеста
        /// </summary>
        /// <param name="questId">ID Квеста</param>
        private void UpdateIndexes(Guid questId)
        {
            var questionGroups = _QuestionGroupRepository.RetrieveMultiple(g => g.QuestId == questId);
            for (var i = 0; i < questionGroups.Count; i++)
            {
                questionGroups[i].Index = i + 1;
                _QuestionGroupRepository.Update(questionGroups[i]);
            }
        }
    }
}