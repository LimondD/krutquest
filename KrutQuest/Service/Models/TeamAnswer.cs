using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrutQuest.Service.Models
{
    public class TeamAnswer : IDbObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Answer { get; set; }
        public int Score { get; set; }
        public bool IsDone { get; set; }
        public int AttemptNumber { get; set; }
        public bool IsHintUsed { get; set; }

        public Guid? QuestionId { get; set; }
        public Question Question { get; set; }

        public Guid? TeamId { get; set; }
        public Team Team { get; set; }
    }
}