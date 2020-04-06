namespace KarlovoPharm.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.ViewModels.Addresses;
    using KarlovoPharm.Web.ViewModels.OrderProducts;

    public class OrderDetailsViewModel : IMapFrom<Order>
    {
        public DateTime? OrderDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string OfficeAddress { get; set; }

        public AddressViewModel DeliveryAddress { get; set; }

        public IEnumerable<OrderProductViewModel> OrderProducts { get; set; }
    }
}
