namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.FavouriteProductsService;
    using KarlovoPharm.Web.Paging;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsFavouriteController : BaseController
    {
        private const string ProductDeleteFromFavouriteErrorMessage = "Възникна грешка при опит за премахване на продукта от любими";

        private const string ProductAddToFavouriteErrorMessage = "Възникна грешка при опит за добавяне на продукта в любими";

        private readonly IFavouriteProductsService favouriteProductsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsFavouriteController(IFavouriteProductsService favouriteProductsService, UserManager<ApplicationUser> userManager)
        {
            this.favouriteProductsService = favouriteProductsService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All(int? pageNumber, string userId)
        {
            this.TempData["UserId"] = userId;

            var userFavouriteProducts = this.favouriteProductsService.All<ProductFavouriteViewModel>(userId);

            return this.View(await PaginatedList<ProductFavouriteViewModel>
                .CreateAsync(userFavouriteProducts, pageNumber ?? GlobalConstants.DefaultPageIndex, GlobalConstants.DefaultPageSize));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string productId, string userId)
        {
            if (!await this.favouriteProductsService.DeleteAsync(productId, userId))
            {
                this.TempData["Error"] = ProductDeleteFromFavouriteErrorMessage;
                return this.RedirectToAction("All", new { userId });
            }

            return this.RedirectToAction("All", new { userId });
        }

    }
}
