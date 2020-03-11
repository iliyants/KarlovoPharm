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
        [Display(Name = "Name")]
        [Required(ErrorMessage = ValidationMessages.ProductNameRequiredErrorMessage)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationMessages.ProductNameLenghtErrorMessage)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ValidationMessages.ProductDescriptionRequiredErrorMessage)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "Specification")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = ValidationMessages.ProductSpecificationLengthErrorMessage)]
        public string Specification { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = ValidationMessages.ProductPriceRequiredErrorMessage)]
        [Range(0.0, double.MaxValue, ErrorMessage = ValidationMessages.ProductPriceNegativeErrorMessage)]
        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string SubCategoryId { get; set; }

        [Display(Name = "Picture")]
        [Required(ErrorMessage = ValidationMessages.ProductPictureRequiredErrorMessage)]
        [NotMapped]
        public IFormFile Picture { get; set; }

        [NotMapped]
        public IEnumerable<CategoryDisplayInputModel> SubCategories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProductCreateInputModel, Product>()
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.PictureUrl));
        }
    }
}
