namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal class RootSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var root = new ApplicationUser
            {
                UserName = "Admin",
                Email = "iliyan_tz@abv.bg",
                PasswordHash = "123456",
                EmailConfirmed = true,
            };

            var rootPassword = "123456";

            var result = await userManager.CreateAsync(root, rootPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(root, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
