namespace KarlovoPharm.Services.Data.SubCategories
{
    using KarlovoPharm.Web.InputModels.SubCategories.Create;
    using KarlovoPharm.Web.InputModels.SubCategories.Edit;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubCategoryService
    {
        Task<string> GetNameByIdAsync(string subCategoryId);

        public Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> CreateAsync(SubCategoryCreateInputModel subCategoryCreateInputModel);

        public T GetSubCategoryById<T>(string subCategoryId);

        Task<bool> EditSubCategory(SubCategoryEditInputModel subCategoryEditInputModel);

        Task<string> GetMainCategoryNameBySubCategoryId(string id);

        Task<string> GetMainCategegoryIdBySubCategoryId(string id);
    }
}
