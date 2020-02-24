namespace KarlovoPharm.Data.Models
{
    using System;
    using System.Collections.Generic;
    using KarlovoPharm.Data.Common.Models;

    public class SubCategory : BaseDeletableModel<string>
    {
        public SubCategory()
        {
            this.Products = new HashSet<Product>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Product> Products { get; set; }
        = new HashSet<Product>();
    }
}
