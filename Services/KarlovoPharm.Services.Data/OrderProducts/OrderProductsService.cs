using KarlovoPharm.Data.Common.Repositories;
using KarlovoPharm.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.OrderProducts
{
    public class OrderProductsService : IOrderProductsService
    {
        private readonly IDeletableEntityRepository<OrderProduct> orderProductRepository;

        public OrderProductsService(IDeletableEntityRepository<OrderProduct> orderProductRepository)
        {
            this.orderProductRepository = orderProductRepository;
        }
        public async Task DeleteAll(string orderId)
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
        }
    }
}
