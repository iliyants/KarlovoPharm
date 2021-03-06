﻿namespace KarlovoPharm.Services.Data.ShoppingCartProducts
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartProductsService : IShoppingCartProductsService
    {
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartProductsService(
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IShoppingCartService shoppingCartService)
        {
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.shoppingCartService = shoppingCartService;
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

        public async Task<IEnumerable<T>> GetAllProductsAsync<T>(string shoppingCartId)
        {
            if (shoppingCartId == null)
            {
                throw new ArgumentNullException("ShoppingcartId was null");
            }

            return await this.shoppingCartProductsRepository
                .AllAsNoTracking()
                .Where(x => x.ShoppingCartId == shoppingCartId)
                .To<T>()
                .ToListAsync();
        }

        public async Task QuantityEdit(string productId, int quantity, string userId)
        {

            var userShoppingCart = await this.shoppingCartService.GetIdByUserId(userId);

            if (productId == null || userId == null || userShoppingCart == null)
            {
                throw new ArgumentNullException("Product or user or shoppingCart was null");
            }

            var shoppingCartProduct = await this.shoppingCartProductsRepository
                .All()
                .SingleOrDefaultAsync(x => x.ProductId == productId && x.ShoppingCartId == userShoppingCart);

            if (quantity <= 0)
            {
                quantity = 1;
            }

            shoppingCartProduct.Quantity = quantity;

            await this.shoppingCartProductsRepository.SaveChangesAsync();
        }

        public async Task Delete(string productId, string userId)
        {
            var userShoppingCartId = await this.shoppingCartService.GetIdByUserId(userId);

            if (productId == null || userId == null || userShoppingCartId == null)
            {
                throw new ArgumentNullException("Product or user or shoppingCart was null");
            }


            var shoppingCartProduct = await this.shoppingCartProductsRepository
                .All()
                .SingleOrDefaultAsync(x => x.ProductId == productId && x.ShoppingCartId == userShoppingCartId);

            this.shoppingCartProductsRepository.HardDelete(shoppingCartProduct);

            await this.shoppingCartProductsRepository.SaveChangesAsync();
        }

        public async Task DeleteAll(string shoppingCartId)
        {
            if (shoppingCartId == null)
            {
                throw new ArgumentNullException("SHoppingCartId was null");
            }

            var shoppingCartProducts = await this.shoppingCartProductsRepository
              .All()
              .Where(x => x.ShoppingCartId == shoppingCartId)
              .ToListAsync();

            foreach (var scproduct in shoppingCartProducts)
            {
                this.shoppingCartProductsRepository.HardDelete(scproduct);
            }

            await this.shoppingCartProductsRepository.SaveChangesAsync();

        }

        public async Task<bool> RemoveDisabledProducts(string shoppingCartId)
        {
            var disabledProducts = await this.shoppingCartProductsRepository
                .All()
                .Include(x => x.Product)
                .Where(x => x.ShoppingCartId == shoppingCartId && !x.Product.Available)
                .ToListAsync();

            if (disabledProducts.Count() != 0)
            {
                foreach (var scp in disabledProducts)
                {
                    this.shoppingCartProductsRepository.HardDelete(scp);
                }

                await this.shoppingCartProductsRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
