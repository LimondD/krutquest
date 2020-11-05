using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Квест
    /// </summary>
    public class Quest : IDbObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Duration { get; set; }
        public string RulesText { get; set; }
        public string FinishText { get; set; }

        public Guid? FinishPictureId { get; set; }
        public ServerImage FinishPicture { get; set; }

        [JsonIgnore]
        public List<Team> Teams { get; set; }

        [JsonIgnore]
        public List<QuestionGroup> Groups { get; set; }
    }
}