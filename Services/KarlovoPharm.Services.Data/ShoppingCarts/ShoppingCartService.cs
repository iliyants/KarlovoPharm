using KarlovoPharm.Data.Common.Repositories;
using KarlovoPharm.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.ShoppingCarts
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartRepoitory;

        public ShoppingCartService(IDeletableEntityRepository<ShoppingCart> shoppingCartRepoitory)
        {
            this.shoppingCartRepoitory = shoppingCartRepoitory;
        }

        public async Task<bool> CreateAsync(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId was null");
            }

             await this.shoppingCartRepoitory.AddAsync(new ShoppingCart()
            {
                UserId = userId
            });

            var result = await this.shoppingCartRepoitory.SaveChangesAsync();

            return result > 1;
        }

        public async Task<string> GetIdByUserId(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId was null");
            }

            return await this.shoppingCartRepoitory
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<int> GetProductsCountAsync(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId was null");
            }

            var shoppingCartProducts = await this.shoppingCartRepoitory
                 .AllAsNoTracking()
                 .Where(x => x.UserId == userId)
                 .Select(x => new 
                 {
                    ShoppingCartProducts =  x.ShoppingCartProducts.Select(x => !x.Product.IsDeleted).ToList()
                 })
                 .SingleOrDefaultAsync();

            return shoppingCartProducts.ShoppingCartProducts.Count();

        }
    }
}
