namespace KarlovoPharm.Web.ViewComponents
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.OrderProducts;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class PopularProductsComponent : ViewComponent
    {
        private readonly IOrderProductsService orderProductsService;

        public PopularProductsComponent(IOrderProductsService orderProductsService)
        {
            this.orderProductsService = orderProductsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newestProductsViewModel = await this.orderProductsService.MostPurchased<ProductSingleViewModel>();

            return this.View(newestProductsViewModel);
        }
    }
}
