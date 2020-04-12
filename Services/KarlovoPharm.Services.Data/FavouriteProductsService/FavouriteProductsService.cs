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


        private async Task<bool> ProductsExistsInFavourites(string productId, string userId)
        {
            var favouriteProduct = await this.userFavouriteProductRepository
                .AllAsNoTracking()
                .Where(x => x.ProductId == productId && x.UserId == userId)
                .SingleOrDefaultAsync();

            return favouriteProduct != null ? true : false;
        }

        public async Task<bool> AddAsync(string productId, string userId)
        {

            if (productId == null || userId == null)
            {
                throw new ArgumentNullException("ProductId or userId were null");
            }

            if (await this.ProductsExistsInFavourites(productId, userId))
            {
                return false;
            }

             await this.userFavouriteProductRepository.AddAsync(new UserFavouriteProduct
            {
                ProductId = productId,
                UserId = userId,
            });


            await this.userFavouriteProductRepository.SaveChangesAsync();
   
            return true;
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
