namespace KarlovoPharm.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.Store;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
            var productsServiceModel = await this.productService.GetAll();

            var productsSingleViewModel = productsServiceModel.To<ProductSingleViewModel>();

            var productsAllViewModel = new ProductAllViewModel()
            {
                Products = productsSingleViewModel,
            };

            return this.View(productsAllViewModel);
        }
    }
}
