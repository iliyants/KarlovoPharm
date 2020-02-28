namespace KarlovoPharm.Services.Data.SubCategories
{
    using System.Linq;

    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public SubCategoryService(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }


     
    }
}
