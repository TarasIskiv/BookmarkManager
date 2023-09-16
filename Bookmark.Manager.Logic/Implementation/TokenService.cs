using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Logic.Abstraction;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Bookmark.Manager.Core.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace Bookmark.Manager.Logic.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;
        public TokenService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<string> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = await Task.Run(() => tokenHandler.CreateToken(tokenDescriptor));
            var userToken = await Task.Run(() => tokenHandler.WriteToken(token));

            return userToken.ToString();
        }

    }
}