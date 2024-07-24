using BaseProject.Application.Interfaces.Services.Tokens;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BaseProject.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace BaseProject.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings tokenSettings;

        public TokenService(IOptions<TokenSettings> options)
        {
            this.tokenSettings = options.Value;
        }

        public async Task<string> GenerateTokenAsync(string userId)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim("UserId", JsonSerializer.Serialize(userId)),
                   new Claim("IssuedAt", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")),
                   new Claim("Expires", DateTime.UtcNow.AddMinutes(tokenSettings.TokenLifeTime).ToString("yyyy-MM-dd HH:mm:ss")),
                   new Claim("IsAdmin", "false"),
                   new Claim(JwtRegisteredClaimNames.Email, "hakanyavaseng@gmail.com")
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenSettings.TokenLifeTime),
                Issuer = tokenSettings.ValidIssuer,
                Audience = tokenSettings.ValidAudience,
                NotBefore = DateTime.UtcNow,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }

        public async Task<string> GetClaim(string token, string claimType)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
            var stringClaimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
            return await Task.FromResult(stringClaimValue);
        }

        public async Task<bool> ValidateCurrentToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = tokenSettings.ValidateIssuer,
                    ValidateAudience =  tokenSettings.ValidateAudience,
                    ValidateLifetime = tokenSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = tokenSettings.ValidateIssuerSigningKey,
                    ValidIssuer = tokenSettings.ValidIssuer,
                    ValidAudience = tokenSettings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }


        public async Task<string> GenerateRefreshToken()
        {
            //Will be implemented in the future
            return await Task.FromResult(Guid.NewGuid().ToString());
        }

    }
}
