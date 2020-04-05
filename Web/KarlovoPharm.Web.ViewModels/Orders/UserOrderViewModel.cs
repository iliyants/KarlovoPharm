namespace KarlovoPharm.Web.ViewModels.Orders
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Mapping;

    public class UserOrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
