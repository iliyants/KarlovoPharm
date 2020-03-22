namespace KarlovoPharm.Web.ViewComponents
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Categories;
    using KarlovoPharm.Web.ViewComponents.Models;
    using Microsoft.AspNetCore.Mvc;

    public class NavBarCategoryComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public NavBarCategoryComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await this.categoryService.GetAllAsync<NavBarCategoryViewModel>();

            return this.View(categories);
        }
    }
}
