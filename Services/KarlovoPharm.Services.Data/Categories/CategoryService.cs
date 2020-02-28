namespace KarlovoPharm.Services.Data.Categories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        private bool CategoryNameIsNotUnique(string name)
        {
            return this.categoryRepository.AllAsNoTracking().Select(x => x.Name).ToList().Contains(name);
        }

        public async Task<bool> CreateCategoryAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Category name was null or whitespace!");
            }

            if (this.CategoryNameIsNotUnique(name))
            {
                return false;
            }

            var categoryId = Guid.NewGuid().ToString();
            var category = new Category { Id = categoryId, Name = name };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();

            return true;
        }

        
    }
}
