using KarlovoPharm.Data.Common.Repositories;
using KarlovoPharm.Data.Models;
using KarlovoPharm.Services.Data.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.OrderProducts
{
    public class OrderProductsService : IOrderProductsService
    {
        private readonly IDeletableEntityRepository<OrderProduct> orderProductRepository;
        private readonly IProductService productService;

        public OrderProductsService(IDeletableEntityRepository<OrderProduct> orderProductRepository, IProductService productService)
        {
            this.orderProductRepository = orderProductRepository;
            this.productService = productService;
        }

        public async Task<bool> DeleteAll(string orderId)
        {
            if (orderId == null)
            {
                throw new ArgumentNullException();
            }

            var orderProducts = await this.orderProductRepository.All()
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            foreach (var orderProduct in orderProducts)
            {
                this.orderProductRepository.HardDelete(orderProduct);
            }

            var result = await this.orderProductRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<T>> MostPurchased<T>()
        {
            var mostPurchasedProductsIds = await this.orderProductRepository.AllAsNoTrackingWithDeleted()
                .Include(x => x.Order)
                .Where(x => x.Order.IsDeleted)
                .GroupBy(x => x.ProductId)
                .Select(x => new { ProductId = x.Key, QuantitySum = x.Sum(a => a.Quantity) })
                .OrderByDescending(x => x.QuantitySum)
                .Select(x => x.ProductId)
                .Take(5)
                .ToListAsync();

            var products = await this.productService.GetAllByIds<T>(mostPurchasedProductsIds);

            return products;
        }
    }
}
