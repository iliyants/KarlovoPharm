using KarlovoPharm.Services.Models.Products;
namespace KarlovoPharm.Services.Data.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        public Task<bool> CreateAsync(ProductServiceModel productServiceModel);

        public Task<IEnumerable<ProductSingleServiceModel>> GetAll();
    }
}
