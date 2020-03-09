namespace KarlovoPharm.Data.Models
{
    using System;
    using System.Collections.Generic;
    using KarlovoPharm.Data.Common.Models;
    using KarlovoPharm.Data.Models.Common;

    public class ShoppingCart : BaseDeletableModel<string>
    {
        public ShoppingCart()
        {
            this.ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}
