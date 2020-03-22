namespace KarlovoPharm.Web.InputModels.SubCategories.Create
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Categories.Display;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SubCategoryCreateInputModel : IMapTo<SubCategory>
    {
        [Display(Name = "SubCategoryName")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.SubCategoryLenghtErrorMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        public string CategoryId { get; set; }

        [NotMapped]
        public IEnumerable<CategoryDisplayInputModel> Categories { get; set; }
    }
}
