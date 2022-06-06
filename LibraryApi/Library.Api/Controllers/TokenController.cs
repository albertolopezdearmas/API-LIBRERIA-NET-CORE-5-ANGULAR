using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;


        public TokenController(IConfiguration configuration, ISecurityService securityService)
        {
            _configuration = configuration;
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            //if it is a valid user
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                string token = GenerateToken(validation.Item2);
                return Ok(new AuthResult()
                {
                    Token = token,
                    Success = true,
                });
            }

            return Ok(new AuthResult()
            {
                Success = false,
                Errors = new List<string>() {
                    "Usuario y/o contraseña no validos"
                }
            });
        }

        private async Task<(bool, Security)> IsValidUser(UserLogin login)
        {
            var user = await _securityService.GetLoginByCredentials(login, _configuration["UriApiData"]);
            return (user != null, user);
        }

        private string GenerateToken(Security security)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.UserName),
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddDays(10)
            );

            var token = new JwtSecurityToken(header, payload);
            string tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);
            Response.Cookies.Append("X-Access-Token", tokenResponse, new CookieOptions() { HttpOnly = false, Secure=true,SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Username", security.UserName, new CookieOptions() { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict });
            //Response.Cookies.Append("X-Refresh-Token", user.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            return tokenResponse;
        }
        
    }
}
