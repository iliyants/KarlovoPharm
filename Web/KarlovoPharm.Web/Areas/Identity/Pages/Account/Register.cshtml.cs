namespace KarlovoPharm.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Messaging;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.Response.Redirect("/");
            }

            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? "/Users/AddAdditionalInfo";
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.Username,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    Email = this.Input.Email,
                    ShoppingCart = new ShoppingCart(),
                };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    //await this.emailSender.SendEmailAsync(
                    //    this.Input.Email,
                    //    "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Display(Name = "Потребителско име")]
            [Required(ErrorMessage = ValidationMessages.RequiredErrorMessage)]
            [StringLength(10, MinimumLength = 3, ErrorMessage = ValidationMessages.UsernameLengthErrorMessage)]
            public string Username { get; set; }

            [Display(Name = "Име")]
            [Required(ErrorMessage = ValidationMessages.RequiredErrorMessage)]
            [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.NameLenghtErrorMessage)]
            public string FirstName { get; set; }

            [Display(Name = "Фамилия")]
            [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.NameLenghtErrorMessage)]
            public string LastName { get; set; }

            [Display(Name = "Имейл")]
            [Required(ErrorMessage = ValidationMessages.RequiredErrorMessage)]
            [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = ValidationMessages.EmailValidationErrorMessage)]
            public string Email { get; set; }

            [Display(Name = "Парола")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = ValidationMessages.RequiredErrorMessage)]
            [StringLength(15, MinimumLength = 5, ErrorMessage = ValidationMessages.PasswordValidationErrorMessage)]
            public string Password { get; set; }

            [Display(Name = "Потвърди парола")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = ValidationMessages.PasswordAndConfirmPasswordDoNotMatchErrorMessage)]
            public string ConfirmPassword { get; set; }

        }
    }
}