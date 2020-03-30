using KarlovoPharm.Data.Common.Repositories;
using KarlovoPharm.Data.Models;
using KarlovoPharm.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.FavouriteProductsService
{
    public class FavouriteProductsService : IFavouriteProductsService
    {
        private readonly IDeletableEntityRepository<UserFavouriteProduct> userFavouriteProductRepository;

        public FavouriteProductsService(IDeletableEntityRepository<UserFavouriteProduct> userFavouriteProductRepository)
        {
            this.userFavouriteProductRepository = userFavouriteProductRepository;
        }

        public async Task<bool> AddAsync(string productId, string userId)
        {
             await this.userFavouriteProductRepository.AddAsync(new UserFavouriteProduct
            {
                ProductId = productId,
                UserId = userId,
            });

            var result = await this.userFavouriteProductRepository.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<T> All<T>(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("UserId was null");
            }

            return
                this.userFavouriteProductRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<T>();
        }

        public async Task<bool> DeleteAsync(string productId, string userId)
        {
            var userFavouriteProduct = await this.userFavouriteProductRepository.All()
                .SingleOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

            if (productId == null)
            {
                throw new ArgumentNullException("Product id was null");
            }

            this.userFavouriteProductRepository.HardDelete(userFavouriteProduct);

            var result = await this.userFavouriteProductRepository.SaveChangesAsync();

            return result > 0;

        }
    }
}
