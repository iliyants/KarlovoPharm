namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;

    public class SubCategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SubCategories.Any())
            {
                return;
            }

            var testMainCategoryId = dbContext.Categories.Where(x => x.Name == "TestCategory").Select(x => x.Id).SingleOrDefault();

            await dbContext.SubCategories.AddAsync(
                new SubCategory()
                {
                    Name = "TestSubCategory",
                    CategoryId = testMainCategoryId,
                });
        }
    }
}
