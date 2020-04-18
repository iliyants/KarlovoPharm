namespace KarlovoPharm.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.ViewModels.Addresses;
    using KarlovoPharm.Web.ViewModels.OrderProducts;

    public class OrderDetailsAdminViewModel : IMapFrom<Order>
    {

        public string Id { get; set; }

        [NotMapped]
        public string OrderNumber
            => this.Id.Substring(0, 5);

        public DateTime? OrderDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Recipient { get; set; }

        public string RecipientLastName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string UserEmail { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string OfficeAddress { get; set; }

        public string PromoCodeName { get; set; }

        public decimal PromoCodeDiscountInPercentage { get; set; }

        public AddressViewModel DeliveryAddress { get; set; }

        public IEnumerable<OrderProductViewModel> OrderProducts { get; set; }
    }
}
