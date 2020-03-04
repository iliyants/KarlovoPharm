namespace KarlovoPharm.Services.Data.SubCategories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class SubCategoryService : ISubCategoryService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public SubCategoryService(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        private async Task<bool> SubCategoryNameIsNotUnique(string name)
        {
            var test = await this.subCategoryRepository.AllAsNoTracking().Select(x => x.Name).ToListAsync();

            return test.Contains(name);
        }

        public async Task<bool> CreateSubCategoryAsync(string name, string categoryId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("SubCategory name was null or whitespace!");
            }

            if (await this.SubCategoryNameIsNotUnique(name))
            {
                return false;
            }

            var subCategory = new SubCategory { CategoryId = categoryId,Name = name };

            await this.subCategoryRepository.AddAsync(subCategory);
            await this.subCategoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<string> GetNameByIdAsync(string id)
        {
            var subCategory = await this.subCategoryRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (subCategory == null)
            {
                throw new ArgumentNullException("Invalid SubCategory!");
            }

            return subCategory.Name;
        }
    }
}
