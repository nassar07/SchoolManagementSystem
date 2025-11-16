using System.Security.Claims;

namespace School.Domain.Interfaces.Authentication
{
    public interface ITokenManagement
    {
        string GetRefreshToken();
        List<Claim> GetClaimsFromToken(string token);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<Guid> GetUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(Guid userId, string refreshToken);
        Task<int> UpdateRefreshToken(Guid userId, string refreshToken);
        string GenerateToken(List<Claim> claims);
    }
}
