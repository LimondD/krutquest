using System;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Интерфейс личности пользователя
    /// </summary>
    interface IUserIdentity : IDbObject
    {
        string Login { get; set; }

        string Password { get; set; }
    }
}
