namespace KarlovoPharm.Web.InputModels.Users
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Address;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProfileEdintInputModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string Id { get; set; }


        [Display(Name = "Username")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = ValidationMessages.UsernameLengthErrorMessage)]
        public string Username { get; set; }

        [Display(Name = "FirstName")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.NameLenghtErrorMessage)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.LastNameLenghtErrorMessage)]
        public string LastName { get; set; }

        [Display(Name = "PhoneNumber")]
        [Phone(ErrorMessage = ValidationMessages.PhoneNumberLengthErrorMessage)]
        public string PhoneNumber { get; set; }

        public List<AddressEditInputModel> UserAddresses { get; set; }
    }
}
