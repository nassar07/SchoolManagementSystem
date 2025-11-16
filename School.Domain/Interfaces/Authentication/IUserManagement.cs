using System.Security.Claims;
using School.Domain.Entities.Identity;

namespace School.Domain.Interfaces.Authentication
{
    public interface IUserManagement
    {
        Task<bool> CreateUser(ApplicationUser user);
        Task<bool> LoginUser(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserById(Guid id);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<int> RemoveUserByEmail(string email);
        Task<List<Claim>> GetUserClaims(string email);
    }
}
