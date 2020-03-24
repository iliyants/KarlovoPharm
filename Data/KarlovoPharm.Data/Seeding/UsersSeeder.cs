namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

            var users = new List<(string UserName,
                                    string FirstName,
                                    string LastName,
                                    string Email,
                                    string Password)>
            {
                ("FirstUser", "FirstName", "LastName", "firstUser@abv.bg", "111111"),
                ("SecondUser", "SecondFirstName", "SecondLastName", "secondUser@abv.bg", "111111"),
            };

            foreach (var user in users)
            {
               var currentUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PasswordHash = user.Password,
                };

               await userManager.CreateAsync(currentUser, user.Password);
            }
        }
    }
}
