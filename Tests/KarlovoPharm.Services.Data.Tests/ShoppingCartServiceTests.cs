namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using Xunit;

    public class ShoppingCartServiceTests
    {
        [Fact]
        public async Task GetIdByUserId_FunctionsCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartRepository = new EfDeletableEntityRepository<ShoppingCart>(context);
            var shoppingCartService = new ShoppingCartService(shoppingCartRepository);
            var userSeeder = new UserTestSeeder();
            await userSeeder.SeedUsersWithAddressesAsync(context);

            var user = context.Users.First(x => x.UserName == "UserWithoutAddresses");

            var result = await shoppingCartService.GetIdByUserId(user.Id);

            Assert.Equal(user.ShoppingCart.Id, result);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await shoppingCartService.GetIdByUserId(null);
            });
        }

        [Fact]
        public async Task GetProductsCountAsync_FunctionsCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartRepository = new EfDeletableEntityRepository<ShoppingCart>(context);
            var shoppingCartService = new ShoppingCartService(shoppingCartRepository);
            var userSeeder = new UserTestSeeder();
            await userSeeder.SeedUsersWithAddressesAsync(context);
            var user = context.Users.First(x => x.UserName == "UserWithoutAddresses");
            var shoppingCartProductSeeder = new ShoppingCartProductSeeder();
            await shoppingCartProductSeeder.SeedProductsToUser(context, user);

            var resultCount = await shoppingCartService.GetProductsCountAsync(user.Id);

            var actualCount = user.ShoppingCart.ShoppingCartProducts.Select(x => x.Product).Count();

            Assert.Equal(actualCount, resultCount);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await shoppingCartService.GetProductsCountAsync(null);
            });
        }
    }
}
