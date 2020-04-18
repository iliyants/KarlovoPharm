namespace KarlovoPharm.Data.Models
{
    using System.Collections.Generic;

    using KarlovoPharm.Data.Common.Models;

    public class PromoCode : BaseDeletableModel<string>
    {

        public PromoCode()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Name { get; set; }

        public decimal DiscountInPercentage { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
