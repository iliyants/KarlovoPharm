namespace KarlovoPharm.Web.InputModels.Users
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Addresses.Display;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProfileEdintInputModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(10, MinimumLength = 3, ErrorMessage = ValidationMessages.UsernameLengthErrorMessage)]
        public string Username { get; set; }

        [Display(Name = "FirstName")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.NameLenghtErrorMessage)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.LastNameLenghtErrorMessage)]
        public string LastName { get; set; }

        [Display(Name = "PhoneNumber")]
        [RegularExpression(ValidationRegexes.PhoneRegex, ErrorMessage = ValidationMessages.PhoneNumberLengthErrorMessage)]
        public string PhoneNumber { get; set; }

        public List<AddressDisplayInputModel> UserAddresses { get; set; }
    }
}
