namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Common;

    public class ShoppingCartProductSeeder
    {
        public async Task SeedProductsToUser(ApplicationDbContext context, ApplicationUser user)
        {
            var products = context.Products;

            foreach (var product in products)
            {
                await context.ShoppingCartProducts.AddAsync(new ShoppingCartProduct()
                {
                    ProductId = product.Id,
                    ShoppingCartId = user.ShoppingCartId,
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
