namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Users;
    using KarlovoPharm.Web.InputModels.Users;
    using KarlovoPharm.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Profile(string userId)
        {
            var user = this.userService.GetUserInfo<UserProfileViewModel>(userId);
            user.NormalizedEmail = user.NormalizedEmail.ToLower();

            return this.View(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ProfileEdit(string userId)
        {
            var user = this.userService.GetUserInfo<ProfileEdintInputModel>(userId);

            return this.View(user);
        }
    }
}
