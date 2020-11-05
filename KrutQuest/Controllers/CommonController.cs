using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrutQuest.Service.Data;
using KrutQuest.Service.Models;
using KrutQuest.Service.Repositories.Abstract;
using KrutQuest.Service.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KrutQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/Common")]
    public class CommonController : ControllerBase
    {
        private readonly ITechUserRepository _TechUserRepository;

        public CommonController(ITechUserRepository techUserRepository)
        {
            _TechUserRepository = techUserRepository;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(DateTime.Now);
        }

        [Authorize]
        [HttpGet("whoAmI")]
        public IActionResult WhoAmI()
        {            
            return Ok(User.Identity.Name);
        }

        [HttpGet("getContactInfo")]
        public IActionResult GetContactInfo()
        {
            var techUser = _TechUserRepository.RetrieveFirstOrDefault(user => user.Login == "Admin");
            if (techUser != null)
            {
                return Ok(ServiceResponse.Ok(new TechUser
                {
                    ContactInfo = techUser.ContactInfo
                }));
            }

            return Ok(ServiceResponse.Ok());
        }

        [HttpPost("setContactInfo")]
        public IActionResult SetContactInfo([FromBody]TechUser techUser)
        {
            var techUserToUpdate = _TechUserRepository.RetrieveFirstOrDefault(user => user.Login == "Admin");
            if (techUserToUpdate != null)
            {
                techUserToUpdate.ContactInfo = techUser.ContactInfo;
                _TechUserRepository.Update(techUserToUpdate);
            }

            return Ok(ServiceResponse.Ok());
        }
    }
}