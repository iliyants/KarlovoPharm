namespace KarlovoPharm.Data.Models
{
    using System;
    using System.Collections.Generic;
    using KarlovoPharm.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
         = new HashSet<SubCategory>();
    }
}
