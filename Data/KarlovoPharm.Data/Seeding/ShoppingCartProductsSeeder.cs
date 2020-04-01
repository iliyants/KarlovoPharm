namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ShoppingCartProducts.Any())
            {
                return;
            }

            var products = await dbContext.Products.Take(15).ToListAsync();

            var firstUserShoppingCart = await dbContext.Users.Where(x => x.UserName == "FirstUser").Select(x => x.ShoppingCart).SingleOrDefaultAsync();

            foreach (var product in products)
            {
                await dbContext.ShoppingCartProducts.AddAsync(new ShoppingCartProduct()
                {
                    ShoppingCart = firstUserShoppingCart,
                    Product = product,
                });
            }
        }
    }
}
