namespace KarlovoPharm.Web.InputModels.Categories.Create
{
    using KarlovoPharm.Common;
    using System.ComponentModel.DataAnnotations;


    public class CategoryCreateInputModel
    {

        [Display(Name = "CategoryName")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.CategoryLenghtErrorMessage)]
        public string Name { get; set; }
    }
}
