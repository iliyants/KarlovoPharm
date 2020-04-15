namespace KarlovoPharm.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.OrderProducts;
    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.ViewModels.Products;
    using Moq;
    using Xunit;

    public class OrderProductsServiceTests
    {
        [Fact]
        public async Task MostPurchased_ReturnsCorrectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderProductsRepository = new EfDeletableEntityRepository<OrderProduct>(context);
            var orderProductsService = this.GetOrderProductService(orderProductsRepository, context);
            var orderTestSeeder = new OrderTestSeeder();
            var orderProductsTestSeeder = new OrderProductsTestSeeder();

            await orderTestSeeder.SeedOrdersForMostPurchased(context);

            var products = await orderProductsService.MostPurchased<ProductSingleViewModel>();

            Assert.Equal(5, products.Count());
        }

        private OrderProductsService GetOrderProductService(EfDeletableEntityRepository<OrderProduct> orderProductsRepository, ApplicationDbContext context)
        {
            var productsServiceMock = new Mock<IProductService>();

            productsServiceMock
                .Setup(x => x.GetAllByIds<ProductSingleViewModel>(It.IsAny<List<string>>()))
                .Returns((List<string> ids) =>
                Task.FromResult((IEnumerable<ProductSingleViewModel>)context
                .OrdersProducts.Where(x => ids.Contains(x.ProductId))
                .To<ProductSingleViewModel>()
                .ToList()));

            var orderProductService = new OrderProductsService(
               orderProductsRepository,
               productsServiceMock.Object);

            return orderProductService;
        }
    }
}
