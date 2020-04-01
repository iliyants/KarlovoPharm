namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ShoppingCarts.Any())
            {
                return;
            }

            var firstUser = await dbContext.Users.Where(x => x.UserName == "FirstUser").SingleOrDefaultAsync();

            var secondUser = await dbContext.Users.Where(x => x.UserName == "SecondUser").SingleOrDefaultAsync();

            await dbContext.ShoppingCarts.AddAsync(new ShoppingCart()
            {
                User = firstUser,
            });

            await dbContext.ShoppingCarts.AddAsync(new ShoppingCart()
            {
                User = secondUser,
            });
        }
    }
}
