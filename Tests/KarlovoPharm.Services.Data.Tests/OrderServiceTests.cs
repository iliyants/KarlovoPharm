namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.OrderProducts;
    using KarlovoPharm.Services.Data.Orders;
    using KarlovoPharm.Services.Data.PromoCodes;
    using KarlovoPharm.Services.Data.ShoppingCartProducts;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Services.Data.Users;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Orders.Create;
    using KarlovoPharm.Web.InputModels.Orders.Display;
    using KarlovoPharm.Web.InputModels.ShoppingCartProducts;
    using KarlovoPharm.Web.ViewModels.Orders;
    using Moq;
    using Xunit;

    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateUproccessedOrder_ThrowsException_WhenShoppingCartIsEmptyOrNull()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var userTestSeeder = new UserTestSeeder();
            var shoppingCartProductSeeder = new ShoppingCartProductSeeder();

            //seed user
            await userTestSeeder.SeedUsersWithAddressesAsync(context);
            var userWithoutProducts = context.Users.First(x => x.UserName == "UserWithoutAddresses");

            var orderCreateInputModel = new OrderCreateInputModel() { };

            var orderService = this.GetOrderService(orderRepository, context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.CreateUproccessedOrder(orderCreateInputModel, null);
            });

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.CreateUproccessedOrder(orderCreateInputModel, userWithoutProducts.ShoppingCart.Id);
            });
        }

        [Fact]
        public async Task CreateUnproccessedOrder_RemovesProductsFromShoppingCart_ProperlyCreatesAnOrderWithTheProducts()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var userTestSeeder = new UserTestSeeder();
            var shoppingCartProductSeeder = new ShoppingCartProductSeeder();
            var productsTestSeeder = new ProductsTestSeeder();

            //seed products
            await productsTestSeeder.SeedProducts(context);

            //seed users
            await userTestSeeder.SeedUsersWithAddressesAsync(context);
            var userWithProducts = context.Users.First(x => x.UserName == "UserWithoutAddresses");

            //seed products in shoppingCart for user
            await shoppingCartProductSeeder.SeedProductsToUser(context, userWithProducts);

            var orderCreateInputModel = new OrderCreateInputModel()
            {
                UserId = userWithProducts.Id,
            };

            var orderService = this.GetOrderService(orderRepository, context);

            await orderService.CreateUproccessedOrder(orderCreateInputModel, userWithProducts.ShoppingCart.Id);

            var actualShoppingCartCount = userWithProducts.ShoppingCart.ShoppingCartProducts.Count();
            var actualOrderCount = context.Orders.Count();
            var order = context.Orders.FirstOrDefault();

            var deliveryPrice = userWithProducts.ShoppingCart.ShoppingCartProducts.Sum(x => x.Quantity * x.Product.Price) >= 20m ? 0m : 3.5m;
            var totalPrice = userWithProducts.ShoppingCart.ShoppingCartProducts.Sum(x => x.Quantity * x.Product.Price) + deliveryPrice;

            Assert.Equal(OrderStatus.UnProccessed, order.OrderStatus);
            Assert.Equal(deliveryPrice, order.DeliveryPrice);
            Assert.Equal(totalPrice, order.TotalPrice);
            Assert.Equal(0, actualShoppingCartCount);
            Assert.Equal(1, actualOrderCount);
        }

        [Fact]
        public async Task CreateDisplayModel_ThrowsException_WhenUserIdISNull()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.CreateDisplayModel<OrderDisplayInputModel>(null);
            });
        }

        [Fact]
        public async Task Details_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneUnprocessedOrder(context);

            var result = orderService.Details<OrderDetailsViewModel>("UserId1", "OrderId1");

            Assert.NotNull(result);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.Details<OrderDisplayInputModel>(null, null);
            });
        }

        [Fact]
        public async Task DetailsAdmin_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneUnprocessedOrder(context);

            var result = orderService.DetailsAdmin<OrderDetailsViewModel>("OrderId1");

            Assert.NotNull(result);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.DetailsAdmin<OrderDisplayInputModel>(null);
            });
        }

        [Fact]
        public async Task Finish_WorksCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneProcessedOrder(context);
            var order = context.Orders.FirstOrDefault();

            await orderService.Finish(order.Id);

            Assert.Equal(OrderStatus.Delivered, order.OrderStatus);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.Finish("invalid");
            });
        }

        [Fact]
        public async Task Process_WorksCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneProcessedOrder(context);
            var order = context.Orders.FirstOrDefault();

            await orderService.Process(order.Id);

            Assert.Equal(OrderStatus.Proccessed, order.OrderStatus);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.Finish("invalid");
            });
        }


        [Fact]
        public async Task Renew_WorksCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneCanceledOrder(context);
            var order = context.Orders.FirstOrDefault();

            await orderService.Renew(order.Id);

            Assert.Equal(OrderStatus.UnProccessed, order.OrderStatus);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.Renew("invalid");
            });
        }


        [Fact]
        public async Task Cancel_WorksCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneUnprocessedOrder(context);

            var order = context.Orders.FirstOrDefault();

            await orderService.Cancel(order.Id);

            Assert.Equal(OrderStatus.Canceled, order.OrderStatus);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.Cancel("invalid");
            });
        }

        [Fact]
        public async Task Delete_WorksCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneUnprocessedOrder(context);
            var unProcessed = context.Orders.First(x => x.Recipient == "UnProcessed");

            await orderTestSeeder.SeedOneProcessedOrder(context);
            var processed = context.Orders.First(x => x.Recipient == "Processed");

            await orderTestSeeder.SeedOneDeliveredOrder(context);
            var delivered = context.Orders.First(x => x.Recipient == "Delivered");


            await orderService.Delete(unProcessed.Id);
            await orderService.Delete(processed.Id);
            await orderService.Delete(delivered.Id);

            var orders = context.Orders.Count();

            Assert.True(delivered.IsDeleted);

            Assert.Equal(0, orders);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.Delete("invalid");
            });

        }

        [Fact]
        public async Task UserOrders_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneUnprocessedOrder(context);

            var userOrders = await orderService.UserOrders<OrderDetailsViewModel>("UserId1");

            Assert.Single(userOrders);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await orderService.UserOrders<OrderDetailsViewModel>("invalid");
            });

        }

        [Fact]
        public async Task GetAllUnprocessed_ReturnsCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneUnprocessedOrder(context);

            var result = orderService.GetAllUnprocessed<OrderUnprocessedViewModel>();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllProcessed_ReturnsCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneProcessedOrder(context);

            var result = orderService.GetAllProcessed<OrderProcessedViewModel>();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllDelivered_ReturnsCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var orderRepository = new EfDeletableEntityRepository<Order>(context);
            var orderService = this.GetOrderService(orderRepository, context);
            var orderTestSeeder = new OrderTestSeeder();

            await orderTestSeeder.SeedOneDeliveredOrder(context);

            var result = orderService.GetAllDelivered<OrderProcessedViewModel>();

            Assert.Single(result);
        }

        private OrderService GetOrderService(EfDeletableEntityRepository<Order> orderRepository, ApplicationDbContext context)
        {

            var shoppingCartProductsServiceMock = new Mock<IShoppingCartProductsService>();

            var userServiceMock = new Mock<IUserService>();

            var promoCodeServiceMock = new Mock<IPromoCodeService>();


            userServiceMock
                .Setup(x => x.GetUserWithAllPropertiesById(It.IsAny<string>()))
                .Returns((string userId) =>
                Task.FromResult(
                context.Users
                .SingleOrDefault(x => x.Id == userId)));

            shoppingCartProductsServiceMock
                .Setup(x => x.GetAllProductsAsync<ShoppingCartProductInputModel>(It.IsAny<string>()))
                .Returns((string shoppingCartId) =>
                Task.FromResult((IEnumerable<ShoppingCartProductInputModel>)
                context.ShoppingCartProducts
                .Where(x => x.ShoppingCartId == shoppingCartId)
                .To<ShoppingCartProductInputModel>()
                .ToList()));

            shoppingCartProductsServiceMock
                .Setup(x => x.DeleteAll(It.IsAny<string>()))
                .Callback((string shoppingCartId) =>
                {
                    context.ShoppingCartProducts.RemoveRange(context.ShoppingCartProducts.Where(x => x.ShoppingCartId == shoppingCartId));
                });

            var orderProductsServiceMock = new Mock<IOrderProductsService>();

            var orderService = new OrderService(
                orderRepository,
                userServiceMock.Object,
                shoppingCartProductsServiceMock.Object,
                orderProductsServiceMock.Object,
                promoCodeServiceMock.Object);

            return orderService;
        }


    }
}
