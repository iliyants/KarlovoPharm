
namespace KarlovoPharm.Services.Data.ShoppingCartProducts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingCartProductsService
    {
        Task<bool> AddAsync(string productId, string shoppingCartId);

        public Task<IEnumerable<T>> GetAllProductsAsync<T>(string shoppingCartId);

        public Task QuantityEdit(string productId, int quantity, string userId);

        public Task Delete(string productId, string userId);

        public Task DeleteAll(string shoppingCartId);
    }
}
