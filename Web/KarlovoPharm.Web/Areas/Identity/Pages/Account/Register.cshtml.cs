namespace KarlovoPharm.Web
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Services.Data.Users;
    using KarlovoPharm.Services.Messaging;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class RegisterModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IUserService userService;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IShoppingCartService shoppingCartService,
            ILogger<RegisterModel> logger,
            IUserService userService,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.shoppingCartService = shoppingCartService;
            this.logger = logger;
            this.userService = userService;
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
            returnUrl = returnUrl ?? "~/";
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.Username,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    PhoneNumber = this.Input.PhoneNumber,
                    Email = this.Input.Email,
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        GlobalConstants.AdminEmail,
                        "Account Confirmation",
                        this.Input.Email,
                        "Потрвърждение на акаунт.",
                        $"Моля потвърдете си акаунта като натиснете <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>ТУК.</a>");

                    await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                    await this.shoppingCartService.CreateAsync(user.Id);

                    this.TempData["InfoMessage"] = ValidationMessages.ConfirmYourEmailToLogin;

                    return this.RedirectToPage("./Login");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                    if (error.Code == "DuplicateUserName")
                    {
                        this.TempData["Error"] = $"{ValidationMessages.UserNameExistsErrorMessage}";
                    }
                    else if (error.Code == "InvalidUserName")
                    {
                        this.TempData["Error"] = $"{ValidationMessages.UserNameMustBeInLatinErrorMessage}";

                    }
                    else if (error.Code == "DuplicateEmail")
                    {
                        this.TempData["Error"] = $"{ValidationMessages.EmailExistsErrorMessage}";
                    }
                }

                return this.Page();
            }

            return this.Page();
        }

        public class InputModel
        {
            [Display(Name = "Username")]
            [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
            [StringLength(10, MinimumLength = 3, ErrorMessage = ValidationMessages.UsernameLengthErrorMessage)]
            public string Username { get; set; }

            [Display(Name = "FirstName")]
            [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.NameLenghtErrorMessage)]
            public string FirstName { get; set; }

            [Display(Name = "LastName")]
            [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.LastNameLenghtErrorMessage)]
            public string LastName { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
            [DataType(DataType.EmailAddress, ErrorMessage = ValidationMessages.EmailValidationErrorMessage)]
            public string Email { get; set; }

            [Display(Name = "PhoneNumber")]
            [RegularExpression(ValidationRegexes.PhoneRegex, ErrorMessage = ValidationMessages.PhoneNumberLengthErrorMessage)]
            public string PhoneNumber { get; set; }

            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
            [StringLength(15, MinimumLength = 5, ErrorMessage = ValidationMessages.PasswordValidationErrorMessage)]
            public string Password { get; set; }

            [Display(Name = "ConfirmPassword")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = ValidationMessages.PasswordAndConfirmPasswordDoNotMatchErrorMessage)]
            public string ConfirmPassword { get; set; }
        }
    }
}