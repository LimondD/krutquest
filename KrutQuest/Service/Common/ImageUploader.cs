using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace KrutQuest.Service.Common
{
    /// <summary>
    /// Класс для загрузки изображений на сервер
    /// </summary>
    public static class ImageUploader
    {
        /// <summary>
        /// Виртуальный путь к папке с изображениями
        /// </summary>
        private const string _ImagesRootPath = "images";

        /// <summary>
        /// Объект для синхронизации потоков
        /// </summary>
        private static readonly object _SyncRoot = new object();

        /// <summary>
        /// Загружает файл на сервер
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="imageFile"></param>
        /// <returns>Виртуальный путь до загруженного файла</returns>
        public static string Upload(IHostingEnvironment hostingEnvironment, IFormFile imageFile)
        {
            var imagesPath = Path.Combine(hostingEnvironment.WebRootPath, _ImagesRootPath);
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            var fileName = Path.GetFileName(imageFile.FileName);
            var targetPath = Path.Combine(imagesPath, fileName);

            using (var targetStream = new FileStream(targetPath, FileMode.Create))
            {
                imageFile.CopyTo(targetStream);
            }

            return Path.Combine(_ImagesRootPath, fileName);
        }

        /// <summary>
        /// Удаление файла на сервере
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="virtualFilePath"></param>
        public static void Delete(IHostingEnvironment hostingEnvironment, string virtualFilePath)
        {
            var targetPath = Path.Combine(hostingEnvironment.WebRootPath, virtualFilePath);
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }
        }
    }
}
