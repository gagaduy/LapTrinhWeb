using KiemTraGiuaKy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KiemTraGiuaKy.Data;

public static class DbInitializer
{
    private const string AdminRole = "Admin";
    private const string StudentRole = "Student";
    private const string AdminEmail = "admin@courseapp.local";
    private const string AdminPassword = "Admin123!";

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await context.Database.MigrateAsync();
        await EnsureRolesAsync(roleManager);
        await EnsureAdminAsync(userManager);
        await SeedCatalogAsync(context);
    }

    private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in new[] { AdminRole, StudentRole })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task EnsureAdminAsync(UserManager<ApplicationUser> userManager)
    {
        var admin = await userManager.FindByEmailAsync(AdminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = "admin",
                Email = AdminEmail,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(admin, AdminPassword);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", createResult.Errors.Select(error => error.Description)));
            }
        }

        if (!await userManager.IsInRoleAsync(admin, AdminRole))
        {
            await userManager.AddToRoleAsync(admin, AdminRole);
        }
    }

    private static async Task SeedCatalogAsync(ApplicationDbContext context)
    {
        if (await context.Categories.AnyAsync() || await context.Courses.AnyAsync())
        {
            return;
        }

        var categories = new[]
        {
            new Category { Name = "Cong nghe phan mem" },
            new Category { Name = "He thong thong tin" },
            new Category { Name = "Khoa hoc du lieu" }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        var courses = new[]
        {
            new Course { Name = "Lap trinh Web nang cao", Credits = 3, Lecturer = "ThS. Nguyen Lan", Image = "https://images.unsplash.com/photo-1498050108023-c5249f4df085?auto=format&fit=crop&w=900&q=80", CategoryId = categories[0].Id },
            new Course { Name = "Kien truc phan mem", Credits = 3, Lecturer = "TS. Tran Minh", Image = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?auto=format&fit=crop&w=900&q=80", CategoryId = categories[0].Id },
            new Course { Name = "Kiem thu phan mem", Credits = 2, Lecturer = "ThS. Le Hoa", Image = "https://images.unsplash.com/photo-1516321497487-e288fb19713f?auto=format&fit=crop&w=900&q=80", CategoryId = categories[0].Id },
            new Course { Name = "Co so du lieu", Credits = 3, Lecturer = "TS. Pham Son", Image = "https://images.unsplash.com/photo-1551288049-bebda4e38f71?auto=format&fit=crop&w=900&q=80", CategoryId = categories[1].Id },
            new Course { Name = "Phan tich thiet ke he thong", Credits = 3, Lecturer = "ThS. Vu Trang", Image = "https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&w=900&q=80", CategoryId = categories[1].Id },
            new Course { Name = "Thuong mai dien tu", Credits = 2, Lecturer = "ThS. Do Quynh", Image = "https://images.unsplash.com/photo-1556742049-0cfed4f6a45d?auto=format&fit=crop&w=900&q=80", CategoryId = categories[1].Id },
            new Course { Name = "Nhap mon khoa hoc du lieu", Credits = 3, Lecturer = "TS. Hoang Mai", Image = "https://images.unsplash.com/photo-1518186233392-c232efbf2373?auto=format&fit=crop&w=900&q=80", CategoryId = categories[2].Id },
            new Course { Name = "Phan tich du lieu voi Python", Credits = 3, Lecturer = "ThS. Dang Khoa", Image = "https://images.unsplash.com/photo-1515879218367-8466d910aaa4?auto=format&fit=crop&w=900&q=80", CategoryId = categories[2].Id }
        };

        await context.Courses.AddRangeAsync(courses);
        await context.SaveChangesAsync();
    }
}
