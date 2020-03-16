namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.Store;
    using KarlovoPharm.Web.Paging;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class StoreController : BaseController
    {
        private readonly IStoreService storeService;
        private readonly IProductService productService;

        public StoreController(IStoreService storeService, IProductService productService)
        {
            this.storeService = storeService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber, string sortOrder)
        {
            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["CurrentPage"] = pageNumber;

            var products = this.productService.GetAll<ProductSingleViewModel>();

            products = this.productService.OrderProducts(sortOrder, products);

            return this.View(await PaginatedList<ProductSingleViewModel>
                .CreateAsync(products, pageNumber ?? GlobalConstants.DefaultPageIndex, GlobalConstants.DefaultPageSize));
        }

        [HttpGet]
        public async Task<IActionResult> AllFiltered(string id, int? pageNumber, string sortOrder)
        {
            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["CurrentPage"] = pageNumber;

            var productsByCategory = this.productService.GetAllBySubCategory<ProductSingleViewModel>(id);

            productsByCategory = this.productService.OrderProducts(sortOrder, productsByCategory);

            return this.View(await PaginatedList<ProductSingleViewModel>
               .CreateAsync(productsByCategory, pageNumber ?? GlobalConstants.DefaultPageIndex, GlobalConstants.DefaultPageSize));
        }
    }
}
