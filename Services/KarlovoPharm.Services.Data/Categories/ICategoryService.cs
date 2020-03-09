namespace KarlovoPharm.Services.Data.Categories
{
    using KarlovoPharm.Web.ViewModels.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        public Task<bool> CreateCategoryAsync(string name);

        public Task<IEnumerable<T>> GetAllAsync<T>();

        public Task<IEnumerable<CategoryViewModel>> GetAllNavBarAsync();

        Task<string> GetNameByIdAsync(string categoryId);

    }
}
