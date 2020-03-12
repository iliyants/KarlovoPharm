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

            var testSubCategoryId = dbContext.SubCategories.Where(x => x.Name == "Test")
                .Select(x => x.Id)
                .SingleOrDefault();

            var products = new List<(string name, string subCategoryId, string picture, string description, decimal price)>
            {
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
                ("TestProduct", testSubCategoryId, "https://cdn.cnn.com/cnnnext/dam/assets/191114120109-dog-aging-project-1-super-tease.jpg", "Some description that is above 10 symbols", 10 ),
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
