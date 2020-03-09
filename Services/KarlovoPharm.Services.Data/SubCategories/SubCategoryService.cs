namespace KarlovoPharm.Services.Data.SubCategories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Web.InputModels.SubCategories.Create;
    using Microsoft.EntityFrameworkCore;

    using KarlovoPharm.Services.Mapping;

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

        public async Task<bool> CreateAsync(SubCategoryCreateInputModel subCategoryCreateInputModel)
        {
            if (string.IsNullOrEmpty(subCategoryCreateInputModel.Name) || string.IsNullOrWhiteSpace(subCategoryCreateInputModel.Name))
            {
                throw new ArgumentNullException("SubCategory name was null or whitespace!");
            }

            if (await this.SubCategoryNameIsNotUnique(subCategoryCreateInputModel.Name))
            {
                return false;
            }

            var subCategory = subCategoryCreateInputModel.To<SubCategory>();

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
