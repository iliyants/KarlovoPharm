﻿namespace KarlovoPharm.Web.ViewModels.Addresses
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class UserAddressViewModel : IMapFrom<UserAddress>
    {
        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressPostCode { get; set; }

        public string AddressBuildingNumber { get; set; }

        public string AddressDescription { get; set; }
    }
}
