namespace KarlovoPharm.Web.InputModels.Addresses.Display
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    public class AddressDisplayInputModel : IMapFrom<UserAddress>, IMapTo<UserAddress>
    {
        public string UserId { get; set; }

        public string AddressId { get; set; }

        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressPostCode { get; set; }

        public string AddressBuildingNumber { get; set; }

        public string AddressDescription { get; set; }
    }
}
