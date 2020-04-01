
namespace KarlovoPharm.Services.Data.ShoppingCartProducts
{
    using System.Threading.Tasks;

    public interface IShoppingCartProductsService
    {
        Task<bool> AddAsync(string productId, string shoppingCartId);
    }
}
