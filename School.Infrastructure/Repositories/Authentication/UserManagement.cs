using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities.Identity;
using School.Domain.Interfaces.Authentication;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.Authentication
{
    public class UserManagement(IRoleManagement roleManagement, UserManager<ApplicationUser> userManager, AppDbContext context) : IUserManagement
    {
        public async Task<bool> CreateUser(ApplicationUser user)
        {
            ApplicationUser _user = await GetUserByEmail(user.Email!);
            if (_user != null)
                return false;

            var result = await userManager.CreateAsync(user, user.PasswordHash!);
            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return null!;
            return user;
        }

        public async Task<ApplicationUser> GetUserById(Guid id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return null!;
            return user;
        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await GetUserByEmail(email);
            var userRoles = await roleManagement.GetUserRole(email);

            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, userRoles)
            ];

            return claims;
        }

        public async Task<bool> LoginUser(ApplicationUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user is null) return false;

            string RoleName = await roleManagement.GetUserRole(user.Email!);
            if (RoleName == null) return false;

            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            context.Users.Remove(user!);
            return await context.SaveChangesAsync();
        }
    }
}
