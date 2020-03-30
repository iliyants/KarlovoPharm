namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.FavouriteProductsService;
    using KarlovoPharm.Web.ViewModels.ProductsAPI;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsAPIController : BaseController
    {
        private readonly IFavouriteProductsService favouriteProductsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsAPIController(
            IFavouriteProductsService favouriteProductsService,
            UserManager<ApplicationUser> userManager)
        {
            this.favouriteProductsService = favouriteProductsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddToFavourites(FavouriteProductAPIViewModel favouriteProductAPIViewModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            if (!await this.favouriteProductsService.AddAsync(favouriteProductAPIViewModel.ProductId, userId))
            {
                return this.BadRequest();
            }

            return this.Ok(favouriteProductAPIViewModel);
        }
    }
}
