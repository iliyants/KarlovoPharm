namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.SubCategories;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Products.Create;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdministrationController
    {

        private const string CreateErrorMessage = "Възникна грешка при опит за създаване на продукт.";


        private readonly ISubCategoryService subCategoryService;
        private readonly IProductService productService;

        public ProductsController(ISubCategoryService subCategoryService, IProductService productService)
        {
            this.subCategoryService = subCategoryService;
            this.productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            var subCategoryName = await this.subCategoryService.GetNameByIdAsync(id);

            this.ViewData["SubcategoryName"] = subCategoryName;

            return this.View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Add(string id, ProductCreateInputModel productCreateInputModel)
        //{

        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(productCreateInputModel);
        //    }

        //    productCreateInputModel.SubCategoryId = id;
        //    var productServiceModel = productCreateInputModel.To<ProductServiceModel>();

        //    if (!await this.productService.CreateAsync(productServiceModel))
        //    {
        //        this.TempData["Error"] = CreateErrorMessage;
        //    }

        //    return this.Redirect("/");

        //}
    }
}
