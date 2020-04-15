namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class FavouriteProductsSeeder
    {
        public async Task SeedFavouriteProducts(ApplicationDbContext context)
        {
            var favouriteProduct1 = new UserFavouriteProduct()
            {
                UserId = "userId1",
                ProductId = "productId1",
            };

            var favouriteProduct2 = new UserFavouriteProduct()
            {
                UserId = "userId2",
                ProductId = "productId2",
            };

            await context.UserFavouriteProducts.AddAsync(favouriteProduct1);
            await context.UserFavouriteProducts.AddAsync(favouriteProduct2);

            await context.SaveChangesAsync();
        }
    }
}
