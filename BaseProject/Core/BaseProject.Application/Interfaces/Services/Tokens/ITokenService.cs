using System.Threading.Tasks;

namespace BaseProject.Application.Interfaces.Services.Tokens
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string userId);
        Task<bool> ValidateCurrentToken(string token);
        Task<string> GetClaim(string token, string claimType);
        Task<string> GenerateRefreshToken();
    }
}
