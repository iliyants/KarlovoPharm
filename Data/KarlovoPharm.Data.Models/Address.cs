namespace KarlovoPharm.Data.Models
{
    using System;
    using System.Collections.Generic;

    using KarlovoPharm.Data.Common.Models;

    public class Address : BaseDeletableModel<string>
    {
        public Address()
        {
            this.AddressUsers = new HashSet<UserAddress>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Description { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public ICollection<UserAddress> AddressUsers { get; set; }
    }
}
