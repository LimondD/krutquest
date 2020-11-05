using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KrutQuest.Service.Auth
{
    /// <summary>
    /// Настройки для генерации JWT-токена
    /// </summary>
    public static class JwtSettings
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public static string Issuer = "KrutQuest";

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public static string Audience = "KrutQuestUser";

        /// <summary>
        /// Время жизни токена в минутах
        /// </summary>
        public static int TokenLifeTimeMinutes = 2880;

        /// <summary>
        /// Ключ для подписи токена
        /// </summary>
        public static string SigningKey = "4Y9z5q+w?dUgwQr+";
    }
}
