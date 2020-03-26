namespace KarlovoPharm.Web.InputModels.Addresses.Edit
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AddressEditInputModel : IMapFrom<Address>, IMapTo<Address>
    {
        public string Id { get; set; }

        [NotMapped]
        public string UserId { get; set; }

        [Display(Name = "AddressCity")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(20), MinLength(2, ErrorMessage = ValidationMessages.CityLengthErrorMessage)]
        public string City { get; set; }

        [Display(Name = "AddressStreet")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(40), MinLength(5, ErrorMessage = ValidationMessages.StreetLengthErrorMessage)]
        public string Street { get; set; }

        [Display(Name = "AddressPostCode")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(10), MinLength(2, ErrorMessage = ValidationMessages.PostCodeLengthErrorMessage)]
        public string PostCode { get; set; }

        [Display(Name = "AddressBuildingNumber")]
        [StringLength(10), MinLength(1, ErrorMessage = ValidationMessages.BuildingNumberLengthErrorMessage)]
        public string BuildingNumber { get; set; }

        [Display(Name = "AddressDescription")]
        [StringLength(500), MinLength(10, ErrorMessage = ValidationMessages.DescriptionLengthErrorMessage)]
        public string Description { get; set; }
    }
}
