namespace KarlovoPharm.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Data.Orders;
    using KarlovoPharm.Services.Data.ShoppingCarts;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Orders.Create;
    using KarlovoPharm.Web.InputModels.Orders.Display;
    using KarlovoPharm.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private const string DeliveryAddressCantBeNull = "Моля изберете адрес на доставката или офис на Еконт, като въведете адреса на офиса.";

        private const string ShoppingCartIsEmptyError = "Количката ви е празна ! Моля добавете продукти в нея, за да продължите.";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderService orderService;
        private readonly IShoppingCartService shoppingCartService;

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            IOrderService orderService,
            IShoppingCartService shoppingCartService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
            this.shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var orderCreateInputModel = await this.orderService.CreateDisplayModel<OrderDisplayInputModel>(user.Id);

            if (orderCreateInputModel.ShoppingCart.ShoppingCartProducts.Count() == 0)
            {
                this.TempData["Error"] = ShoppingCartIsEmptyError;

                return this.Redirect("/ShoppingCart");
            }

            return this.View(orderCreateInputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrderDisplayInputModel orderDisplayInputModel)
        {
            if ((orderDisplayInputModel.DeliveryAddressId == "on" && orderDisplayInputModel.OfficeAddress == null)
                || (orderDisplayInputModel.DeliveryAddressId == null && orderDisplayInputModel.OfficeAddress == null))
            {
                this.TempData["Error"] = DeliveryAddressCantBeNull;
                return this.RedirectToAction(nameof(this.Create));
            }

            this.ProccessCreateForm(orderDisplayInputModel);

            var user = await this.userManager.GetUserAsync(this.User);
            var orderCreateInputModel = orderDisplayInputModel.To<OrderCreateInputModel>();

            var shoppingCartId = await this.shoppingCartService.GetIdByUserId(user.Id);

            await this.orderService.CreateUproccessedOrder(orderCreateInputModel, shoppingCartId);

            return this.RedirectToAction(nameof(this.SuccesfullPayment));
        }

        [HttpGet]
        [Authorize]
        public IActionResult SuccesfullPayment()
        {
            return this.View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserOrders()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userOrdersViewModel = await this.orderService.UserOrders<UserOrderViewModel>(user.Id);

            return this.View(userOrdersViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string orderId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var orderDetailsViewModel = await this.orderService.Details<OrderDetailsViewModel>(user.Id, orderId);

            return this.View(orderDetailsViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cancel(string orderId)
        {
            await this.orderService.Cancel(orderId);

            return this.RedirectToAction(nameof(this.UserOrders));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string orderId)
        {
            await this.orderService.Delete(orderId);

            return this.RedirectToAction(nameof(this.UserOrders));
        }

        private void ProccessCreateForm(OrderDisplayInputModel orderDisplayInputModel)
        {
            if (orderDisplayInputModel.Recipient == null)
            {
                orderDisplayInputModel.Recipient = orderDisplayInputModel.FirstName;
            }

            if (orderDisplayInputModel.RecipientLastName == null)
            {
                orderDisplayInputModel.RecipientLastName = orderDisplayInputModel.LastName;
            }

            if (orderDisplayInputModel.RecipientPhoneNumber == null)
            {
                orderDisplayInputModel.RecipientPhoneNumber = orderDisplayInputModel.PhoneNumber;
            }

            if (orderDisplayInputModel.DeliveryAddressId == "on")
            {
                orderDisplayInputModel.DeliveryAddressId = null;
            }
        }
    }
}
