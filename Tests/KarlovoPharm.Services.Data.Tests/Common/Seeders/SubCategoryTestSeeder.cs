namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class SubCategoryTestSeeder
    {
        public async Task SeedSubCategories(ApplicationDbContext context)
        {
            var subCategory1 = new SubCategory()
            {
                Id = "1",
                Name = "SubCategory1",
                CategoryId = "categoryId1",
            };

            var subCategory2 = new SubCategory()
            {
                Id = "2",
                Name = "SubCategory2",
                CategoryId = "categoryId2",
            };

            await context.SubCategories.AddAsync(subCategory1);
            await context.SubCategories.AddAsync(subCategory2);

            await context.SaveChangesAsync();
        }
    }
}
