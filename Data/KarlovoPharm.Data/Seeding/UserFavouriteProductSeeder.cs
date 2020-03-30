namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserFavouriteProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UserFavouriteProducts.Any())
            {
                return;
            }

            var firstUser = await dbContext.Users.Where(x => x.UserName == "FirstUser").SingleOrDefaultAsync();

            var products = await dbContext.Products.Take(10).ToListAsync();

            foreach (var product in products)
            {
                await dbContext.UserFavouriteProducts.AddAsync(new UserFavouriteProduct()
                {
                    User = firstUser,
                    Product = product,
                });
            }
        }
    }
}
