namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.Categories;
    using KarlovoPharm.Web.InputModels.Categories.Create;
    using KarlovoPharm.Web.InputModels.Categories.Display;
    using KarlovoPharm.Web.InputModels.Categories.Edit;
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

            return this.RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categoryDisplayViewModel = await this.categoryService.GetAllAsync<CategoryDisplayInputModel>();

            var result = new List<CategoryDisplayInputModel>(categoryDisplayViewModel);

            return this.View(result);
        }

        [HttpGet]
        public IActionResult Edit(string categoryId)
        {
            var category = this.categoryService.GetCategoryById<CategoryEditInputModel>(categoryId);

            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditInputModel categoryEditInputModel)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(categoryEditInputModel);
            }

            if (!await this.categoryService.EditCategory(categoryEditInputModel))
            {
                this.TempData["Error"] = ValidationMessages.CategoryUniqieNameErrorMessage;
                return this.RedirectToAction("Edit", "Categories", new { categoryId = categoryEditInputModel.Id });
            }

            return this.RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string categoryId)
        {
            if (!await this.categoryService.DeleteCategory(categoryId))
            {
                this.TempData["Error"] = ValidationMessages.CategoryCannotBeDeletedErrorMessage;
                return this.RedirectToAction("All");
            }

            return this.RedirectToAction("All");
        }
    }
}
