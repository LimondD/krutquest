using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Группа вопросов
    /// </summary>
    public class QuestionGroup : IDbObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Index { get; set; }

        public Guid? QuestId { get; set; }
        public Quest Quest { get; set; }

        [JsonIgnore]
        public List<Question> Questions { get; set; }

        public Guid? MapPictureId { get; set; }
        public ServerImage MapPicture { get; set; }
    }
}