using System;
using Microsoft.AspNetCore.Http;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Transport
{
    public class QuestSaveRequest
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }
        public int Duration { get; set; }
        public string RulesText { get; set; }
        public string FinishText { get; set; }

        public IFormFile FinishPicture { get; set; }

        public Quest ToQuest()
        {
            var quest = new Quest
            {
                Name = Name,
                Duration = Duration,
                RulesText = RulesText,
                FinishText = FinishText                
            };

            if (Id.HasValue)
            {
                quest.Id = Id.Value;
            }

            return quest;
        }
    }
}
