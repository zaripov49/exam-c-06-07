using Microsoft.AspNetCore.Identity;

namespace Infrastucture.Seeds;

public static class DefaultUsers
{
    public static async Task SeedUserAsync(UserManager<IdentityUser> userManager)
    {
        var existingUser = await userManager.FindByNameAsync("Admin");
        if (existingUser != null) { return; }

        var user = new IdentityUser()
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "918829595",
            PhoneNumberConfirmed = true,
        };

        await userManager.CreateAsync(user, "12345");
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
