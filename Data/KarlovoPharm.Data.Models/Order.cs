namespace KarlovoPharm.Data.Models
{
    using System;
    using System.Collections.Generic;

    using KarlovoPharm.Data.Common.Models;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Data.Models.Enums;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Recipient { get; set; }

        public string RecipientLastName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentType PaymentType { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string OfficeAddress { get; set; }

        public string DeliveryAddressId { get; set; }

        public virtual Address DeliveryAddress { get; set; }
    }
}
