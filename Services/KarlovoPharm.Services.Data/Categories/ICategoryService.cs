namespace KarlovoPharm.Services.Data.Categories
{
    using KarlovoPharm.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        public Task<bool> CreateCategoryAsync(string name);

        public IEnumerable<Category> GetAll();

        Task<string> GetNameByIdAsync(string categoryId);

    }
}
