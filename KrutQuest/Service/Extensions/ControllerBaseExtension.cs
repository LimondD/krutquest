using System;
using Microsoft.AspNetCore.Mvc;

namespace KrutQuest.Service.Extensions
{
    /// <summary>
    /// Расширение для класса ControllerBase
    /// </summary>
    public static class ControllerBaseExtension
    {
        /// <summary>
        /// Возвращает URL-путь для локального файла по виртуальному пути
        /// </summary>
        /// <param name="controller">Контроллер</param>
        /// <param name="virtualFilePath">Виртуальный путь до файла</param>
        /// <returns>URL до файла</returns>
        public static string GetLocalFileUrl(ControllerBase controller, string virtualFilePath)
        {
            return controller.Request.Scheme + "://" + controller.Request.Host + "/" + virtualFilePath.Replace('\\', '/');
        }
    }
}
