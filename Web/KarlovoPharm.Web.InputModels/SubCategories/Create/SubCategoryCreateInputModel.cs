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
        [Required(ErrorMessage = ValidationMessages.RequiredSubCategoryNameMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.RequiredSubCategoryNameMessage)]
        public string Name { get; set; }

        public string CategoryId { get; set; }

        [NotMapped]
        public IEnumerable<CategoryDisplayInputModel> Categories { get; set; }
    }
}
