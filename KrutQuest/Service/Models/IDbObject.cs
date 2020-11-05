using System;

namespace KrutQuest.Service.Models
{
    /// <summary>
    /// Интерфейс сущности БД
    /// </summary>
    public interface IDbObject
    {
        Guid Id { get; set; }
    }
}
