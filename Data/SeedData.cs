using Buoi6.Models;
using Microsoft.AspNetCore.Identity;

namespace Buoi6.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Add Roles
            string[] roleNames = { "Admin", "Member" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Add Admin User
            var adminEmail = "admin@abc.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Administrator",
                    Address = "Admin Address"
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
            }
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Add Member User
            var memberEmail = "member@abc.com";
            var memberUser = await userManager.FindByEmailAsync(memberEmail);
            if (memberUser == null)
            {
                memberUser = new ApplicationUser
                {
                    UserName = memberEmail,
                    Email = memberEmail,
                    FullName = "Normal Member",
                    Address = "Member Address"
                };
                await userManager.CreateAsync(memberUser, "Member@123");
            }
            if (!await userManager.IsInRoleAsync(memberUser, "Member"))
            {
                await userManager.AddToRoleAsync(memberUser, "Member");
            }
        }
    }
}
