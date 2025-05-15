using FinalProjectLibrary.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectLibrary.Helpers
{
    public class Helper
    {
        public static class IdentityDataInitializer
        {
            public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                string[] roles = { "SuperAdmin", "Librarian" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Example: Assign SuperAdmin role to a user
                var admin = await userManager.FindByEmailAsync("admin@example.com");
                if (admin != null && !await userManager.IsInRoleAsync(admin, "SuperAdmin"))
                {
                    await userManager.AddToRoleAsync(admin, "SuperAdmin");
                }
            }
        }


    }
}
