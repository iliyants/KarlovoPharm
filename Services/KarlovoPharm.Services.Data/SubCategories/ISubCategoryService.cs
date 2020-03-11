namespace KarlovoPharm.Services.Data.SubCategories
{
    using KarlovoPharm.Web.InputModels.SubCategories.Create;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubCategoryService
    {
        Task<string> GetNameByIdAsync(string subCategoryId);

        public Task<IEnumerable<T>> GetAllAsync<T>();
        Task<bool> CreateAsync(SubCategoryCreateInputModel subCategoryCreateInputModel);
    }
}
