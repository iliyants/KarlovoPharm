namespace KarlovoPharm.Services.Data.Categories
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Web.InputModels.Categories.Edit;
    using KarlovoPharm.Web.ViewModels.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        public Task<bool> CreateCategoryAsync(string name);

        public Task<IEnumerable<T>> GetAllAsync<T>();

        public Task<IEnumerable<CategoryViewModel>> GetAllNavBarAsync();

        Task<string> GetNameByIdAsync(string categoryId);

        Task<bool> EditCategory(CategoryEditInputModel categoryEditInputModel);

        public T GetCategoryById<T>(string id);

        public Task<bool> DeleteCategory(string categoryId);


    }
}
