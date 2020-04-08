namespace KarlovoPharm.Web.ViewModels.Orders
{
    using System;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Mapping;

    public class OrderUnprocessedViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public DateTime? OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentType PaymentType { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
