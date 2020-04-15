namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Common;
    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartProductSeeder
    {
        public async Task SeedProductsToUser(ApplicationDbContext context, ApplicationUser user)
        {
            var products = await context.Products.ToListAsync();

            foreach (var product in products)
            {
                await context.ShoppingCartProducts.AddAsync(new ShoppingCartProduct()
                {
                    ProductId = product.Id,
                    ShoppingCartId = user.ShoppingCart.Id,
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task SeedProducts(ApplicationDbContext context)
        {
            var shoppingCart1 = new ShoppingCart()
            {
                Id = "1",
            };

            var shoppingCartProduct1 = new ShoppingCartProduct()
            {
                ShoppingCartId = "1",
                ProductId = "2",
                Quantity = 2,
            };

            var shoppingCart2 = new ShoppingCart()
            {
                Id = "2",
            };

            var shoppingCartProduct2 = new ShoppingCartProduct()
            {
                ShoppingCartId = "2",
                ProductId = "3",
                Quantity = 4,
            };

            await context.ShoppingCarts.AddAsync(shoppingCart1);
            await context.ShoppingCarts.AddAsync(shoppingCart2);

            await context.ShoppingCartProducts.AddAsync(shoppingCartProduct1);
            await context.ShoppingCartProducts.AddAsync(shoppingCartProduct2);

            await context.SaveChangesAsync();
        }
    }
}
