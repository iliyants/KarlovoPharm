namespace KarlovoPharm.Services.Data.ShoppingCartProducts
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartProductsService : IShoppingCartProductsService
    {
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCardRepository;

        public ShoppingCartProductsService(
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IDeletableEntityRepository<ShoppingCart> shoppingCardRepository)
        {
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.shoppingCardRepository = shoppingCardRepository;
        }

        private async Task<bool> UserAlreadyHasProductInCart(string productId, string shoppingCartId)
        {
            var shoppingCartProductsRepository = await this.shoppingCartProductsRepository
                .AllAsNoTracking()
                .Where(x => x.ProductId == productId && x.ShoppingCartId == shoppingCartId)
                .SingleOrDefaultAsync();

            return shoppingCartProductsRepository != null ? true : false;
        }
        public async Task<bool> AddAsync(string productId, string shoppingCartId)
        {

            if (productId == null || shoppingCartId == null)
            {
                throw new ArgumentNullException("ProductId or ShoppingCartId was null");
            }

            if (await this.UserAlreadyHasProductInCart(productId, shoppingCartId))
            {
                return false;
            }

            await this.shoppingCartProductsRepository
                .AddAsync(new ShoppingCartProduct() { ProductId = productId, ShoppingCartId = shoppingCartId });

            await this.shoppingCartProductsRepository.SaveChangesAsync();

            return true;

        }
    }
}
