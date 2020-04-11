using KarlovoPharm.Common;
using System.ComponentModel.DataAnnotations;

namespace KarlovoPharm.Web.InputModels.Contacts
{
    public class EmailCreateInputModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.NameLenghtErrorMessage)]
        public string Name { get; set; }


        [Display (Name= "Email")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [EmailAddress(ErrorMessage = ValidationMessages.EmailValidationErrorMessage)]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(40, MinimumLength = 3, ErrorMessage = ValidationMessages.SubjectLenghtErrorMessage)]
        public string Subject { get; set; }


        [Display(Name = "Message")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(300, MinimumLength = 10, ErrorMessage = ValidationMessages.MessageLenghtErrorMessage)]
        public string Message { get; set; }
    }
}
