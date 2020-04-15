namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class OrderProductsTestSeeder
    {
        public async Task SeedProductsToOrder(ApplicationDbContext context, Order order)
        {
            order.OrderProducts.Add(new OrderProduct()
            {
                OrderId = "testId1",
                ProductId = "testId1",
            });

            order.OrderProducts.Add(new OrderProduct()
            {
                OrderId = "testId2",
                ProductId = "testId2",
            });

            await context.SaveChangesAsync();
        }
    }
}
