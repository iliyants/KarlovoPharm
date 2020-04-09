namespace KarlovoPharm.Web.Controllers
{
    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {

        private readonly IProductService productService;

        public ProductsController( IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Details(string productId)
        {
            var productViewModel = this.productService.GetProductDetailsById<ProductDetailsViewModel>(productId);

            return this.View(productViewModel);
        }

    }
}
