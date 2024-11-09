using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsuraceClaimApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly string? _secretKey;

        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration["JWT:SecretKey"];
        }

        public virtual async Task<string> GenerateToken(UserTokenDTO user)
        {
            string _token = string.Empty;
            var _claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                        new Claim(ClaimTypes.Role, user.Roles)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var _tokenDescriptor = new JwtSecurityToken(
                claims: _claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

           _token = new JwtSecurityTokenHandler().WriteToken(_tokenDescriptor);
            return _token;
        }
    }
}
