namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            if (dbContext.Users.Count() > 1)
            {
                return;
            }

            var firstUser = new ApplicationUser
            {
                UserName = "FirstUser",
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "firstUser@email.bg",
                PasswordHash = "111111",
                EmailConfirmed = true,
                PhoneNumber = "0898767889",
            };

            var secondUser = new ApplicationUser
            {
                UserName = "SecondUser",
                Email = "secondUser@email.bg",
                PasswordHash = "111111",
                EmailConfirmed = true,
                PhoneNumber = "0898767889",
            };

            await userManager.CreateAsync(firstUser, firstUser.PasswordHash);
            await userManager.CreateAsync(secondUser, secondUser.PasswordHash);

            await userManager.AddToRoleAsync(firstUser, GlobalConstants.UserRoleName);
            await userManager.AddToRoleAsync(secondUser, GlobalConstants.UserRoleName);
        }
    }
}
