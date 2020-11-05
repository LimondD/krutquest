using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Команда
    /// </summary>
    public class Team : IDbObject, IUserIdentity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool HasBegun { get; set; }
        public DateTime? BeginDate { get; set; }
        public string QuestionGroupsOrder { get; set; }
        public bool HasFinished { get; set; }
        public DateTime? FinishDate { get; set; }

        public Guid? QuestId { get; set; }
        public Quest Quest { get; set; }

        [JsonIgnore]
        public List<TeamAnswer> TeamAnswers { get; set; }
    }
}