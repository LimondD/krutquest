using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Вопрос
    /// </summary>
    public class Question : IDbObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }
        public string Answers { get; set; }
        public int Score { get; set; }
        public string Hint { get; set; }

        public Guid? GroupId { get; set; } 
        public QuestionGroup Group { get; set; }

        [JsonIgnore]
        public List<TeamAnswer> TeamAnswers { get; set; }

        public Guid? PictureId { get; set; }
        public ServerImage Picture { get; set; }
    }
}