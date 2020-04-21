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
    using KarlovoPharm.Web.InputModels.Categories.Edit;

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

        public async Task<bool> EditCategory(CategoryEditInputModel categoryEditInputModel)
        {
            var category = this.categoryRepository.All().SingleOrDefault(x => x.Id == categoryEditInputModel.Id);

            if (category == null)
            {
                throw new ArgumentNullException($"Cannot find a category with id {categoryEditInputModel.Id}");
            }

            if (await this.CategoryNameIsNotUnique(categoryEditInputModel.Name))
            {
                return false;
            }

            category.Name = categoryEditInputModel.Name;
            await this.categoryRepository.SaveChangesAsync();

            return true;

        }

        public T GetCategoryById<T>(string id)
        {
            var category = this.categoryRepository.All().Where(x => x.Id == id).SingleOrDefault()
                .To<T>();

            if (category == null)
            {
                throw new ArgumentNullException($"Cannot find a category with id {id}");
            }

            return category;
        }

        public async Task<bool> DeleteCategory(string categoryId)
        {
            if (categoryId == null)
            {
                throw new ArgumentNullException("CategoryId was null");
            }

            if (this.categoryRepository
                .All()
                .Include(x => x.SubCategories)
                .SingleOrDefault(x => x.Id == categoryId)
                .SubCategories.Any())
            {
                return false;
            }

            var category = await this.categoryRepository.All().SingleOrDefaultAsync(x => x.Id == categoryId);

            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();

            return true;
        }


    }
}
