namespace KarlovoPharm.Data.Models
{

    using KarlovoPharm.Data.Models.Common;

    public class UserAddress
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string AddressId { get; set; }

        public Address Address { get; set; }
    }
}
