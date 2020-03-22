namespace KarlovoPharm.Web.InputModels.SubCategories.Edit
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Categories.Display;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SubCategoryEditInputModel : IMapFrom<SubCategory>, IMapTo<SubCategory>
    {
        public string Id { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Display(Name = "SubCategoryName")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.SubCategoryLenghtErrorMessage)]
        public string Name { get; set; }

        [NotMapped]
        public IEnumerable<CategoryDisplayInputModel> Categories { get; set; }
    }
}
