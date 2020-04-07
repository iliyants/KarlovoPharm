﻿namespace KarlovoPharm.Services.Data.Orders
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Data.OrderProducts;
    using KarlovoPharm.Services.Data.ShoppingCartProducts;
    using KarlovoPharm.Services.Data.Users;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Orders.Create;
    using KarlovoPharm.Web.InputModels.ShoppingCartProducts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IUserService userService;
        private readonly IShoppingCartProductsService shoppingCartProductsService;
        private readonly IOrderProductsService orderProductsService;

        public OrderService(
            IDeletableEntityRepository<Order> orderRepository,
            IUserService userService,
            IShoppingCartProductsService shoppingCartProductsService,
            IOrderProductsService orderProductsService)
        {
            this.orderRepository = orderRepository;
            this.userService = userService;
            this.shoppingCartProductsService = shoppingCartProductsService;
            this.orderProductsService = orderProductsService;
        }

        public async Task CreateUproccessedOrder(OrderCreateInputModel orderCreateInputModel, string shoppingCartId)
        {
            var order = orderCreateInputModel.To<Order>();

            await this.orderRepository.AddAsync(order);

            var shoppingCartProducts = await this.shoppingCartProductsService.GetAllProductsAsync<ShoppingCartProductInputModel>(shoppingCartId);

            foreach (var product in shoppingCartProducts)
            {
                order.OrderProducts.Add(new OrderProduct()
                {
                    OrderId = order.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                });
            }

            var deliveryPrice = shoppingCartProducts.Sum(x => x.Quantity * x.ProductPrice) >= 20m ? 0m : 3.5m;

            order.OrderStatus = OrderStatus.UnProccessed;
            order.DeliveryPrice = deliveryPrice;
            order.TotalPrice = shoppingCartProducts.Sum(x => x.Quantity * x.ProductPrice) + deliveryPrice;
            order.OrderDate = DateTime.UtcNow;

            await this.shoppingCartProductsService.DeleteAll(shoppingCartId);

            await this.orderRepository.SaveChangesAsync();
        }

        public async Task<T> Details<T>(string userId)
        {
            var user = await this.userService
                .GetUserWithAllPropertiesById(userId);


            return user.To<T>();
        }

        public async Task<T> Details<T>(string userId, string orderId)
        {
            var order = await this.orderRepository.AllAsNoTracking()
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .Include(x => x.DeliveryAddress)
                .Where(x => x.Id == orderId && x.UserId == userId)
                .SingleOrDefaultAsync();

            return order.To<T>();
        }

        public async Task<T> DetailsAdmin<T>(string orderId)
        {
            var order = await this.orderRepository
                .AllAsNoTracking()
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .Include(x => x.DeliveryAddress)
                .SingleOrDefaultAsync(x => x.Id == orderId);

            return order.To<T>();

        }

        public async Task Finish(string orderId)
        {
            await this.orderProductsService.DeleteAll(orderId);

            var order = await this.orderRepository.All().SingleOrDefaultAsync(x => x.Id == orderId);

            this.orderRepository.HardDelete(order); 
        }

        public IQueryable<T> GetAllProcessed<T>()
        {
            var query = this.orderRepository.AllAsNoTracking()
               .Where(x => x.OrderStatus == OrderStatus.Proccessed)
               .OrderByDescending(x => x.DispatchDate);

            return query.To<T>();
        }

        public IQueryable<T> GetAllUnprocessed<T>()
        {
            var query = this.orderRepository.AllAsNoTracking()
                .Where(x => x.OrderStatus == OrderStatus.UnProccessed)
                .OrderByDescending(x => x.OrderDate);

            return query.To<T>();
        }

        public async Task Process(string orderId)
        {
            var order = await this.orderRepository.All()
                .SingleOrDefaultAsync(x => x.Id == orderId);

            order.OrderStatus = OrderStatus.Proccessed;
            order.DispatchDate = DateTime.UtcNow;
            order.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(3);

            await this.orderRepository.SaveChangesAsync();

        }


        public async Task<IEnumerable<T>> UserOrders<T>(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("UserId was null");
            }

            return await this.orderRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToListAsync();
        }
    }
}
