using LGWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LGWebsite.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public AuthController()
        {
        }

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] LoginModel model)
        {
            if (model.ClientId == AppSettings.Authentication.ClientId && model.ClientSecret == AppSettings.Authentication.ClientSecret)
            {
                var accessToken = GenerateAccessToken(model.ClientId);

                return Ok(new { accessToken });
            }

            return Unauthorized();
        }

        private string GenerateAccessToken(string clientId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(AppSettings.JwtConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("client_id", clientId) }),
                Expires = DateTime.UtcNow.AddHours(1), // Short-lived access token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = AppSettings.JwtConfig.Issuer,
                Audience = AppSettings.JwtConfig.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
