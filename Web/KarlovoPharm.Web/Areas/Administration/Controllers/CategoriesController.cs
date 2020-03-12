namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.Categories;
    using KarlovoPharm.Web.InputModels.Categories.Create;
    using KarlovoPharm.Web.InputModels.Categories.Display;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryCreateInputModel categoryCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(categoryCreateInputModel);
            }

            if (!await this.categoryService.CreateCategoryAsync(categoryCreateInputModel.Name))
            {
                this.TempData["Error"] = ValidationMessages.CategoryUniqieNameErrorMessage;
                return this.View();
            }

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categoryDisplayViewModel = await this.categoryService.GetAllAsync<CategoryDisplayInputModel>();

            var result = new List<CategoryDisplayInputModel>(categoryDisplayViewModel);

            return this.View(result);
        }
    }
}
