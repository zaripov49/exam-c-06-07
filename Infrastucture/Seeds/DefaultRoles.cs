using Microsoft.AspNetCore.Identity;

namespace Infrastucture.Seeds;

public static class DefaultRoles
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new List<string>()
        {
            "Admin",
            "Manager",
            "User",
        };

        foreach (var role in roles)
        {
            var existingRole = await roleManager.FindByNameAsync(role);
            if (existingRole != null) { continue; }

            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
