namespace KarlovoPharm.Data.Models
{
    using System;

    using KarlovoPharm.Data.Common.Models;
    using KarlovoPharm.Data.Models.Common;

    public class UserFavouriteProduct : BaseDeletableModel<string>
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
