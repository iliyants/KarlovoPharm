namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Web.Paging;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class StoreController : BaseController
    {
        private readonly IProductService productService;

        public StoreController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber, string sortOrder, string searchString)
        {
            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["CurrentPage"] = pageNumber;
            this.ViewData["SearchString"] = searchString;

            var products = this.productService.GetAll<ProductSingleViewModel>(searchString);

            products = this.productService.OrderProducts(sortOrder, products);

            return this.View(await PaginatedList<ProductSingleViewModel>
                .CreateAsync(products, pageNumber ?? GlobalConstants.DefaultPageIndex, GlobalConstants.DefaultPageSize));
        }

        [HttpGet]
        public async Task<IActionResult> AllFiltered(string id, int? pageNumber, string sortOrder, string searchString)
        {
            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["CurrentPage"] = pageNumber;
            this.ViewData["SearchString"] = searchString;

            var productsByCategory = this.productService.GetAllBySubCategory<ProductSingleViewModel>(id, searchString);

            productsByCategory = this.productService.OrderProducts(sortOrder, productsByCategory);

            return this.View(await PaginatedList<ProductSingleViewModel>
               .CreateAsync(productsByCategory, pageNumber ?? GlobalConstants.DefaultPageIndex, GlobalConstants.DefaultPageSize));
        }
    }
}
