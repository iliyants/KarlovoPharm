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

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public bool Available { get; set; }

        public string Picture { get; set; }

        public string Designation { get; set; }

        public string Effect { get; set; }

        public string Composition { get; set; }

        public string WayOfuse { get; set; }

        public string Specification { get; set; }

        public string Manufacturer { get; set; }

        public string CountryOfOrigin { get; set; }

        public string SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public ICollection<ShoppingCartProduct> ProductShoppingCarts { get; set; }

        public ICollection<OrderProduct> ProductOrders { get; set; }
    }
}
