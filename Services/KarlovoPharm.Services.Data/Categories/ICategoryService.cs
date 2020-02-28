namespace KarlovoPharm.Services.Data.Categories
{
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        public Task<bool> CreateCategoryAsync(string name);
    }
}
