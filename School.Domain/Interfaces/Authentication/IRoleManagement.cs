using School.Domain.Entities.Identity;

namespace School.Domain.Interfaces.Authentication
{
    public interface IRoleManagement
    {
        Task<string> GetUserRole(string userEmail);
        Task<bool> AddUserToRole(ApplicationUser user, string roleName);
        Task<string> GetRoleByUserId(Guid userId);
    }
}
