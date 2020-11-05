using System;

namespace KrutQuest.Service.Transport
{
    /// <summary>
    /// Ответ сервиса
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Флаг успешного выполнения запроса
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Содержимое ответа
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Создать принятый ответ на запрос
        /// </summary>
        /// <param name="content">Содержимое ответа</param>
        /// <returns>Ответ сервиса</returns>
        public static ServiceResponse Ok(object content = null)
        {
            return new ServiceResponse
            {
                Success = true,
                Content = content
            };
        }

        /// <summary>
        /// Создать отклоненный ответ на запрос
        /// </summary>
        /// <param name="error">Текст ошибки</param>
        /// <returns>Ответ сервиса</returns>
        public static ServiceResponse Cancel(string error)
        {
            return new ServiceResponse
            {
                Success = false,
                Error = error
            };
        }
    }
}
