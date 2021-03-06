﻿namespace KarlovoPharm.Services.Data.Orders
{
    using KarlovoPharm.Web.InputModels.Orders.Create;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IOrderService
    {
        Task<T> CreateDisplayModel<T>(string userId);

        Task<bool> Cancel(string orderId);

        Task<bool> Delete(string orderId);

        Task<bool> Renew(string orderId);

        Task CreateUproccessedOrder(OrderCreateInputModel orderCreateInputModel, string shoppingCartId);

        Task<IEnumerable<T>> UserOrders<T>(string userId);

        Task<T> Details<T>(string userId, string orderId);

        IQueryable<T> GetAllUnprocessed<T>();

        Task<T> DetailsAdmin<T>(string orderId);

        Task<bool> Process(string orderId);

        Task<bool> Finish(string orderId);

        IQueryable<T> GetAllProcessed<T>();

        IQueryable<T> GetAllDelivered<T>();

        Task<int> GetUnprocessedOrdersCountAsync();

    }
}
