namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;

    public class OrderTestSeeder
    {
        public async Task SeedOneUnprocessedOrder(ApplicationDbContext context)
        {
            var order = new Order()
            {
                Recipient = "UnProcessed",
                OrderStatus = OrderStatus.UnProccessed,
            };

            await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();
        }

        public async Task SeedOneProcessedOrder(ApplicationDbContext context)
        {
            var order = new Order()
            {
                Recipient = "Processed",
                OrderStatus = OrderStatus.Proccessed,
            };

            await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();
        }

        public async Task SeedOneCanceledOrder(ApplicationDbContext context)
        {
            var order = new Order()
            {
                Recipient = "Canceled",
                OrderStatus = OrderStatus.Canceled,
            };

            await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();
        }

        public async Task SeedOneDeliveredOrder(ApplicationDbContext context)
        {
            var order = new Order()
            {
                Recipient = "Delivered",
                OrderStatus = OrderStatus.Delivered,
            };

            order.OrderProducts.Add(new OrderProduct()
            {
                ProductId = "asd",
                OrderId = "asd",
                Quantity = 2,
            });

            await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();
        }

        public async Task SeedOrdersForMostPurchased(ApplicationDbContext context)
        {
            var order = new Order()
            {
                Id = "1",
                Recipient = "Delivered",
                OrderStatus = OrderStatus.Delivered,
                IsDeleted = true,
            };

            var orderProduct1 = new OrderProduct()
            {
                ProductId = "1",
                OrderId = "1",
                Quantity = 5,
            };

            var orderProduct2 = new OrderProduct()
            {
                ProductId = "2",
                OrderId = "1",
                Quantity = 5,
            };
            var orderProduct3 = new OrderProduct()
            {
                ProductId = "3",
                OrderId = "1",
                Quantity = 5,
            };
            var orderProduct4 = new OrderProduct()
            {
                ProductId = "4",
                OrderId = "1",
                Quantity = 5,
            };
            var orderProduct5 = new OrderProduct()
            {
                ProductId = "5",
                OrderId = "1",
                Quantity = 5,
            };

            var orderProduct6 = new OrderProduct()
            {
                ProductId = "6",
                OrderId = "1",
                Quantity = 1,
            };

            await context.Orders.AddAsync(order);
            await context.OrdersProducts.AddAsync(orderProduct1);
            await context.OrdersProducts.AddAsync(orderProduct2);
            await context.OrdersProducts.AddAsync(orderProduct3);
            await context.OrdersProducts.AddAsync(orderProduct4);
            await context.OrdersProducts.AddAsync(orderProduct5);
            await context.OrdersProducts.AddAsync(orderProduct6);

            await context.SaveChangesAsync();
        }
    }
}
