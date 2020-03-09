namespace KarlovoPharm.Web.Controllers
{
    using System.Diagnostics;

    using KarlovoPharm.Services.Data.Categories;
    using KarlovoPharm.Web.ViewModels;
    using KarlovoPharm.Web.ViewModels.Categories;
    using KarlovoPharm.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService )
        {
            this.categoryService = categoryService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
