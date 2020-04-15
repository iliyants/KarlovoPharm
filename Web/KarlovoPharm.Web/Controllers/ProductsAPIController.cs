namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.FavouriteProducts;
    using KarlovoPharm.Services.Data.ShoppingCartProducts;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Web.ViewModels.ProductsAPI;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsAPIController : ControllerBase
    {

        private const string ProductAddedToFavourites = "Продуктът е добавен в любими";
        private const string ProductExistsInFavourites = "Продуктът вече съществува в любими";

        private const string ProductAddedInCart = "Продуктът е добавен в количка";
        private const string ProductExistsInCart = "Продуктът вече съществува в количка";

        private readonly IFavouriteProductsService favouriteProductsService;
        private readonly IShoppingCartProductsService shoppingCartProductsService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsAPIController(
            IFavouriteProductsService favouriteProductsService,
            IShoppingCartProductsService shoppingCartProductsService,
            IShoppingCartService shoppingCartService,
            UserManager<ApplicationUser> userManager)
        {
            this.favouriteProductsService = favouriteProductsService;
            this.shoppingCartProductsService = shoppingCartProductsService;
            this.shoppingCartService = shoppingCartService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductAPIViewModel>> AddToFavourites(ProductAPIViewModel productAPIViewModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            if (!await this.favouriteProductsService.AddAsync(productAPIViewModel.ProductId, userId))
            {
                productAPIViewModel.Message = ProductExistsInFavourites;
            }
            else
            {
                productAPIViewModel.Message = ProductAddedToFavourites;
            }

            return productAPIViewModel;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductAPIViewModel>> AddToCart(ProductAPIViewModel productAPIViewModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var shoppingCartId = await this.shoppingCartService.GetIdByUserId(userId);

            if (!await this.shoppingCartProductsService.AddAsync(productAPIViewModel.ProductId, shoppingCartId))
            {
                productAPIViewModel.Message = ProductExistsInCart;
            }
            else
            {
                productAPIViewModel.Message = ProductAddedInCart;
            }

            var productsCount = await this.shoppingCartService.GetProductsCountAsync(userId);
            productAPIViewModel.Count = productsCount;

            return productAPIViewModel;
        }
    }
}
