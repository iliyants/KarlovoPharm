namespace KarlovoPharm.Web
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models.Common;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class LogoutModel : PageModel
    {
        private const string SuccesfullLogout = "Успешно излязохте от профила си.";

        private readonly SignInManager<ApplicationUser> signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await this.signInManager.SignOutAsync();

            this.TempData["Status"] = SuccesfullLogout;

            return this.Redirect("/");
        }
    }
}
