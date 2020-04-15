using System.Linq;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.FavouriteProducts
{
    public interface IFavouriteProductsService
    {
        IQueryable<T> All<T>(string userId);

        Task<bool> DeleteAsync(string productId, string userId);

        Task<bool> AddAsync(string productId, string userId);
    }
}
