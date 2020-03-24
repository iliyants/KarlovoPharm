namespace KarlovoPharm.Web.ViewModels.Address
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class AddressViewModel : IMapFrom<UserAddress>
    {
        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressPostCode { get; set; }

        public string AddressBuildingNumber { get; set; }

        public string AddressDescription { get; set; }
    }
}
