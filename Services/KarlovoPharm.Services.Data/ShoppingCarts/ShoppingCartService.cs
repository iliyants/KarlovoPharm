using KarlovoPharm.Data.Common.Repositories;
using KarlovoPharm.Data.Models;
using Microsoft.EntityFrameworkCore;
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
             await this.shoppingCartRepoitory.AddAsync(new ShoppingCart()
            {
                UserId = userId
            });

            var result = await this.shoppingCartRepoitory.SaveChangesAsync();

            return result > 1;
        }

        public async Task<string> GetIdByUserId(string userId)
        {
            return await this.shoppingCartRepoitory
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<int> GetProductsCountAsync(string userId)
        {
            return await this.shoppingCartRepoitory
                 .AllAsNoTracking()
                 .Where(x => x.UserId == userId)
                 .Include(x => x.ShoppingCartProducts)
                 .Select(x => x.ShoppingCartProducts.Count())
                 .SingleOrDefaultAsync();
        }
    }
}
