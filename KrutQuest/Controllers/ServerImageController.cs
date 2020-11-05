using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KrutQuest.Service.Common;
using KrutQuest.Service.Models;
using KrutQuest.Service.Extensions;
using KrutQuest.Service.Repositories.Abstract;
using KrutQuest.Service.Transport;
using Microsoft.AspNetCore.Hosting;

namespace KrutQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/ServerImage")]
    public class ServerImageController : ControllerBase
    {
        /// <summary>
        /// Репозиторий (Изображение)
        /// </summary>
        private readonly IServerImageRepository _ServerImageRepository;

        private readonly IHostingEnvironment _HostingEnvironment;

        public ServerImageController(IHostingEnvironment hostingEnvironment, IServerImageRepository serverImageRepository)
        {
            _HostingEnvironment = hostingEnvironment;
            _ServerImageRepository = serverImageRepository;
        }

        [HttpPost("Upload")]
        public IActionResult Upload()
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile != null)
            {
                var virtualFilePath = ImageUploader.Upload(_HostingEnvironment, formFile);
                
                var image = new ServerImage
                {
                    Name = formFile.FileName,
                    VirtualFilePath = virtualFilePath
                };

                _ServerImageRepository.Create(image);
            }

            return Ok(ServiceResponse.Ok());
        }

        [HttpGet("Delete")]
        public IActionResult Delete(Guid serverImageId)
        {
            var image = _ServerImageRepository.Retrieve(serverImageId);
            if (image != null && !string.IsNullOrWhiteSpace(image.VirtualFilePath))
            {
                ImageUploader.Delete(_HostingEnvironment, image.VirtualFilePath);
                _ServerImageRepository.Delete(serverImageId);
            }
            
            return Ok(ServiceResponse.Ok());
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _ServerImageRepository.RetrieveAll();
            return Ok(ServiceResponse.Ok(result));
        }
    }
}