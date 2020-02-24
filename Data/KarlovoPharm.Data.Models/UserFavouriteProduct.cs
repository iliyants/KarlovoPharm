namespace KarlovoPharm.Data.Models
{
    using KarlovoPharm.Data.Models.Common;
    using System;

    public class UserFavouriteProduct
    {
        public UserFavouriteProduct()
        {
            this.AddedOn = DateTime.UtcNow;
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
