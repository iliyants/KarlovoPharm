namespace KarlovoPharm.Services.Data.Orders
{
    using KarlovoPharm.Web.InputModels.Orders.Create;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IOrderService
    {
        Task<T> Details<T>(string userId);

        Task CreateUproccessedOrder(OrderCreateInputModel orderCreateInputModel, string shoppingCartId);

        Task<IEnumerable<T>> UserOrders<T>(string userId);
    }
}
