using System;
using System.Collections.Generic;

namespace KrutQuest.Service.Models.View
{
    public class MainViewModel
    {               
        public MainViewTeam Team { get; set; }
        public MainViewQuest Quest { get; set; }
        public MainViewQuestionGroup CurrentGroup { get; set; }
        public string StatusCode { get; set; }
    }

    public class MainViewTeam
    {
        public string Name { get; set; }
        public bool HasBegun { get; set; }
        public DateTime? BeginDate { get; set; }
        public bool IsQuestDone { get; set; }
        public int TotalScore { get; set; }
    }

    public class MainViewQuest
    {
        public string Name { get; set; }
        public string RulesText { get; set; }
        public string FinishText { get; set; }
        public ServerImage FinishPicture { get; set; }
    }

    public class MainViewQuestionGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int Index { get; set; }
        public ServerImage MapPicture { get; set; }

        public List<MainViewQuestion> Questions { get; set; }
    }

    public class MainViewQuestion
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public bool IsDone { get; set; }
        public string Hint { get; set; }
        public bool IsHintUsed { get; set; }
        public ServerImage Picture { get; set; }
        public string Answer { get; set; }
    }
}