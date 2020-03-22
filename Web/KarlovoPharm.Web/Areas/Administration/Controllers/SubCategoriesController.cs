namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.Categories;
    using KarlovoPharm.Services.Data.SubCategories;
    using KarlovoPharm.Web.InputModels.Categories.Display;
    using KarlovoPharm.Web.InputModels.SubCategories.Create;
    using KarlovoPharm.Web.InputModels.SubCategories.Edit;
    using KarlovoPharm.Web.ViewModels.Display;
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
        public async Task<IActionResult> Add()
        {
            var subCategoryCreateViewModel = new SubCategoryCreateInputModel()
            {
                Categories = await this.categoryService.GetAllAsync<CategoryDisplayInputModel>(),
            };

            return this.View(subCategoryCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubCategoryCreateInputModel subCategoryCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Add");
            }

            if (!await this.subCategoryService.CreateAsync(subCategoryCreateInputModel))
            {
                this.TempData["Error"] = ValidationMessages.SubCategoryUniqieNameErrorMessage;
                return await this.Add();
            }

            return this.RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var subCategoryDisplayViewModel = await this.subCategoryService.GetAllAsync<SubCategoryDisplayViewModel>();

            var result = new List<SubCategoryDisplayViewModel>(subCategoryDisplayViewModel);

            return this.View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string subCategoryId)
        {
            var subCategory = this.subCategoryService.GetSubCategoryById<SubCategoryEditInputModel>(subCategoryId);

            subCategory.Categories = await this.categoryService.GetAllAsync<CategoryDisplayInputModel>();

            return this.View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubCategoryEditInputModel subCategoryEditInputModel)
        {
            if (!await this.subCategoryService.EditSubCategory(subCategoryEditInputModel))
            {
                this.TempData["Error"] = ValidationMessages.SubCategoryUniqieNameErrorMessage;
                return this.RedirectToAction("Edit", "SubCategories", new { subCategoryId = subCategoryEditInputModel.Id });
            }

            return this.RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string subCategoryId)
        {
            if (!await this.subCategoryService.DeleteSubCategory(subCategoryId))
            {
                this.TempData["Error"] = ValidationMessages.SubCategoryCannotBeDeletedErrorMessage;
                return this.RedirectToAction("All");
            }

            return this.RedirectToAction("All");
        }
    }
}
