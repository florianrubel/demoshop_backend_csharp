using AuthApi.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Shared.Models.Api;

namespace AuthApi.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<IdentityResult> AssignUserToRole(User user, Role role);
        Task<IdentityResult> ConfirmEmail(User user, string token);
        Task<IdentityResult> Create(User user, string plainPassword);
        Task<string> GetEmailConfirmationToken(User user);
        Task<IEnumerable<User>> GetMultiple(IEnumerable<string> userIds, ShapingWithOrderingParameters parameters);
        Task<PagedList<User>> GetMultiple(SearchParameters parameters);
        Task<User?> GetOneOrDefault(string id);
        Task<User?> GetOneOrDefaultByName(string username);
        Task<string> GetPasswordResetToken(User user);
        Task<IEnumerable<string>> GetUserRoles(User user);
        Task<IdentityResult> RemoveUserFromRole(User user, Role role);
        Task<IdentityResult> ResetUserPassword(User user, string passwordResetToken, string plainPassword);
        Task<IdentityResult> SetUserLockout(User user, bool lockout);
        Task<IdentityResult> Update(User user);
    }
}