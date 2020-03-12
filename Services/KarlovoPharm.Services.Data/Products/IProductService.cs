namespace KarlovoPharm.Services.Data.Products
{
    using KarlovoPharm.Web.InputModels.Products.Create;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        public Task<bool> CreateAsync(ProductCreateInputModel productServiceModel);

        public Task<IEnumerable<T>> GetAllAsync<T>();

        public Task<IEnumerable<T>> GetAllBySubCategoryAsync<T>(string id);

    }
}
