namespace KarlovoPharm.Services.Data.Products
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Models.Products;
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Mapping;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<bool> CreateAsync(ProductServiceModel productServiceModel)
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

        public async Task<IEnumerable<ProductSingleServiceModel>> GetAll()
        {
            var products =  await this.productRepository
                 .AllAsNoTracking()
                 .To<ProductSingleServiceModel>()
                 .ToListAsync();

            return products;
        }
    }
}
