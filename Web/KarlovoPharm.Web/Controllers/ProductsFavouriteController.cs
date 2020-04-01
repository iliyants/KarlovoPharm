namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.FavouriteProductsService;
    using KarlovoPharm.Web.Paging;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsFavouriteController : BaseController
    {
        private const string ProductDeleteFromFavouriteErrorMessage = "Възникна грешка при опит за премахване на продукта от любими";

        private readonly IFavouriteProductsService favouriteProductsService;

        public ProductsFavouriteController(IFavouriteProductsService favouriteProductsService)
        {
            this.favouriteProductsService = favouriteProductsService;
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
