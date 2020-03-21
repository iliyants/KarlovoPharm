namespace KarlovoPharm.Services.Data.Products
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Mapping;
    using System.Linq;
    using KarlovoPharm.Web.InputModels.Products.Create;
    using KarlovoPharm.Web.ViewModels.Products;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<bool> CreateAsync(ProductCreateInputModel productServiceModel)
        {
            if (productServiceModel.Name == null ||
                productServiceModel.Description == null ||
                productServiceModel.Price == 0 ||
                productServiceModel.Price < 0 ||
                productServiceModel.Picture == null)
            {
                throw new ArgumentNullException("One or more required properties are null");
            }

            var product = productServiceModel.To<Product>();

            await this.productRepository.AddAsync(product);
            var result = await this.productRepository.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<T> GetAll<T>(string searchString = null)
        {
            var productsQuery = this.productRepository.All();

            if (searchString != null)
            {
                return productsQuery
                .Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
                .To<T>();
            }

            return productsQuery
                .To<T>();
        }

        public IQueryable<T> GetAllBySubCategory<T>(string id, string searchString)
        {

            if (searchString != null)
            {
                return this.productRepository.All()
                    .Where(x => x.SubCategoryId == id && x.Name.ToLower()
                    .Contains(searchString.ToLower()))
                    .To<T>();
            }

            return this.productRepository.All()
                    .Where(x => x.SubCategoryId == id)
                    .To<T>();
        }

        public IQueryable<ProductSingleViewModel> OrderProducts(string criteria, IQueryable<ProductSingleViewModel> products)
        {
            switch (criteria)
            {
                case "price-highest-to-lowest": return this.OrderByPriceDesc(products);
                case "price-lowest-to-highest": return this.OrderByPriceAsc(products);
                default: return products;
            }
        }

        private IQueryable<ProductSingleViewModel> OrderByPriceDesc(IQueryable<ProductSingleViewModel> products)
        {
            return products
                .OrderByDescending(x => x.Price);
        }

        private IQueryable<ProductSingleViewModel> OrderByPriceAsc(IQueryable<ProductSingleViewModel> products)
        {
            return products
                .OrderBy(x => x.Price);
        }

        public T GetProductDetailsById<T>(string productId)
        {
            var product = this.productRepository.All().Where(x => x.Id == productId).SingleOrDefault();

            if (product == null)
            {
                throw new ArgumentNullException("There is no such product in the database!");
            }

            return product.To<T>();
        }
    }
}
