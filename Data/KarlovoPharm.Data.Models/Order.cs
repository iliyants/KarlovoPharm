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

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DispatchDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
