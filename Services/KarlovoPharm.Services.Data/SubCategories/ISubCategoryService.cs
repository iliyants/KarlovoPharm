namespace KarlovoPharm.Services.Data.SubCategories
{
    using System.Threading.Tasks;

    public interface ISubCategoryService
    {
        Task<string> GetNameByIdAsync(string subCategoryId);
        public Task<bool> CreateSubCategoryAsync(string name, string categoryId);
    }
}
