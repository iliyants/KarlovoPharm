namespace KarlovoPharm.Web.Controllers
{
    using System.Diagnostics;

    using KarlovoPharm.Common;
    using KarlovoPharm.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {

            return this.View();
        }


        [HttpGet]
        public IActionResult InternalServer()
        {
            return this.StatusCode(500);
        }


        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            var errorViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, StatusCode = statusCode };

            if (statusCode == StatusCodes.NotFound)
            {
                return this.View("ErrorNotFound", errorViewModel);
            }

            return this.View("ErrorInternalServer", errorViewModel);
        }
    }
}
