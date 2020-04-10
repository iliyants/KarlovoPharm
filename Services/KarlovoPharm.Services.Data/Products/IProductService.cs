namespace KarlovoPharm.Services.Data.Products
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Web.InputModels.Products.Create;
    using KarlovoPharm.Web.InputModels.Products.Edit;
    using KarlovoPharm.Web.ViewModels.Products;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductService
    {
        public Task<bool> CreateAsync(ProductCreateInputModel productServiceModel);

        public IQueryable<T> GetAll<T>(string searchString = null);

        public Task<IEnumerable<T>> GetAllByIds<T>(List<string> ids);

        public IQueryable<T> GetAllBySubCategory<T>(string id, string searchString);

        public T GetProductDetailsById<T>(string productId);

        public IQueryable<ProductSingleViewModel> OrderProducts(string criteria, IQueryable<ProductSingleViewModel> products);

        Task<bool> EditProductAsync(ProductEditInputModel productEditInputModel);

        public Task<bool> DeleteProductAsync(string productId);

        public Task<IEnumerable<T>> GetNewest<T>();

    }
}
