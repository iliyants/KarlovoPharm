namespace KarlovoPharm.Web.InputModels.Orders.Display
{
    using AutoMapper;
    using KarlovoPharm.Common;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Addresses.Display;
    using KarlovoPharm.Web.InputModels.ShoppingCarts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class OrderDisplayInputModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string UserId { get; set; }
        public IEnumerable<AddressDisplayInputModel> UserAddresses { get; set; }

        public ShoppingCartInputModel ShoppingCart { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [NotMapped]
        [Display(Name = "RecipientName")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.NameLenghtErrorMessage)]
        public string Recipient { get; set; }

        [NotMapped]
        [Display(Name = "RecipientLastName")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationMessages.LastNameLenghtErrorMessage)]
        public string RecipientLastName { get; set; }

        [NotMapped]
        [Display(Name = "DeliveryAddressId")]
        public string DeliveryAddressId { get; set; }

        [NotMapped]
        [Display(Name = "RecipientPhoneNumber")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        public string RecipientPhoneNumber { get; set; }

        [NotMapped]
        [Display(Name = "PaymentType")]
        [Required(ErrorMessage = ValidationMessages.RequiredFieldErrorMessage)]
        public PaymentType PaymentType { get; set; }

        public string OfficeAddress { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, OrderDisplayInputModel>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(y => y.Id));
        }
    }
}
