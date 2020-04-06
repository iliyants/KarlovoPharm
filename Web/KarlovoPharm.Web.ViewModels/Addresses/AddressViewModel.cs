namespace KarlovoPharm.Web.ViewModels.Addresses
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class AddressViewModel : IMapFrom<Address>
    {
        public string City { get; set; }

        public string Street { get; set; }

        public string PostCode { get; set; }

        public string BuildingNumber { get; set; }

        public string Description { get; set; }
    }
}
