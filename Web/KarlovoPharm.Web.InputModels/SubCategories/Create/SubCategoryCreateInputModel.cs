namespace KarlovoPharm.Web.InputModels.SubCategories.Create
{
    using KarlovoPharm.Common;
    using System.ComponentModel.DataAnnotations;

    public class SubCategoryCreateInputModel
    {
        public string CategoryId { get; set; }

        [Display(Name = "SubCategoryName")]
        [Required(ErrorMessage = ValidationMessages.RequiredSubCategoryNameMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.RequiredSubCategoryNameMessage)]
        public string Name { get; set; }
    }
}
