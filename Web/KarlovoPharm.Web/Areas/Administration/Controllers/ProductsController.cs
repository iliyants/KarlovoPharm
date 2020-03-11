namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services;
    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.SubCategories;
    using KarlovoPharm.Web.InputModels.Categories.Display;
    using KarlovoPharm.Web.InputModels.Products.Create;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdministrationController
    {
        private const string CloudinaryProductPictureFolder = "product_images";
        private const string CreateErrorMessage = "Възникна грешка при опит за създаване на продукт.";

        private readonly ISubCategoryService subCategoryService;
        private readonly IProductService productService;
        private readonly ICloudinaryService cloudinaryService;

        public ProductsController(ISubCategoryService subCategoryService, IProductService productService, ICloudinaryService cloudinaryService)
        {
            this.subCategoryService = subCategoryService;
            this.productService = productService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var productCreateInputModel = new ProductCreateInputModel()
            {
                SubCategories = await this.subCategoryService.GetAllAsync<CategoryDisplayInputModel>(),
            };

            return this.View(productCreateInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateInputModel productCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return await this.Add();
            }

            var pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                productCreateInputModel.Picture,
                productCreateInputModel.Name,
                CloudinaryProductPictureFolder);

            productCreateInputModel.PictureUrl = pictureUrl;

            if (!await this.productService.CreateAsync(productCreateInputModel))
            {
                this.TempData["Error"] = CreateErrorMessage;
                return await this.Add();
            }

            return this.Redirect("/");
        }
    }
}
