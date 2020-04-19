namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class ProductTestSeeder
    {
        public async Task SeedProducts(ApplicationDbContext context)
        {
            var product1 = new Product()
            {
                Id = "1",
                Name = "testProduct1",
                Price = 5,
                Description = "testDescription",
                CreatedOn = DateTime.UtcNow.AddDays(-2),
            };

            var product2 = new Product()
            {
                Id = "2",
                Name = "testProduct2",
                Price = 6,
                Description = "testDescription2",
                SubCategoryId = "subId1",
                CreatedOn = DateTime.UtcNow.AddDays(-1),
            };

            var product3 = new Product()
            {
                Id = "3",
                Name = "testProduct3",
                Price = 7,
                Description = "testDescription3",
                CreatedOn = DateTime.UtcNow.AddDays(-20),
            };

            await context.Products.AddAsync(product1);
            await context.Products.AddAsync(product2);
            await context.Products.AddAsync(product3);

            await context.SaveChangesAsync();
        }

        public async Task SeedOneProductForDetails(ApplicationDbContext context)
        {
            var product = new Product()
            {
                Id = "1",
                Name = "testProduct",
                Description = "testDescription",
                Price = 5,
                Designation = "testDesignation",
                Effect = "testEffect",
                Composition = "testcomposition",
                WayOfuse = "testwayfouse",
                Specification = "testspecification",
                Manufacturer = "testmanufacturer",
                CountryOfOrigin = "testcountry",
                SubCategory = new SubCategory()
                {
                    Id = "subcategoryId",
                },
            };

            await context.Products.AddAsync(product);

            await context.SaveChangesAsync();
        }

    }
}
