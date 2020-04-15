namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class CategoryTestSeeder
    {
        public async Task SeedCategories(ApplicationDbContext context)
        {
            var category = new Category()
            {
                Id = "1",
                Name = "TestCategory",
            };

            category.SubCategories.Add(new SubCategory()
            {
                Name = "Testsub",
            });

            var category2 = new Category()
            {
                Id = "2",
                Name = "TestCategory2",
            };

            await context.Categories.AddAsync(category);
            await context.Categories.AddAsync(category2);

            await context.SaveChangesAsync();
        }
    }
}
