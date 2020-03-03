namespace KarlovoPharm.Services.Data.SubCategories
{
    using System.Threading.Tasks;

    public interface ISubCategoryService
    {
        public Task<bool> CreateSubCategoryAsync(string name, string categoryId);
    }
}
