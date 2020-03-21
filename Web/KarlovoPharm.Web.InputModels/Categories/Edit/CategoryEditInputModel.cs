using KarlovoPharm.Common;
using KarlovoPharm.Data.Models;
using KarlovoPharm.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace KarlovoPharm.Web.InputModels.Categories.Edit
{
    public class CategoryEditInputModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        [Display(Name = "CategoryName")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.CategoryLenghtErrorMessage)]
        public string Name { get; set; }
    }
}
