using KarlovoPharm.Common;
using KarlovoPharm.Data.Models;
using KarlovoPharm.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace KarlovoPharm.Web.InputModels.Address
{
    public class AddressEditInputModel : IMapFrom<UserAddress>, IMapTo<UserAddress>
    {

        public string AddressId { get; set; }

        [Display(Name = "AddressCity")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(20), MinLength(2, ErrorMessage = ValidationMessages.CityLengthErrorMessage)]
        public string AddressCity { get; set; }

        [Display(Name = "AddressStreet")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(40), MinLength(5, ErrorMessage = ValidationMessages.StreetLengthErrorMessage)]
        public string AddressStreet { get; set; }

        [Display(Name = "AddressPostCode")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(10), MinLength(2, ErrorMessage = ValidationMessages.PostCodeLengthErrorMessage)]
        public string AddressPostCode { get; set; }

        [Display(Name = "AddressBuildingNumber")]
        [StringLength(10), MinLength(1, ErrorMessage = ValidationMessages.BuildingNumberLengthErrorMessage)]
        public string AddressBuildingNumber { get; set; }

        [Display(Name = "AddressDescription")]
        [StringLength(500), MinLength(10, ErrorMessage = ValidationMessages.DescriptionLengthErrorMessage)]
        public string AddressDescription { get; set; }
    }
}
