namespace KarlovoPharm.Services.Data.Products
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Mapping;
    using System.Linq;
    using System.Collections.Generic;
    using KarlovoPharm.Web.InputModels.Products.Create;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.productRepository.All()
                .To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllBySeachAsync<T>(string searchString)
        {
            var searchStringClean = searchString.Split(new string[] { ",", ".", " " }, StringSplitOptions.RemoveEmptyEntries);

            return await this.productRepository.All()
                .Where(x => searchStringClean.All(c => x.Name.ToLower().Contains(c.ToLower())))
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllBySubCategoryAsync<T>(string id)
        {
            return await this.productRepository.All()
                .Where(x => x.SubCategoryId == id)
                .To<T>().ToListAsync();
        }

    }
}
