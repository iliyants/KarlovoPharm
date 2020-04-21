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
    using System.Collections.Generic;
    using KarlovoPharm.Web.InputModels.SubCategories.Edit;

    public class SubCategoryService : ISubCategoryService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public SubCategoryService(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        private async Task<bool> SubCategoryNameIsNotUnique(string name)
        {
            var subCategory = await this.subCategoryRepository.AllAsNoTracking().Select(x => x.Name).ToListAsync();

            if (subCategory == null)
            {
                throw new ArgumentNullException("subCategry was null");
            }

            return subCategory.Contains(name);
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

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.subCategoryRepository.All()
              .To<T>().ToListAsync();
        }

        public T GetSubCategoryById<T>(string subCategoryId)
        {
            var subCategory = this.subCategoryRepository.All()
                .Include(x => x.Category)
                .Where(x => x.Id == subCategoryId).SingleOrDefault()
                .To<T>();

            if (subCategoryId == null)
            {
                throw new ArgumentNullException($"SubCategory was null");
            }

            return subCategory;
        }

        public async Task<bool> EditSubCategory(SubCategoryEditInputModel subCategoryEditInputModel)
        {
            var subCategory = this.subCategoryRepository.All().SingleOrDefault(x => x.Id == subCategoryEditInputModel.Id);

            if (subCategory == null)
            {
                throw new ArgumentNullException("SubCategory was null!");
            }


            if (subCategory.Name != subCategoryEditInputModel.Name &&
                await this.SubCategoryNameIsNotUnique(subCategoryEditInputModel.Name))
            {
                return false;
            }
            else
            {
                subCategory.Name = subCategoryEditInputModel.Name;

                if (subCategory.CategoryId != subCategoryEditInputModel.CategoryId)
                {
                    subCategory.CategoryId = subCategoryEditInputModel.CategoryId;                       
                }
            }

            await this.subCategoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<string> GetMainCategoryNameBySubCategoryId(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Main category Id was null");
            }

            return await this.subCategoryRepository.All().Where(x => x.Id == id)
                .Include(x => x.Category)
                .Select(x => x.Category.Name)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> DeleteSubCategory(string subCategoryId)
        {
            if (subCategoryId == null)
            {
                throw new ArgumentNullException("SubcategoryId was null");
            }
            if (this.subCategoryRepository
                .All()
                .Include(x => x.Products)
                .SingleOrDefault(x => x.Id == subCategoryId)
                .Products.Any())
            {
                return false;
            }

            var subCategory = await this.subCategoryRepository.All().SingleOrDefaultAsync(x => x.Id == subCategoryId);

            this.subCategoryRepository.Delete(subCategory);
            await this.subCategoryRepository.SaveChangesAsync();

            return true;
        }
    }
}
