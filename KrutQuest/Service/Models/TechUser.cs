using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Технический пользователь
    /// </summary>
    public class TechUser : IUserIdentity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string ContactInfo { get; set; }
    }
}