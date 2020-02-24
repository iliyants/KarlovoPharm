namespace KarlovoPharm.Data.Models
{
    using System;
    using System.Collections.Generic;
    using KarlovoPharm.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.ProductShoppingCarts = new HashSet<ShoppingCartProduct>();
            this.ProductOrders = new HashSet<OrderProduct>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Description { get; set; }

        public string Specification { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

        public string Picture { get; set; }

        public string SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public ICollection<ShoppingCartProduct> ProductShoppingCarts { get; set; }

        public ICollection<OrderProduct> ProductOrders { get; set; }
    }
}
