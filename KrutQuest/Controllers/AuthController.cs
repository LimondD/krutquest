using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using KrutQuest.Service.Auth;
using KrutQuest.Service.Models;
using KrutQuest.Service.Transport;
using KrutQuest.Service.Repositories.Abstract;

namespace KrutQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Репозиторий (Команда)
        /// </summary>
        private readonly ITeamRepository _TeamRepository;

        /// <summary>
        /// Репозиторий (Технический пользователь)
        /// </summary>
        private readonly ITechUserRepository _TechUserRepository;

        public AuthController(ITeamRepository teamRepository, ITechUserRepository techUserRepository)
        {
            _TeamRepository = teamRepository;
            _TechUserRepository = techUserRepository;
        }

        [HttpPost("LoginAdmin")]
        public IActionResult LoginAdmin([FromBody]LoginRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Login)
                && !string.IsNullOrWhiteSpace(request.Password))
            {                
                var techUser = _TechUserRepository.GetByLoginAndPassword(request.Login, request.Password);
                if (techUser != null)
                {
                    var encodedJwt = MakeAuthToken(techUser, "TechUser");
                    return Ok(ServiceResponse.Ok(new { accessToken = encodedJwt, userType = "tech" }));
                }
            }

            return Ok(ServiceResponse.Cancel(ServiceMessages.NoSuchLoginAndPasswordUserError));
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Login)
                && !string.IsNullOrWhiteSpace(request.Password))
            {
                var teamUser = _TeamRepository.GetByLoginAndPassword(request.Login, request.Password);
                if (teamUser != null)
                {
                    var encodedJwt = MakeAuthToken(teamUser, "Team");
                    return Ok(ServiceResponse.Ok(new { accessToken = encodedJwt, userType = "team" }));
                }
            }

            return Ok(ServiceResponse.Cancel(ServiceMessages.NoSuchLoginAndPasswordUserError));
        }

        /// <summary>
        /// Создает токен авторизации для пользователя
        /// </summary>
        /// <param name="userIdentity">Пользователь</param>
        /// <param name="roleName">Имя роли пользователя</param>
        /// <returns>Токен</returns>
        private string MakeAuthToken(IUserIdentity userIdentity, string roleName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userIdentity.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName),
            };

            var identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: JwtSettings.Issuer,
                    audience: JwtSettings.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(JwtSettings.TokenLifeTimeMinutes)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SigningKey)), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}