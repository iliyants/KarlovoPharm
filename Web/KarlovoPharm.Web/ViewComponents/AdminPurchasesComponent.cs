namespace KarlovoPharm.Web.ViewComponents
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.Orders;
    using KarlovoPharm.Web.ViewComponents.Models;
    using Microsoft.AspNetCore.Mvc;

    public class AdminPurchasesComponent : ViewComponent
    {
        private readonly IOrderService orderService;

        public AdminPurchasesComponent(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var unprocessedOrdersCount = await this.orderService.GetUnprocessedOrdersCountAsync();

            var unprocessedOrdersModel = new AdminPurchasesModel() { Count = unprocessedOrdersCount };

            return this.View(unprocessedOrdersModel);
        }
    }
}
