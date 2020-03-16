namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;

    public class ProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Products.Any())
            {
                return;
            }

            var testSubCategoryId = dbContext.SubCategories.Where(x => x.Name == "TestSubCategory")
                .Select(x => x.Id)
                .SingleOrDefault();

            var products = new List<(string name, string subCategoryId, string picture, string description, decimal price)>
            {
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 20 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 30 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 40 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 50 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 60 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 70 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 80 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 90 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 100 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 200 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 300 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 400 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 500 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 600 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 700 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 800 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 900 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 1000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 2000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 3000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 4000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 5000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 6000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 7000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 8000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 9000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 20000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 30000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 40000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 50000 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 60000 ),
            };


            foreach (var product in products)
            {
                await dbContext.Products.AddAsync(new Product
                {
                    Name = product.name,
                    Description = product.description,
                    Price = product.price,
                    Picture = product.picture,
                    SubCategoryId = product.subCategoryId,
                });
            }
        }
    }
}
