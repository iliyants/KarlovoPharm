namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.Categories;
    using KarlovoPharm.Services.Data.SubCategories;
    using KarlovoPharm.Web.InputModels.SubCategories.Create;
    using Microsoft.AspNetCore.Mvc;

    public class SubCategoriesController : AdministrationController
    {
        private readonly ISubCategoryService subCategoryService;
        private readonly ICategoryService categoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            this.subCategoryService = subCategoryService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(string categoryId)
        {
            var categoryName = await this.categoryService.GetNameByIdAsync(categoryId);

            this.ViewData["CategoryName"] = categoryName;

            var subCategoryCreateInputModel = new SubCategoryCreateInputModel()
            {
                CategoryId = categoryId
            };

            return this.View(subCategoryCreateInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubCategoryCreateInputModel subCategoryCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(subCategoryCreateInputModel);
            }

            if (!await this.subCategoryService.CreateSubCategoryAsync(subCategoryCreateInputModel.Name, subCategoryCreateInputModel.CategoryId))
            {
                this.TempData["Error"] = ValidationMessages.SubCategoryUniqieNameErrorMessage;
                return this.View(subCategoryCreateInputModel);
            }

            return this.Redirect("/");
        }
    }
}
