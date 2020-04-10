
namespace KarlovoPharm.Web.ViewComponents
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class NewestProductsComponent : ViewComponent
    {
        private readonly IProductService productService;

        public NewestProductsComponent(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newestProductsViewModel = await this.productService.GetNewest<ProductSingleViewModel>();

            return this.View(newestProductsViewModel);
        }
    }
}
