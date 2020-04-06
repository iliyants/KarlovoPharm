namespace KarlovoPharm.Web.ViewModels.User
{
    using System.Collections.Generic;

    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.ViewModels.Addresses;

    public class UserProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<UserAddressViewModel> UserAddresses { get; set; }
    }
}
