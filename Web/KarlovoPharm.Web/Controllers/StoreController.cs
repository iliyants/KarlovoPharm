namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.Store;
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
        public async Task<IActionResult> All()
        {
            var productsServiceModel = await this.productService.GetAllAsync<ProductSingleViewModel>();

            var productsAllViewModel = new ProductAllViewModel()
            {
                Products = productsServiceModel,
            };

            return this.View(productsAllViewModel);
        }
    }
}
