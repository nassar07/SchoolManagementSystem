using Microsoft.AspNetCore.Identity;
using School.Domain.Entities.Identity;
using School.Domain.Interfaces.Authentication;

namespace School.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<ApplicationUser> userManager) : IRoleManagement
    {
        public async Task<bool> AddUserToRole(ApplicationUser user, string roleName) =>
           (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<string> GetRoleByUserId(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return string.Empty;
            var role = await userManager.GetRolesAsync(user!).ContinueWith(t => t.Result.FirstOrDefault() ?? string.Empty);
            return role;
        }

        public async Task<string> GetUserRole(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            return await userManager.GetRolesAsync(user!).ContinueWith(t => t.Result.FirstOrDefault() ?? string.Empty);
        }
    }
}
