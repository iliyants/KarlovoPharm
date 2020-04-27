namespace KarlovoPharm.Web
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private const string InvalidUserNameOrPasswordErrorMessage = "Невалидно потребителско име или парола.";

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IGooglereCaptchaService googlereCaptchaService;
        private readonly UserManager<ApplicationUser> userManager;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            IGooglereCaptchaService googlereCaptchaService,
            UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.googlereCaptchaService = googlereCaptchaService;
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

            var googlereCaptcha = this.googlereCaptchaService.GooglereCaptchaVerification(this.Input.Token);

            if (!googlereCaptcha.Result.Success && googlereCaptcha.Result.Score <= 0.5)
            {
                this.ModelState.AddModelError(string.Empty, "You are a robot, fuck off please.");
                return this.Page();
            }

            var user = await this.userManager.FindByNameAsync(this.Input.Username);

            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    this.TempData["InfoMessage"] = ValidationMessages.ConfirmYourEmailToLogin;
                    return this.Page();
                }
            }

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true


                var result = await this.signInManager.PasswordSignInAsync(this.Input.Username, this.Input.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return this.LocalRedirect(returnUrl);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, InvalidUserNameOrPasswordErrorMessage);
                    this.TempData["Error"] = InvalidUserNameOrPasswordErrorMessage;
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

            [Required]
            public string Token { get; set; }

            [Display(Name = "Password")]
            [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}