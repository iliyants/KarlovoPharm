namespace KarlovoPharm.Services.Data.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using KarlovoPharm.Data.Common.Repositories;
    using Microsoft.EntityFrameworkCore;

    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Web.ViewModels.Categories;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        private async Task<bool> CategoryNameIsNotUnique(string name)
        {
           var test = await this.categoryRepository.AllAsNoTracking().Select(x => x.Name).ToListAsync();

            return test.Contains(name);
        }

        public async Task<bool> CreateCategoryAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Category name was null or whitespace!");
            }

            if (await this.CategoryNameIsNotUnique(name))
            {
                return false;
            }

            var category = new Category {Name = name };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.categoryRepository.All()
                .To<T>().ToListAsync();
        }

        public async Task<string> GetNameByIdAsync(string categoryId)
        {
            var category =  await this.categoryRepository
                .AllAsNoTracking()
                .Where(x => x.Id == categoryId)
                .SingleOrDefaultAsync();

            if (category == null)
            {
                throw new ArgumentNullException("Invalid category!");
            }

            return category.Name;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllNavBarAsync()
        {
            return await this.GetAllAsync<CategoryViewModel>();
        }
    }
}
