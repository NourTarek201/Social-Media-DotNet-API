using Microsoft.IdentityModel.Tokens;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Servises
{

    namespace SocialMedia.Servises
    {
        public class LoginToken : ILogingToken
        {
            private readonly IConfiguration _configuration;
            public LoginToken(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public string GenerateJwtToken(User user)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["AppSettings:Subject"]!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email.ToString()),
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["AppSettings:Issuer"],
                    _configuration["AppSettings:Audience"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signIn
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

        }
    }

}
