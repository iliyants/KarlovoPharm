namespace KarlovoPharm.Web.InputModels.Products.Create
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductCreateInputModel : IMapTo<Product>
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = ValidationMessages.ProductNameRequiredErrorMessage)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationMessages.ProductNameLenghtErrorMessage)]
        public string Name { get; set; }

        [Display(Description = "Description")]
        [Required(ErrorMessage = ValidationMessages.ProductDescriptionRequiredErrorMessage)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Description = "Specification")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductSpecificationLengthErrorMessage)]
        public string Specification { get; set; }

        [Display(Description = "Price")]
        [Required(ErrorMessage = ValidationMessages.ProductPriceRequiredErrorMessage)]
        [Range(0.0, double.MaxValue, ErrorMessage = ValidationMessages.ProductPriceNegativeErrorMessage)]
        public decimal Price { get; set; }

        [Display(Description = "Picture")]
        [Required(ErrorMessage = ValidationMessages.ProductPictureRequiredErrorMessage)]
        public string Picture { get; set; }
        public string SubCategoryId { get; set; }

    }
}
