namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class ProductsTestSeeder
    {
        public async Task SeedProducts(ApplicationDbContext context)
        {
            var product1 = new Product()
            {
                Name = "Product1",
                Price = 1,
                Description = "testDescription",
                Specification = "testSpecification",
            };

            var product2 = new Product()
            {
                Name = "Product2",
                Price = 1,
                Description = "testDescription",
                Specification = "testSpecification",
            };

            await context.AddAsync(product1);
            await context.AddAsync(product2);

            await context.SaveChangesAsync();
        }
    }
}
