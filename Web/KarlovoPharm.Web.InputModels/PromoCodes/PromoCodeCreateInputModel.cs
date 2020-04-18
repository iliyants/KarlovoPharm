namespace KarlovoPharm.Web.InputModels.PromoCodes
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class PromoCodeCreateInputModel :  IMapTo<PromoCode>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredAllFieldsErrorMessage)]
        [StringLength(10, MinimumLength = 3, ErrorMessage = ValidationMessages.PromoCodeLengthErrorMessage)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [Range(0.1, double.MaxValue, ErrorMessage = ValidationMessages.PromoCodePercetnageErrorMessage)]
        [Display(Name = "DiscountInPercentage")]
        public decimal DiscountInPercentage { get; set; }

    }
}
