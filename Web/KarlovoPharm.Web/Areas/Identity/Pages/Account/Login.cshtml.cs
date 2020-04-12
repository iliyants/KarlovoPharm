namespace KarlovoPharm.Web
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LoginModel> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.Response.Redirect("/");
            }

            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                this.ModelState.AddModelError(string.Empty, this.ErrorMessage);
            }

            returnUrl = returnUrl ?? this.Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                var user = await this.userManager.FindByNameAsync(this.Input.Username);

                if (user != null)
                {
                    if (!user.EmailConfirmed && user.UserName != "Admin")
                    {
                        this.TempData["InfoMessage"] = ValidationMessages.ConfirmYourEmailToLogin;
                        return this.Page();
                    }
                }

                var result = await this.signInManager.PasswordSignInAsync(this.Input.Username, this.Input.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");
                    return this.LocalRedirect(returnUrl);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Невалидно потребителско име или парола.");
                    return this.Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Display(Name = "Username")]
            [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
            public string Username { get; set; }

            [Display(Name = "Password")]
            [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}