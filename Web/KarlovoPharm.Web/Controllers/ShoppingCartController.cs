namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.ShoppingCartProducts;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Web.InputModels.ShoppingCartProducts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IShoppingCartProductsService shoppingCartProductsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartController(
            IShoppingCartService shoppingCartService,
            IShoppingCartProductsService shoppingCartProductsService,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.shoppingCartProductsService = shoppingCartProductsService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var shoppingCartId = await this.shoppingCartService.GetIdByUserId(user.Id);

            var shoppingCartProducts = await this.shoppingCartProductsService.GetAllProductsAsync<ShoppingCartProductInputModel>(shoppingCartId);

            return this.View(shoppingCartProducts);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> QuantityEdit(string productId, int quantity)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.shoppingCartProductsService.QuantityEdit(productId, quantity, user.Id);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string productId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.shoppingCartProductsService.Delete(productId, user.Id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
