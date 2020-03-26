namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Addresses;
    using KarlovoPharm.Services.Data.Users;
    using KarlovoPharm.Services.Data.UsersAddresses;
    using KarlovoPharm.Web.InputModels.Addresses.Create;
    using KarlovoPharm.Web.InputModels.Addresses.Edit;
    using Microsoft.AspNetCore.Mvc;

    public class AddressesController : BaseController
    {
        private const string UserCantHaveMoreThanThreeAddressesErrorMessage = "Не можете да добавята повече от 3 адреса за акаунт !";

        private const string UnexpectedErrorEditingAddressErrorMessage = "Възникна грешка при опит за редакция на адрес.";

        private const string UnexpectedErrorDeletingAddressErrorMessage = "Възникна грешка при опит за изтриване на адрес.";


        private readonly IAddressService addressService;
        private readonly IUsersAddressesService usersAddressesService;
        private readonly IUserService userService;

        public AddressesController(IAddressService addressService, IUsersAddressesService usersAddressesService, IUserService userService)
        {
            this.addressService = addressService;
            this.usersAddressesService = usersAddressesService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Create(string userId)
        {
            var addressCreateInputModel = new AddressCreateInputModel();
            addressCreateInputModel.UserId = userId;

            return this.View(addressCreateInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddressCreateInputModel addressCreateInputModel)
        {
            if (await this.userService.UserHasThreeAddresses(addressCreateInputModel.UserId))
            {
                this.TempData["Error"] = UserCantHaveMoreThanThreeAddressesErrorMessage;
                return this.RedirectToAction("Create", addressCreateInputModel);
            }

            var address = await this.addressService.CreateAsync(addressCreateInputModel);
            await this.usersAddressesService.CreateAsync(addressCreateInputModel.UserId, address.Id);

            return this.Redirect($"/Users/profile?userId={addressCreateInputModel.UserId}");
        }

        [HttpGet]
        public IActionResult Edit(string addressId, string userId)
        {
            var addressEditInputModel = this.addressService.GetById<AddressEditInputModel>(addressId);
            addressEditInputModel.UserId = userId;

            return this.View(addressEditInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddressEditInputModel addressEditInputModel)
        {
            if (!this.ModelState.IsValid ||
                await this.addressService.Edit(addressEditInputModel) == false)
            {
                this.TempData["Error"] = UnexpectedErrorEditingAddressErrorMessage;
                return this.RedirectToAction("Edit", new { addressId = addressEditInputModel.Id, userId = addressEditInputModel.UserId });
            }

            return this.Redirect($"/Users/profile?userId={addressEditInputModel.UserId}");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string addressId, string userId)
        {
            if (!await this.usersAddressesService.DeleteAsync(addressId) ||
                !await this.addressService.DeleteAsync(addressId))
            {
                this.TempData["Error"] = UnexpectedErrorDeletingAddressErrorMessage;
                return this.RedirectToAction("Edit", new { addressId, userId });
            }

            return this.Redirect($"/Users/profile?userId={userId}");
        }

    }
}
