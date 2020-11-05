using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Изображение на сервере
    /// </summary>
    public class ServerImage : IDbObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string VirtualFilePath { get; set; }

        [JsonIgnore]
        public List<Quest> Quests { get; set; }

        [JsonIgnore]
        public List<Question> Questions { get; set; }

        [JsonIgnore]
        public List<QuestionGroup> Groups { get; set; }
    }
}
