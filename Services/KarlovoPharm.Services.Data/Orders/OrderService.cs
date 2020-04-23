namespace KarlovoPharm.Services.Data.Orders
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Data.OrderProducts;
    using KarlovoPharm.Services.Data.PromoCodes;
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
        private readonly IPromoCodeService promoCodeService;

        public OrderService(
            IDeletableEntityRepository<Order> orderRepository,
            IUserService userService,
            IShoppingCartProductsService shoppingCartProductsService,
            IOrderProductsService orderProductsService,
            IPromoCodeService promoCodeService)
        {
            this.orderRepository = orderRepository;
            this.userService = userService;
            this.shoppingCartProductsService = shoppingCartProductsService;
            this.orderProductsService = orderProductsService;
            this.promoCodeService = promoCodeService;
        }

        public async Task CreateUproccessedOrder(OrderCreateInputModel orderCreateInputModel, string shoppingCartId)
        {
            var shoppingCartProducts = await this.shoppingCartProductsService.GetAllProductsAsync<ShoppingCartProductInputModel>(shoppingCartId);

            if (shoppingCartProducts == null || shoppingCartProducts.Count() == 0)
            {
                throw new ArgumentNullException("ShoppingCartId was null or empty");
            }


            decimal totalPrice = 0m;
            decimal deliveryPrice = 0m;

            var order = orderCreateInputModel.To<Order>();

            await this.orderRepository.AddAsync(order);


            foreach (var product in shoppingCartProducts)
            {
                order.OrderProducts.Add(new OrderProduct()
                {
                    OrderId = order.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                });
            }

            totalPrice = shoppingCartProducts.Sum(x => x.Quantity * x.ProductPrice);

            if (orderCreateInputModel.PromoCodeId != null)
            {
                totalPrice = await this.promoCodeService.DeductPercentageFromPrice(totalPrice, orderCreateInputModel.PromoCodeId);
            }

            deliveryPrice = totalPrice >= 20m ? 0m : 3.5m;

            order.OrderStatus = OrderStatus.UnProccessed;
            order.DeliveryPrice = deliveryPrice;
            order.TotalPrice = totalPrice + deliveryPrice;
            order.OrderDate = DateTime.UtcNow;

            await this.shoppingCartProductsService.DeleteAll(shoppingCartId);

            await this.orderRepository.SaveChangesAsync();
        }

        public async Task<T> CreateDisplayModel<T>(string userId)
        {

            if (userId == null)
            {
                throw new ArgumentNullException("UserId was null");
            }
            var user = await this.userService
                .GetUserWithAllPropertiesById(userId);


            return user.To<T>();
        }

        public async Task<T> Details<T>(string userId, string orderId)
        {
            if (userId == null || orderId == null)
            {
                throw new ArgumentNullException("UserId or orderId were null");
            }

            var order = await this.orderRepository.AllAsNoTracking()
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .Include(x => x.DeliveryAddress)
                .Include(x => x.User)
                .Include(x => x.PromoCode)
                .Where(x => x.Id == orderId && x.UserId == userId)
                .SingleOrDefaultAsync();

            return order.To<T>();
        }

        public async Task<T> DetailsAdmin<T>(string orderId)
        {

            if (orderId == null)
            {
                throw new ArgumentNullException("OrderId was null");
            }

            var order = await this.orderRepository
                .AllAsNoTrackingWithDeleted()
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .Include(x => x.DeliveryAddress)
                .Include(x => x.PromoCode)
                .SingleOrDefaultAsync(x => x.Id == orderId);

            return order.To<T>();

        }

        public async Task Finish(string orderId)
        {
            var order = await this.orderRepository.All().SingleOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new ArgumentNullException("Order was null");
            }

            order.OrderStatus = OrderStatus.Delivered;
            order.DeliveryDate = DateTime.UtcNow;

            await this.orderRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllProcessed<T>()
        {
            var query = this.orderRepository.AllAsNoTracking()
               .Where(x => x.OrderStatus == OrderStatus.Proccessed || x.OrderStatus == OrderStatus.Delivered)
               .OrderByDescending(x => x.DispatchDate);

            return query.To<T>();
        }

        public IQueryable<T> GetAllUnprocessed<T>()
        {
            var query = this.orderRepository.AllAsNoTracking()
                .Where(x => x.OrderStatus == OrderStatus.UnProccessed || x.OrderStatus == OrderStatus.Canceled)
                .OrderByDescending(x => x.OrderDate);

            return query.To<T>();
        }

        public async Task Process(string orderId)
        {
            var order = await this.orderRepository.All()
                .SingleOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new ArgumentException("OrderId was null");
            }


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

        public async Task Cancel(string orderId)
        {
            var order = await this.orderRepository.All()
             .SingleOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new ArgumentNullException("OrderId was null");
            }


            order.OrderStatus = OrderStatus.Canceled;

            await this.orderRepository.SaveChangesAsync();
        }

        public async Task Delete(string orderId)
        {
            var order = await this.orderRepository.All().SingleOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new ArgumentNullException("OrderId was null");
            }

            if (order.OrderStatus == OrderStatus.Delivered)
            {
                this.orderRepository.Delete(order);
            }
            else
            {
                await this.orderProductsService.DeleteAll(orderId);

                this.orderRepository.HardDelete(order);
            }

            await this.orderRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllDelivered<T>()
        {
            var query = this.orderRepository.AllAsNoTrackingWithDeleted()
             .Where(x => x.OrderStatus == OrderStatus.Delivered)
             .OrderByDescending(x => x.OrderDate);

            return query.To<T>();
        }

        public async Task<int> GetUnprocessedOrdersCountAsync()
        {
            return await this.orderRepository.AllAsNoTracking()
                .Where(x => x.OrderStatus == OrderStatus.UnProccessed)
                .CountAsync();
        }

        public async Task Renew(string orderId)
        {
            var order = await this.orderRepository.All()
        .SingleOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new ArgumentNullException("OrderId was null");
            }


            order.OrderStatus = OrderStatus.UnProccessed;

            await this.orderRepository.SaveChangesAsync();
        }
    }
}
