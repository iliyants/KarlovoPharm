namespace KarlovoPharm.Web.ViewComponents
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.SubCategories;
    using KarlovoPharm.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class FooterLinksComponent : ViewComponent
    {
        private readonly ISubCategoryService subCategoryService;

        public FooterLinksComponent(ISubCategoryService subCategoryService)
        {
            this.subCategoryService = subCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var subCategories = await this.subCategoryService.GetAllAsync<SubCategoryViewModel>();

            return this.View(subCategories);
        }
    }
}
