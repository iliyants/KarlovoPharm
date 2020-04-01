using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.ShoppingCarts
{
    public interface IShoppingCartService
    {
        Task<bool> CreateAsync(string userId);

        Task<int> GetProductsCountAsync(string userId);

        Task<string> GetIdByUserId(string userId);
    }
}
