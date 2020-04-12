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
    using Microsoft.EntityFrameworkCore;
    using KarlovoPharm.Web.InputModels.Products.Edit;
    using KarlovoPharm.Common;
    using System.Collections.Generic;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        private readonly ICloudinaryService cloudinaryService;

        public ProductService(IDeletableEntityRepository<Product> productRepository, ICloudinaryService cloudinaryService)
        {
            this.productRepository = productRepository;
            this.cloudinaryService = cloudinaryService;
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
            var product = this.productRepository
                .All()
                .Where(x => x.Id == productId)
                .Include(x => x.SubCategory)
                .SingleOrDefault();

            if (product == null)
            {
                throw new ArgumentNullException("There is no such product in the database!");
            }

            return product.To<T>();
        }


        private async Task<bool> ProductNameIsNotUnique(string name)
        {
            var product = await this.productRepository.AllAsNoTracking().Select(x => x.Name).ToListAsync();

            if (product == null)
            {
                throw new ArgumentNullException("Product was null");
            }

            return product.Contains(name);
        }

        public async Task<bool> EditProductAsync(ProductEditInputModel productEditInputModel)
        {
            var product = await this.productRepository.All().SingleOrDefaultAsync(x => x.Id == productEditInputModel.Id);

            if (product == null)
            {
                throw new ArgumentException("Product was null !");
            }

            if (product.Name != productEditInputModel.Name &&
                await this.ProductNameIsNotUnique(productEditInputModel.Name))
            {
                return false;
            }

            if (productEditInputModel.PictureFile != null)
            {
                var pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                    productEditInputModel.PictureFile,
                    productEditInputModel.Name,
                    GlobalConstants.CloudinaryProductPictureFolder);

                product.Picture = pictureUrl;
            }

            productEditInputModel.To(product);

            await this.productRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
            var product = await this.productRepository.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new ArgumentNullException("Product was null !");
            }


            this.productRepository.Delete(product);

            var result = await this.productRepository.SaveChangesAsync();

            return result > 0;

        }

        public async Task<IEnumerable<T>> GetNewest<T>()
        {
            var newestProducts = await this.productRepository.AllAsNoTracking()
                .Where(x => x.CreatedOn.Date > DateTime.UtcNow.Date.AddDays(-3))
                .ToListAsync();

            return newestProducts.To<T>();
        }

        public async Task<IEnumerable<T>> GetAllByIds<T>(List<string> ids)
        {
            var result = new List<Product>();

            foreach (var id in ids)
            {
                result.Add(await this.productRepository.AllAsNoTrackingWithDeleted().SingleOrDefaultAsync(x => x.Id == id));
            }

            return result.To<T>();
        }
    }
}
