using AuthApi.Entities.Identity;
using AuthApi.Models.Identity.User;
using AuthApi.Repositories.Identity;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace AuthApi.Seeding.Identity
{
    public static class Users
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
                var userRepository = scope.ServiceProvider.GetService<IUserRepository>();

                var roleSuperadmin = await roleManager.FindByNameAsync(LibUniversal.Constants.Identity.ROLE_SUPERADMIN);
                var roleAdmin = await roleManager.FindByNameAsync(LibUniversal.Constants.Identity.ROLE_ADMIN);

                if (roleSuperadmin == null || roleAdmin == null)
                {
                    throw new Exception("Roles not seeded.");
                }

                var superadminSeed = JsonSerializer.Deserialize<SuperAdminSeed>(app.Configuration.GetSection("Superadmin").ToString() ?? "");

                var superadmin = await CreateUser(
                    userRepository,
                    superadminSeed.Email,
                    superadminSeed.Password,
                    new List<Role>() { roleSuperadmin, roleAdmin }
                );
                await userRepository.AssignUserToRole(superadmin, roleSuperadmin);
            }
        }

        private static async Task<User?> CreateUser(IUserRepository userRepository, string username, string password, List<Role> roles)
        {
            var exception = new Exception($"Creating user {username} failed.");
            var existing = await userRepository.GetOneOrDefaultByName(username);

            if (existing != null) return existing;

            var user = new User
            {
                UserName = username,
                Email = username
            };

            // UserName is handled in the UserProfile.
            IdentityResult result = await userRepository.Create(user, password);
            if (result.Succeeded)
            {
                string token = await userRepository.GetEmailConfirmationToken(user);
                IdentityResult confirmationResult = await userRepository.ConfirmEmail(user, token);
                if (!confirmationResult.Succeeded) throw exception;
                IdentityResult unlockResult = await userRepository.SetUserLockout(user, false);
                if (!unlockResult.Succeeded) throw exception;
                return user;
            }
            else
            {
                throw exception;
            }
        }
    }
}
