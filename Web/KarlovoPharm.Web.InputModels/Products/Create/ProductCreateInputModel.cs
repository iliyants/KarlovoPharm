namespace KarlovoPharm.Web.InputModels.Products.Create
{
    using AutoMapper;
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Categories.Display;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductCreateInputModel : IMapTo<Product>, IHaveCustomMappings
    {

        [Display(Name = "Picture")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [NotMapped]
        public IFormFile Picture { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationMessages.ProductNameLenghtErrorMessage)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [Range(0.1, double.MaxValue, ErrorMessage = ValidationMessages.ProductPriceNegativeErrorMessage)]
        public decimal Price { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductDesignationLengthErrorMessage)]
        public string Designation { get; set; }

        [Display(Name = "Effect")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductDesignationLengthErrorMessage)]
        public string Effect { get; set; }

        [Display(Name = "Composition")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductEffectLengthErrorMessage)]
        public string Composition { get; set; }

        [Display(Name = "WayOfUse")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductWayOfUseLengthErrorMessage)]
        public string WayOfUse { get; set; }

        [Display(Name = "Specification")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductSpecificationLengthErrorMessage)]
        public string Specification { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ValidationMessages.ProductManufacturerLengthErrorMessage)]
        public string Manufacturer { get; set; }

        [Display(Name = "CountryOfOrigin")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ValidationMessages.ProductCountryOfOriginLengthErrorMessage)]
        public string CountryOfOrigin { get; set; }

        public string PictureUrl { get; set; }

        public string SubCategoryId { get; set; }

        [NotMapped]
        public IEnumerable<CategoryDisplayInputModel> SubCategories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProductCreateInputModel, Product>()
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.PictureUrl));
        }
    }
}
