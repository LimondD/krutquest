using System;
using System.Collections.Generic;

namespace KrutQuest.Service.Models.View
{
    public class TeamAnswerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public string QuestName { get; set; }

        public List<TeamAnswerViewDetails> Details { get; set; }
    }

    public class TeamAnswerViewDetails
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int Score { get; set; }
        public string GroupName { get; set; }
        public string Answer { get; set; }
        public bool IsHintUsed { get; set; }
    }
}
