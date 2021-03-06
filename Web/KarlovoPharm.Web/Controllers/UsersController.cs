﻿namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.Users;
    using KarlovoPharm.Web.InputModels.Users;
    using KarlovoPharm.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private const string RelogMessage = "Моля влезте отното в профила си за да се отразят промените!";

        private readonly IUserService userService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(IUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile(string userId)
        {
            var user = await this.userService.GetUserInfo<UserProfileViewModel>(userId);
            user.NormalizedEmail = user.NormalizedEmail.ToLower();

            return this.View(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileEdit(string userId)
        {
            var user = await this.userService.GetUserInfo<ProfileEdintInputModel>(userId);

            return this.View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileEdintInputModel profileEdintInputModel)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(profileEdintInputModel);
            }

            if (!await this.userService.EditProfileAsync(profileEdintInputModel))
            {
                this.TempData["Error"] = ValidationMessages.UsernameNotUniqueErrorMessage;
                return this.RedirectToAction("ProfileEdit", "Users", new { userId = profileEdintInputModel.Id });
            }

            return this.RedirectToAction(nameof(this.Profile), new { userId = profileEdintInputModel.Id});
        }
    }
}
