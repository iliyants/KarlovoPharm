namespace KarlovoPharm.Services.Data.Products
{
    using KarlovoPharm.Web.InputModels.Products.Create;
    using KarlovoPharm.Web.ViewModels.Products;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductService
    {
        public Task<bool> CreateAsync(ProductCreateInputModel productServiceModel);

        public IQueryable<T> GetAll<T>();

        public IQueryable<T> GetAllBySubCategory<T>(string id);

        public IQueryable<ProductSingleViewModel> OrderProducts(string criteria, IQueryable<ProductSingleViewModel> products);
    }
}
