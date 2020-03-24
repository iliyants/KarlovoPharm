namespace KarlovoPharm.Data.Models
{
    using KarlovoPharm.Data.Common.Models;
    using KarlovoPharm.Data.Models.Common;

    public class UserAddress : BaseDeletableModel<string>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}
