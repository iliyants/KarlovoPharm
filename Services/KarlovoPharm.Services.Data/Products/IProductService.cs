using KarlovoPharm.Services.Models.Products;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.Products
{
    public interface IProductService
    {
        public Task<bool> CreateAsync(ProductServiceModel productServiceModel);
    }
}
