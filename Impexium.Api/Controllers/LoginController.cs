using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Impexium.Entities.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Impexium.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private const string USER_AND_PASS = "admin";

        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var user = AutenticarUsuarioAsync(loginRequest.User, loginRequest.Password);
            if (!string.IsNullOrEmpty(user))
            {
                return Ok(new { token = GenerarTokenJWT(user) });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string AutenticarUsuarioAsync(string usuario, string password)
        {
            var result = string.Empty;
            if(usuario == USER_AND_PASS && password == USER_AND_PASS)
            {
                result = "ADMIN";
            }
            return result;
        }

        private string GenerarTokenJWT(string user)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(configuration["JWT:Pass"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("User", user)
            };

            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(24)
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
