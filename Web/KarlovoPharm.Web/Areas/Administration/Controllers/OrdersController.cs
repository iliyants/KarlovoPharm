namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Data.Orders;
    using KarlovoPharm.Web.Paging;
    using KarlovoPharm.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> AllUnprocessed(int? pageNumber)
        {
            var unprocessedOrders = this.orderService.GetAllUnprocessed<OrderUnprocessedViewModel>();

            return this.View(await PaginatedList<OrderUnprocessedViewModel>
                .CreateAsync(unprocessedOrders, pageNumber ?? GlobalConstants.DefaultPageIndex, GlobalConstants.DefaultPageSize));
        }

        [HttpGet]
        public async Task<IActionResult> AllProcessed(int? pageNumber)
        {
            var processedOrders = this.orderService.GetAllProcessed<OrderProcessedViewModel>();

            return this.View(await PaginatedList<OrderProcessedViewModel>
                .CreateAsync(processedOrders, pageNumber ?? GlobalConstants.DefaultPageIndex, GlobalConstants.DefaultPageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string orderId)
        {
            var orderAdminDetailsViewModel = await this.orderService.DetailsAdmin<OrderDetailsAdminViewModel>(orderId);

            return this.View(orderAdminDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Process(string orderId)
        {
            await this.orderService.Process(orderId);

            return this.RedirectToAction(nameof(this.AllUnprocessed));
        }

        [HttpGet]
        public async Task<IActionResult> Finish(string orderId)
        {
            await this.orderService.Finish(orderId);

            return this.RedirectToAction(nameof(this.AllProcessed));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string orderId)
        {
            await this.orderService.Delete(orderId);

            return this.RedirectToAction(nameof(this.AllProcessed));
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(string orderId)
        {
            await this.orderService.Cancel(orderId);

            return this.RedirectToAction(nameof(this.AllUnprocessed));
        }

    }
}
