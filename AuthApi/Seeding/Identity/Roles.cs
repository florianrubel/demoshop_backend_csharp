using AuthApi.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Seeding.Identity
{
    public static class Roles
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                //Resolve ASP .NET Core Identity with DI help

                var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();

                foreach (var roleName in Shared.Constants.Identity.ROLES)
                {
                    if (await roleManager.FindByNameAsync(roleName) == null)
                    {
                        await roleManager.CreateAsync(new Role { Name = roleName });
                    }
                }
            }
        }
    }
}
