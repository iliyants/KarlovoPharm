namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.FavouriteProducts;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Web.ViewModels.Products;
    using Xunit;

    public class FavouriteProductsServiceTests
    {
        [Fact]
        public async Task AddAsync_ThrowsException_IfParamatersAreNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var favouriteProductsRepository = new EfDeletableEntityRepository<UserFavouriteProduct>(context);
            var favouriteProductService = new FavouriteProductsService(favouriteProductsRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await favouriteProductService.AddAsync(null, null);
            });
        }

        [Fact]
        public async Task AddAsync_ReturnsFalse_IfProductAlreadyExists()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var favouriteProductsRepository = new EfDeletableEntityRepository<UserFavouriteProduct>(context);
            var favouriteProductService = new FavouriteProductsService(favouriteProductsRepository);

            var favouriteProductsSeeder = new FavouriteProductsSeeder();

            await favouriteProductsSeeder.SeedFavouriteProducts(context);

            var shouldBeFalse = await favouriteProductService.AddAsync("productId1", "userId1");

            Assert.False(shouldBeFalse);
        }


        [Fact]
        public async Task AddAsync_ReturnsTrue_IfProductDoesntExist()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var favouriteProductsRepository = new EfDeletableEntityRepository<UserFavouriteProduct>(context);
            var favouriteProductService = new FavouriteProductsService(favouriteProductsRepository);

            var shouldBeTrue = await favouriteProductService.AddAsync("productId1", "userId1");

            Assert.True(shouldBeTrue);
        }


        [Fact]
        public void All_ThrowsExceptionIfIdIsNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var favouriteProductsRepository = new EfDeletableEntityRepository<UserFavouriteProduct>(context);
            var favouriteProductService = new FavouriteProductsService(favouriteProductsRepository);

            Assert.Throws<ArgumentNullException>(() =>
            {
                 favouriteProductService.All<ProductSingleViewModel>(null);
            });
        }

        [Fact]
        public async Task DeleteAsync_ThrowsExceptionIdIdISNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var favouriteProductsRepository = new EfDeletableEntityRepository<UserFavouriteProduct>(context);
            var favouriteProductService = new FavouriteProductsService(favouriteProductsRepository);


            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await favouriteProductService.DeleteAsync(null, null);
            });
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_IfDataIsCorect()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var favouriteProductsRepository = new EfDeletableEntityRepository<UserFavouriteProduct>(context);
            var favouriteProductService = new FavouriteProductsService(favouriteProductsRepository);

            var favouriteProductsSeeder = new FavouriteProductsSeeder();

            await favouriteProductsSeeder.SeedFavouriteProducts(context);

            var shouldBeTrue = await favouriteProductService.DeleteAsync("productId1", "userId1");

            Assert.True(shouldBeTrue);
        }
    }
}
