namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.FavouriteProducts;
    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.ShoppingCartProducts;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private const string ProductAddedToFavourites = "Продуктът е добавен в любими";
        private const string ProductExistsInFavourites = "Продуктът вече съществува в любими";

        private const string ProductAddedInCart = "Продуктът е добавен в количка";
        private const string ProductExistsInCart = "Продуктът вече съществува в количка";

        private readonly IProductService productService;
        private readonly IFavouriteProductsService favouriteProductsService;
        private readonly IShoppingCartProductsService shoppingCartProductsService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController( 
            IProductService productService,
            IFavouriteProductsService favouriteProductsService,
            IShoppingCartProductsService shoppingCartProductsService,
            IShoppingCartService shoppingCartService,
            UserManager<ApplicationUser> userManager
            )
        {
            this.productService = productService;
            this.favouriteProductsService = favouriteProductsService;
            this.shoppingCartProductsService = shoppingCartProductsService;
            this.shoppingCartService = shoppingCartService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Details(string productId)
        {
            var productViewModel = this.productService.GetProductDetailsById<ProductDetailsViewModel>(productId);

            return this.View(productViewModel);
        }

        [Authorize]
        [HttpPost]
        [Route("Products/AddToFavourites/{productId}")]
        public async Task<ActionResult<ProductAJAXViewModel>> AddToFavourites(ProductAJAXViewModel productAJAXViewModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            if (!await this.favouriteProductsService.AddAsync(productAJAXViewModel.ProductId, userId))
            {
                productAJAXViewModel.Message = ProductExistsInFavourites;
            }
            else
            {
                productAJAXViewModel.Message = ProductAddedToFavourites;
            }

            return productAJAXViewModel;
        }

        [Authorize]
        [HttpPost]
        [Route("Products/AddToCart/{productId}")]
        public async Task<ActionResult<ProductAJAXViewModel>> AddToCart(ProductAJAXViewModel productAJAXViewModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var shoppingCartId = await this.shoppingCartService.GetIdByUserId(userId);

            if (!await this.shoppingCartProductsService.AddAsync(productAJAXViewModel.ProductId, shoppingCartId))
            {
                productAJAXViewModel.Message = ProductExistsInCart;
            }
            else
            {
                productAJAXViewModel.Message = ProductAddedInCart;
            }

            var productsCount = await this.shoppingCartService.GetProductsCountAsync(userId);
            productAJAXViewModel.Count = productsCount;

            return productAJAXViewModel;
        }

    }
}
