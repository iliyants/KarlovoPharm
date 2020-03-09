namespace KarlovoPharm.Services.Data.SubCategories
{
    using KarlovoPharm.Web.InputModels.SubCategories.Create;
    using System.Threading.Tasks;

    public interface ISubCategoryService
    {
        Task<string> GetNameByIdAsync(string subCategoryId);


        Task<bool> CreateAsync(SubCategoryCreateInputModel subCategoryCreateInputModel);
    }
}
