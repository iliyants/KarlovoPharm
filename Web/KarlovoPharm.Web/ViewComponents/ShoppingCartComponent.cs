namespace KarlovoPharm.Web.ViewComponents
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Web.ViewComponents.Models;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartComponent : ViewComponent
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartComponent(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var count = await this.shoppingCartService.GetProductsCountAsync(userId);

            var shoppingCartQuantityViewModel = new ShoppingCartQuantityViewModel() { Quantity = count };

            return this.View(shoppingCartQuantityViewModel);
        }
    }
}
