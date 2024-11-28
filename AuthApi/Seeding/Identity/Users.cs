using AuthApi.Entities.Identity;
using AuthApi.Repositories.Identity;
using Microsoft.AspNetCore.Identity;

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

                var roleSuperadmin = await roleManager.FindByNameAsync(Shared.Constants.Identity.ROLE_SUPERADMIN);
                var roleAdmin = await roleManager.FindByNameAsync(Shared.Constants.Identity.ROLE_ADMIN);

                if (roleSuperadmin == null || roleAdmin == null)
                {
                    throw new Exception("Roles not seeded.");
                }

                var superadmin = await CreateUser(
                    userRepository,
                    "superadmin@example.org",
                    "Superadmin123!",
                    new List<Role>() { roleSuperadmin, roleAdmin }
                );
                await userRepository.AssignUserToRole(superadmin, roleSuperadmin);
                await userRepository.AssignUserToRole(superadmin, roleAdmin);
                var admin = await CreateUser(
                    userRepository,
                    "admin@example.org",
                    "Admin123!",
                    new List<Role>() { roleAdmin }
                );
                await userRepository.AssignUserToRole(admin, roleAdmin);
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
