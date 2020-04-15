namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.ShoppingCartProducts;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Web.ViewModels.Products;
    using Moq;
    using Xunit;

    public class ShoppingCartProductServiceTests
    {
        [Fact]
        public async Task AddAsync_ThrowsExceptionIf_ShoppingCartIdOrProductIdAreNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartProductsRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(context);
            var shoppingCartProductService = this.GetShoppingCartProductService(shoppingCartProductsRepository, context);
            var shoppingCartProductSeeder = new ShoppingCartProductSeeder();
            await shoppingCartProductSeeder.SeedProducts(context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await shoppingCartProductService.AddAsync(null, null);
            });
        }

        [Fact]
        public async Task AddAsync_ShouldReturnTrueOrFalse_DependingOnTheParams()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartProductsRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(context);
            var shoppingCartProductService = this.GetShoppingCartProductService(shoppingCartProductsRepository, context);
            var shoppingCartProductSeeder = new ShoppingCartProductSeeder();

            await shoppingCartProductSeeder.SeedProducts(context);

            var shouldReturnTrue = await shoppingCartProductService.AddAsync("1", "4");
            var shouldReturnFalse = await shoppingCartProductService.AddAsync("2", "1");

            Assert.True(shouldReturnTrue);
            Assert.False(shouldReturnFalse);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldThrowException_IfShoppingCartIdIsNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartProductsRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(context);
            var shoppingCartProductService = this.GetShoppingCartProductService(shoppingCartProductsRepository, context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await shoppingCartProductService.GetAllProductsAsync<ProductSingleViewModel>(null);
            });
        }

        [Fact]
        public async Task QuantityEdit_EdistTheQuantityCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartProductsRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(context);
            var shoppingCartProductService = this.GetShoppingCartProductService(shoppingCartProductsRepository, context);
            var shoppingCartProductSeeder = new ShoppingCartProductSeeder();
            await shoppingCartProductSeeder.SeedProducts(context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await shoppingCartProductService.QuantityEdit(null, 0, null);
            });

        }

        [Fact]
        public async Task Delete_ThrowsException_IfProductIdOrUserIdWereNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartProductsRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(context);
            var shoppingCartProductService = this.GetShoppingCartProductService(shoppingCartProductsRepository, context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await shoppingCartProductService.Delete(null, null);
            });
        }

        [Fact]
        public async Task DeleteAll_ThrowsException_IfShoppingCartIdWasNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var shoppingCartProductsRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(context);
            var shoppingCartProductService = this.GetShoppingCartProductService(shoppingCartProductsRepository, context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await shoppingCartProductService.DeleteAll(null);
            });
        }


        private ShoppingCartProductsService GetShoppingCartProductService(EfDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository, ApplicationDbContext context)
        {
            var shoppingCartService = new Mock<IShoppingCartService>();

            shoppingCartService
                .Setup(x => x.GetIdByUserId(It.IsAny<string>()))
                .Returns((string id) =>
                Task.FromResult((string)context
                .ShoppingCarts.Where(x => x.Id == id)
                .Select(x => x.Id)
                .SingleOrDefault()));

            var shoppingCartProductService = new ShoppingCartProductsService(
                shoppingCartProductsRepository,
                shoppingCartService.Object);

            return shoppingCartProductService;
        }
    }
}

