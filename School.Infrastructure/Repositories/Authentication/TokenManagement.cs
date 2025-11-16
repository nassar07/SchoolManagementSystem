using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.Domain.Interfaces.Authentication;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context, IConfiguration config) : ITokenManagement
    {
        public async Task<int> AddRefreshToken(Guid userId, string refreshToken)
        {
            context.RefreshTokens.Add(new()
            {
                Token = refreshToken,
                UserId = userId
            });
            return await context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(2);
            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<Claim> GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Jwttoken = tokenHandler.ReadJwtToken(token);
            if (Jwttoken == null) return [];
            return Jwttoken.Claims.ToList();
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomNumber = new byte[byteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            string token = Convert.ToBase64String(randomNumber);
            return WebUtility.UrlEncode(token);
        }

        public async Task<Guid> GetUserIdByRefreshToken(string refreshToken)
        {
            var UserId = await context.RefreshTokens
                .Where(rt => rt.Token == refreshToken)
                .Select(rt => rt.UserId)
                .FirstOrDefaultAsync();
            return UserId;
        }

        public Task<int> UpdateRefreshToken(Guid userId, string refreshToken)
        {
            var existingToken = context.RefreshTokens
                .FirstOrDefault(rt => rt.UserId == userId);
            if (existingToken != null)
            {
                existingToken.Token = refreshToken;
            }
            return context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var userToken = await context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            return userToken != null;
        }
    }
}
